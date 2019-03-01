// --------------------------------------------------------------------------------------------------------------------
// Importer.cs
//
// Copyright 2015-2018 Lucas Alexander
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.ServiceModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Discovery;
using log4net;
using System.Xml;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Xml.Schema;

namespace AlexanderDevelopment.ConfigDataMover.Lib
{
    public class Importer
    {
        public string SourceString { get; set; }
        public string TargetString { get; set; }
        public bool MapBaseBu { get; set; }
        public bool MapBaseCurrency { get; set; }
        public List<GuidMapping> GuidMappings { get; set; }
        public List<JobStep> JobSteps { get; set; }
        public bool StopLogging { get; set; } 

        public int ErrorCount { get { return _errorCount; } }
        private int _errorCount;

        private static CrmServiceClient _sourceClient;
        private static CrmServiceClient _targetClient;
        private static string _sourceFile;
        private static string _targetFile;
        private static bool _isFileSource;
        private static bool _isFileTarget;
        //private static string _targetVersion = "0.0.0.0";

        private static ExportedData _savedSourceData;

        List<GuidMapping> _mappings = new List<GuidMapping>();

        private enum operationTypes { Create, Update, Associate };
        
        /// <summary>
        /// log4net logger
        /// </summary>
        private ILog logger;

        public delegate void ProgressUpdate(string value);
        public event ProgressUpdate OnProgressUpdate;

        public Importer()
        {
            _sourceFile = string.Empty;
            _targetFile = string.Empty;
            _isFileSource = false;
            _isFileTarget = false;
            _mappings = new List<GuidMapping>();
            _errorCount = 0;
            log4net.Config.XmlConfigurator.Configure();
            StopLogging = true;
        }

        /// <summary>
        /// used to report progress and log status via a single method
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        private void LogMessage(string level, string message)
        {
            if (OnProgressUpdate != null)
            {
                OnProgressUpdate(message.Trim());
            }
            switch (level.ToUpper())
            {
                case "INFO":
                    logger.Info(message);
                    break;
                case "ERROR":
                    logger.Error(message);
                    break;
                case "WARN":
                    logger.Warn(message);
                    break;
                case "DEBUG":
                    logger.Debug(message);
                    break;
                case "FATAL":
                    logger.Fatal(message);
                    break;
                default:
                    logger.Info(message); //default to info
                    break;
            }
        }

        /// <summary>
        /// parse supplied crm connection strings to get crmconnection objects
        /// </summary>
        private void ParseConnections()
        {
            //source can be rawjson to parse, file to read/parse or crm connection string
            LogMessage("INFO", "parsing source connection");

            //check for rawjson source
            if (SourceString.ToUpper().StartsWith("RAWJSON="))
            {
                _isFileSource = true;
                LogMessage("INFO", "source is raw json");
                LogMessage("INFO", "  deserializing source data from raw json");
                // remove the "rawjson=" from the beginning
                String lines = SourceString.Remove(0, "RAWJSON=".Length);

                //deserialze source data
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.TypeNameHandling = TypeNameHandling.None;
                _savedSourceData = (ExportedData)JsonConvert.DeserializeObject<ExportedData>(lines, settings);
                LogMessage("INFO", "  source data deserialization complete");

            }
            else
            {
                //check for file source
                if (SourceString.ToUpper().StartsWith("FILE="))
                {
                    string sourcepath = Regex.Replace(SourceString, "FILE=", "", RegexOptions.IgnoreCase);
                    _sourceFile = Path.GetFullPath(sourcepath);
                    _isFileSource = true;
                    LogMessage("INFO", "source is file - " + _sourceFile);

                    //deserialze source data
                    using (StreamReader sr = new StreamReader(_sourceFile))
                    {
                        LogMessage("INFO", "  deserializing source data from file");
                        // Read the stream to a string, and write the string to the console.
                        String lines = sr.ReadToEnd();
                        JsonSerializerSettings settings = new JsonSerializerSettings();
                        settings.TypeNameHandling = TypeNameHandling.None;
                        _savedSourceData = (ExportedData)JsonConvert.DeserializeObject<ExportedData>(lines, settings);
                        LogMessage("INFO", "  source data deserialization complete");

                    }
                }
                //if not rawjson or file target, it must (should?) be a crm connection string
                else
                {
                    _sourceClient = new CrmServiceClient(SourceString);

                    //validate login works
                    try
                    {
                        using (OrganizationServiceProxy service = _sourceClient.OrganizationServiceProxy)
                        {
                            string testFetch = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                          <entity name='businessunit'>
                            <attribute name='name' />
                            <attribute name='businessunitid' />
                          </entity>
                        </fetch>";
                            EntityCollection buEntities = service.RetrieveMultiple(new FetchExpression(testFetch));
                            if (buEntities.Entities.Count < 1)
                            {
                                throw new Exception("Test query returned zero results.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string errormsg = string.Format(string.Format("Could not validate source connection: {0}", ex.Message));
                        LogMessage("ERROR", errormsg);
                        throw new InvalidOperationException(errormsg);
                    }

                    _isFileSource = false;
                }
            }

            //target can be file to write or crm connection string
            LogMessage("INFO", "parsing target connection");

            //check for file target
            if (TargetString.ToUpper().StartsWith("FILE="))
            {
                string targetpath = Regex.Replace(TargetString, "FILE=", "", RegexOptions.IgnoreCase);
                _targetFile = Path.GetFullPath(targetpath);
                _savedSourceData = new ExportedData();
                _isFileTarget = true;
                LogMessage("INFO", "target is file - " + _targetFile);
            }
            //not file target, so it must (should?) be crm connection string
            else
            {
                _targetClient = new CrmServiceClient(TargetString);

                //validate login works
                try
                {
                    using (OrganizationServiceProxy service = _targetClient.OrganizationServiceProxy)
                    {
                        string testFetch = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                          <entity name='businessunit'>
                            <attribute name='name' />
                            <attribute name='businessunitid' />
                          </entity>
                        </fetch>";
                        EntityCollection buEntities = service.RetrieveMultiple(new FetchExpression(testFetch));
                        if (buEntities.Entities.Count < 1)
                        {
                            throw new Exception("Test query returned zero results.");
                        }

                        //get the organization id
                        Guid orgId = ((WhoAmIResponse)service.Execute(new WhoAmIRequest())).OrganizationId;

                        LogMessage("INFO", "target version is - " + _targetClient.ConnectedOrgVersion.ToString());
                    }
                }
                catch (Exception ex)
                {
                    string errormsg = string.Format(string.Format("Could not validate target connection: {0}", ex.Message));
                    LogMessage("ERROR", errormsg);
                    throw new InvalidOperationException(errormsg);
                }

                _isFileTarget = false;
            }
        }

        /// <summary>
        /// handle base business unit, base currency and other GUID mappings
        /// </summary>
        private void SetupGuidMappings()
        {
            LogMessage("INFO","setting up GUID mappings");
            _mappings.Clear();

            Guid sourceBaseBu = Guid.Empty;
            Guid targetBaseBu = Guid.Empty;
            Guid sourceBaseTeam = Guid.Empty;
            Guid targetBaseTeam = Guid.Empty;
            Guid sourceBaseCurrency = Guid.Empty;
            Guid targetBaseCurrency = Guid.Empty;

            //if data is coming from file source, read base BU and base currency from file
            if (_isFileSource)
            {
                if (MapBaseBu)
                {
                    sourceBaseBu = _savedSourceData.BaseBu;
                    sourceBaseTeam = _savedSourceData.BaseTeam;
                }
                if (MapBaseCurrency)
                {
                    sourceBaseCurrency = _savedSourceData.BaseCurrency;
                }
            }
            else //otherwise we need to look them up in the source org
            {
                using (OrganizationServiceProxy service = _sourceClient.OrganizationServiceProxy)
                {
                    if (MapBaseBu)
                    {
                        LogMessage("INFO", "querying source base business unit");
                        try
                        {
                            string baseBuFetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                          <entity name='businessunit'>
                            <attribute name='name' />
                            <attribute name='businessunitid' />
                            <filter type='and'>
                              <condition attribute='parentbusinessunitid' operator='null' />
                            </filter>
                          </entity>
                        </fetch>";
                            EntityCollection buEntities = service.RetrieveMultiple(new FetchExpression(baseBuFetchXml));
                            sourceBaseBu = (Guid)(buEntities[0]["businessunitid"]);

                            string baseTeamFetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                          <entity name='team'>
                            <attribute name='name' />
                            <attribute name='businessunitid' />
                            <attribute name='teamid' />
                            <filter type='and'>
                              <condition attribute='teamtype' operator='eq' value='0' />
                              <condition attribute='isdefault' operator='eq' value='1' />
                              <condition attribute='businessunitid' operator='eq' value='{0}' />
                            </filter>
                          </entity>
                        </fetch>";
                            EntityCollection teamEntities = service.RetrieveMultiple(new FetchExpression(string.Format(baseTeamFetchXml, sourceBaseBu)));
                            sourceBaseTeam = (Guid)(teamEntities[0]["teamid"]);
                        }
                        catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
                        {
                            string errormsg = string.Format(string.Format("could not retrieve source base business unit and team: {0}", ex.Message));
                            LogMessage("ERROR", errormsg);
                            throw new InvalidOperationException(errormsg);
                        }
                    }

                    if (MapBaseCurrency)
                    {
                        LogMessage("INFO", "querying source base currency");
                        try
                        {
                            string baseCurrencyFetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                          <entity name='organization'>
                            <attribute name='basecurrencyid' />
                          </entity>
                        </fetch>";
                            EntityCollection currencyEntities = service.RetrieveMultiple(new FetchExpression(baseCurrencyFetchXml));
                            sourceBaseCurrency = ((EntityReference)currencyEntities[0]["basecurrencyid"]).Id;
                        }
                        catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
                        {
                            string errormsg = string.Format(string.Format("could not retrieve source target base currency: {0}", ex.Message));
                            LogMessage("ERROR", errormsg);
                            throw new InvalidOperationException(errormsg);
                        }
                    }
                }
            }

            //if we're not writing data to a target crm org instead of a file, we need to look up the target base BU and currency
            if (!_isFileTarget)
            {
                using (OrganizationServiceProxy service = _targetClient.OrganizationServiceProxy)
                {
                    if (MapBaseBu)
                    {
                        LogMessage("INFO", "querying target base business unit");
                        try
                        {
                            string baseBuFetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                          <entity name='businessunit'>
                            <attribute name='name' />
                            <attribute name='businessunitid' />
                            <filter type='and'>
                              <condition attribute='parentbusinessunitid' operator='null' />
                            </filter>
                          </entity>
                        </fetch>";
                            EntityCollection buEntities = service.RetrieveMultiple(new FetchExpression(baseBuFetchXml));
                            targetBaseBu = (Guid)(buEntities[0]["businessunitid"]);

                            string baseTeamFetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                          <entity name='team'>
                            <attribute name='name' />
                            <attribute name='businessunitid' />
                            <attribute name='teamid' />
                            <filter type='and'>
                              <condition attribute='teamtype' operator='eq' value='0' />
                              <condition attribute='isdefault' operator='eq' value='1' />
                              <condition attribute='businessunitid' operator='eq' value='{0}' />
                            </filter>
                          </entity>
                        </fetch>";
                            EntityCollection teamEntities = service.RetrieveMultiple(new FetchExpression(string.Format(baseTeamFetchXml, targetBaseBu)));
                            targetBaseTeam = (Guid)(teamEntities[0]["teamid"]);
                        }
                        catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
                        {
                            string errormsg = string.Format("could not retrieve target base business unit: {0}", ex.Message);
                            LogMessage("ERROR", errormsg);
                            throw new InvalidOperationException(errormsg);
                        }
                    }

                    if (MapBaseCurrency)
                    {
                        LogMessage("INFO", "querying target base currency");
                        try
                        {
                            string baseCurrencyFetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                          <entity name='organization'>
                            <attribute name='basecurrencyid' />
                          </entity>
                        </fetch>";
                            EntityCollection currencyEntities = service.RetrieveMultiple(new FetchExpression(baseCurrencyFetchXml));
                            targetBaseCurrency = ((EntityReference)currencyEntities[0]["basecurrencyid"]).Id;
                        }
                        catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
                        {
                            string errormsg = string.Format(string.Format("could not retrieve target base currency: {0}", ex.Message));
                            LogMessage("ERROR", errormsg);
                            throw new InvalidOperationException(errormsg);
                        }
                    }
                }

            }

            //add the source/target mappings to the guid mappings list
            if (MapBaseBu)
            {
                LogMessage("INFO","setting base business unit GUID mapping");
                if (sourceBaseBu != Guid.Empty && targetBaseBu != Guid.Empty)
                {
                    _mappings.Add(new GuidMapping { sourceId = sourceBaseBu, targetId = targetBaseBu });
                }

                LogMessage("INFO", "setting base business unit default team GUID mapping");
                if (sourceBaseTeam != Guid.Empty && targetBaseTeam != Guid.Empty)
                {
                    _mappings.Add(new GuidMapping { sourceId = sourceBaseTeam, targetId = targetBaseTeam });
                }

                //if our target is a file, make sure we save the base BU
                if (_isFileTarget)
                {
                    _savedSourceData.BaseBu = sourceBaseBu;
                    _savedSourceData.BaseTeam = sourceBaseTeam;
                }
            }

            if (MapBaseCurrency)
            {
                LogMessage("INFO","setting base currency GUID mapping");
                if (sourceBaseCurrency != Guid.Empty && targetBaseCurrency != Guid.Empty)
                {
                    _mappings.Add(new GuidMapping { sourceId = sourceBaseCurrency, targetId = targetBaseCurrency });
                }

                //if our target is a file, make sure we save the base currency
                if (_isFileTarget)
                {
                    _savedSourceData.BaseCurrency = sourceBaseCurrency;
                }
            }

            foreach (var item in GuidMappings)
            {
                _mappings.Add(item);
            }
        }

        /// <summary>
        /// used to enable paging in the fetchxml queries - https://msdn.microsoft.com/en-us/library/gg328046.aspx
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="cookie"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public string CreateXml(string xml, string cookie, int page, int count)
        {
            StringReader stringReader = new StringReader(xml);
            XmlTextReader reader = new XmlTextReader(stringReader);

            // Load document
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            return CreateXml(doc, cookie, page, count);
        }

        /// <summary>
        /// used to enable paging in the fetchxml queries - https://msdn.microsoft.com/en-us/library/gg328046.aspx
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="cookie"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public string CreateXml(XmlDocument doc, string cookie, int page, int count)
        {
            XmlAttributeCollection attrs = doc.DocumentElement.Attributes;

            if (cookie != null)
            {
                XmlAttribute pagingAttr = doc.CreateAttribute("paging-cookie");
                pagingAttr.Value = cookie;
                attrs.Append(pagingAttr);
            }

            XmlAttribute pageAttr = doc.CreateAttribute("page");
            pageAttr.Value = System.Convert.ToString(page);
            attrs.Append(pageAttr);

            XmlAttribute countAttr = doc.CreateAttribute("count");
            countAttr.Value = System.Convert.ToString(count);
            attrs.Append(countAttr);

            StringBuilder sb = new StringBuilder(1024);
            StringWriter stringWriter = new StringWriter(sb);

            XmlTextWriter writer = new XmlTextWriter(stringWriter);
            doc.WriteTo(writer);
            writer.Close();

            return sb.ToString();
        }

        /// <summary>
        /// method used to validate job steps before job actually starts running
        /// </summary>
        void ValidateSteps()
        {
            LogMessage("INFO", "Validating job steps");

            //load CRM fetchxml schema to use for validation later- https://msdn.microsoft.com/en-us/library/gg309405.aspx
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            System.Reflection.Assembly execAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            try
            {
                //schemaSet.Add(null, "fetch.xsd");
                using (Stream schemaStream = execAssembly.GetManifestResourceStream("AlexanderDevelopment.ConfigDataMover.Lib.fetch.xsd"))
                {
                    using (XmlReader schemaReader = XmlReader.Create(schemaStream))
                    {
                        schemaSet.Add(null, schemaReader);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Could not load FetchXML XSD to use for validation.");
            }

            //loop through each job step
            for (int i=0;i<JobSteps.Count;i++)
            {
                LogMessage("INFO", string.Format("  Validating step {0} of {1}", i + 1, JobSteps.Count));
                string errormsg = "";
                if (string.IsNullOrWhiteSpace(JobSteps[i].StepName))
                {
                    errormsg = string.Format("Job steps validation failed - Step #{0} has no name", i + 1);
                    LogMessage("ERROR", errormsg);
                    throw new Exception(errormsg);
                }
                LogMessage("INFO", string.Format("    Non-empty step name"));

                if (string.IsNullOrWhiteSpace(JobSteps[i].StepFetch))
                {
                    errormsg = string.Format("Job steps validation failed - Step \"{0}\" has no query statement", JobSteps[i].StepName);
                    LogMessage("ERROR", errormsg);
                    throw new Exception(errormsg);
                }
                LogMessage("INFO", string.Format("    Non-empty query"));

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add(schemaSet);
                settings.ValidationEventHandler += (sender, args) =>
                {
                    if (args.Severity == XmlSeverityType.Warning)
                    {
                        errormsg = string.Format("Job steps validation failed - Step \"{0}\" matching schema not found.  No validation occurred. {1}", JobSteps[i].StepName, args.Message);
                    }
                    else
                    {
                        errormsg = string.Format("Job steps validation failed - Step \"{0}\" query schema validation error: {1}", JobSteps[i].StepName, args.Message);
                    }
                    LogMessage("ERROR", errormsg);
                    throw new Exception(errormsg);
                };
                settings.ValidationType = ValidationType.Schema;
                try
                {
                    //create an xmlreader from the step fetch with the validation settings from above
                    StringReader stringReader = new StringReader(JobSteps[i].StepFetch);
                    XmlReader xreader = XmlReader.Create(stringReader, settings);

                    //read just to trigger the validation
                    while (xreader.Read()) { }

                    //close the reader
                    xreader.Close();
                }
                catch (XmlException ex)
                {
                    errormsg = string.Format("Job steps validation failed - Step \"{0}\" query could not be loaded or read", JobSteps[i].StepName);
                    LogMessage("ERROR", errormsg);
                    throw new Exception(errormsg);
                }
                catch (InvalidOperationException ex)
                {
                    errormsg = string.Format("Job steps validation failed - Step \"{0}\" query could not be loaded or read", JobSteps[i].StepName);
                    LogMessage("ERROR", errormsg);
                    throw new Exception(errormsg);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                LogMessage("INFO", string.Format("    Valid FetchXML query"));
            }
        }

        /// <summary>
        /// runs the import process
        /// </summary>
        public void Process()
        {
            OrganizationServiceProxy sourceService = null;
            OrganizationServiceProxy targetService = null;

            //set up logging
            logger = LogManager.GetLogger(typeof(Importer));
            LogMessage("INFO", "starting job");

            //validate job steps
            ValidateSteps();

            //establish connections and/or read source data from file
            ParseConnections();

            //connect to source and target if necessary
            if (!_isFileSource)
            {
                sourceService = _sourceClient.OrganizationServiceProxy;
            }
            if (!_isFileTarget)
            {
                targetService = _targetClient.OrganizationServiceProxy;
            }

            //create the guid mappings table
            SetupGuidMappings();

            LogMessage("INFO", "processing records");

            //loop through each job step
            for (int i = 0; i < JobSteps.Count; i++)
            {
                var item = JobSteps[i];
                JobStep step = (JobStep)item;
                LogMessage("INFO", string.Format("starting step {0}", step.StepName));

                //create a list of entities to hold retrieved entities so we can page through results
                List<Entity> ec = new List<Entity>();

                //if data is coming from a file
                if (_isFileSource)
                {
                    LogMessage("INFO", "  preparing data from source file for update/import");

                    //get the recordset in the file that corresponds to the current job step and loop through it
                    ec = TransformExportEntityList(_savedSourceData.RecordSets[i]);

                }
                else //source is live crm org
                {
                    string fetchQuery = step.StepFetch;

                    LogMessage("INFO", "  retrieving records");

                    // Set the number of records per page to retrieve.
                    int fetchCount = 5000;

                    // Initialize the page number.
                    int pageNumber = 1;

                    // Specify the current paging cookie. For retrieving the first page, 
                    // pagingCookie should be null.
                    string pagingCookie = null;

                    while (true)
                    {
                        // Build fetchXml string with the placeholders.
                        string fetchXml = CreateXml(fetchQuery, pagingCookie, pageNumber, fetchCount);

                        EntityCollection retrieved = sourceService.RetrieveMultiple(new FetchExpression(fetchXml));
                        ec.AddRange(retrieved.Entities);

                        if (retrieved.MoreRecords)
                        {
                            // Increment the page number to retrieve the next page.
                            pageNumber++;

                            // Set the paging cookie to the paging cookie returned from current results.                            
                            pagingCookie = retrieved.PagingCookie;
                        }
                        else
                        {
                            // If no more records in the result nodes, exit the loop.
                            break;
                        }
                    }
                    LogMessage("INFO", string.Format("  {0} records retrieved", ec.Count));
                }

                if (ec.Count > 0)
                {
                    //if the target is a live crm org
                    if (!_isFileTarget)
                    {
                        //check target crm version so we know its capabilities for specialized operations - https://msdn.microsoft.com/en-us/library/dn932124(v=crm.7).aspx
                        int majorversion = _targetClient.ConnectedOrgVersion.Major;
                        int minorversion = _targetClient.ConnectedOrgVersion.Minor;

                        //set variable to hold operation type - create or update
                        operationTypes importoperation = new operationTypes();

                        //loop through each entity in the collection
                        Dictionary<string, string> ManyManyRelationshipNamesDict = new Dictionary<string, string>();
                        foreach (Entity entity in ec)
                        {
                            //set a flag for whether we should execute specialized operations post-create/update
                            bool executeSpecializedOperations = true;

                            //get statecode and statuscode values for later
                            int statecode = -1;
                            int statuscode = -1;

                            if (entity.Attributes.Contains("statecode"))
                                statecode = ((OptionSetValue)entity["statecode"]).Value;

                            if(entity.Attributes.Contains("statuscode"))
                                statuscode = ((OptionSetValue)entity["statuscode"]).Value;

                            //create a list to hold the replacement guids. a second pass is required because c# disallows modifying a collection while enumerating
                            List<KeyValuePair<string, object>> guidsToUpdate = new List<KeyValuePair<string, object>>();
                            LogMessage("INFO", string.Format("  processing record {0}, {1}", entity.Id, entity.LogicalName));
                            try
                            {
                                LogMessage("INFO", "    processing GUID replacements");
                                foreach (KeyValuePair<string, object> attribute in entity.Attributes)
                                {
                                    //LogMessage("INFO",string.Format("Attribute - {0} {1}", attribute.Key, attribute.Value.GetType().ToString()));
                                    if (attribute.Value is Microsoft.Xrm.Sdk.EntityReference)
                                    {
                                        //LogMessage("INFO","getting source");

                                        EntityReference source = ((EntityReference)attribute.Value);
                                        try
                                        {
                                            //LogMessage("INFO","looking for GUID replacement");
                                            Guid sourceId = source.Id;
                                            Guid targetId = _mappings.Find(t => t.sourceId == source.Id).targetId;
                                            source.Id = targetId;
                                            guidsToUpdate.Add(new KeyValuePair<string, object>(attribute.Key, source));
                                            //LogMessage("INFO",string.Format("replacement found - {0} -> {1}", sourceId, targetId));
                                        }
                                        catch (System.NullReferenceException ex)
                                        {
                                            //LogMessage("INFO", "NullReferenceException happened");
                                            //do nothing because nullreferenceexception means there's no guid mapping to use
                                        }
                                    }
                                }

                                //now actually update the GUIDs with the mapped values
                                foreach (KeyValuePair<string, object> attribute in guidsToUpdate)
                                {
                                    //LogMessage("INFO",string.Format("    replacing attribute GUID {0} {1}", attribute.Key, attribute.Value));
                                    entity[attribute.Key] = attribute.Value;
                                }
                                if (step.ManyMany) {
                                    importoperation = operationTypes.Associate;
                                    //LogMessage("INFO", "    processing n:n");

                                    //remove the record id attribute from the collection because we can't use it for create/update operations here
                                    entity.Attributes.Remove(entity.LogicalName + "id");

                                    //put the guid attributes in a list so we can get the first and second one by index
                                    List<KeyValuePair<string, object>> attributes = new List<KeyValuePair<string, object>>();
                                    foreach (KeyValuePair<string, object> attribute in entity.Attributes)
                                    {
                                        //LogMessage("INFO", string.Format("Logical name - {0}", entity.LogicalName));
                                        if (attribute.Value is System.Guid)
                                        {
                                            //LogMessage("INFO", string.Format("Attribute - {0} {1}", attribute.Key, attribute.Value.GetType().ToString()));
                                            attributes.Add(attribute);
                                        }
                                    }

                                    //we should have exactly two guid attributes now. if we don't, then we shouldn't try an n:n associate
                                    if (attributes.Count == 2)
                                    {
                                        //the related records are stored as guids, so we have to figure out the entity logical names by removing the "id" from the end of the attribute name
                                        //the "right" way to do this would be a metadata call to retrieve the relationship details, but this should work and the performance is better
                                        string entity1logicalname = ReplaceLastOccurrence(attributes[0].Key, "id", "");
                                        string entity2logicalname = ReplaceLastOccurrence(attributes[1].Key, "id", "");

                                        //set the second attribute to be the related entity in the associate call
                                        EntityReferenceCollection related = new EntityReferenceCollection();
                                        related.Add(new EntityReference { Id = (Guid)attributes[1].Value, LogicalName = entity2logicalname });

                                        //the relationship name is the name of the entity we're querying
                                        //Relationship relationship = new Relationship(entity.LogicalName);

                                        //the relationship name may be different from the name of the intersect entity we're querying
                                        string NNRelationshipName = null;
                                        if (ManyManyRelationshipNamesDict.ContainsKey(entity.LogicalName))
                                        {
                                            NNRelationshipName = ManyManyRelationshipNamesDict[entity.LogicalName];
                                        }
                                        else
                                        {
                                            try
                                            {
                                                //try to find the correct name of this N:N relationship by querying CRM metadata
                                                RetrieveEntityRequest entityReq = new RetrieveEntityRequest()
                                                {
                                                    LogicalName = entity.LogicalName,
                                                    EntityFilters = EntityFilters.Relationships
                                                };
                                                LogMessage("INFO", "    trying RetrieveEntityRequest");
                                                RetrieveEntityResponse entityRes = (RetrieveEntityResponse)targetService.Execute(entityReq);
                                                LogMessage("INFO", "    got RetrieveEntityResponse");

                                                ManyToManyRelationshipMetadata[] rels = entityRes.EntityMetadata.ManyToManyRelationships;
                                                foreach (ManyToManyRelationshipMetadata r in rels)
                                                {
                                                    if (r.IntersectEntityName == entity.LogicalName)
                                                    {
                                                        NNRelationshipName = r.SchemaName;
                                                        ManyManyRelationshipNamesDict.Add(entity.LogicalName, NNRelationshipName);
                                                        LogMessage("INFO", "    N:N found relationship schema name: " + NNRelationshipName);
                                                    }
                                                }
                                            }
                                            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
                                            {
                                                //rethrow the exception so it gets handled in the end catch block
                                                throw ex;
                                            }
                                        }

                                        NNRelationshipName = string.IsNullOrEmpty(NNRelationshipName) ? entity.LogicalName : NNRelationshipName;
                                        Relationship relationship = new Relationship(NNRelationshipName);

                                        try
                                        {
                                            //try the associate
                                            LogMessage("INFO", "    trying target n:n associate");
                                            targetService.Associate(entity1logicalname, (Guid)attributes[0].Value, relationship, related);
                                            LogMessage("INFO", "    target n:n associate ok");
                                        }
                                        catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
                                        {
                                            //if we get duplicate key error, the association already exists, so that's ok
                                            if (ex.Message.ToUpper().Contains("CANNOT INSERT DUPLICATE KEY"))
                                            {
                                                LogMessage("INFO", "    target n:n already exists");
                                            }
                                            else
                                            {
                                                //if we get some other error, rethrow the exception so it gets handled in the end catch block
                                                throw ex;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        LogMessage("INFO", "    found more than two GUID attributes, exiting n:n step");
                                    }
                                }
                                else
                                {
                                    //if create-only step
                                    if (step.CreateOnly)
                                    {
                                        //remove statecode and statuscode if they are still included in entity attribute collection
                                        if (entity.Attributes.Contains("statecode"))
                                            entity.Attributes.Remove("statecode");

                                        if (entity.Attributes.Contains("statuscode"))
                                            entity.Attributes.Remove("statuscode");

                                        importoperation = operationTypes.Create;
                                        LogMessage("INFO", "    trying target create only");
                                        targetService.Create(entity);
                                        LogMessage("INFO", "    create ok");

                                    }
                                    else
                                    {
                                        //try to update first
                                        try
                                        {
                                            //if version is below 7.1 then remove statecode and statuscode attributes from entity attribute collection
                                            //if version is 7.1 or later, setting statecode and statuscode in an update will work fine
                                            if (majorversion < 7 || (majorversion == 7 && minorversion < 1))
                                            {
                                                LogMessage("INFO", "    removing statecode/statuscode");
                                                if (entity.Attributes.Contains("statecode"))
                                                    entity.Attributes.Remove("statecode");

                                                if (entity.Attributes.Contains("statuscode"))
                                                    entity.Attributes.Remove("statuscode");
                                            }
                                            importoperation = operationTypes.Update;
                                            LogMessage("INFO", "    trying target update");
                                            targetService.Update(entity);
                                            LogMessage("INFO", "    update ok");
                                        }
                                        catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
                                        {
                                            //only try a create if it's not an updateonly step
                                            if (!step.UpdateOnly)
                                            {
                                                //remove statecode and statuscode if they are still included in entity attribute collection
                                                if (entity.Attributes.Contains("statecode"))
                                                    entity.Attributes.Remove("statecode");

                                                if (entity.Attributes.Contains("statuscode"))
                                                    entity.Attributes.Remove("statuscode");

                                                //only try the create step if the update failed because the record doesn't already exist to update
                                                if (ex.Message.ToUpper().EndsWith("DOES NOT EXIST")||ex.Message.ToUpper().Contains("NO OBJECT MATCHED THE QUERY"))
                                                {
                                                    importoperation = operationTypes.Create;
                                                    LogMessage("INFO", "    trying target create");
                                                    //if update fails and step is not update-only then try to create
                                                    targetService.Create(entity);
                                                    LogMessage("INFO", "    create ok");
                                                }
                                                else //if the update failed for any reason other than "does not exist" we have a problem and don't want to try the create step
                                                {
                                                    //don't do the specialized operation steps (assign, setstate)
                                                    executeSpecializedOperations = false;

                                                    LogMessage("INFO", string.Format("    update failed: {0}", ex.Message));
                                                    throw new FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>(ex.Detail);
                                                }
                                            }
                                            else
                                            {
                                                //don't do the specialized operation steps (assign, setstate)
                                                executeSpecializedOperations = false;

                                                throw new FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>(ex.Detail);
                                            }
                                        }
                                    }

                                    //if initial create/update is successful, do specialized operations
                                    if (executeSpecializedOperations)
                                    {
                                        //record ownership for updates (only crm versions below 7.1.x.x)
                                        if (importoperation == operationTypes.Update)
                                        {
                                            //only do this if ownerid is in the collection
                                            if (entity.Attributes.Contains("ownerid"))
                                            {
                                                //only do this if crm version is less than 7.1.x.x
                                                if (majorversion < 7 || (majorversion == 7 && minorversion < 1))
                                                {
                                                    AssignRequest assign = new AssignRequest
                                                    {
                                                        Assignee = (EntityReference)entity["ownerid"],
                                                        Target = new EntityReference(entity.LogicalName, entity.Id)
                                                    };
                                                    try
                                                    {
                                                        LogMessage("INFO", "    trying target assignment update");
                                                        targetService.Execute(assign);
                                                        LogMessage("INFO", "    assignment ok");
                                                    }
                                                    catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
                                                    {
                                                        LogMessage("INFO", string.Format("    assignment failed: {0}", ex.Message));
                                                        throw new FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>(ex.Detail);
                                                    }
                                                }
                                            }
                                        }

                                        //record statecode & statuscode for creates (all crm versions)
                                        if (importoperation == operationTypes.Create && !step.UpdateOnly)
                                        {
                                            //only do this if we have both a statecode and statuscode specified
                                            if (statecode != -1 && statuscode != -1)
                                            {
                                                SetStateRequest setstate = new SetStateRequest
                                                {
                                                    State = new OptionSetValue(statecode),
                                                    Status = new OptionSetValue(statuscode),
                                                    EntityMoniker = new EntityReference(entity.LogicalName, entity.Id)
                                                };
                                                try
                                                {
                                                    LogMessage("INFO", "    trying target statecode/statuscode update");
                                                    targetService.Execute(setstate);
                                                    LogMessage("INFO", "    set statecode/statuscode ok");
                                                }
                                                catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
                                                {
                                                    LogMessage("INFO", string.Format("    set statecode/statuscode failed: {0}", ex.Message));
                                                    throw new FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>(ex.Detail);
                                                }
                                            }
                                        }

                                        //record statecode & statuscode for updates (only crm versions below 7.1.x.x)
                                        if (importoperation == operationTypes.Update)
                                        {
                                            //only do this if we have both a statecode and statuscode specified
                                            if (statecode != -1 && statuscode != -1)
                                            {
                                                //only do this if crm version is less than 7.1.x.x
                                                if (majorversion < 7 || (majorversion == 7 && minorversion < 1))
                                                {
                                                    SetStateRequest setstate = new SetStateRequest
                                                    {
                                                        State = new OptionSetValue(statecode),
                                                        Status = new OptionSetValue(statuscode),
                                                        EntityMoniker = new EntityReference(entity.LogicalName, entity.Id)
                                                    };
                                                    try
                                                    {
                                                        LogMessage("INFO", "    trying target statecode/statuscode update");
                                                        targetService.Execute(setstate);
                                                        LogMessage("INFO", "    set statecode/statuscode ok");
                                                    }
                                                    catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
                                                    {
                                                        LogMessage("INFO", string.Format("    set statecode/statuscode failed: {0}", ex.Message));
                                                        throw new FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>(ex.Detail);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
                            {
                                string operation = (importoperation == operationTypes.Create) ? "CREATE" : "UPDATE";
                                //if everything fails, log error
                                //to main log
                                LogMessage("ERROR", string.Format("    record transfer failed"));

                                //to record error log
                                LogMessage("ERROR", string.Format("RECORD ERROR: {0}, {1}, OPERATION: {2}, MESSAGE: {3}", entity.Id, entity.LogicalName, operation, ex.Detail?.Message));

                                //increment the error count
                                _errorCount++;
                            }
                        }
                    }
                }
                
                //if the target is a file - prepare the records in this step for serialization later
                if (_isFileTarget)
                {
                    LogMessage("INFO", "  preparing records for serialization");

                    //instantiate a new list of exportentity objects
                    List<ExportEntity> entitiesToExport = new List<ExportEntity>();
                    entitiesToExport = TransformEntityList(ec);

                    //add the recordset to the exporteddata object to be serialized
                    _savedSourceData.RecordSets.Add(entitiesToExport);
               }
            }

            //if the target is a file - serialize the data and write it to a file
            if (_isFileTarget)
            {
                LogMessage("INFO", "  serializing data to target file");

                //instantiate a new jsonserializer
                JsonSerializer serializer = new JsonSerializer();

                //write to the target file path
                using (StreamWriter sw = new StreamWriter(_targetFile))
                {
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        //some jsonwriter options
                        //leave out null values - this might cause problems if trying to unset a value, but not sure what the alternative approach would be
                        serializer.NullValueHandling = NullValueHandling.Ignore;
                        
                        //the import will vomit if this isn't "none"
                        serializer.TypeNameHandling = TypeNameHandling.None;
                        
                        //you can change this if you want a more easily readable output file, but this makes for a smaller file size
                        serializer.Formatting = Newtonsoft.Json.Formatting.None; 

                        //serialize and save
                        serializer.Serialize(writer, _savedSourceData);
                    }
                }
                LogMessage("INFO", "data serialization complete");
            }
            LogMessage("INFO", "job complete");

            if (StopLogging)
            {
                //stop logging
                logger.Logger.Repository.Shutdown();
            }
        }

        /// <summary>
        /// transforms a list of exportentity records into a list of crm entity records
        /// </summary>
        /// <param name="entitylist"></param>
        /// <returns></returns>
        private List<Entity> TransformExportEntityList(List<ExportEntity> entitylist)
        {
            List<Entity> ec = new List<Entity>();
            foreach (var e in entitylist)
            {
                //instantiate a new crm entity object
                Entity entity = new Entity(e.LogicalName);
                entity.Id = e.Id;
                entity.LogicalName = e.LogicalName;

                //loop through the attributes stored in the file
                foreach (ExportAttribute exportAttribute in e.Attributes)
                {
                    //JObject object if we need to parse a complex type
                    Newtonsoft.Json.Linq.JObject jObject;

                    //JArray object if we need to parse an entitycollection
                    Newtonsoft.Json.Linq.JArray jArray;

                    //instantiate a new object to hold the attribute value
                    object attributeValue = null;

                    //give the attribute the correct name
                    string attributeName = exportAttribute.AttributeName;
                    try
                    {
                        //check the stored attribute type in the file and set the crm entity's attribute values accordingly
                        switch (exportAttribute.AttributeType)
                        {
                            //if it's a system.guid
                            case "System.Guid":
                                attributeValue = new Guid((string)exportAttribute.AttributeValue);
                                break;
                            //if it's a system.decimal
                            case "System.Decimal":
                                string typename = exportAttribute.AttributeValue.GetType().ToString();
                                attributeValue = Convert.ToDecimal(exportAttribute.AttributeValue);
                                break;
                            //if it's a system.double
                            case "System.Double":
                                attributeValue = Convert.ToDouble(exportAttribute.AttributeValue);
                                break;
                            //if it's an int32
                            case "System.Int32":
                                attributeValue = Convert.ToInt32(exportAttribute.AttributeValue);
                                break;
                            //if it's an entityreference
                            case "Microsoft.Xrm.Sdk.EntityReference":
                                jObject = (Newtonsoft.Json.Linq.JObject)exportAttribute.AttributeValue;
                                EntityReference lookup = new EntityReference((string)jObject["LogicalName"], (Guid)jObject["Id"]);
                                attributeValue = lookup;
                                break;
                            //if it's an optionsetvalue
                            case "Microsoft.Xrm.Sdk.OptionSetValue":
                                jObject = (Newtonsoft.Json.Linq.JObject)exportAttribute.AttributeValue;
                                attributeValue = new OptionSetValue { Value = (int)jObject["Value"] };
                                break;
                            //if it's a multiselectoptionset
                            case "Microsoft.Xrm.Sdk.OptionSetValueCollection":
                                jArray = (Newtonsoft.Json.Linq.JArray)exportAttribute.AttributeValue;

                                OptionSetValueCollection multiOption = new OptionSetValueCollection { };
                                foreach (var child in jArray.Children())
                                {
                                    multiOption.Add(new OptionSetValue { Value = (int)child["Value"] });
                                }
                                attributeValue = multiOption;
                                break;
                            //if it's money
                            case "Microsoft.Xrm.Sdk.Money":
                                jObject = (Newtonsoft.Json.Linq.JObject)exportAttribute.AttributeValue;
                                attributeValue = new Microsoft.Xrm.Sdk.Money { Value = (decimal)jObject["Value"] };
                                break;
                            //if it's a collection of child entities
                            case "Microsoft.Xrm.Sdk.EntityCollection":
                                //json.net will see it as a jarray
                                jArray = (Newtonsoft.Json.Linq.JArray)exportAttribute.AttributeValue;

                                //create and populate a list of exportentity objects so we can recurse
                                List<ExportEntity> childentities = new List<ExportEntity>();
                                foreach (var child in jArray.Children())
                                {
                                    ExportEntity childentity = (ExportEntity)JsonConvert.DeserializeObject<ExportEntity>(child.ToString());
                                    childentities.Add(childentity);
                                }

                                //create an empty entitycollection
                                EntityCollection collection = new EntityCollection();

                                //recurse and parse the returned list of entities to add each item in the list to the entitycollection object
                                foreach (var item in TransformExportEntityList(childentities))
                                {
                                    collection.Entities.Add(item);
                                }

                                //set the attribute value to the entitycollection
                                attributeValue = collection;
                                break;
                            //for entity images
                            case "System.Byte[]":
                                attributeValue = Convert.FromBase64String(exportAttribute.AttributeValue.ToString());
                                break;
                            //if it's anything else - i think this covers everything we would typically need
                            default:
                                attributeValue = exportAttribute.AttributeValue;
                                break;
                        }
                    }
                    catch (Exception ex) //if we get an error deserializing, set the attribute value to null and move along - this may not be the behavior you want!
                    {
                        attributeValue = null;
                        LogMessage("INFO", string.Format("    error deserializing {3} attribute {0} for entity type {1}, id {2}", attributeName, e.LogicalName, e.Id.ToString(), exportAttribute.AttributeType));
                    }

                    //add the attribute name and value to the entity's attributes collection
                    entity.Attributes.Add(attributeName, attributeValue);
                }

                //add the entity to the entity collection
                ec.Add(entity);
            }
            return ec;
        }

        /// <summary>
        /// transforms a list of crm entity records into a list of exportentity records
        /// </summary>
        /// <param name="entitylist"></param>
        /// <returns></returns>
        private List<ExportEntity> TransformEntityList(List<Entity> entitylist)
        {
            List<ExportEntity> entitiesToExport = new List<ExportEntity>();
            foreach (Entity e in entitylist)
            {
                //instantiate a new exportentity object and set its fields appropriately
                ExportEntity exportEntity = new ExportEntity();
                exportEntity.Id = e.Id;
                exportEntity.LogicalName = e.LogicalName;
                foreach (var attribute in e.Attributes)
                {
                    //leave out the entity id and logical name from the attribute collection - they cause problems on import
                    if ((attribute.Key.ToUpper() != e.LogicalName.ToUpper() + "ID")
                        && (attribute.Key.ToUpper() != "LOGICALNAME"))
                    {
                        ExportAttribute exportAttribute = new ExportAttribute();
                        exportAttribute.AttributeName = attribute.Key;
                        exportAttribute.AttributeType = attribute.Value.GetType().ToString();
                        if (exportAttribute.AttributeType == "Microsoft.Xrm.Sdk.EntityCollection")
                        {
                            //do recursion
                            EntityCollection ec = (EntityCollection)attribute.Value;
                            List<Entity> entities = new List<Entity>();
                            foreach(var entity in ec.Entities)
                            {
                                entities.Add(entity);
                            }
                            exportAttribute.AttributeValue = TransformEntityList(entities);
                        }
                        else
                        {
                            exportAttribute.AttributeValue = attribute.Value;
                        }
                        exportEntity.Attributes.Add(exportAttribute);
                    }
                }

                //add the exportentity object to the recordset
                entitiesToExport.Add(exportEntity);
            }

            return entitiesToExport;
        }

        private string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int place = Source.LastIndexOf(Find);

            if (place == -1)
                return Source;

            string result = Source.Remove(place, Find.Length).Insert(place, Replace);
            return result;
        }
    }

    /// <summary>
    /// class used to save data to a file
    /// </summary>
    [Serializable()]
    class ExportedData
    {
        public Guid BaseBu { get; set; }
        public Guid BaseTeam { get; set; }
        public Guid BaseCurrency { get; set; }
        public List<List<ExportEntity>> RecordSets { get; set; }

        public ExportedData()
        {
            RecordSets = new List<List<ExportEntity>>();
            BaseBu = Guid.Empty;
            BaseTeam = Guid.Empty;
            BaseCurrency = Guid.Empty;
            Entity e = new Entity();
        }
    }

    /// <summary>
    /// class used to represent a crm record - tried serializing crm sdk classes, but they would not deserialize properly
    /// </summary>
    [Serializable()]
    class ExportEntity
    {
        public string LogicalName { get; set; }
        public Guid Id { get; set; }
        public List<ExportAttribute> Attributes { get; set; }

        public ExportEntity()
        {
            Attributes = new List<ExportAttribute>(); ;
        }
    }

    /// <summary>
    /// class used to represent a crm attribute value
    /// </summary>
    [Serializable()]
    class ExportAttribute
    {
        public string AttributeName { get; set; }
        public object AttributeValue { get; set; }
        public string AttributeType { get; set; }
    }
}
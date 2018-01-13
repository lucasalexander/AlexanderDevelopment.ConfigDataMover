// --------------------------------------------------------------------------------------------------------------------
// MainWindow.xaml.cs
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using Microsoft.Win32;
using AlexanderDevelopment.ConfigDataMover.Lib;

namespace AlexanderDevelopment.ConfigDataMover.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int _stepCounter;
        Importer _importer;

        private string _source;
        private string _target;

        public MainWindow()
        {
            InitializeComponent();
            stepListBox.DisplayMemberPath = "StepName";
            _stepCounter = 0;
            _source = string.Empty;
            _target = string.Empty;
            Application.Current.MainWindow = this;
        }

        protected void openJobButton_Click(object sender, EventArgs args)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XML file|*.xml";
            openFileDialog1.Title = "Open job file";
            if (openFileDialog1.ShowDialog() == true)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                string jobdata = (sr.ReadToEnd());
                sr.Close();

                XmlDocument xml = new XmlDocument();
                try
                {
                    xml.LoadXml(jobdata);
                    stepListBox.Items.Clear();
                    mappingsTextBox.Clear();
                    saveConnectionsCheckBox.IsChecked = false;
                    mapBuCheckBox.IsChecked = false;
                    mapCurrencyCheckBox.IsChecked = false;
                    //sourceTextBox.Text = string.Empty;
                    //targetTextBox.Text = string.Empty;
                    _source = string.Empty;
                    _target = string.Empty;

                    XmlNodeList stepList = xml.GetElementsByTagName("Step");
                    foreach (XmlNode xn in stepList)
                    {
                        JobStep step = new JobStep();
                        step.StepName = xn.SelectSingleNode("Name").InnerText;
                        step.StepFetch = xn.SelectSingleNode("Fetch").InnerText;

                        step.UpdateOnly = false;
                        if (xn.Attributes["updateOnly"] != null)
                            step.UpdateOnly = Convert.ToBoolean(xn.Attributes["updateOnly"].Value);

                        step.CreateOnly = false;
                        if (xn.Attributes["createOnly"] != null)
                            step.CreateOnly = Convert.ToBoolean(xn.Attributes["createOnly"].Value);

                        step.ManyMany = false;
                        if (xn.Attributes["manyMany"] != null)
                            step.ManyMany = Convert.ToBoolean(xn.Attributes["manyMany"].Value);

                        stepListBox.Items.Add(step);
                    }

                    XmlNodeList configData = xml.GetElementsByTagName("JobConfig");
                    mapBuCheckBox.IsChecked = Convert.ToBoolean(configData[0].Attributes["mapBuGuid"].Value);
                    mapCurrencyCheckBox.IsChecked = Convert.ToBoolean(configData[0].Attributes["mapCurrencyGuid"].Value);

                    XmlNodeList mappingList = xml.GetElementsByTagName("GuidMapping");

                    foreach (XmlNode xn in mappingList)
                    {

                        string sourceId = xn.Attributes["source"].Value;
                        string targetId = xn.Attributes["target"].Value;
                        mappingsTextBox.Text += string.Format("{0}->{1}"+ Environment.NewLine, sourceId, targetId);
                        //_mappingDt.Rows.Add(sourceId, targetId);
                    }
                    //guidMappingDataGrid.DataContext = _mappingDt.DefaultView;

                    XmlNodeList connectionNodes = xml.GetElementsByTagName("ConnectionDetails");
                    if (connectionNodes.Count > 0)
                    {
                        SetSource(connectionNodes[0].Attributes["source"].Value);
                        SetTarget(connectionNodes[0].Attributes["target"].Value);

                        saveConnectionsCheckBox.IsChecked = Convert.ToBoolean(connectionNodes[0].Attributes["save"].Value);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Could not parse job configuration data in {0}", openFileDialog1.FileName));
                }
            }

        }

        /// <summary>
        /// saves the job from a file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveJobButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "XML file|*.xml";
            saveFileDialog1.Title = "Save job data";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                try
                {
                    ValidateSteps();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("The job steps could not be validated. Save aborted.\n\n{0}", ex.Message), "Validation error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                XmlDocument doc = new XmlDocument();
                XmlElement elRoot = (XmlElement)doc.AppendChild(doc.CreateElement("ConfigDataJob"));
                XmlElement elJobConfig = (XmlElement)elRoot.AppendChild(doc.CreateElement("JobConfig"));
                elJobConfig.SetAttribute("mapBuGuid", (mapBuCheckBox.IsChecked.HasValue ? mapBuCheckBox.IsChecked.Value : false).ToString());
                elJobConfig.SetAttribute("mapCurrencyGuid", (mapCurrencyCheckBox.IsChecked.HasValue ? mapCurrencyCheckBox.IsChecked.Value : false).ToString()); 

                XmlElement elSteps = (XmlElement)elRoot.AppendChild(doc.CreateElement("JobSteps"));
                foreach (var item in stepListBox.Items)
                {
                    JobStep step = (JobStep)item;
                    XmlElement elStep = doc.CreateElement("Step");
                    elStep.AppendChild(doc.CreateElement("Name")).InnerText = step.StepName;

                    //default createonly, updateonly and manymany attributes to false
                    elStep.SetAttribute("updateOnly", false.ToString());
                    elStep.SetAttribute("createOnly", false.ToString());
                    elStep.SetAttribute("manyMany", false.ToString());

                    //check whether to set any createonly, updateonly and manymany attributes to true
                    if (step.CreateOnly)
                    {
                        elStep.SetAttribute("updateOnly", false.ToString());
                        elStep.SetAttribute("createOnly", true.ToString());
                        elStep.SetAttribute("manyMany", false.ToString());
                    }
                    if (step.UpdateOnly)
                    {
                        elStep.SetAttribute("updateOnly", true.ToString());
                        elStep.SetAttribute("createOnly", false.ToString());
                        elStep.SetAttribute("manyMany", false.ToString());
                    }
                    if (step.ManyMany)
                    {
                        elStep.SetAttribute("updateOnly", false.ToString());
                        elStep.SetAttribute("createOnly", false.ToString());
                        elStep.SetAttribute("manyMany", true.ToString());
                    }
                    elStep.AppendChild(doc.CreateElement("Fetch")).InnerText = step.StepFetch;
                    elSteps.AppendChild(elStep);
                }

                XmlElement elMappings = (XmlElement)elRoot.AppendChild(doc.CreateElement("GuidMappings"));
                List<string> mappingslist = new List<string>(System.Text.RegularExpressions.Regex.Split(mappingsTextBox.Text, Environment.NewLine));
                foreach(var mapping in mappingslist)
                {
                    try
                    {
                        string[] mappingarr = mapping.Split(new string[] { "->" }, StringSplitOptions.None);
                        if(mappingarr.Length==2)
                        {
                            Guid sourceGuid = new Guid(mappingarr[0]);
                            Guid targetGuid = new Guid(mappingarr[1]);
                            XmlElement elMapping = doc.CreateElement("GuidMapping");
                            elMapping.SetAttribute("source", sourceGuid.ToString());
                            elMapping.SetAttribute("target", targetGuid.ToString());
                            elMappings.AppendChild(elMapping);
                        }
                    }
                    catch(Exception ex)
                    {
                        //assume if we get exception, line is formatted badly or the values aren't guids - just do nothing
                    }
                }
                if (saveConnectionsCheckBox.IsChecked.HasValue ? saveConnectionsCheckBox.IsChecked.Value : false)
                {
                    XmlElement elConnection = (XmlElement)elRoot.AppendChild(doc.CreateElement("ConnectionDetails"));
                    elConnection.SetAttribute("source", _source);
                    elConnection.SetAttribute("target", _target);
                    elConnection.SetAttribute("save", "True");
                }

                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                sw.Write(doc.OuterXml);
                sw.Close();
            }
        }

        /// <summary>
        /// runs the job
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runJobButton_Click(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to run this job?", "Confirm run job", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel)
            {
                return;
            }

            //do some basic validations
            if (string.IsNullOrEmpty(_source))
            {
                MessageBox.Show("no source connection specified");
                return;
            }
            if (string.IsNullOrEmpty(_target))
            {
                MessageBox.Show("no target connection specified");
                return;
            }
            if (!(stepListBox.Items.Count > 0))
            {
                MessageBox.Show("no steps in job");
                return;
            }

            //change the cursor
            Cursor = Cursors.Wait;

            //prepare the list of GUID mappings to pass to the importer object
            List<GuidMapping> mappings = new List<GuidMapping>();
            List<string> mappingslist = new List<string>(System.Text.RegularExpressions.Regex.Split(mappingsTextBox.Text, Environment.NewLine));
            foreach (var mapping in mappingslist)
            {
                try
                {
                    string[] mappingarr = mapping.Split(new string[] { "->" }, StringSplitOptions.None);
                    if (mappingarr.Length == 2)
                    {
                        Guid sourceGuid = new Guid(mappingarr[0]);
                        Guid targetGuid = new Guid(mappingarr[1]);
                        mappings.Add(new GuidMapping { sourceId = sourceGuid, targetId = targetGuid });
                    }
                }
                catch (Exception ex)
                {
                    //assume if we get exception, line is formatted badly or the values aren't guids - just do nothing
                }
            }

            //prepare the list of job steps to pass to the importer object
            List<JobStep> steps = new List<JobStep>();
            foreach (var item in stepListBox.Items)
            {
                JobStep step = (JobStep)item;
                steps.Add(step);
            }

            //instantiate the importer object and set its properties
            _importer = new Importer();
            _importer.GuidMappings = mappings;
            _importer.JobSteps = steps;
            _importer.SourceString = _source;
            _importer.TargetString = _target;
            _importer.MapBaseBu = mapBuCheckBox.IsChecked.HasValue ? mapBuCheckBox.IsChecked.Value : false;
            _importer.MapBaseCurrency = mapCurrencyCheckBox.IsChecked.HasValue ? mapCurrencyCheckBox.IsChecked.Value : false;

            //subscribe to the importer object progress update event
            _importer.OnProgressUpdate += ImportStatusUpdate;

            //set up and call the backgroundworker to do the CRM queries and writing
            var worker = new BackgroundWorker();
            worker.DoWork += WorkerDoWork;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;

            worker.RunWorkerAsync();
        }

        /// <summary>
        /// updates the status label
        /// </summary>
        /// <param name="status"></param>
        void ImportStatusUpdate(string status)
        {
            Dispatcher.Invoke((Action)delegate
            {
                statusLabel.Content = status;
            });
        }

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var bw = (BackgroundWorker)sender;

            //start the importer process
            _importer.Process();
        }

        private void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //change cursor back
            Cursor = Cursors.Arrow;

            //unsubscribe from the importer's progress updates
            _importer.OnProgressUpdate -= ImportStatusUpdate;

            if (e.Error != null)
            {
                //MessageBox.Show(string.Format("An error prevented the job from executing: {0}", e.Error.Message), "Fatal job error", MessageBoxButton.OK, MessageBoxImage.Error);
                JobError errorbox = new JobError();
                errorbox.SetDetails(string.Format("An error prevented the job from executing: {0}", e.Error.Message), e.Error.ToString());
                errorbox.ShowDialog();
            }
            else
            {
                int errorCount = _importer.ErrorCount;

                _importer = null;
                //clear the status label
                ImportStatusUpdate("");


                //show a message to the user
                if (errorCount == 0)
                {
                    MessageBox.Show("Job finished with no errors.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Job finished with errors. See the RecordError.log file for more details.", "Import errors", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }




        public void SetSource(string connectionstring)
        {
            this._source = connectionstring;
            //this.sourceTextBox.Text = connectionstring;

            var dict = Utility.ParseConnectionString(connectionstring);

            if (dict.ContainsKey("FILE"))
            {
                this.sourceLabel.Content = dict["FILE"];
            }
            else if (dict.ContainsKey("URL"))
            {
                this.sourceLabel.Content = dict["URL"];
            }
        }

        public void SetTarget(string connectionstring)
        {
            this._target = connectionstring;
            //this.targetTextBox.Text = connectionstring;

            var dict = Utility.ParseConnectionString(connectionstring);

            if (dict.ContainsKey("FILE"))
            {
                this.targetLabel.Content = dict["FILE"];
            }
            else if (dict.ContainsKey("URL"))
            {
                this.targetLabel.Content = dict["URL"];
            }
        }

        /// <summary>
        /// adds a new step
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addStepButton_Click(object sender, EventArgs e)
        {
            _stepCounter++;
            string stepname = "step " + (_stepCounter).ToString();
            stepListBox.Items.Add(new JobStep { StepName = stepname, StepFetch = string.Empty });
        }

        /// <summary>
        /// loads the step details for editing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stepListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (stepListBox.SelectedIndex != -1)
            {
                JobStep step = (JobStep)stepListBox.SelectedItem;
                stepNameTextBox.Text = step.StepName;
                stepFetchTextBox.Text = step.StepFetch;
                stepTypeComboBox.SelectedValue = "Create and update";
                if (step.UpdateOnly)
                {
                    stepTypeComboBox.SelectedValue = "Update only";
                }
                if (step.CreateOnly)
                {
                    stepTypeComboBox.SelectedValue = "Create only";
                }
                if(step.ManyMany)
                {
                    stepTypeComboBox.SelectedValue = "Many to many";
                }
                //updateOnlyCheckBox.IsChecked = step.UpdateOnly;
            }
            else
            {
                stepNameTextBox.Text = string.Empty;
                stepFetchTextBox.Text = string.Empty;
                stepTypeComboBox.SelectedIndex = 0;
                //updateOnlyCheckBox.IsChecked = false;
            }
        }

        /// <summary>
        /// moves a step forward in the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveUpButton_Click(object sender, EventArgs e)
        {
            var selectedIndex = stepListBox.SelectedIndex;

            if (selectedIndex > 0)
            {
                var itemToMoveUp = stepListBox.Items[selectedIndex];
                stepListBox.Items.RemoveAt(selectedIndex);
                stepListBox.Items.Insert(selectedIndex - 1, itemToMoveUp);
                stepListBox.SelectedIndex = selectedIndex - 1;
            }
        }

        /// <summary>
        /// moves a step backward in the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveDownButton_Click(object sender, EventArgs e)
        {
            var selectedIndex = stepListBox.SelectedIndex;

            if (selectedIndex + 1 < stepListBox.Items.Count)
            {
                var itemToMoveDown = stepListBox.Items[selectedIndex];
                stepListBox.Items.RemoveAt(selectedIndex);
                stepListBox.Items.Insert(selectedIndex + 1, itemToMoveDown);
                stepListBox.SelectedIndex = selectedIndex + 1;
            }
        }

        /// <summary>
        /// clears all steps from a job
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearAllMappingsButton_Click(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to remove all mappings? There is no undo.", "Confirm clear all mappings", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                mappingsTextBox.Clear();
            }
        }

        /// <summary>
        /// clears all steps from a job
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearAllButton_Click(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to remove all steps? There is no undo.", "Confirm clear all steps", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                stepListBox.Items.Clear();
                stepNameTextBox.Text = string.Empty;
                stepFetchTextBox.Text = string.Empty;
                stepTypeComboBox.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// removes a step
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeStepButton_Click(object sender, EventArgs e)
        {
            if (stepListBox.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to remove this step? There is no undo.", "Confirm step removal", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    stepListBox.Items.RemoveAt(stepListBox.SelectedIndex);
                }
            }
        }

        /// <summary>
        /// updates the step details
        /// </summary>
        private void updateStepButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedindex = stepListBox.SelectedIndex;
            if (stepListBox.SelectedIndex != -1)
            {
                //if fetchxml looks ok, update the step
                if (isParseableXml())
                {
                    JobStep step = (JobStep)stepListBox.SelectedItem;
                    step.StepName = stepNameTextBox.Text;
                    step.StepFetch = stepFetchTextBox.Text;
                    switch ((string)stepTypeComboBox.SelectedValue) {
                        case "Create only":
                            step.UpdateOnly = false;
                            step.CreateOnly = true;
                            step.ManyMany = false;
                            break;
                        case "Update only":
                            step.UpdateOnly = true;
                            step.CreateOnly = false;
                            step.ManyMany = false;
                            break;
                        case "Many to many":
                            step.UpdateOnly = false;
                            step.CreateOnly = false;
                            step.ManyMany = true;
                            break;
                        default:
                            step.UpdateOnly = false;
                            step.CreateOnly = false;
                            step.ManyMany = false;
                            break;
                    }
                    stepListBox.Items[stepListBox.SelectedIndex] = stepListBox.SelectedItem;
                    stepListBox.Items.Refresh();
                    stepListBox.SelectedIndex = selectedindex;
                }
                else
                {
                    //otherwise alert the user and set focus back on the inputbox
                    MessageBox.Show("Input could not be parsed as XML.");
                    this.stepFetchTextBox.Focus();
                }
            }
            else
            {
                //otherwise alert the user and set focus back on the inputbox
                MessageBox.Show("No step selected");
            }
        }


        /// <summary>
        /// tries to load the step fetchxml as an xmldocument just to make sure it's well-formed xml - does not actually validate the fetchxml schema
        /// </summary>
        /// <returns></returns>
        bool isParseableXml()
        {
            //only validate if the textbox is not null, empty or full of whitespace
            if (!string.IsNullOrWhiteSpace(this.stepFetchTextBox.Text))
            {
                try
                {
                    XmlDocument document = new XmlDocument();
                    document.LoadXml(this.stepFetchTextBox.Text);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        void ValidateSteps()
        {
            //load CRM fetchxml schema to use for validation later- https://msdn.microsoft.com/en-us/library/gg309405.aspx
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            try
            {
                schemaSet.Add(null, "fetch.xsd");
            }
            catch (Exception ex)
            {
                throw new Exception("Could not load FetchXML XSD to use for validation.");
            }

            //loop through each job step
            for (int i = 0; i < stepListBox.Items.Count; i++)
            {
                string errormsg = "";
                if (string.IsNullOrWhiteSpace(((JobStep)stepListBox.Items[i]).StepName))
                {
                    errormsg = string.Format("Step #{0} has no name", i + 1);
                    throw new Exception(errormsg);
                }

                if (string.IsNullOrWhiteSpace(((JobStep)stepListBox.Items[i]).StepFetch))
                {
                    errormsg = string.Format("Step \"{0}\" has no query statement", ((JobStep)stepListBox.Items[i]).StepName);
                    throw new Exception(errormsg);
                }

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add(schemaSet);
                settings.ValidationEventHandler += (sender, args) =>
                {
                    if (args.Severity == XmlSeverityType.Warning)
                    {
                        errormsg = string.Format("Step \"{0}\" matching schema not found.  No validation occurred. {1}", ((JobStep)stepListBox.Items[i]).StepName, args.Message);
                    }
                    else
                    {
                        errormsg = string.Format("Step \"{0}\" query schema validation error: {1}", ((JobStep)stepListBox.Items[i]).StepName, args.Message);
                    }
                    throw new Exception(errormsg);
                };
                settings.ValidationType = ValidationType.Schema;
                try
                {
                    //create an xmlreader from the step fetch with the validation settings from above
                    StringReader stringReader = new StringReader(((JobStep)stepListBox.Items[i]).StepFetch);
                    XmlReader xreader = XmlReader.Create(stringReader, settings);

                    //read just to trigger the validation
                    while (xreader.Read()) { }

                    //close the reader
                    xreader.Close();
                }
                catch (XmlException ex)
                {
                    errormsg = string.Format("Step \"{0}\" query could not be loaded or read", ((JobStep)stepListBox.Items[i]).StepName);
                    throw new Exception(errormsg);
                }
                catch (InvalidOperationException ex)
                {
                    errormsg = string.Format("Step \"{0}\" query could not be loaded or read", ((JobStep)stepListBox.Items[i]).StepName);
                    throw new Exception(errormsg);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private void aboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void checkversionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            CheckLatestVersion checkversion = new CheckLatestVersion();
            checkversion.ShowDialog();
        }

        private void setsourceButton_Click(object sender, RoutedEventArgs e)
        {
            SetConnection setconnection = new SetConnection(_source, true);
            setconnection.ShowDialog();
        }

        private void settargetButton_Click(object sender, RoutedEventArgs e)
        {
            SetConnection setconnection = new SetConnection(_target, false);
            setconnection.ShowDialog();
        }
    }

    public static class Utility
    {
        public static Dictionary<string, string> ParseConnectionString(string connectionstring)
        {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (!string.IsNullOrWhiteSpace(connectionstring))
            {
                var elements = connectionstring.Split(';');
                foreach (var element in elements)
                {
                    string trimelement = element.Trim();
                    if (!string.IsNullOrWhiteSpace(trimelement))
                    {
                        if (trimelement.Contains("="))
                        {
                            var kvp = trimelement.Split("=".ToCharArray());
                            dict.Add(kvp[0], kvp[1]);
                        }
                    }
                }
            }
            else
            {
                throw new Exception("Connection string cannot be null or empty");
            }
            return dict;
        }
    }
}

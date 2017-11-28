# AlexanderDevelopment.ConfigDataMover
This is a tool used for making sure that configuration data like teams and queues have the same record GUIDs in all of a Dynamics CRM project's different environments for development, testing and production. For more information, please see the related posts on my blog: [http://alexanderdevelopment.net/tag/configuration-data-mover/](http://alexanderdevelopment.net/tag/configuration-data-mover/)

Here's an explanation of the various settings and parameters that can be specified in the GUI:

1. Source (one of the following):
   1. CRM connection string for source organization 
   1. Path to source data file - value will start with "file="
      1. Full path to source data JSON file in the form of "C:\datadirectory\datafile.json."
      1. Relative path to source data JSON file in the form of "..\datafile.json." (Path is relative to working directory.)
   1. Raw JSON string with contents of an export file - value will start with "rawjson="
1. Target (one of the following)
   1. CRM connection string for source organization 
   1. Path to target data file - value will start with "file="
      1. Full path to target data JSON file in the form of "C:\datadirectory\datafile.json."
      1. Relative path to target data JSON file in the form of "..\datafile.json." (Path is relative to working directory.)
1. Save connection details? - If checked, connection details should be saved in configuration file. Note, connection details are unencrypted.
1. Map root business unit GUID? - If checked, the job will automatically change any GUID references from the source organization root business unit to the target organization root business unit. This will also apply to any GUID references for the root business unit's default team.
1. Map base currency GUID? - If checked, the job will automatically change any GUID references from the source organization base currency to the target organization base currency.
1. Job steps - A list of steps for each distinct entity transfer operation. 
1. Step name - Sets the name of the selected job step. The step name is used in logging outputs.
1. Step FetchXML query - The FetchXML query that determines which records and specific attributes will be transfered.
1. Step type - Sets a step's behavior to (a) only create new records, (b) only update existing records, (c) create new records and update existing records or (d) create a many-to-many association
1. GUID mappings - List of GUIDs in the source organization that will be replaced with GUIDs in the target organization.

### Caveats/considerations:

1. The GUI client does a basic validation to check if the supplied query is valid XML when you enter it. When you save a job, each step's FetchXML is valdiated against the CRM 2016 FetchXML XSD, and any validation issues will prevent saving. Note, this only validates the FetchXML is structurally valid, but it does not check for correctness as far as entity names, attribute names, relationships, etc. All FetchXML queries are also checked by the importer at runtime, so any validation problems will prevent job execution.
1. Following up on the previous point, if you include an attribute in the query that you aren't able to update in the target, the tool won't know any better, and you will get an error when the job is executed.
1. Because only the attributes included in the FetchXML query are used for updating/creating a record in the target system, you can sequence record creation and update steps by using different FetchXML queries in different steps. 
1. The FetchXML query for a many-to-many job step should be a query against the relationship entity (relationship entity name on the many-to-many relationship form) that includes the GUID fields for each entity. The relationship entities cannot be queried in the advanced find builder, so you must write the FetchXML manually or use a separate query builder.
1. A many-to-many job step will create many-to-many record associations, but it will not delete any existing N:N associations in the target system that have been removed in the source.
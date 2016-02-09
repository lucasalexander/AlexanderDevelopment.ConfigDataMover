# AlexanderDevelopment.ConfigDataMover
This is a tool used for making sure that configuration data like teams and queues have the same record GUIDs in all of a Dynamics CRM project's different environments for development, testing and production. For more information, please see the related posts on my blog: http://alexanderdevelopment.net/tag/configuration-data-mover

Here's an explanation of the various settings and parameters that can be specified in the GUI:

1. Source - Simplified CRM connection string for source organization OR full path to source data file in the form of "FILE=C:\datadirectory\datafile.json."
2. Target - Simplified CRM connection string for target organization OR full path to target data file in the form of "FILE=C:\datadirectory\datafile.json."
3. Save connection details? - If checked, connection details should be saved in configuration file. Note, connection details are unencrypted.
4. Map root business unit GUID? - If checked, the job will automatically change any GUID references from the source organization root business unit to the target organization root business unit.
5. Map base currency GUID? - If checked, the job will automatically change any GUID references from the source organization base currency to the target organization base currency.
6. Job steps - A list of steps for each distinct entity transfer operation. 
7. Step name - Sets the name of the selected job step. The step name is used in logging outputs.
8. Step FetchXML query - The FetchXML query that determines which records and specific attributes will be transfered.
9. Update-only step? - If checked, the step will not attempt a create operation if the update attempt fails.
10. GUID mappings - List of GUIDs in the source organization that will be replaced with GUIDs in the target organization.

Sample job files are available in the sample files directory on GitHub here: https://github.com/lucasalexander/AlexanderDevelopment.ConfigDataMover/tree/master/samplefiles.

###Caveats/considerations:

1. The GUI client does a basic validation to check if the supplied query is valid XML, but it does not check for a valid FetchXML query. If something is wrong with your query, the error won't show up until a job is executed.
2. Following up on the previous point, if you include an attribute in the query that you aren't able to update in the target, the tool won't know any better, and you will get an error when the job is executed.
3. Because only the attributes included in the FetchXML query are used for updating/creating a record in the target system, you can sequence record creation and update steps by using different FetchXML queries in different steps. 
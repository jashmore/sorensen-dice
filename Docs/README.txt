To use this project:

1. In the SorensenDice.sql file (in this folder), create the MedicalTerminology table on your SQL Server.

2. Import the data from CMS32_DESC_LONG_SHORT_DX.xlsx (in this folder) to the MedicalTerminology, mapping the columns appropriately. This file contains a list of approximately 14,000 medical diagnoses.

3. Compile the solution. Note that the version of .NET that the Helper project is compiled with will have to be no later than the version of .NET that the SQL Server is using. You can determine this using the query at the top of the included SorensenDice.sql file.

4. Copy the dlls from helper\bin\{debug|release}. There is a batch file in the root of the project to help with this, but you will need to change the paths as required. Note that your SQL Server instance will need read / execute permissions on the path that the files are copied to.

5. On line 38 of SorensenDice.sql, enter the path to which you copied the dll in (4).

6. Run the rest of the SorensenDice.sql script. If all is well you will see a list of search results based on the search term 'pulm exam'.

7. Run the Console project (first changing the connection string in app.config to allow access to the database where you ran the scripts).

8. Test the implementation using different search terms. Note that the search is based on the 'Short Description' in the database table. You can change this to the long description on line 53 of the sql script.
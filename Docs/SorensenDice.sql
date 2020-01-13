/*

https://docs.microsoft.com/en-us/sql/relational-databases/clr-integration-database-objects-user-defined-functions/clr-scalar-valued-functions?view=sql-server-ver15
https://www.skylinetechnologies.com/Blog/Skyline-Blog/March-2013/CLR-Functions-in-SQL-Server-A-Tutorial

create table MedicalProcedure
(	
	Code varchar(8) not null primary key,
	ShortDescription varchar(64),
	LongDescription varchar(256),
	MedicalProcedure add Fingerprint varchar(max)
)

select * from MedicalProcedure

*/


sp_configure 'show advanced options', 1
RECONFIGURE
GO
sp_configure 'clr enabled', 1
RECONFIGURE
GO
EXEC sp_configure 'clr strict security', 0
RECONFIGURE
GO
sp_configure 'show advanced options', 0
RECONFIGURE
GO

-- drop Assembly SorensenDice.Helper
create Assembly SorensenDice from 'C:\Users\Jo209000\Documents\CLR Libraries\SorensenDice.Helper.dll' with Permission_set = SAFE
GO


SELECT name, value   
FROM sys.dm_clr_properties;  


-- drop Function HelloWorld
Create Function HelloWorld() returns nvarchar(max)
AS
	External name SorensenDice.[SorensenDice.Helper.CLRFunctions].HelloWorld
GO


-- drop Function HelloWorld
Create Function ComputeSorensenDiceIndex(@IPFingerprint1 varchar(max), @IPFingerprint2 varchar(max)) returns decimal
AS
	External name SorensenDice.[SorensenDice.Helper.CLRFunctions].ComputeSorensenDiceIndex
GO

select dbo.HelloWorld()
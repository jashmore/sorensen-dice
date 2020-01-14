/*

NOTE: THE ClrFunctions dll needs to be compiled using the same version as the sql server is using. 
Use the following query to determine this:

SELECT Name, Value   
FROM sys.dm_clr_properties; 

*/

GO

IF NOT EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='MedicalTerminology')
BEGIN
	CREATE TABLE MedicalTerminology
	(	
		Code nvarchar(8) not null primary key,
		ShortDescription nvarchar(64),
		LongDescription nvarchar(256),
		Fingerprint nvarchar(max)
	)
END

GO
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

CREATE ASSEMBLY SorensenDice FROM 'YOUR_PATH\SorensenDice.Helper.dll' WITH PERMISSION_SET = SAFE
GO

Create Function ComputeSorensenDiceIndex(@IPFingerprint1 NVARCHAR(MAX), @IPFingerprint2 NVARCHAR(MAX)) RETURNS DECIMAL(8,6)
AS
	External name SorensenDice.[SorensenDice.Helper.CLRFunctions].ComputeSorensenDiceIndex
GO

--drop function GetSorensenDiceFingerprint
Create Function GetSorensenDiceFingerprint(@IPInput NVARCHAR(MAX)) RETURNS NVARCHAR(MAX)
AS
	External name SorensenDice.[SorensenDice.Helper.CLRFunctions].GetSorensenDiceFingerprint
GO


UPDATE MedicalTerminology SET Fingerprint = dbo.GetSorensenDiceFingerprint (ShortDescription)

GO

IF EXISTS(SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES R
 WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME='GetMatchesBySorensenDice')
	DROP PROCEDURE GetMatchesBySorensenDice
GO
CREATE PROCEDURE GetMatchesBySorensenDice

@IPInputFingerprint NVARCHAR(MAX)

AS
BEGIN

	SELECT 
		TOP 20
		Code,
		ShortDescription,
		LongDescription,
		dbo.ComputeSorensenDiceIndex(@IPInputFingerprint, Fingerprint) AS Score
	FROM
		MedicalTerminology
	ORDER BY 
		Score DESC
END

GO

-- Test it!
DECLARE  @fingerprint NVARCHAR(MAX)
SET @fingerprint = (select dbo.GetSorensenDiceFingerprint ('puln ex'));
print @fingerprint;
EXEC GetMatchesBySorensenDice @fingerprint;
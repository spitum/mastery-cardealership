USE master
GO

DECLARE @DatabaseName nvarchar(50)
SET @DatabaseName = N'GuildCars'

DECLARE @SQL varchar(max)

SELECT @SQL = COALESCE(@SQL,'') + 'Kill ' + Convert(varchar, SPId) + ';'
FROM MASTER..SysProcesses
WHERE DBId = DB_ID(@DatabaseName) AND SPId <> @@SPId

--SELECT @SQL 
EXEC(@SQL)

IF EXISTS(SELECT * FROM sys.databases WHERE name='GuildCars')
DROP DATABASE GuildCars
GO

CREATE DATABASE GuildCars
GO
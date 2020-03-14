IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'Twitt')

BEGIN
ALTER DATABASE [Twitt] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
DROP database [Twitt]
END



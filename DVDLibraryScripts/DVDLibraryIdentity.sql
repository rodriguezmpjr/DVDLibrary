USE master
GO

IF NOT EXISTS 
    (SELECT name
     FROM master.sys.server_principals
     WHERE name = 'dvdUser')
BEGIN
    create login dvdUser with password ='testing123'
END

USE DVDLibrary;
GO

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'dvdUser')
BEGIN
    CREATE USER dvdUser FOR LOGIN dvdUser
END;
GO



USE DVDLibrary
GRANT EXECUTE on AddADvd to dvdUser
GO
GRANT EXECUTE on EditADvd to dvdUser
GO


GRANT SELECT on DVD to dvdUser
GRANT INSERt on DVD to dvdUser
GRANT Update on DVD to dvdUser
GRANT DELETE on DVD to dvdUser
GO

GRANT SELECT on Director to dvdUser
GRANT INSERt on Director to dvdUser
GRANT Update on Director to dvdUser
GRANT DELETE on Director to dvdUser
GO

GRANT SELECT on Rating to dvdUser
GRANT INSERt on Rating to dvdUser
GRANT Update on Rating to dvdUser
GRANT DELETE on Rating to dvdUser
GO
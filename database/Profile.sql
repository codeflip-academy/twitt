USE [Twitt]

GO

CREATE TABLE [Profile]
(
    [ID] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    [FullName] VARCHAR(50) NOT NULL,
    [UserName] VARCHAR(50) NOT NULL,
    [Password] VARCHAR(16) NOT NULL,
    [Picture] VARBINARY NULL,
	[Description] VARCHAR(200) NULL,
	[Status] BIT NOT NULL,
)

GO
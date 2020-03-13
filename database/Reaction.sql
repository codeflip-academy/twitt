USE [Twitt]

GO

CREATE TABLE [Reaction]
(
	[ID] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[LikeOrDislike] bit NOT NULL,
	[Profile] INT NOT NULL,
	[Message] INT NOT NULL,
	 FOREIGN KEY ([Profile]) REFERENCES [Profile](ID),
	 FOREIGN KEY ([Message]) REFERENCES [Message](ID)
)

GO
USE [Twitt]

GO

CREATE TABLE [Reaction]
(
	[ID] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[LikeOrDislike] bit NOT NULL,
	[Profile] INT NOT NULL,
	[Post] INT NOT NULL,
	 FOREIGN KEY ([Profile]) REFERENCES [Profile](ID),
	 FOREIGN KEY ([Post]) REFERENCES [Post](ID)
)

GO
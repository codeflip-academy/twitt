USE [Twitt]

GO

CREATE TABLE [Reaction]
(
	[State] BIT NOT NULL,
	[Profile] INT NOT NULL,
	[Post] INT NOT NULL,
	 FOREIGN KEY ([Profile]) REFERENCES [Profile](ID),
	 FOREIGN KEY ([Post]) REFERENCES [Post](ID)
)

GO
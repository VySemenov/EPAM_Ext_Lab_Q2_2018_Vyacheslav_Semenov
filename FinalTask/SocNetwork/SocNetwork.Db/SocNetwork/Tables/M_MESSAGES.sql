﻿CREATE TABLE [dbo].[M_MESSAGES]
(
	[MESSAGE_ID] INT NOT NULL PRIMARY KEY IDENTITY,
	[DIALOG_ID] INT NOT NULL,
	[AUTHOR_ID] INT NOT NULL,
	[TIME] DATETIME  NOT NULL,
	[TEXT] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_M_MESSAGES_M_DIALOGUES] FOREIGN KEY ([DIALOG_ID]) REFERENCES [M_DIALOGUES]([DIALOG_ID])
)
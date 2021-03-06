﻿CREATE TABLE [dbo].[M_POSTS]
(
	[PID] INT NOT NULL PRIMARY KEY IDENTITY,
	[AUTHOR_ID] INT NOT NULL,
	[PAGE_ID] INT NOT NULL,
	[PDATE] DATETIME NOT NULL,
	[CONTENT] nvarchar(800), 
    CONSTRAINT [FK_M_POSTS_M_USERS] FOREIGN KEY ([AUTHOR_ID]) REFERENCES [M_USERS]([UID])

)

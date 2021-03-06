﻿CREATE TABLE [dbo].[M_USERDETAILINFO]
(
	[UID] INT NOT NULL PRIMARY KEY,
	[CITY] NVARCHAR(30),
	[DOB] DATE,
	[INTERESTS] NVARCHAR(255),
	[AVATARFILENAME] NCHAR(50) NOT NULL, 
    CONSTRAINT [FK_M_USERDETAILINFO_M_USERS] FOREIGN KEY ([UID]) REFERENCES [M_USERS]([UID])
)

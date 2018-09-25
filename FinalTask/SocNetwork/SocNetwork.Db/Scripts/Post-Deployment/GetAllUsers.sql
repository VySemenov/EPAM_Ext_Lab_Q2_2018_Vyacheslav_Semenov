IF OBJECT_ID('[dbo].[GetAllUsers]') IS NOT NULL
	DROP PROCEDURE [dbo].[GetAllUsers]
GO

CREATE PROCEDURE [dbo].[GetAllUsers]
	@num INT
AS
BEGIN
	SELECT TOP(@num) * FROM [dbo].[M_USERS]
END
GO
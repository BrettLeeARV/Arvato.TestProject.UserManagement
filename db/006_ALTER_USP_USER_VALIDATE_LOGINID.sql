USE [UserManagement]
GO
/****** Object:  StoredProcedure [dbo].[USP_USER_VALIDATE_LOGINID]    Script Date: 15/5/2014 2:47:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[USP_USER_VALIDATE_LOGINID]
@LoginID NVARCHAR(50)
,@ID TINYINT
AS BEGIN

SELECT * FROM [User]
WHERE LoginID = @LoginID
AND ID != @ID

END

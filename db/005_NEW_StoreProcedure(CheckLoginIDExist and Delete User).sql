USE [UserManagement]
GO
/****** Object:  StoredProcedure [dbo].[USP_USER_DELETE]    Script Date: 15/5/2014 9:39:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_USER_DELETE]
@ID TINYINT
AS BEGIN

DELETE FROM [User]
WHERE ID = @ID

END

GO
/****** Object:  StoredProcedure [dbo].[USP_USER_VALIDATE_LOGINID]    Script Date: 15/5/2014 9:39:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_USER_VALIDATE_LOGINID]
@LoginID NVARCHAR(50)
AS BEGIN

SELECT * FROM [User]
WHERE LoginID = @LoginID

END

USE [UserManagement]
GO
/****** Object:  StoredProcedure [dbo].[USP_VIEW_BOOKING]    Script Date: 27/5/2014 5:59:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_VIEW_BOOKING]
(
@RefNum nvarchar(50)
)

AS BEGIN

SELECT ID,UserID,RoomID,StartDate,EndDate,DateCreated FROM Booking WHERE RefNum = @RefNum

END



GO

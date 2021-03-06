USE [UserManagement]
GO
/****** Object:  StoredProcedure [dbo].[USP_EDIT_BOOKING]    Script Date: 24/6/2014 11:12:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[USP_EDIT_BOOKING]
(
@ID int,
@RoomID int,
@StartDate Datetime,
@EndDate Datetime,
@ModifiedBy int
)

AS BEGIN

UPDATE Booking SET RoomID = @RoomID, StartDate = @StartDate, EndDate = @EndDate,
	ModifiedBy = @ModifiedBy, ModifiedDateTime = getdate()
WHERE ID = @ID

--Update all AssetBooking status to 0, only update back to 1 if save AssetBooking
UPDATE AssetBooking SET Status = 0 WHERE BookingID = @ID

SELECT @@RowCount
END


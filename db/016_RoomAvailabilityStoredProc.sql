USE [UserManagement]
GO

-- Create new USP_ROOM_AVAILABILITY
/****** Object:  StoredProcedure [dbo].[USP_ROOM_AVAILABILITY]    Script Date: 4/6/2014 4:55:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[USP_ROOM_AVAILABILITY]
@BookingID int
, @StartDate datetime
, @EndDate datetime
, @RoomID int
AS
BEGIN
	SELECT * FROM Booking b
		JOIN Room r on b.RoomID = r.ID
	WHERE b.ID <> @BookingID AND b.RoomID = @RoomID AND b.IsCanceled = 0
		AND (DATEADD(SECOND, 1,@StartDate) BETWEEN b.StartDate AND b.EndDate
		OR DATEADD(SECOND,-1,@EndDate) BETWEEN b.StartDate AND b.EndDate --If the input date clashes with other booking date
		OR DATEADD(SECOND,1,b.StartDate) BETWEEN @StartDate AND @EndDate
		OR DATEADD(SECOND,-1,b.EndDate) BETWEEN @StartDate AND @EndDate)--If the other booking is not in the input date range
END

GO


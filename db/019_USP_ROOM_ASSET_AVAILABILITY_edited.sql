USE [UserManagement]
GO
/****** Object:  StoredProcedure [dbo].[USP_ROOM_ASSET_AVAILABILITY]    Script Date: 23/6/2014 11:20:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[USP_ROOM_ASSET_AVAILABILITY]
@ID int
, @StartDate datetime
, @EndDate datetime
AS
BEGIN
	SELECT 'R-' + Cast(b.RoomID as varchar(20)) as BookedItem
	FROM Booking b
		JOIN Room r on b.RoomID = r.ID
	WHERE b.ID <> @ID AND b.IsCanceled = 0
		AND (DATEADD(SECOND, 1,@StartDate) BETWEEN b.StartDate AND b.EndDate
		OR DATEADD(SECOND,-1,@EndDate) BETWEEN b.StartDate AND b.EndDate --If the input date crash between other booking date
		OR DATEADD(SECOND,1,b.StartDate) BETWEEN @StartDate AND @EndDate
		OR DATEADD(SECOND,-1,b.EndDate) BETWEEN @StartDate AND @EndDate)--If the other booking is not in the input date range
	UNION ALL
	SELECT 'A-' + Cast(a.AssetID as varchar(20)) as BookedItem
	FROM Booking b
		JOIN AssetBooking a on b.ID = a.BookingID
		JOIN Asset c on a.AssetID = c.ID
	WHERE b.ID <> @ID AND b.IsCanceled = 0 AND a.Status = 1
		AND (DATEADD(SECOND, 1,@StartDate) BETWEEN b.StartDate AND b.EndDate
		OR DATEADD(SECOND,-1,@EndDate) BETWEEN b.StartDate AND b.EndDate --If the input date crash between other booking date
		OR DATEADD(SECOND,1,b.StartDate) BETWEEN @StartDate AND @EndDate
		OR DATEADD(SECOND,-1,b.EndDate) BETWEEN @StartDate AND @EndDate)--If the other booking is not in the input date range
END

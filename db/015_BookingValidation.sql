USE [UserManagement]
GO
ALTER TABLE AssetBooking
ALTER COLUMN Status bit NOT NULL

ALTER TABLE AssetBooking
ADD CONSTRAINT DF_mytable_Status DEFAULT 1 FOR Status
GO
/****** Object:  StoredProcedure [dbo].[USP_ROOM_ASSET_AVAILABILITY]    Script Date: 30/5/2014 11:33:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_ROOM_ASSET_AVAILABILITY]
@BookingID int
, @StartDate datetime
, @EndDate datetime
, @RoomID int
, @AssetID varchar(500)
, @Type varchar(10)
AS
BEGIN
IF @Type = 'Room'
BEGIN
	SELECT r.Name + ' : ' + Convert(varchar(20),b.StartDate,120)  + ' - ' + Convert(varchar(20),b.EndDate,120) AS Details FROM Booking b
		JOIN Room r on b.RoomID = r.ID
	WHERE b.ID <> @BookingID AND b.RoomID = @RoomID AND b.IsCanceled = 0
		AND (DATEADD(SECOND, 1,@StartDate) BETWEEN b.StartDate AND b.EndDate
		OR DATEADD(SECOND,-1,@EndDate) BETWEEN b.StartDate AND b.EndDate --If the input date crash between other booking date
		OR DATEADD(SECOND,1,b.StartDate) BETWEEN @StartDate AND @EndDate
		OR DATEADD(SECOND,-1,b.EndDate) BETWEEN @StartDate AND @EndDate)--If the other booking is not in the input date range
END
ELSE
BEGIN
	DECLARE @AssetTable as Table (Val int)

	DECLARE @xml XML
    SET @xml = N'<t>' + REPLACE(@AssetID,'|','</t><t>') + '</t>'
    INSERT INTO @AssetTable(Val)
    SELECT  r.value('.','varchar(MAX)') as item
    FROM  @xml.nodes('/t') as records(r)

	SELECT c.Name + ' : ' + Convert(varchar(20),b.StartDate,120)  + ' - ' + Convert(varchar(20),b.EndDate,120) AS Details FROM Booking b
		JOIN AssetBooking a on b.ID = a.BookingID
		JOIN Asset c on a.AssetID = c.ID
	WHERE b.ID <> @BookingID AND a.AssetID IN (Select Val from @AssetTable) AND b.IsCanceled = 0 AND a.Status = 1
		AND (DATEADD(SECOND, 1,@StartDate) BETWEEN b.StartDate AND b.EndDate
		OR DATEADD(SECOND,-1,@EndDate) BETWEEN b.StartDate AND b.EndDate --If the input date crash between other booking date
		OR DATEADD(SECOND,1,b.StartDate) BETWEEN @StartDate AND @EndDate
		OR DATEADD(SECOND,-1,b.EndDate) BETWEEN @StartDate AND @EndDate)--If the other booking is not in the input date range
END
END

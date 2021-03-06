USE [UserManagement]
GO
/****** Object:  StoredProcedure [dbo].[USP_MAKE_BOOKING]    Script Date: 27/5/2014 5:23:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[USP_MAKE_BOOKING]
(
@UserID int,
@RoomID int,
@StartDate datetime,
@EndDate datetime--,
--@RefNum Varchar(50) OUTPUT
)
AS BEGIN

DECLARE @r varchar(8)

SELECT @r = coalesce(@r, '') + n
FROM (SELECT top 8 
CHAR(number) n FROM
master..spt_values
WHERE type = 'P' AND 
(number between ascii(0) and ascii(9)
or number between ascii('A') and ascii('Z')
or number between ascii('a') and ascii('z'))
ORDER BY newid()) a

INSERT INTO Booking (UserID,RoomID,StartDate,EndDate,RefNum) VALUES (@UserID,@RoomID,@StartDate,@EndDate,@r)

--select @RefNum = @r
SELECT * FROM Booking WHERE ID =@@IDENTITY

Return @@ROWCOUNT
END


GO

USE [UserManagement]
GO
/****** Object:  StoredProcedure [dbo].[USP_SAVE_ASSET_BOOKING]    Script Date: 27/5/2014 5:23:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_SAVE_ASSET_BOOKING]
@BookingID as int
,@AssetID as int
,@Status as int
AS

--If the asset is selected before for the booking just update it status
UPDATE [dbo].[AssetBooking]
SET [Status] = 1
WHERE BookingID = @BookingID 
AND AssetID = @AssetID

--If update status return 0, mean norecord found, then insert the asset.
IF @@ROWCOUNT = 0
BEGIN
	INSERT INTO [dbo].[AssetBooking]
	(BookingID,AssetID,Status)
	Values
	(@BookingID,@AssetID,@Status)
END

SELECT @@ROWCOUNT


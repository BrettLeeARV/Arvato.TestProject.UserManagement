/*
   Monday, 9 June, 20145:41:48 PM
   User: 
   Server: ASYMY1-TANJ009
   Database: UserManagement
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Booking
	DROP CONSTRAINT FK_Booking_Room
GO
ALTER TABLE dbo.Room SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Booking
	DROP CONSTRAINT FK_Booking_User
GO
ALTER TABLE dbo.[User] SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Booking
	DROP CONSTRAINT DF_Booking_IsCanceled
GO
CREATE TABLE dbo.Tmp_Booking
	(
	ID int NOT NULL IDENTITY (1, 1),
	UserID int NOT NULL,
	RoomID int NOT NULL,
	StartDate datetime NOT NULL,
	EndDate datetime NOT NULL,
	RefNum varchar(50) NOT NULL,
	DateCreated datetime NOT NULL,
	IsCanceled bit NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Booking SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_Booking ADD CONSTRAINT
	DF_Booking_IsCanceled DEFAULT ((0)) FOR IsCanceled
GO
SET IDENTITY_INSERT dbo.Tmp_Booking ON
GO
IF EXISTS(SELECT * FROM dbo.Booking)
	 EXEC('INSERT INTO dbo.Tmp_Booking (ID, UserID, RoomID, StartDate, EndDate, RefNum, DateCreated, IsCanceled)
		SELECT ID, UserID, RoomID, StartDate, EndDate, RefNum, DateCreated, IsCanceled FROM dbo.Booking WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Booking OFF
GO
ALTER TABLE dbo.AssetBooking
	DROP CONSTRAINT FK_AssetBooking_Booking
GO
DROP TABLE dbo.Booking
GO
EXECUTE sp_rename N'dbo.Tmp_Booking', N'Booking', 'OBJECT' 
GO
ALTER TABLE dbo.Booking ADD CONSTRAINT
	PK_Booking PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Booking ADD CONSTRAINT
	FK_Booking_User FOREIGN KEY
	(
	UserID
	) REFERENCES dbo.[User]
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Booking ADD CONSTRAINT
	FK_Booking_Room FOREIGN KEY
	(
	RoomID
	) REFERENCES dbo.Room
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AssetBooking ADD CONSTRAINT
	FK_AssetBooking_Booking FOREIGN KEY
	(
	BookingID
	) REFERENCES dbo.Booking
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.AssetBooking SET (LOCK_ESCALATION = TABLE)
GO
COMMIT



/* Now alter USP_MAKE_BOOKING to automatically set date created */

USE [UserManagement]
GO
/****** Object:  StoredProcedure [dbo].[USP_MAKE_BOOKING]    Script Date: 9/6/2014 5:46:26 PM ******/
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

INSERT INTO Booking (UserID,RoomID,StartDate,EndDate,RefNum,DateCreated) VALUES (@UserID,@RoomID,@StartDate,@EndDate,@r,GETDATE())

--select @RefNum = @r
SELECT * FROM Booking WHERE ID =@@IDENTITY

Return @@ROWCOUNT
END



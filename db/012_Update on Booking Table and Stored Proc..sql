USE [UserManagement]
GO
/****** Object:  StoredProcedure [dbo].[USP_MAKE_BOOKING]    Script Date: 26/5/2014 1:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_MAKE_BOOKING]
(
@UserID int,
@RoomID int,
@StartDate datetime,
@EndDate datetime,
@RefNum Varchar(50) OUTPUT
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

select @RefNum = @r

Return @@ROWCOUNT
END




GO
/****** Object:  Table [dbo].[Booking]    Script Date: 26/5/2014 1:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Booking](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[RoomID] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[RefNum] [varchar](50) NOT NULL,
	[DateCreated]  AS (getdate()),
	[IsCanceled] [bit] NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Booking] ADD  CONSTRAINT [DF_Booking_IsCanceled]  DEFAULT ((0)) FOR [IsCanceled]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Room] FOREIGN KEY([RoomID])
REFERENCES [dbo].[Room] ([ID])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Room]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_User]
GO

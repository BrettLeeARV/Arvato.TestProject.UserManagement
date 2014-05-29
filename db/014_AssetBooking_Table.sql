USE [UserManagement]
GO

/****** Object:  Table [dbo].[AssetBooking]    Script Date: 29/5/2014 11:10:44 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AssetBooking](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BookingID] [int] NOT NULL,
	[AssetID] [int] NOT NULL,
	[Status] [int] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[AssetBooking] ADD  CONSTRAINT [DF_AssetBooking_Status]  DEFAULT ((1)) FOR [Status]
GO

ALTER TABLE [dbo].[AssetBooking]  WITH CHECK ADD  CONSTRAINT [FK_AssetBooking_Asset] FOREIGN KEY([AssetID])
REFERENCES [dbo].[Asset] ([ID])
GO

ALTER TABLE [dbo].[AssetBooking] CHECK CONSTRAINT [FK_AssetBooking_Asset]
GO

ALTER TABLE [dbo].[AssetBooking]  WITH CHECK ADD  CONSTRAINT [FK_AssetBooking_Booking] FOREIGN KEY([BookingID])
REFERENCES [dbo].[Booking] ([ID])
GO

ALTER TABLE [dbo].[AssetBooking] CHECK CONSTRAINT [FK_AssetBooking_Booking]
GO



USE [UserManagement]
GO

/* Insert dummy users */

SET IDENTITY_INSERT [dbo].[User] ON 

GO
INSERT [dbo].[User] ([ID], [FirstName], [LastName], [Email], [LoginID], [Password]) VALUES (1, N'Jing Tao', N'Tan', N'jt@email.com', N'jingtao', N'password')
GO
INSERT [dbo].[User] ([ID], [FirstName], [LastName], [Email], [LoginID], [Password]) VALUES (2, N'Ian', N'Lim', N'ian@email.com', N'ianlim', N'password')
GO
INSERT [dbo].[User] ([ID], [FirstName], [LastName], [Email], [LoginID], [Password]) VALUES (3, N'Weng Loon', N'Beh', N'wl@email.com', N'wengloon', N'password')
GO
INSERT [dbo].[User] ([ID], [FirstName], [LastName], [Email], [LoginID], [Password]) VALUES (4, N'Benjamin', N'Sh', N'ben@email.com', N'benjamin', N'password')
GO
INSERT [dbo].[User] ([ID], [FirstName], [LastName], [Email], [LoginID], [Password]) VALUES (5, N'Tristan', N'Liaw', N'tristan@email.com', N'tristan', N'password')
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO

/* Insert dummy rooms */

SET IDENTITY_INSERT [dbo].[Room] ON 

GO
INSERT [dbo].[Room] ([ID], [Name], [Location], [Capacity], [IsEnabled]) VALUES (1, N'Winterfell', N'24th Floor', 10, 1)
GO
INSERT [dbo].[Room] ([ID], [Name], [Location], [Capacity], [IsEnabled]) VALUES (2, N'King''s Landing', N'25th Floor', 15, 1)
GO
INSERT [dbo].[Room] ([ID], [Name], [Location], [Capacity], [IsEnabled]) VALUES (3, N'Unavailable', N'Outer space', 4, 0)
GO
SET IDENTITY_INSERT [dbo].[Room] OFF
GO

/* Insert dummy assets */

SET IDENTITY_INSERT [dbo].[Asset] ON 

GO
INSERT [dbo].[Asset] ([ID], [RoomID], [Name], [IsEnabled]) VALUES (1, 1, N'Epson projector', 1)
GO
INSERT [dbo].[Asset] ([ID], [RoomID], [Name], [IsEnabled]) VALUES (2, 2, N'Dell projector', 1)
GO
INSERT [dbo].[Asset] ([ID], [RoomID], [Name], [IsEnabled]) VALUES (3, 3, N'Blackboard', 0)
GO
SET IDENTITY_INSERT [dbo].[Asset] OFF
GO

/* Insert dummy bookings */

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

/* Insert dummy assets */

/* Insert dummy bookings */

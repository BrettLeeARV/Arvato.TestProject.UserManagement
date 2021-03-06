USE [UserManagement]
GO
/****** Object:  Table [dbo].[AccessMatrix]    Script Date: 25/6/2014 2:46:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessMatrix](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[ModuleControlID] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_AccessMatrix] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ModuleControl]    Script Date: 25/6/2014 2:46:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleControl](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ModuleName] [nvarchar](150) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_ModuleControl] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 25/6/2014 2:46:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[AccessMatrix] ON 

GO
INSERT [dbo].[AccessMatrix] ([ID], [RoleID], [ModuleControlID], [IsActive], [CreatedAt], [CreatedBy]) VALUES (1, 1, 1, 1, CAST(0x0000A3500111F46B AS DateTime), N'SQL')
GO
INSERT [dbo].[AccessMatrix] ([ID], [RoleID], [ModuleControlID], [IsActive], [CreatedAt], [CreatedBy]) VALUES (2, 1, 2, 1, CAST(0x0000A3500111F46B AS DateTime), N'SQL')
GO
INSERT [dbo].[AccessMatrix] ([ID], [RoleID], [ModuleControlID], [IsActive], [CreatedAt], [CreatedBy]) VALUES (3, 1, 3, 1, CAST(0x0000A3500111F477 AS DateTime), N'SQL')
GO
INSERT [dbo].[AccessMatrix] ([ID], [RoleID], [ModuleControlID], [IsActive], [CreatedAt], [CreatedBy]) VALUES (4, 1, 4, 1, CAST(0x0000A3500111F478 AS DateTime), N'SQL')
GO
INSERT [dbo].[AccessMatrix] ([ID], [RoleID], [ModuleControlID], [IsActive], [CreatedAt], [CreatedBy]) VALUES (5, 2, 1, 0, CAST(0x0000A35401244D7F AS DateTime), N'SQL')
GO
INSERT [dbo].[AccessMatrix] ([ID], [RoleID], [ModuleControlID], [IsActive], [CreatedAt], [CreatedBy]) VALUES (6, 2, 1, 0, CAST(0x0000A3540124537F AS DateTime), N'SQL')
GO
INSERT [dbo].[AccessMatrix] ([ID], [RoleID], [ModuleControlID], [IsActive], [CreatedAt], [CreatedBy]) VALUES (7, 2, 3, 0, CAST(0x0000A35401245B05 AS DateTime), N'SQL')
GO
INSERT [dbo].[AccessMatrix] ([ID], [RoleID], [ModuleControlID], [IsActive], [CreatedAt], [CreatedBy]) VALUES (8, 2, 4, 1, CAST(0x0000A35401246221 AS DateTime), N'SQL')
GO
SET IDENTITY_INSERT [dbo].[AccessMatrix] OFF
GO
SET IDENTITY_INSERT [dbo].[ModuleControl] ON 

GO
INSERT [dbo].[ModuleControl] ([ID], [ModuleName], [Title], [IsEnabled], [CreatedAt], [CreatedBy]) VALUES (1, N'Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels.UsersListViewModel', N'Manage Users', 1, CAST(0x0000A350011175B2 AS DateTime), N'SQL')
GO
INSERT [dbo].[ModuleControl] ([ID], [ModuleName], [Title], [IsEnabled], [CreatedAt], [CreatedBy]) VALUES (2, N'Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels.RoomsListViewModel', N'Manage Rooms', 1, CAST(0x0000A350011175B3 AS DateTime), N'SQL')
GO
INSERT [dbo].[ModuleControl] ([ID], [ModuleName], [Title], [IsEnabled], [CreatedAt], [CreatedBy]) VALUES (3, N'Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels.AssetsListViewModel', N'Manage Assets', 1, CAST(0x0000A350011175B3 AS DateTime), N'SQL')
GO
INSERT [dbo].[ModuleControl] ([ID], [ModuleName], [Title], [IsEnabled], [CreatedAt], [CreatedBy]) VALUES (4, N'Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels.BookingsListViewModel', N'Manage Bookings', 1, CAST(0x0000A350011175B4 AS DateTime), N'SQL')
GO
SET IDENTITY_INSERT [dbo].[ModuleControl] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

GO
INSERT [dbo].[Role] ([ID], [RoleName], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (1, N'Administrator', 1, CAST(0x0000A3500111BCEE AS DateTime), N'SQL', NULL, NULL)
GO
INSERT [dbo].[Role] ([ID], [RoleName], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (2, N'Test Role', 1, CAST(0x0000A35300B010A8 AS DateTime), N'Unit Test', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
ALTER TABLE [dbo].[AccessMatrix] ADD  CONSTRAINT [DF_AccessMatrix_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ModuleControl] ADD  CONSTRAINT [DF_ModuleControl_IsEnabled]  DEFAULT ((1)) FOR [IsEnabled]
GO

ALTER TABLE [dbo].[User]
ADD [RoleID] INT
GO
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [FK_User_Role]
FOREIGN KEY (RoleID)
REFERENCES [UserManagement].[dbo].[Role](ID)
GO

UPDATE [dbo].[User]
SET RoleID = 1
GO

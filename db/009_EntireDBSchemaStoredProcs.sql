USE [master]
GO
/****** Object:  Database [UserManagement]    Script Date: 21/5/2014 2:11:55 PM ******/
CREATE DATABASE [UserManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'UserManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\UserManagement.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'UserManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\UserManagement_log.ldf' , SIZE = 1536KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [UserManagement] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [UserManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [UserManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [UserManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [UserManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [UserManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [UserManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [UserManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [UserManagement] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [UserManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [UserManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [UserManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [UserManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [UserManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [UserManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [UserManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [UserManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [UserManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [UserManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [UserManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [UserManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [UserManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [UserManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [UserManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [UserManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [UserManagement] SET RECOVERY FULL 
GO
ALTER DATABASE [UserManagement] SET  MULTI_USER 
GO
ALTER DATABASE [UserManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [UserManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [UserManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [UserManagement] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'UserManagement', N'ON'
GO
USE [UserManagement]
GO
/****** Object:  StoredProcedure [dbo].[USP_CANCEL_BOOKING]    Script Date: 21/5/2014 2:11:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CANCEL_BOOKING]

(
@ID tinyint
)
AS BEGIN
UPDATE Booking SET IsCanceled = 1 WHERE ID = @ID;

RETURN @@ROWCOUNT
END

GO
/****** Object:  StoredProcedure [dbo].[USP_EDIT_BOOKING]    Script Date: 21/5/2014 2:11:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_EDIT_BOOKING]
(
@ID tinyint,
@RoomID tinyint,
@StartDate Datetime,
@EndDate Datetime
)

AS BEGIN

UPDATE Booking SET RoomID = @RoomID, StartDate = @StartDate, EndDate = @EndDate WHERE ID = @ID

RETURN @@RowCount
END

GO
/****** Object:  StoredProcedure [dbo].[USP_MAKE_BOOKING]    Script Date: 21/5/2014 2:11:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_MAKE_BOOKING]
(
@LoginID nvarchar(50),
@RoomID tinyint,
@StartDate datetime,
@EndDate datetime,
@RefNum int OUTPUT
)
AS BEGIN

DECLARE @Random INT;
DECLARE @Uppder INT;
DECLARE @Lower INT;

SET @Lower = 1000;
SET @Uppder = 9999;
SELECT @Random = ROUND(((@Uppder - @Lower -1) * RAND() + @Lower), 0)
INSERT INTO Booking (LoginID,RoomID,StartDate,EndDate,RefNum) VALUES (@LoginID,@RoomID,@StartDate,@EndDate,@Random)

select @RefNum = @Random

Return @@ROWCOUNT
END



GO
/****** Object:  StoredProcedure [dbo].[USP_USER_DELETE]    Script Date: 21/5/2014 2:11:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_USER_DELETE]
@ID TINYINT
AS BEGIN

DELETE FROM [User]
WHERE ID = @ID

END


GO
/****** Object:  StoredProcedure [dbo].[USP_USER_LOGIN]    Script Date: 21/5/2014 2:11:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_USER_LOGIN]
(
@LoginID nvarchar(50),
@Password nvarchar(50)
)
AS BEGIN

DECLARE @ID as tinyint;
DECLARE @FirstName as nvarchar(50);
DECLARE @LastName as nvarchar(50);
DECLARE @Email as nvarchar(50);
SET @ID = 0;

SELECT @ID=ID, @FirstName = FirstName, @LastName = LastName, @Email = Email, @LoginID = LoginID, @Password = [Password] FROM [User] WHERE @LoginID = LoginID AND @Password = [Password];

SELECT @ID AS ID,@FirstName AS FirstName, @LastName AS LastName, @Email AS Email, @LoginID AS LoginID, @Password AS [Password];

END

GO
/****** Object:  StoredProcedure [dbo].[USP_USER_REGISTER]    Script Date: 21/5/2014 2:11:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_USER_REGISTER]
(
@FirstName nvarchar(50),
@LastName nvarchar(50),
@Email nvarchar(50),
@LoginID nvarchar(50),
@Password nvarchar(50)
)
AS BEGIN

INSERT INTO [User](FirstName,LastName,Email,LoginID,[Password]) VALUES (@FirstName,@LastName,@Email,@LoginID,@Password)

RETURN @@ROWCOUNT
END

GO
/****** Object:  StoredProcedure [dbo].[USP_USER_SELECTDETAILS]    Script Date: 21/5/2014 2:11:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_USER_SELECTDETAILS]

AS BEGIN

SELECT * FROM [User]

END

GO
/****** Object:  StoredProcedure [dbo].[USP_USER_UPDATE]    Script Date: 21/5/2014 2:11:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_USER_UPDATE]
(
@ID tinyint,
@FirstName nvarchar(50),
@LastName nvarchar(50),
@Email nvarchar(50),
@LoginID nvarchar(50),
@Password nvarchar(50)
)
AS BEGIN

UPDATE [User] SET FirstName = @FirstName, LastName= @LastName, Email = @Email, LoginID = @LoginID, [Password] = @Password WHERE ID = @ID

Return @@ROWCOUNT
END

GO
/****** Object:  StoredProcedure [dbo].[USP_USER_VALIDATE_LOGINID]    Script Date: 21/5/2014 2:11:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_USER_VALIDATE_LOGINID]
@LoginID NVARCHAR(50)
,@ID TINYINT
AS BEGIN

SELECT * FROM [User]
WHERE LoginID = @LoginID
AND ID != @ID

END

GO
/****** Object:  StoredProcedure [dbo].[USP_VIEW_BOOKING]    Script Date: 21/5/2014 2:11:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_VIEW_BOOKING]
(
@RefNum int
)

AS BEGIN

SELECT ID,LoginID,RoomID,StartDate,EndDate,DateCreated FROM Booking WHERE RefNum = @RefNum

END


GO
/****** Object:  Table [dbo].[Asset]    Script Date: 21/5/2014 2:11:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Asset](
	[ID] [tinyint] IDENTITY(1,1) NOT NULL,
	[RoomID] [tinyint] NULL,
	[Name] [nvarchar](50) NULL,
	[IsEnabled] [bit] NULL,
 CONSTRAINT [PK_Asset] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Booking]    Script Date: 21/5/2014 2:11:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[ID] [tinyint] IDENTITY(1,1) NOT NULL,
	[LoginID] [nvarchar](50) NOT NULL,
	[RoomID] [tinyint] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[RefNum] [int] NOT NULL,
	[DateCreated]  AS (getdate()),
	[IsCanceled] [bit] NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Room]    Script Date: 21/5/2014 2:11:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[ID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Location] [nvarchar](50) NULL,
	[Capacity] [int] NULL,
	[IsEnabled] [bit] NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 21/5/2014 2:11:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [tinyint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[LoginID] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_User] UNIQUE NONCLUSTERED 
(
	[LoginID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Booking] ADD  CONSTRAINT [DF_Booking_IsCanceled]  DEFAULT ((0)) FOR [IsCanceled]
GO
ALTER TABLE [dbo].[Asset]  WITH CHECK ADD  CONSTRAINT [FK_Asset_Room] FOREIGN KEY([RoomID])
REFERENCES [dbo].[Room] ([ID])
GO
ALTER TABLE [dbo].[Asset] CHECK CONSTRAINT [FK_Asset_Room]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_User] FOREIGN KEY([LoginID])
REFERENCES [dbo].[User] ([LoginID])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_User]
GO
USE [master]
GO
ALTER DATABASE [UserManagement] SET  READ_WRITE 
GO

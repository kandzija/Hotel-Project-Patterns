USE HotelProject

ALTER DATABASE [HotelProject] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HotelProject] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HotelProject] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HotelProject] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HotelProject] SET ARITHABORT OFF 
GO
ALTER DATABASE [HotelProject] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HotelProject] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HotelProject] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HotelProject] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HotelProject] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HotelProject] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HotelProject] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HotelProject] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HotelProject] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HotelProject] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HotelProject] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HotelProject] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HotelProject] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HotelProject] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HotelProject] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HotelProject] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HotelProject] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HotelProject] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HotelProject] SET  MULTI_USER 
GO
ALTER DATABASE [HotelProject] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HotelProject] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HotelProject] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HotelProject] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HotelProject] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HotelProject] SET QUERY_STORE = OFF
GO
USE [HotelProject]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 16.4.2020. 2:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[Id] [uniqueidentifier] NOT NULL,
	[HotelId] [uniqueidentifier] NOT NULL,
	[GuestId] [uniqueidentifier] NOT NULL,
	[RoomId] [uniqueidentifier] NULL,
	[Number] [bigint] IDENTITY(1,1) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Service] [nvarchar](200) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_Bill] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Guest]    Script Date: 16.4.2020. 2:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Guest](
	[Id] [uniqueidentifier] NOT NULL,
	[HotelId] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[PIN] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](100) NULL,
	[Phone] [nvarchar](100) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_Guest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hotel]    Script Date: 16.4.2020. 2:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hotel](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](100) NULL,
	[Phone] [nvarchar](100) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_Hotel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservation]    Script Date: 16.4.2020. 2:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[Id] [uniqueidentifier] NOT NULL,
	[HotelId] [uniqueidentifier] NOT NULL,
	[GuestId] [uniqueidentifier] NOT NULL,
	[RoomId] [uniqueidentifier] NOT NULL,
	[DateArrival] [datetime] NOT NULL,
	[DateDeparture] [datetime] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 16.4.2020. 2:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[Id] [uniqueidentifier] NOT NULL,
	[HotelId] [uniqueidentifier] NOT NULL,
	[Number] [int] NOT NULL,
	[OneBed] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Bill] ON 

INSERT [dbo].[Bill] ([Id], [HotelId], [GuestId], [RoomId], [Number], [Amount], [Service], [DateCreated], [DateUpdated]) VALUES (N'05f03cae-87e8-46c1-b4d5-03068b5f4120', N'61cbcce6-ed2a-4d01-9c8e-d8463ab547c7', N'6b44eb82-e1ad-4f04-ab2c-bdeddbd2f897', N'f9d7e2d6-576c-40c1-90c0-c1a6e914acc3', 8, CAST(40.00 AS Decimal(18, 2)), N'Bazen', CAST(N'2020-04-16T02:41:27.863' AS DateTime), CAST(N'2020-04-16T02:41:27.863' AS DateTime))
INSERT [dbo].[Bill] ([Id], [HotelId], [GuestId], [RoomId], [Number], [Amount], [Service], [DateCreated], [DateUpdated]) VALUES (N'eedfec3f-a604-490e-83bd-95707891bf9a', N'61cbcce6-ed2a-4d01-9c8e-d8463ab547c7', N'6b44eb82-e1ad-4f04-ab2c-bdeddbd2f897', N'f9d7e2d6-576c-40c1-90c0-c1a6e914acc3', 1, CAST(100.00 AS Decimal(18, 2)), N'Nocenje', CAST(N'2020-04-15T22:32:33.820' AS DateTime), CAST(N'2020-04-16T02:40:58.277' AS DateTime))
INSERT [dbo].[Bill] ([Id], [HotelId], [GuestId], [RoomId], [Number], [Amount], [Service], [DateCreated], [DateUpdated]) VALUES (N'c3d289a9-142c-40d5-9a4d-bc5734a172f4', N'61cbcce6-ed2a-4d01-9c8e-d8463ab547c7', N'6b44eb82-e1ad-4f04-ab2c-bdeddbd2f897', N'f9d7e2d6-576c-40c1-90c0-c1a6e914acc3', 9, CAST(70.00 AS Decimal(18, 2)), N'Doručak', CAST(N'2020-04-16T02:41:37.710' AS DateTime), CAST(N'2020-04-16T02:41:37.710' AS DateTime))
INSERT [dbo].[Bill] ([Id], [HotelId], [GuestId], [RoomId], [Number], [Amount], [Service], [DateCreated], [DateUpdated]) VALUES (N'9b6f20e2-4910-41f7-a4f4-e244b4e5f734', N'61cbcce6-ed2a-4d01-9c8e-d8463ab547c7', N'6b44eb82-e1ad-4f04-ab2c-bdeddbd2f897', N'f9d7e2d6-576c-40c1-90c0-c1a6e914acc3', 3, CAST(80.00 AS Decimal(18, 2)), N'Rucak', CAST(N'2020-04-15T22:32:33.820' AS DateTime), CAST(N'2020-04-16T02:41:10.653' AS DateTime))
SET IDENTITY_INSERT [dbo].[Bill] OFF
INSERT [dbo].[Guest] ([Id], [HotelId], [FirstName], [LastName], [PIN], [Address], [Phone], [DateCreated], [DateUpdated]) VALUES (N'6b44eb82-e1ad-4f04-ab2c-bdeddbd2f897', N'61cbcce6-ed2a-4d01-9c8e-d8463ab547c7', N'Anders', N'Hejlsberg', N'1', N'My address 4', N'223-322', CAST(N'2020-04-15T21:05:35.577' AS DateTime), CAST(N'2020-04-15T21:05:35.577' AS DateTime))
INSERT [dbo].[Hotel] ([Id], [Name], [Address], [Phone], [DateCreated], [DateUpdated]) VALUES (N'61cbcce6-ed2a-4d01-9c8e-d8463ab547c7', N'Osijek', N' Šamacka 4, 31000, Osijek', N'123-321', CAST(N'2020-04-15T21:05:35.573' AS DateTime), CAST(N'2020-04-15T21:05:35.573' AS DateTime))
INSERT [dbo].[Room] ([Id], [HotelId], [Number], [OneBed], [DateCreated], [DateUpdated]) VALUES (N'f9d7e2d6-576c-40c1-90c0-c1a6e914acc3', N'61cbcce6-ed2a-4d01-9c8e-d8463ab547c7', 1, 1, CAST(N'2020-04-15T21:05:35.580' AS DateTime), CAST(N'2020-04-15T21:05:35.580' AS DateTime))
/****** Object:  Index [IX_Bill]    Script Date: 16.4.2020. 2:42:19 ******/
ALTER TABLE [dbo].[Bill] ADD  CONSTRAINT [IX_Bill] UNIQUE NONCLUSTERED 
(
	[Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Guest]    Script Date: 16.4.2020. 2:42:19 ******/
ALTER TABLE [dbo].[Guest] ADD  CONSTRAINT [IX_Guest] UNIQUE NONCLUSTERED 
(
	[PIN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_Guest] FOREIGN KEY([GuestId])
REFERENCES [dbo].[Guest] ([Id])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_Guest]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_Hotel] FOREIGN KEY([HotelId])
REFERENCES [dbo].[Hotel] ([Id])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_Hotel]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_Room]
GO
ALTER TABLE [dbo].[Guest]  WITH CHECK ADD  CONSTRAINT [FK_Guest_Hotel] FOREIGN KEY([HotelId])
REFERENCES [dbo].[Hotel] ([Id])
GO
ALTER TABLE [dbo].[Guest] CHECK CONSTRAINT [FK_Guest_Hotel]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Guest] FOREIGN KEY([GuestId])
REFERENCES [dbo].[Guest] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Guest]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Hotel] FOREIGN KEY([HotelId])
REFERENCES [dbo].[Hotel] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Hotel]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Room]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Hotel] FOREIGN KEY([HotelId])
REFERENCES [dbo].[Hotel] ([Id])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Hotel]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [CK_Bill] CHECK  (([Amount]>=(0)))
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [CK_Bill]
GO
USE [master]
GO
ALTER DATABASE [HotelProject] SET  READ_WRITE 
GO

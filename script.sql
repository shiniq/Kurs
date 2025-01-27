USE [master]
GO
/****** Object:  Database [School]    Script Date: 28.12.2024 1:16:24 ******/
CREATE DATABASE [School]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'School', FILENAME = N'D:\games noy steam\MSSQL15.MSSQLSERVER\MSSQL\DATA\School.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'School_log', FILENAME = N'D:\games noy steam\MSSQL15.MSSQLSERVER\MSSQL\DATA\School_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [School] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [School].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [School] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [School] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [School] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [School] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [School] SET ARITHABORT OFF 
GO
ALTER DATABASE [School] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [School] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [School] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [School] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [School] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [School] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [School] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [School] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [School] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [School] SET  ENABLE_BROKER 
GO
ALTER DATABASE [School] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [School] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [School] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [School] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [School] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [School] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [School] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [School] SET RECOVERY FULL 
GO
ALTER DATABASE [School] SET  MULTI_USER 
GO
ALTER DATABASE [School] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [School] SET DB_CHAINING OFF 
GO
ALTER DATABASE [School] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [School] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [School] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [School] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'School', N'ON'
GO
ALTER DATABASE [School] SET QUERY_STORE = OFF
GO
USE [School]
GO
/****** Object:  Table [dbo].[Ocenka]    Script Date: 28.12.2024 1:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ocenka](
	[OcenkaId] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[PredmetId] [int] NOT NULL,
	[Value] [int] NOT NULL,
 CONSTRAINT [PK__Ocenka__DC04D1D8B2956FD7] PRIMARY KEY CLUSTERED 
(
	[OcenkaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Predmet]    Script Date: 28.12.2024 1:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Predmet](
	[PredmetId] [int] NOT NULL,
	[PredmetName] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PredmetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prepod]    Script Date: 28.12.2024 1:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prepod](
	[PrepodId] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Post] [varchar](50) NOT NULL,
	[PredmetId] [int] NOT NULL,
 CONSTRAINT [PK__Prepod__9F2B6A6611A121B3] PRIMARY KEY CLUSTERED 
(
	[PrepodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Propuski]    Script Date: 28.12.2024 1:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Propuski](
	[PropuskId] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[PredmetId] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[Reason] [varchar](50) NULL,
 CONSTRAINT [PK__Propuski__3D7BEE2775408BDE] PRIMARY KEY CLUSTERED 
(
	[PropuskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 28.12.2024 1:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[EnrollmentDate] [date] NOT NULL,
	[GroupName] [varchar](50) NOT NULL,
 CONSTRAINT [PK__Students__32C52B994DD76AA7] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Ocenka]  WITH CHECK ADD  CONSTRAINT [FK__Ocenka__PredmetI__2B3F6F97] FOREIGN KEY([PredmetId])
REFERENCES [dbo].[Predmet] ([PredmetId])
GO
ALTER TABLE [dbo].[Ocenka] CHECK CONSTRAINT [FK__Ocenka__PredmetI__2B3F6F97]
GO
ALTER TABLE [dbo].[Ocenka]  WITH CHECK ADD  CONSTRAINT [FK__Ocenka__StudentI__2A4B4B5E] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([StudentId])
GO
ALTER TABLE [dbo].[Ocenka] CHECK CONSTRAINT [FK__Ocenka__StudentI__2A4B4B5E]
GO
ALTER TABLE [dbo].[Prepod]  WITH CHECK ADD  CONSTRAINT [FK_Prepod_Predmet] FOREIGN KEY([PredmetId])
REFERENCES [dbo].[Predmet] ([PredmetId])
GO
ALTER TABLE [dbo].[Prepod] CHECK CONSTRAINT [FK_Prepod_Predmet]
GO
ALTER TABLE [dbo].[Propuski]  WITH CHECK ADD  CONSTRAINT [FK__Propuski__Predme__2F10007B] FOREIGN KEY([PredmetId])
REFERENCES [dbo].[Predmet] ([PredmetId])
GO
ALTER TABLE [dbo].[Propuski] CHECK CONSTRAINT [FK__Propuski__Predme__2F10007B]
GO
ALTER TABLE [dbo].[Propuski]  WITH CHECK ADD  CONSTRAINT [FK__Propuski__Studen__2E1BDC42] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([StudentId])
GO
ALTER TABLE [dbo].[Propuski] CHECK CONSTRAINT [FK__Propuski__Studen__2E1BDC42]
GO
USE [master]
GO
ALTER DATABASE [School] SET  READ_WRITE 
GO

USE [master]
GO
/****** Object:  Database [vsm_db_order_payment]    Script Date: 27/08/2023 22:42:13 ******/
CREATE DATABASE [vsm_db_order_payment]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'vsm_db_order_payment', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.NUARI_PROJECT\MSSQL\DATA\vsm_db_order_payment.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'vsm_db_order_payment_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.NUARI_PROJECT\MSSQL\DATA\vsm_db_order_payment_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [vsm_db_order_payment] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [vsm_db_order_payment].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [vsm_db_order_payment] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [vsm_db_order_payment] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [vsm_db_order_payment] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [vsm_db_order_payment] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [vsm_db_order_payment] SET ARITHABORT OFF 
GO
ALTER DATABASE [vsm_db_order_payment] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [vsm_db_order_payment] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [vsm_db_order_payment] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [vsm_db_order_payment] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [vsm_db_order_payment] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [vsm_db_order_payment] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [vsm_db_order_payment] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [vsm_db_order_payment] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [vsm_db_order_payment] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [vsm_db_order_payment] SET  ENABLE_BROKER 
GO
ALTER DATABASE [vsm_db_order_payment] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [vsm_db_order_payment] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [vsm_db_order_payment] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [vsm_db_order_payment] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [vsm_db_order_payment] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [vsm_db_order_payment] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [vsm_db_order_payment] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [vsm_db_order_payment] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [vsm_db_order_payment] SET  MULTI_USER 
GO
ALTER DATABASE [vsm_db_order_payment] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [vsm_db_order_payment] SET DB_CHAINING OFF 
GO
ALTER DATABASE [vsm_db_order_payment] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [vsm_db_order_payment] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [vsm_db_order_payment] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [vsm_db_order_payment] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [vsm_db_order_payment] SET QUERY_STORE = OFF
GO
USE [vsm_db_order_payment]
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 27/08/2023 22:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[order_item_id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[subtotal] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED 
(
	[order_item_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 27/08/2023 22:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[status_id] [int] NOT NULL,
	[order_date] [datetime] NOT NULL,
	[total_amount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 27/08/2023 22:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[payment_id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [int] NOT NULL,
	[payment_method_id] [int] NOT NULL,
	[amount] [decimal](18, 2) NOT NULL,
	[payment_date] [datetime] NOT NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[payment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentMethod]    Script Date: 27/08/2023 22:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethod](
	[payment_method_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](10) NOT NULL,
 CONSTRAINT [PK_PaymentMethod] PRIMARY KEY CLUSTERED 
(
	[payment_method_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 27/08/2023 22:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[status_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[status_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Orders] FOREIGN KEY([order_id])
REFERENCES [dbo].[Orders] ([order_id])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Orders]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Status] FOREIGN KEY([status_id])
REFERENCES [dbo].[Status] ([status_id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Status]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Orders] FOREIGN KEY([order_id])
REFERENCES [dbo].[Orders] ([order_id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Orders]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_PaymentMethod] FOREIGN KEY([payment_method_id])
REFERENCES [dbo].[PaymentMethod] ([payment_method_id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_PaymentMethod]
GO
USE [master]
GO
ALTER DATABASE [vsm_db_order_payment] SET  READ_WRITE 
GO

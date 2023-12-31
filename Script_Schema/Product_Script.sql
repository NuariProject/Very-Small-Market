USE [master]
GO
/****** Object:  Database [vsm_db_product]    Script Date: 27/08/2023 22:42:50 ******/
CREATE DATABASE [vsm_db_product]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'vsm_db_product', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.NUARI_PROJECT\MSSQL\DATA\vsm_db_product.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'vsm_db_product_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.NUARI_PROJECT\MSSQL\DATA\vsm_db_product_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [vsm_db_product] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [vsm_db_product].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [vsm_db_product] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [vsm_db_product] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [vsm_db_product] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [vsm_db_product] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [vsm_db_product] SET ARITHABORT OFF 
GO
ALTER DATABASE [vsm_db_product] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [vsm_db_product] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [vsm_db_product] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [vsm_db_product] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [vsm_db_product] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [vsm_db_product] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [vsm_db_product] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [vsm_db_product] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [vsm_db_product] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [vsm_db_product] SET  ENABLE_BROKER 
GO
ALTER DATABASE [vsm_db_product] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [vsm_db_product] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [vsm_db_product] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [vsm_db_product] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [vsm_db_product] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [vsm_db_product] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [vsm_db_product] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [vsm_db_product] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [vsm_db_product] SET  MULTI_USER 
GO
ALTER DATABASE [vsm_db_product] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [vsm_db_product] SET DB_CHAINING OFF 
GO
ALTER DATABASE [vsm_db_product] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [vsm_db_product] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [vsm_db_product] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [vsm_db_product] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [vsm_db_product] SET QUERY_STORE = OFF
GO
USE [vsm_db_product]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 27/08/2023 22:42:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[category_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](30) NOT NULL,
	[is_delete] [bit] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategories]    Script Date: 27/08/2023 22:42:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategories](
	[product_category_id] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NOT NULL,
	[category_id] [int] NOT NULL,
 CONSTRAINT [PK_ProductCategories] PRIMARY KEY CLUSTERED 
(
	[product_category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 27/08/2023 22:42:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[product_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [varchar](50) NOT NULL,
	[price] [decimal](18, 2) NOT NULL,
	[created_date] [datetime] NOT NULL,
	[modified_date] [datetime] NULL,
	[is_delete] [bit] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductCategories]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategories_Categories] FOREIGN KEY([category_id])
REFERENCES [dbo].[Categories] ([category_id])
GO
ALTER TABLE [dbo].[ProductCategories] CHECK CONSTRAINT [FK_ProductCategories_Categories]
GO
ALTER TABLE [dbo].[ProductCategories]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategories_Products] FOREIGN KEY([product_id])
REFERENCES [dbo].[Products] ([product_id])
GO
ALTER TABLE [dbo].[ProductCategories] CHECK CONSTRAINT [FK_ProductCategories_Products]
GO
USE [master]
GO
ALTER DATABASE [vsm_db_product] SET  READ_WRITE 
GO

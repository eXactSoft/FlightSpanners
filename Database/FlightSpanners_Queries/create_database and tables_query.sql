USE [master]
GO

/****** Object:  Database [FlightSpanners]    Script Date: 15-Jan-19 6:26:42 AM ******/
CREATE DATABASE [FlightSpanners]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FlightSpanners', FILENAME = N'd:\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\FlightSpanners.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FlightSpanners_log', FILENAME = N'd:\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\FlightSpanners_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [FlightSpanners] SET COMPATIBILITY_LEVEL = 140
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FlightSpanners].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [FlightSpanners] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [FlightSpanners] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [FlightSpanners] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [FlightSpanners] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [FlightSpanners] SET ARITHABORT OFF 
GO

ALTER DATABASE [FlightSpanners] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [FlightSpanners] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [FlightSpanners] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [FlightSpanners] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [FlightSpanners] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [FlightSpanners] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [FlightSpanners] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [FlightSpanners] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [FlightSpanners] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [FlightSpanners] SET  DISABLE_BROKER 
GO

ALTER DATABASE [FlightSpanners] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [FlightSpanners] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [FlightSpanners] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [FlightSpanners] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [FlightSpanners] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [FlightSpanners] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [FlightSpanners] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [FlightSpanners] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [FlightSpanners] SET  MULTI_USER 
GO

ALTER DATABASE [FlightSpanners] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [FlightSpanners] SET DB_CHAINING OFF 
GO

ALTER DATABASE [FlightSpanners] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [FlightSpanners] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [FlightSpanners] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [FlightSpanners] SET QUERY_STORE = OFF
GO

ALTER DATABASE [FlightSpanners] SET  READ_WRITE 
GO


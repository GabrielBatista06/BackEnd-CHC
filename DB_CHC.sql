USE [master]
GO
/****** Object:  Database [DB_HermanosCastro]    Script Date: 6/3/2024 1:27:33 a. m. ******/
CREATE DATABASE [DB_HermanosCastro]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DB_HermanosCastro', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\DB_HermanosCastro.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DB_HermanosCastro_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\DB_HermanosCastro_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DB_HermanosCastro] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DB_HermanosCastro].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DB_HermanosCastro] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DB_HermanosCastro] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DB_HermanosCastro] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DB_HermanosCastro] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DB_HermanosCastro] SET ARITHABORT OFF 
GO
ALTER DATABASE [DB_HermanosCastro] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DB_HermanosCastro] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DB_HermanosCastro] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DB_HermanosCastro] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DB_HermanosCastro] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DB_HermanosCastro] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DB_HermanosCastro] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DB_HermanosCastro] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DB_HermanosCastro] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DB_HermanosCastro] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DB_HermanosCastro] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DB_HermanosCastro] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DB_HermanosCastro] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DB_HermanosCastro] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DB_HermanosCastro] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DB_HermanosCastro] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [DB_HermanosCastro] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DB_HermanosCastro] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DB_HermanosCastro] SET  MULTI_USER 
GO
ALTER DATABASE [DB_HermanosCastro] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DB_HermanosCastro] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DB_HermanosCastro] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DB_HermanosCastro] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DB_HermanosCastro] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DB_HermanosCastro] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DB_HermanosCastro] SET QUERY_STORE = ON
GO
ALTER DATABASE [DB_HermanosCastro] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DB_HermanosCastro]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/3/2024 1:27:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 6/3/2024 1:27:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Apellidos] [nvarchar](max) NOT NULL,
	[Apodo] [nvarchar](max) NULL,
	[Cedula] [nvarchar](max) NOT NULL,
	[Celular] [nvarchar](max) NOT NULL,
	[Telefono] [nvarchar](max) NULL,
	[Direccion] [nvarchar](max) NULL,
	[Activo] [bit] NOT NULL,
	[FechaCreacion] [datetime2](7) NOT NULL,
	[FechaEdicion] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CuentasPendientes]    Script Date: 6/3/2024 1:27:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CuentasPendientes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idCliente] [int] NULL,
	[total] [decimal](10, 2) NULL,
	[diaPago] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleVenta]    Script Date: 6/3/2024 1:27:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleVenta](
	[idDetalleVenta] [int] IDENTITY(1,1) NOT NULL,
	[idVenta] [int] NULL,
	[idProducto] [int] NULL,
	[cantidad] [int] NULL,
	[precio] [decimal](10, 2) NULL,
	[total] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[idDetalleVenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NumeroDocumento]    Script Date: 6/3/2024 1:27:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NumeroDocumento](
	[idNumeroDocumento] [int] IDENTITY(1,1) NOT NULL,
	[ultimo_Numero] [int] NOT NULL,
	[fechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idNumeroDocumento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pagos]    Script Date: 6/3/2024 1:27:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pagos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[usuarioCobro] [int] NULL,
	[idcuentaPendiente] [int] NULL,
	[balanceAnterior] [decimal](10, 2) NULL,
	[montoPagado] [decimal](10, 2) NULL,
	[fechaPago] [datetime] NULL,
	[tipoPago] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 6/3/2024 1:27:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[idProducto] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](100) NULL,
	[marca] [varchar](100) NULL,
	[modelo] [varchar](100) NULL,
	[tamano] [varchar](100) NULL,
	[stock] [int] NULL,
	[precio] [decimal](10, 2) NULL,
	[esActivo] [bit] NULL,
	[fechaRegistro] [datetime] NULL,
	[fechaEdicion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 6/3/2024 1:27:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NombreUsuario] [varchar](20) NOT NULL,
	[Password] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta]    Script Date: 6/3/2024 1:27:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta](
	[idVenta] [int] IDENTITY(1,1) NOT NULL,
	[usuario] [int] NULL,
	[numeroDocumento] [varchar](40) NULL,
	[fechaRegistro] [datetime] NULL,
	[total] [decimal](10, 2) NULL,
	[idCliente] [int] NULL,
	[diaPago] [int] NULL,
	[comision] [decimal](10, 2) NULL,
	[tipoVenta] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[idVenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Clientes] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Activo]
GO
ALTER TABLE [dbo].[Clientes] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Clientes] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [FechaEdicion]
GO
ALTER TABLE [dbo].[NumeroDocumento] ADD  DEFAULT (getdate()) FOR [fechaRegistro]
GO
ALTER TABLE [dbo].[Pagos] ADD  DEFAULT (getdate()) FOR [fechaPago]
GO
ALTER TABLE [dbo].[Producto] ADD  DEFAULT (getdate()) FOR [fechaRegistro]
GO
ALTER TABLE [dbo].[Producto] ADD  DEFAULT (getdate()) FOR [fechaEdicion]
GO
ALTER TABLE [dbo].[CuentasPendientes]  WITH CHECK ADD FOREIGN KEY([idCliente])
REFERENCES [dbo].[Clientes] ([Id])
GO
ALTER TABLE [dbo].[DetalleVenta]  WITH CHECK ADD FOREIGN KEY([idProducto])
REFERENCES [dbo].[Producto] ([idProducto])
GO
ALTER TABLE [dbo].[DetalleVenta]  WITH CHECK ADD FOREIGN KEY([idVenta])
REFERENCES [dbo].[Venta] ([idVenta])
GO
ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD FOREIGN KEY([idcuentaPendiente])
REFERENCES [dbo].[CuentasPendientes] ([id])
GO
ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD FOREIGN KEY([usuarioCobro])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD FOREIGN KEY([idCliente])
REFERENCES [dbo].[Clientes] ([Id])
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD FOREIGN KEY([usuario])
REFERENCES [dbo].[Usuarios] ([Id])
GO
USE [master]
GO
ALTER DATABASE [DB_HermanosCastro] SET  READ_WRITE 
GO

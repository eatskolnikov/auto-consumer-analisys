USE [master]
GO
/****** Object:  Database [autoconsumeranalisys]    Script Date: 04/13/2013 12:31:57 ******/
CREATE DATABASE [autoconsumeranalisys] ON  PRIMARY 
( NAME = N'autoconsumeranalisys', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER2008\MSSQL\DATA\autoconsumeranalisys.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'autoconsumeranalisys_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER2008\MSSQL\DATA\autoconsumeranalisys_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [autoconsumeranalisys] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [autoconsumeranalisys].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [autoconsumeranalisys] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [autoconsumeranalisys] SET ANSI_NULLS OFF
GO
ALTER DATABASE [autoconsumeranalisys] SET ANSI_PADDING OFF
GO
ALTER DATABASE [autoconsumeranalisys] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [autoconsumeranalisys] SET ARITHABORT OFF
GO
ALTER DATABASE [autoconsumeranalisys] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [autoconsumeranalisys] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [autoconsumeranalisys] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [autoconsumeranalisys] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [autoconsumeranalisys] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [autoconsumeranalisys] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [autoconsumeranalisys] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [autoconsumeranalisys] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [autoconsumeranalisys] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [autoconsumeranalisys] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [autoconsumeranalisys] SET  DISABLE_BROKER
GO
ALTER DATABASE [autoconsumeranalisys] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [autoconsumeranalisys] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [autoconsumeranalisys] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [autoconsumeranalisys] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [autoconsumeranalisys] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [autoconsumeranalisys] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [autoconsumeranalisys] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [autoconsumeranalisys] SET  READ_WRITE
GO
ALTER DATABASE [autoconsumeranalisys] SET RECOVERY FULL
GO
ALTER DATABASE [autoconsumeranalisys] SET  MULTI_USER
GO
ALTER DATABASE [autoconsumeranalisys] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [autoconsumeranalisys] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'autoconsumeranalisys', N'ON'
GO
USE [autoconsumeranalisys]
GO
/****** Object:  Table [dbo].[Rols]    Script Date: 04/13/2013 12:31:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Rols](
	[RolId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ParsedPackages]    Script Date: 04/13/2013 12:31:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ParsedPackages](
	[ParsedPackageId] [int] IDENTITY(1,1) NOT NULL,
	[PackageId] [int] NULL,
	[PackageDate] [int] NOT NULL,
	[PackageTimeOfDay] [float] NOT NULL,
	[LatLng] [varchar](250) NOT NULL,
	[MAC] [varchar](250) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Activo] [bit] NOT NULL,
	[Description] [varchar](500) NOT NULL,
 CONSTRAINT [PK_ParsedPackages] PRIMARY KEY CLUSTERED 
(
	[ParsedPackageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Packages]    Script Date: 04/13/2013 12:31:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Packages](
	[PackageId] [int] IDENTITY(1,1) NOT NULL,
	[Ip] [varchar](35) NOT NULL,
	[Message] [varchar](255) NOT NULL,
	[Parsed] [bit] NOT NULL,
	[Activo] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_Packages] PRIMARY KEY CLUSTERED 
(
	[PackageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Devices]    Script Date: 04/13/2013 12:31:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Devices](
	[DeviceId] [int] IDENTITY(1,1) NOT NULL,
	[Ip] [varchar](35) NOT NULL,
	[LatLng] [varchar](250) NOT NULL,
	[Description] [varchar](300) NOT NULL,
	[Activo] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_Devices] PRIMARY KEY CLUSTERED 
(
	[DeviceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 04/13/2013 12:31:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](100) NOT NULL,
	[Password] [varchar](250) NOT NULL,
	[IsAdmin] [bit] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[Spc_Reporte_Flujo]    Script Date: 04/13/2013 12:32:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Spc_Reporte_Flujo]
(
	@StartDate int = null,
	@EndDate int = null
)
as
Begin
	if(@StartDate is null)
	begin
		set @StartDate = Convert(varchar(10), GetDate(), 112)
		set @EndDate = Convert(varchar(10), GetDate(), 112)
	end
	Select * from dbo.ParsedPackages as a with(ReadPast)
	where a.PackageDate >= @StartDate and a.PackageDate <=@EndDate
End
GO
/****** Object:  StoredProcedure [dbo].[Spc_Reporte_Dispositivos]    Script Date: 04/13/2013 12:32:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Spc_Reporte_Dispositivos](
	@StartDate int = null,
	@EndDate int = null
)as
Begin
	if(@StartDate is null)
	begin
		set @StartDate = Convert(varchar(10), GetDate(), 112)
		set @EndDate = Convert(varchar(10), GetDate(), 112)
	end
	Select a.Message, a.Ip from Packages as a
	where Convert(varchar(10), a.Created, 112) >= @StartDate and Convert(varchar(10), a.Created, 112) <= @EndDate
End
GO
/****** Object:  StoredProcedure [dbo].[Spc_Put_Package]    Script Date: 04/13/2013 12:32:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Spc_Put_Package]
(
	@Accion smallint,
	@PackageId int = null output,
	@ErrorMessage varchar(50) = null OutPut,
	@ErrorNumber int = null OutPut,
	@Ip varchar(35) = null,
	@Message varchar(250) = null,
	@Parsed bit = null,
	@Activo bit = null
)as
Begin
	Set Nocount On;
	Set @ErrorMessage = '';
	Set @ErrorNumber = 0;
	
	If @Accion=1
	Begin
		Insert into packages(ip,[message])
		values(@ip,@Message)
		set @PackageId = scope_identity()
	End
	Else
	Begin
		update 
			 a
		set
			a.Ip = IsNull(@ip,ip),
			a.Message = IsNull(@Message,[message]),
			a.Parsed =IsNull(@Parsed,parsed),
			a.Activo = IsNull(@Activo,Activo)
		from packages as a
		where
			@PackageId = PackageId
	End
End
GO
/****** Object:  StoredProcedure [dbo].[Spc_Put_Device]    Script Date: 04/13/2013 12:32:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Spc_Put_Device]
(
	@Accion smallint,
	@DeviceId int = null output,
	@ErrorMessage varchar(50) = null OutPut,
	@ErrorNumber int = null OutPut,
	@Ip varchar(35) = null,
	@LatLng varchar(250) = null,
	@Description varchar(250) = null,
	@Activo bit=null
)as
Begin
	Set Nocount On;
	Set @ErrorMessage = '';
	Set @ErrorNumber = 0;
	
	If @Accion=1
	Begin
		Insert into devices(Ip,LatLng,[Description])
		values(@Ip,@LatLng,@Description)
		set @DeviceId = scope_identity()
	End
	Else
	Begin
		update 
			devices
		set
			ip = IsNull(@Ip,ip),
			LatLng = IsNull(@LatLng,LatLng),
			[description] = IsNull(@Description,[description]),
			Activo = IsNull(@Activo, Activo)
		where
			@DeviceId = deviceid
	End
End
GO
/****** Object:  StoredProcedure [dbo].[Spc_Parse_Packages]    Script Date: 04/13/2013 12:32:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Spc_Parse_Packages]
as
Begin
	declare 
		@PackageId int= 0,
		@PackageDate int = 0,
		@LatLng varchar(250) = '',
		@MAC varchar(250) = ''
		
	update top (50) a
	set a.Parsed = 1
	from dbo.Packages as a
	where a.Parsed = 0 and a.Activo=1

	Insert Into dbo.ParsedPackages(PackageId,PackageDate,PackageTimeOfDay,LatLng,MAC, [Description])
	Select 
		b.PackageId,
		Convert(varchar(8),b.Created,112) int,
		CONVERT(float,b.Created),
		c.LatLng,
		b.[Message],
		c.Description
	From dbo.Packages as b 
	inner join Devices as c on (c.Ip+':8888') = b.Ip
	where b.Parsed = 1 and b.Activo =1
	
	update top (50) a
	set a.Activo = 0
	from dbo.Packages as a
	where a.Parsed = 1 and Activo=1
End
GO
/****** Object:  StoredProcedure [dbo].[Spc_Get_ParsedPackages]    Script Date: 04/13/2013 12:32:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Spc_Get_ParsedPackages]
(
	@Schema bit,
	@StartDate int = null,
	@EndDate int = null,
	@Mac varchar(255) = null
)
As
Begin
	If @Schema = 1
	Begin
		Declare
			@Accion Tinyint,
			@ErrorMessage Varchar(200),
			@ErrorNumber Int

		Set @ErrorMessage = ''
		Set @ErrorNumber = 0

		Select
			@Accion As Accion,
			@ErrorMessage As ErrorMessage,
			@ErrorNumber As ErrorNumber,
			a.*
		From dbo.ParsedPackages as a
		where 0=1
	End
	Else
	Begin
		if @EndDate is null
		Begin
			Select * from dbo.ParsedPackages as a
			where a.PackageDate >=@StartDate and a.PackageDate <=CONVERT(varchar(8),getdate(),112)
			Order by a.PackageDate, a.PackageTimeOfDay
		End
		Else if @Mac is null
		Begin
			Select * from dbo.ParsedPackages as a
			where a.PackageDate >=@StartDate and a.PackageDate <=@EndDate
			Order by a.PackageDate, a.PackageTimeOfDay
		End
		else
		Begin
			Select * from dbo.ParsedPackages as a
			where PackageDate >=@StartDate and PackageDate <=@EndDate and @Mac = MAC
			Order by a.PackageDate, a.PackageTimeOfDay
		End
	End
End
GO
/****** Object:  StoredProcedure [dbo].[Spc_Get_Package]    Script Date: 04/13/2013 12:32:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Spc_Get_Package]
(
	@Schema bit,
	@PackageId int=null
)as
Begin
	If @Schema=1 Begin
		Declare
			@Accion Tinyint,
			@ErrorMessage Varchar(200),
			@ErrorNumber Int

		Set @ErrorMessage = ''
		Set @ErrorNumber = 0

		Select
			@Accion As Accion,
			@ErrorMessage As ErrorMessage,
			@ErrorNumber As ErrorNumber,
			a.*
		From packages as a with(Readpast)
		Where 0=1
	End
	Else
	Begin
		if @PackageId is Null
		Begin
			Select * from packages
		End
		Else
		Begin
			Select * from packages as a
			where @PackageId = a.PackageId
		End
	End
End
GO
/****** Object:  StoredProcedure [dbo].[Spc_Get_Device]    Script Date: 04/13/2013 12:32:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Spc_Get_Device]
(
	@Schema bit,
	@DeviceId int=null
)as
Begin
	If @Schema=1 Begin
		Declare
			@Accion Tinyint,
			@ErrorMessage Varchar(200),
			@ErrorNumber Int

		Set @ErrorMessage = ''
		Set @ErrorNumber = 0

		Select
			@Accion As Accion,
			@ErrorMessage As ErrorMessage,
			@ErrorNumber As ErrorNumber,
			deviceid,
			ip,
			LatLng,
			[description],
			Activo
		From devices as a with(Readpast)
		Where 0=1
	End
	Else
	Begin
		if @DeviceId is Null
		Begin
			Select * from devices
			where Activo=1
		End
		Else
		Begin
			Select * from devices as a
			where @DeviceId = a.deviceid and Activo=1
		End
	End
End
GO
/****** Object:  Default [DF_ParsedPackages_Created]    Script Date: 04/13/2013 12:31:59 ******/
ALTER TABLE [dbo].[ParsedPackages] ADD  CONSTRAINT [DF_ParsedPackages_Created]  DEFAULT (getdate()) FOR [Created]
GO
/****** Object:  Default [DF_ParsedPackages_Activo]    Script Date: 04/13/2013 12:31:59 ******/
ALTER TABLE [dbo].[ParsedPackages] ADD  CONSTRAINT [DF_ParsedPackages_Activo]  DEFAULT ((1)) FOR [Activo]
GO
/****** Object:  Default [DF_ParsedPackages_Description]    Script Date: 04/13/2013 12:31:59 ******/
ALTER TABLE [dbo].[ParsedPackages] ADD  CONSTRAINT [DF_ParsedPackages_Description]  DEFAULT ('') FOR [Description]
GO
/****** Object:  Default [DF_devicepackages_parsed]    Script Date: 04/13/2013 12:31:59 ******/
ALTER TABLE [dbo].[Packages] ADD  CONSTRAINT [DF_devicepackages_parsed]  DEFAULT ((0)) FOR [Parsed]
GO
/****** Object:  Default [DF_packages_Activo]    Script Date: 04/13/2013 12:31:59 ******/
ALTER TABLE [dbo].[Packages] ADD  CONSTRAINT [DF_packages_Activo]  DEFAULT ((1)) FOR [Activo]
GO
/****** Object:  Default [DF_Packages_Created]    Script Date: 04/13/2013 12:31:59 ******/
ALTER TABLE [dbo].[Packages] ADD  CONSTRAINT [DF_Packages_Created]  DEFAULT (getdate()) FOR [Created]
GO
/****** Object:  Default [DF_devices_activo]    Script Date: 04/13/2013 12:31:59 ******/
ALTER TABLE [dbo].[Devices] ADD  CONSTRAINT [DF_devices_activo]  DEFAULT ((1)) FOR [Activo]
GO
/****** Object:  Default [DF_Devices_Created]    Script Date: 04/13/2013 12:31:59 ******/
ALTER TABLE [dbo].[Devices] ADD  CONSTRAINT [DF_Devices_Created]  DEFAULT (getdate()) FOR [Created]
GO
/****** Object:  Default [DF_Users_IsAdmin]    Script Date: 04/13/2013 12:31:59 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsAdmin]  DEFAULT ((0)) FOR [IsAdmin]
GO

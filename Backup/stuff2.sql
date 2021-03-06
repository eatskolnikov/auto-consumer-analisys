USE [autoconsumeranalisys]
GO
/****** Object:  StoredProcedure [dbo].[Spc_Parse_Packages]    Script Date: 12/09/2013 18:58:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[Spc_Parse_Packages]
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
	Select distinct
		0,
		--b.PackageId,
		Convert(varchar(8),b.Created,112) int,
		REPLACE (CONVERT(VARCHAR(5), GETDATE(), 114),':',''),
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

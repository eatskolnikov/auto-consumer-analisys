use autoconsumeranalisys
go

Alter procedure Spc_Parse_Packages
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

	Insert Into dbo.ParsedPackages(PackageId,PackageDate,PackageTimeOfDay,LatLng,MAC)
	Select 
		b.PackageId,
		Convert(varchar(8),b.Created,112) int,
		CONVERT(float,b.Created),
		c.LatLng,
		b.[Message]
	From dbo.Packages as b 
	inner join Devices as c on (c.Ip+':8888') = b.Ip
	where b.Parsed = 1 and b.Activo =1
	
	update top (50) a
	set a.Activo = 0
	from dbo.Packages as a
	where a.Parsed = 1 and Activo=1
End
go
use autoconsumeranalisys
go

alter Procedure Spc_DummyData_SingleMAC
(@mac varchar(440)= '+INQ:F4:B9:B0CA8,7A020C,7FF')
as
Begin
	Declare @name varchar(50) 
	Declare @ip varchar(30)
	Declare device_cursor Cursor For Select Ip From Devices Order by DeviceId asc
	
	truncate table Packages
	truncate table ParsedPackages
	
	Open device_cursor
	Fetch next from device_cursor into @ip
	While @@FETCH_STATUS = 0   
	Begin
	declare @realIp varchar(50) =(@ip+ ':8888')
		exec Spc_Put_Package 1,null,null,null,@realIp,@mac,0,1
		Fetch next from device_cursor into @ip
	End
	close device_cursor
	deallocate device_cursor
	exec Spc_Parse_Packages
End
go


exec Spc_DummyData_SingleMAC
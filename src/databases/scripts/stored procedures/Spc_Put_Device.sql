use autoconsumeranalisys
go

create procedure Spc_Put_Device
(
	@Accion smallint,
	@DeviceId int = null output,
	@ErrorMessage varchar(50) = null OutPut,
	@ErrorNumber int = null OutPut,
	@ip int = null,
	@lonlat varchar(250) = null,
	@description varchar(250) = null,
	@Activo bit=null
)as
Begin
	Set Nocount On;
	Set @ErrorMessage = '';
	Set @ErrorNumber = 0;
	
	If @Accion=1
	Begin
		Insert into devices(ip,lonlat)
		values(@ip,@lonlat)
		set @DeviceId = scope_identity()
	End
	Else
	Begin
		update 
			devices
		set
			ip = IsNull(@ip,ip),
			lonlat = IsNull(@lonlat,lonlat),
			[description] = IsNull(@description,[description])
		where
			@DeviceId = deviceid
	End
End
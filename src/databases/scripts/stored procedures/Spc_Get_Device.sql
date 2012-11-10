use autoconsumeranalisys
go

create procedure Spc_Get_Device
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
			[description]
		From devices as a with(Readpast)
		Where 0=1
	End
	Else
	Begin
		if @DeviceId is Null
		Begin
			Select * from devices
		End
		Else
		Begin
			Select * from devices as a
			where @DeviceId = a.deviceid
		End
	End
End
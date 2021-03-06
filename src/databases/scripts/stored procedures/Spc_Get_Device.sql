USE [autoconsumeranalisys]
GO
/****** Object:  StoredProcedure [dbo].[Spc_Get_Device]    Script Date: 12/07/2012 18:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[Spc_Get_Device]
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
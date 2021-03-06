USE [autoconsumeranalisys]
GO
/****** Object:  StoredProcedure [dbo].[Spc_Put_Device]    Script Date: 11/26/2012 00:40:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[Spc_Put_Device]
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
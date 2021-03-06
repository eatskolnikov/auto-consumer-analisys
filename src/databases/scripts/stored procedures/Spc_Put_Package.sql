USE [autoconsumeranalisys]
GO
/****** Object:  StoredProcedure [dbo].[Spc_Put_Package]    Script Date: 11/11/2012 17:25:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[Spc_Put_Package]
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
go
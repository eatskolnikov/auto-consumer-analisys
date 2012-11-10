use autoconsumeranalisys
go

create procedure Spc_Put_Package
(
	@Accion smallint,
	@PackageId int = null output,
	@ErrorMessage varchar(50) = null OutPut,
	@ErrorNumber int = null OutPut,
	@Ip int = null,
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
			packages
		set
			ip = IsNull(@ip,ip),
			[message] = IsNull(@Message,[message]),
			parsed =IsNull(@Parsed,parsed),
			Activo = IsNull(@Activo,Activo)
		where
			@PackageId = PackageId
	End
End
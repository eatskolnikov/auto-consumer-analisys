use autoconsumeranalisys
go

create procedure Spc_Get_Package
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
			a.packageid,
			a.ip,
			a.[message],
			a.parsed
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
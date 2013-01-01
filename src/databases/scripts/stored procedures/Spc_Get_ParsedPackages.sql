use autoconsumeranalisys
go 

Alter Procedure Spc_Get_ParsedPackages
(
	@Schema bit,
	@StartDate int = null,
	@EndDate int = null,
	@Mac varchar(255) = null
)
As
Begin
	If @Schema = 1
	Begin
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
			a.*
		From dbo.ParsedPackages as a
		where 0=1
	End
	Else
	Begin
		if @EndDate is null
		Begin
			Select * from dbo.ParsedPackages as a
			where a.PackageDate >=@StartDate and a.PackageDate <=CONVERT(varchar(8),getdate(),112)
			Order by a.PackageDate, a.PackageTimeOfDay
		End
		Else if @Mac is null
		Begin
			Select * from dbo.ParsedPackages as a
			where a.PackageDate >=@StartDate and a.PackageDate <=@EndDate
			Order by a.PackageDate, a.PackageTimeOfDay
		End
		else
		Begin
			Select * from dbo.ParsedPackages as a
			where PackageDate >=@StartDate and PackageDate <=@EndDate and @Mac = MAC
			Order by a.PackageDate, a.PackageTimeOfDay
		End
	End
End
go

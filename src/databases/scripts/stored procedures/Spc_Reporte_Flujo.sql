use autoconsumeranalisys
go

create procedure Spc_Reporte_Flujo
(
	@StartDate int = null,
	@EndDate int = null
)
as
Begin
	if(@StartDate is null)
	begin
		set @StartDate = Convert(varchar(10), GetDate(), 112)
		set @EndDate = Convert(varchar(10), GetDate(), 112)
	end
	Select * from dbo.ParsedPackages as a with(ReadPast)
	where a.PackageDate >= @StartDate and a.PackageDate <=@EndDate
End
go
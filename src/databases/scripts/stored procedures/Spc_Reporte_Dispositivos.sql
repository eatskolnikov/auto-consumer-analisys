Create procedure Spc_Reporte_Dispositivos(
	@StartDate int = null,
	@EndDate int = null
)as
Begin
	if(@StartDate is null)
	begin
		set @StartDate = Convert(varchar(10), GetDate(), 112)
		set @EndDate = Convert(varchar(10), GetDate(), 112)
	end
	Select a.Message, a.Ip from Packages as a
	where Convert(varchar(10), a.Created, 112) >= @StartDate and Convert(varchar(10), a.Created, 112) <= @EndDate
End
go
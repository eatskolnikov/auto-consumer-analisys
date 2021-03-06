USE [autoconsumeranalisys]
GO
/****** Object:  StoredProcedure [dbo].[Spc_Get_Package]    Script Date: 11/11/2012 13:06:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[Spc_Get_Package]
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
			a.*
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
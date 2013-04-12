use master
go

Create database autoconsumeranalisys
go

use autoconsumeranalisys
go


create table Device
(
	DeviceId int identity(1,1) primary key not null,
	Ip varchar(15) not null,
	LatLng varchar(300) not null,
	Description varchar(300) not null,
	Activo bit not null default 1,
	Created datetime not null default getdate()
)
go

create table Package
(
	PackageId int identity(1,1) primary key not null,
	Ip varchar(20) not null,
	Message varchar(250) not null,
	Parsed bit not null default 0,
	Activo bit not null default 1,
	Created datetime not null default getdate()
)
go

create table ParsedPackages
(
	ParsedPackageId int identity(1,1) primary key not null,
	PackageId int not null,
	PackageDate int not null,
	PackageTimeOfDay int not null,
	LatLng varchar(250) not null,
	MAC varchar(250) not null,
	Created datetime not null default getdate()
)
go
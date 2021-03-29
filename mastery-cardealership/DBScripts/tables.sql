USE GuildCars

IF EXISTS(SELECT * FROM sys.tables WHERE name='Purchases')
	DROP TABLE Purchases
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Vehicles')
	DROP TABLE Vehicles
GO


IF EXISTS(SELECT * FROM sys.tables WHERE name='Models')
	DROP TABLE Models
GO


IF EXISTS(SELECT * FROM sys.tables WHERE name='Makes')
	DROP TABLE Makes
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Types')
	DROP TABLE [Types]
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='BodyStyles')
	DROP TABLE BodyStyles
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Transmission')
	DROP TABLE Transmission
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Colors')
	DROP TABLE Colors
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='InteriorColors')
	DROP TABLE InteriorColors
GO


IF EXISTS(SELECT * FROM sys.tables WHERE name='Contacts')
	DROP TABLE Contacts
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='States')
	DROP TABLE States
GO




IF EXISTS(SELECT * FROM sys.tables WHERE name='PurchaseTypes')
	DROP TABLE PurchaseTypes
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Specials')
	DROP TABLE Specials
GO


CREATE TABLE Makes (
	MakeID int identity(1,1) not null primary key,
	MakeName varchar(15) not null,
	CreatedDate datetime2 not null default(getdate()),
	UserID nvarchar(128)
)

CREATE TABLE Models (
	ModelID int identity(1,1) not null primary key,
	MakeID int not null foreign key references Makes(MakeID),
	ModelName varchar(15) not null,
	CreatedDate datetime2 not null default(getdate()),
	UserID nvarchar(128)
)



CREATE TABLE [Types] (
	TypeID int identity(1,1) not null primary key,
	[TypeName] varchar(15) not null
)

CREATE TABLE BodyStyles (
	StyleID int identity(1,1) not null primary key,
	BodyStyleName varchar(15) not null
)

CREATE TABLE Transmission (
	TransmissionID int identity(1,1) not null primary key,
	TransmissionName varchar(15) not null
)

CREATE TABLE Colors (
	ColorID int identity(1,1) not null primary key,
	ColorName varchar(30) not null
)

CREATE TABLE InteriorColors (
	InteriorColorID int identity(1,1) not null primary key,
	InteriorColorName varchar(30) not null
)


CREATE TABLE Vehicles(
	VehicleID int identity(1,1) not null primary key,
	MakeID int not null foreign key references Makes(MakeId),
	ModelID int not null foreign key references Models(ModelID),
	TypeID int not null foreign key references [Types](TypeID),
	StyleID int not null foreign key references BodyStyles(StyleID),
	[Year] int not null,
	TransmissionID int not null foreign key references Transmission(TransmissionID),
	ColorID int not null foreign key references Colors(ColorID),
	InteriorColorID int not null foreign key references InteriorColors(InteriorColorID),
	Mileage int not null,
	VINNumber nvarchar(17) not null,
	MSRP decimal(15,2) not null,
	SalePrice decimal(15,2) null,
	Description varchar(500) not null,
	ImageFileName varchar(50) not null,
	Featured bit default(0),
	CreatedDate datetime2 not null default(getdate())
)

CREATE TABLE Contacts (
	ContactID int not null identity(1,1) primary key,
	ContactName varchar(50) not null,
	EmailAddress varchar(150) null,
	PhoneNumber nvarchar(100) null,
	Message nvarchar(300) not null	
)

CREATE TABLE States (
	StateID char(2) not null primary key,
	StateName varchar(30) not null
)

CREATE TABLE PurchaseTypes (
	PurchaseTypeID int not null identity(1,1) primary key,
	PurchaseTypeName varchar(15) not null
)

CREATE TABLE Specials (
	SpecialID int not null identity(1,1) primary key,
	SpecialTitle varchar(50) not null,
	SpecialDescription varchar(150) not null
)


CREATE TABLE Purchases (
	VehicleID int not null foreign key references Vehicles(VehicleID),
	UserID nvarchar(128) not null,
	[Name] varchar(100) not null,
	EmailAddress varchar(150) null,
	PhoneNumber nvarchar(100) null,
	StreetAddress1 nvarchar(100) not null,
	StreetAddress2 nvarchar(100)  null,
	City varchar(15) not null,
	StateID char(2) not null foreign key references States(StateID),
	Zipcode int not null,
	PurchasePrice decimal(15,2) not null,
	PurchaseTypeID  int foreign key references PurchaseTypes(PurchaseTypeID),
	PurchaseDate datetime2 not null default(getdate())
	primary key (VehicleID, UserID)
)




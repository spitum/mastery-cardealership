USE GuildCars

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DbReset')
      DROP PROCEDURE DbReset
GO

CREATE PROCEDURE DbReset AS
BEGIN
	DELETE FROM Purchases;
	DELETE FROM Vehicles;
	DELETE FROM Models;
	DELETE FROM BodyStyles;
	DELETE FROM Colors;
	DELETE FROM Contacts;
	DELETE FROM InteriorColors;
	DELETE FROM Colors;
	DELETE FROM States;
	DELETE FROM Makes;
	DELETE FROM PurchaseTypes;
	DELETE FROM Transmission;
	DELETE FROM [Types];
	DELETE FROM Specials;
	DELETE FROM AspNetUserRoles;
	DELETE FROM AspNetRoles;
	DELETE FROM AspNetUsers ;


	DBCC CHECKIDENT ('Vehicles', RESEED, 1)
	DBCC CHECKIDENT ('Makes', RESEED, 1)
	DBCC CHECKIDENT ('Models', RESEED, 1)
	DBCC CHECKIDENT ('Colors', RESEED, 1)
	DBCC CHECKIDENT ('InteriorColors', RESEED, 1)
	DBCC CHECKIDENT ('BodyStyles', RESEED, 1)
	DBCC CHECKIDENT ('Specials', RESEED, 1)
	DBCC CHECKIDENT ('Contacts', RESEED, 1)

	SET IDENTITY_INSERT Transmission ON;

	INSERT INTO Transmission (TransmissionID, TransmissionName)
	VALUES (1, 'Automatic'),
	(2, 'Manual')

	SET IDENTITY_INSERT Transmission OFF;

	SET IDENTITY_INSERT [Types] ON;

	INSERT INTO [Types] (TypeID, [TypeName])
	VALUES (1, 'New'),
	(2, 'Used')

	SET IDENTITY_INSERT [Types] OFF;

	SET IDENTITY_INSERT Makes ON;

	insert into dbo.Makes (MakeID, MakeName,UserID) values (1, 'Acura','00000000-0000-0000-0000-000000000000');
	insert into dbo.Makes (MakeID, MakeName,UserID) values (2, 'Jaguar','00000000-0000-0000-0000-000000000000');
	insert into dbo.Makes (MakeID, MakeName,UserID) values (3, 'GMC','00000000-0000-0000-0000-000000000000');
	insert into dbo.Makes (MakeID, MakeName,UserID) values (4, 'Chrysler','00000000-0000-0000-0000-000000000000');
	insert into dbo.Makes (MakeID, MakeName,UserID) values (5, 'Hyundai','00000000-0000-0000-0000-000000000000');
	insert into dbo.Makes (MakeID, MakeName,UserID) values (6, 'Dodge','00000000-0000-0000-0000-000000000000');
	insert into dbo.Makes (MakeID, MakeName,UserID) values (7, 'Chevrolet','00000000-0000-0000-0000-000000000000');
	insert into dbo.Makes (MakeID, MakeName,UserID) values (8, 'Nissan','00000000-0000-0000-0000-000000000000');
	insert into dbo.Makes (MakeID, MakeName,UserID) values (9, 'Mercedes-Benz','00000000-0000-0000-0000-000000000000');
	insert into dbo.Makes (MakeID, MakeName,UserID) values (10, 'Toyota','00000000-0000-0000-0000-000000000000');

	SET IDENTITY_INSERT Makes OFF;

	SET IDENTITY_INSERT Models ON;

	insert into dbo.Models(ModelID, MakeID, ModelName,UserID) values (1, 1, 'NSX','00000000-0000-0000-0000-000000000000');
	insert into dbo.Models (ModelID, MakeID, ModelName,UserID) values (2, 3, 'Vandura 3500','00000000-0000-0000-0000-000000000000');
	insert into dbo.Models (ModelID, MakeID, ModelName,UserID) values (3, 10, 'Camry','00000000-0000-0000-0000-000000000000');
	insert into dbo.Models (ModelID, MakeID, ModelName,UserID) values (4, 10, 'Corolla','00000000-0000-0000-0000-000000000000');
	insert into dbo.Models (ModelID, MakeID, ModelName,UserID) values (5, 10, 'X-Type','00000000-0000-0000-0000-000000000000');
	insert into dbo.Models (ModelID, MakeID, ModelName,UserID) values (6, 10, 'Celica','00000000-0000-0000-0000-000000000000');
	insert into dbo.Models (ModelID, MakeID, ModelName,UserID) values (7, 1, 'RDX','00000000-0000-0000-0000-000000000000');
	insert into dbo.Models (ModelID, MakeID, ModelName,UserID) values (8, 1, 'TSX','00000000-0000-0000-0000-000000000000');
	insert into dbo.Models (ModelID, MakeID, ModelName,UserID) values (9, 1, 'TL','00000000-0000-0000-0000-000000000000');
	insert into dbo.Models (ModelID, MakeID, ModelName,UserID) values (10, 8, '350Z','00000000-0000-0000-0000-000000000000');
	insert into dbo.Models (ModelID, MakeID, ModelName,UserID) values (11, 9, 'S-Class','00000000-0000-0000-0000-000000000000');
	insert into dbo.Models (ModelID, MakeID, ModelName,UserID) values (12, 9, 'SLK-Class','00000000-0000-0000-0000-000000000000');
	insert into dbo.Models (ModelID, MakeID, ModelName,UserID) values (13, 5, 'Santa Fe','00000000-0000-0000-0000-000000000000');

	SET IDENTITY_INSERT Models OFF;


	SET IDENTITY_INSERT PurchaseTypes ON;

	INSERT INTO PurchaseTypes (PurchaseTypeID, PurchaseTypeName)
	VALUES (1, 'Bank Finance'),
	(2, 'Cash'),
	(3, 'Dealer Finance')

	SET IDENTITY_INSERT PurchaseTypes OFF;

	SET IDENTITY_INSERT BodyStyles ON;

	INSERT INTO BodyStyles (StyleID, BodyStyleName)
	VALUES (1, 'Car'),
	(2, 'SUV'),
	(3, 'Truck')

	SET IDENTITY_INSERT BodyStyles OFF;

	SET IDENTITY_INSERT Colors ON;

	insert into dbo.Colors (ColorID, ColorName) values (1, 'Blue');
	insert into dbo.Colors (ColorID, ColorName) values (2, 'Yellow');
	insert into dbo.Colors (ColorID, ColorName) values (3, 'Indigo');
	insert into dbo.Colors (ColorID, ColorName) values (4, 'Crimson');
	insert into dbo.Colors (ColorID, ColorName) values (5, 'Turquoise');
	insert into dbo.Colors (ColorID, ColorName) values (6, 'Red');
	insert into dbo.Colors (ColorID, ColorName) values (7, 'Green');
	insert into dbo.Colors (ColorID, ColorName) values (8, 'Aquamarine');

	SET IDENTITY_INSERT Colors OFF;

	SET IDENTITY_INSERT InteriorColors ON;

	insert into dbo.InteriorColors (InteriorColorID, InteriorColorName) values (1, 'Black');
	insert into dbo.InteriorColors (InteriorColorID, InteriorColorName)  values (2, 'Grey');
	insert into dbo.InteriorColors (InteriorColorID, InteriorColorName)  values (3, 'Brown');


	SET IDENTITY_INSERT InteriorColors OFF;
	
	insert into dbo.States (StateId, StateName) values ('VA', 'Virginia');
	insert into dbo.States (StateId, StateName) values ('CT', 'Connecticut');
	insert into dbo.States (StateId, StateName) values ('CA', 'California');
	insert into dbo.States (StateId, StateName) values ('WA', 'Washington');
	insert into dbo.States (StateId, StateName) values ('KY', 'Kentucky');
	insert into dbo.States (StateId, StateName) values ('LA', 'Louisiana');
	insert into dbo.States (StateId, StateName) values ('GA', 'Georgia');
	insert into dbo.States (StateId, StateName) values ('IA', 'Iowa');
	insert into dbo.States (StateId, StateName) values ('FL', 'Florida');
	insert into dbo.States (StateId, StateName) values ('NY', 'New York');
	insert into dbo.States (StateId, StateName) values ('TX', 'Texas');
	insert into dbo.States (StateId, StateName) values ('AR', 'Arkansas');
	insert into dbo.States (StateId, StateName) values ('MA', 'Massachusetts');
	insert into dbo.States (StateId, StateName) values ('WI', 'Wisconsin');
	insert into dbo.States (StateId, StateName) values ('UT', 'Utah');
	insert into dbo.States (StateId, StateName) values ('NM', 'New Mexico');
	insert into dbo.States (StateId, StateName) values ('PA', 'Pennsylvania');
	insert into dbo.States (StateId, StateName) values ('DC', 'District of Columbia');

	SET IDENTITY_INSERT Vehicles ON;

	insert into dbo.Vehicles (VehicleID, MakeID, ModelID, TypeID, StyleID, Year, TransmissionID, ColorID, InteriorColorID, Mileage, VINNumber, MSRP, SalePrice, Description, ImageFileName) values (1, 1, 1, 2, 1, 2014, 1, 1, 1, 16253, 'WAUKG78E56A885549', 39584, 48656, 'Praesent blandit. Nam nulla. Integer pede justo, lacinia eget, tincidunt eget, tempus vel, pede.', 'NSX.JFIF');
	insert into dbo.Vehicles (VehicleID, MakeID, ModelID, TypeID, StyleID, Year, TransmissionID, ColorID, InteriorColorID, Mileage, VINNumber, MSRP, SalePrice, Description, ImageFileName) values (2, 3, 2, 2, 2, 2009, 2, 2, 2, 12483, 'WAUCFAFH7BN076493', 12088, 9193, 'Integer tincidunt ante vel ipsum. Praesent blandit lacinia erat. Vestibulum sed magna at nunc commodo placerat.', 'graycar.jpg');
	insert into dbo.Vehicles (VehicleID, MakeID, ModelID, TypeID, StyleID, Year, TransmissionID, ColorID, InteriorColorID, Mileage, VINNumber, MSRP, SalePrice, Description, ImageFileName) values (3, 10, 3, 1, 1, 2018, 1, 2, 3, 0, 'JM1NC2SF1F0395664', 39056, 34416, 'Vestibulum quam sapien, varius ut, blandit non, interdum in, ante. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Duis faucibus accumsan odio. Curabitur convallis.', 'NSX.JFIF');
	insert into dbo.Vehicles (VehicleID, MakeID, ModelID, TypeID, StyleID, Year, TransmissionID, ColorID, InteriorColorID, Mileage, VINNumber, MSRP, SalePrice, Description, ImageFileName) values (4, 5, 13, 1, 2, 2021, 2, 2, 2, 1, 'WAUCFAFH7BN075393', 19999, 16500, 'Integer tincidunt ante vel ipsum. Praesent blandit lacinia erat. Vestibulum sed magna at nunc commodo placerat.', 'graycar.jpg');
	insert into dbo.Vehicles (VehicleID, MakeID, ModelID, TypeID, StyleID, Year, TransmissionID, ColorID, InteriorColorID, Mileage, VINNumber, MSRP, SalePrice, Description, ImageFileName) values (5, 10, 4, 1, 1, 2018, 1, 1, 3, 2, 'JM1NC2SF1F0384664', 34000, 29999, 'Vestibulum quam sapien, varius ut, blandit non, interdum in, ante. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Duis faucibus accumsan odio. Curabitur convallis.', 'corolla2.JFIF');
	insert into dbo.Vehicles (VehicleID, MakeID, ModelID, TypeID, StyleID, Year, TransmissionID, ColorID, InteriorColorID, Mileage, VINNumber, MSRP, SalePrice, Description, ImageFileName,Featured) values (6, 10, 4, 1, 1, 2020, 1, 4, 3, 0, 'JM1NC2SF1F0384634', 34000, 28999, 'Vestibulum quam sapien, varius ut, blandit non, interdum in, ante. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Duis faucibus accumsan odio. Curabitur convallis.', 'corolla1.JFIF',1);
	insert into dbo.Vehicles (VehicleID, MakeID, ModelID, TypeID, StyleID, Year, TransmissionID, ColorID, InteriorColorID, Mileage, VINNumber, MSRP, SalePrice, Description, ImageFileName,Featured) values (7, 10, 4, 1, 1, 2020, 1, 2, 3, 0, 'JM1NC2SF1F0384654', 34000, 29699, 'Vestibulum quam sapien, varius ut, blandit non, interdum in, ante. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Duis faucibus accumsan odio. Curabitur convallis.', 'corolla2.JFIF',1);
	insert into dbo.Vehicles (VehicleID, MakeID, ModelID, TypeID, StyleID, Year, TransmissionID, ColorID, InteriorColorID, Mileage, VINNumber, MSRP, SalePrice, Description, ImageFileName,Featured) values (8, 10, 4, 2, 1, 2019, 1, 2, 3, 13000, 'JM1NC2SF1F0384655', 34000, 29899, 'Vestibulum quam sapien, varius ut, blandit non, interdum in, ante. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Duis faucibus accumsan odio. Curabitur convallis.', 'corolla1.JFIF',1);
	insert into dbo.Vehicles (VehicleID, MakeID, ModelID, TypeID, StyleID, Year, TransmissionID, ColorID, InteriorColorID, Mileage, VINNumber, MSRP, SalePrice, Description, ImageFileName,Featured) values (9, 10, 4, 2, 1, 2018, 1, 2, 3, 9020, 'JM1NC2SF1F0384656', 34000, 29999, 'Vestibulum quam sapien, varius ut, blandit non, interdum in, ante. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Duis faucibus accumsan odio. Curabitur convallis.', 'corolla2.JFIF',1);
	insert into dbo.Vehicles (VehicleID, MakeID, ModelID, TypeID, StyleID, Year, TransmissionID, ColorID, InteriorColorID, Mileage, VINNumber, MSRP, SalePrice, Description, ImageFileName,Featured) values (10, 10, 4, 2, 1, 2020, 1, 2, 3, 9205, 'JM1NC2SF1F0384657', 34000, 29999, 'Vestibulum quam sapien, varius ut, blandit non, interdum in, ante. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Duis faucibus accumsan odio. Curabitur convallis.', 'corolla1.JFIF',1);
	SET IDENTITY_INSERT Vehicles OFF;

		INSERT INTO AspNetUsers(Id, EmailConfirmed, PhoneNumberConfirmed, Email, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName,FirstName,LastName,SecurityStamp)
	VALUES('00000000-0000-0000-0000-000000000000', 0, 0, 'test@test.com',  0, 0, 0, 'test','test','Tables','38389a82-01e3-428f-80ac-f1d61316e4e8'),
	('11111111-1111-1111-1111-111111111111', 0, 0, 'test2@test.com',  0, 0, 0, 'test2','test2','Fables','38389a82-02e3-428f-80ac-f1d61316e4e5');

	
	insert into dbo.Purchases (VehicleID, UserID, Name, EmailAddress, PhoneNumber, StreetAddress1, StreetAddress2, City, StateID, ZipCode, PurchasePrice, PurchaseTypeID, PurchaseDate) values (1, '00000000-0000-0000-0000-000000000000', 'Gregor Gibbe', 'ggibbe0@oakley.com', '210-859-4326', '0 Bayside Crossing', null, 'San Antonio', 'TX', '78285', 49863.56, 3, '2018/11/26');
	insert into dbo.Purchases (VehicleID, UserID, Name, EmailAddress, PhoneNumber, StreetAddress1, StreetAddress2, City, StateID, ZipCode, PurchasePrice, PurchaseTypeID, PurchaseDate) values (2, '00000000-0000-0000-0000-000000000000', 'Aindrea Ogilby', 'aogilby1@imgur.com', '361-563-9613', '71049 Caliangt Road', null, 'Corpus Christi', 'TX', '78475', 14702.35, 2, '2018/07/11');
	insert into dbo.Purchases (VehicleID, UserID, Name, EmailAddress, PhoneNumber, StreetAddress1, StreetAddress2, City, StateID, ZipCode, PurchasePrice, PurchaseTypeID, PurchaseDate) values (3, '00000000-0000-0000-0000-000000000000', 'Yetta Cater', 'ycater2@webmd.com', '713-257-0284', '72 Kim Court', null, 'Houston', 'TX', '77010', 38466.97, 3, '2019/01/23');

	SET IDENTITY_INSERT Specials ON;
	insert into dbo.Specials (SpecialID, SpecialTitle, SpecialDescription) values (1, 'Summer Deal', 'Object-based encompassing secured line');
	insert into dbo.Specials (SpecialID, SpecialTitle, SpecialDescription) values (2, 'Winter Deal', 'Triple-buffered value-added challenge');
	insert into dbo.Specials (SpecialID, SpecialTitle, SpecialDescription) values (3, 'New Year''savings', 'Decentralized global open system');
	insert into dbo.Specials (SpecialID, SpecialTitle, SpecialDescription) values (4, 'Presidents Day Special', 'Synchronised needs-based policy');
	insert into dbo.Specials (SpecialID, SpecialTitle, SpecialDescription) values (5, 'End of Year Sale', 'Seamless web-enabled pricing structure');

	SET IDENTITY_INSERT Specials OFF;

	--SET IDENTITY_INSERT AspNetRoles ON;

	insert into dbo.AspNetRoles(id,Name)
	VALUES(1,'Admin'),(2,'Sales'),(3,'Disabled')

	--SET IDENTITY_INSERT AspNetRoles OFF;

	insert into dbo.AspNetUserRoles(UserId,RoleId)
	values('00000000-0000-0000-0000-000000000000',1),
	('11111111-1111-1111-1111-111111111111',2)
	--('4ab95c0e-425f-454c-8d9a-e5394e830b40',1),
	--('94dc7178-3a19-4520-b989-8101ef9c4264',2),
	--('7699c1ce-1956-4dac-a9c5-3721802dea85',1),
	--('b6e6cb41-bc38-495c-b625-4f18f7cb37e0',1)

END
GO
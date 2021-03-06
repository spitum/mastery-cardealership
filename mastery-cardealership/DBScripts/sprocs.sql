USE GuildCars
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'StatesSelectAll')
      DROP PROCEDURE StatesSelectAll
GO

CREATE PROCEDURE StatesSelectAll AS
BEGIN
	SELECT StateId, StateName
	FROM States
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SpecialsSelectAll')
      DROP PROCEDURE SpecialsSelectAll
GO

CREATE PROCEDURE SpecialsSelectAll AS
BEGIN
	SELECT SpecialID, SpecialTitle,SpecialDescription
	FROM Specials
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'FeaturedSelectAll')
      DROP PROCEDURE FeaturedSelectAll
GO

CREATE PROCEDURE FeaturedSelectAll AS
BEGIN
	SELECT VehicleID,VEH.MakeID,VEH.ModelID,SalePrice,ModelName,MakeName,ImageFileName, [Year]
	FROM Vehicles as VEH
	INNER JOIN dbo.Makes as Make ON Veh.MakeID = Make.MakeID
	INNER JOIN dbo.Models as Model ON Veh.ModelID = Model.ModelID
	Where Featured = 1
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectVehicleByID')
      DROP PROCEDURE SelectVehicleByID
GO

CREATE PROCEDURE SelectVehicleByID (
	@VehicleID int
) AS 
BEGIN
	SELECT [VehicleID]
      ,[MakeID]
      ,[ModelID]
      ,[TypeID]
      ,[StyleID]
      ,[Year]
      ,[TransmissionID]
      ,[ColorID]
      ,[InteriorColorID]
      ,[Mileage]
      ,[VINNumber]
      ,[MSRP]
      ,[SalePrice]
      ,[Description]
      ,[ImageFileName]
      ,[Featured]
	FROM GuildCars.dbo.Vehicles
	WHERE VehicleID = @VehicleID 
END

GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectVehicleDetails')
      DROP PROCEDURE SelectVehicleDetails
GO

CREATE PROCEDURE SelectVehicleDetails (
	@VehicleID int
) AS 
BEGIN
	SELECT Veh.VehicleID,Veh.MakeID,Make.MakeName,Model.ModelID,ModelName,[Year],VINNumber,MSRP,SalePrice,Mileage , BS.StyleID, BS.BodyStyleName, T.TransmissionID,T.TransmissionName, Veh.TypeID,TypeName
	,C.ColorID,C.ColorName,IC.InteriorColorID,IC.InteriorColorName,Description,ImageFileName
	FROM GuildCars.dbo.Vehicles as Veh
	INNER JOIN dbo.Makes as Make ON Veh.MakeID = Make.MakeID
	INNER JOIN dbo.Models as Model ON Veh.ModelID = Model.ModelID
	INNER JOIN dbo.BodyStyles as BS ON BS.StyleID = Veh.StyleID
	INNER JOIN dbo.Transmission as T ON T.TransmissionID = Veh.TransmissionID
	INNER JOIN dbo.Colors as C ON C.ColorID = Veh.ColorID
	INNER JOIN dbo.InteriorColors as IC ON IC.InteriorColorID = Veh.InteriorColorID
	INNER JOIN dbo.[Types] as Typ on Typ.TypeID = Veh.TypeID
	WHERE Veh.VehicleID = @VehicleID 
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'InventoryReport')
      DROP PROCEDURE InventoryReport
GO

CREATE PROCEDURE InventoryReport (
	@TypeID int
) AS 
BEGIN
	SELECT Veh.TypeID, TypeName, Veh.MakeID,MakeName,Veh.ModelID,ModelName,[Year],Count([VehicleID]) as [Count],SUM(MSRP) as StockValue  
	FROM GuildCars.dbo.Vehicles as Veh
	INNER JOIN dbo.Makes as Make ON Veh.MakeID = Make.MakeID
	INNER JOIN dbo.Models as Model ON Veh.ModelID = Model.ModelID
	INNER JOIN dbo.Types as T ON T.TypeID = Veh.TypeID
	WHERE Veh.TypeID = @TypeID
	GROUP BY Veh.TypeID, TypeName, Veh.MakeID,MakeName,Veh.ModelID,ModelName,[Year]
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectInventory')
      DROP PROCEDURE SelectInventory
GO

CREATE PROCEDURE SelectInventory
(
	  @QuickSearch	    NVARCHAR(50) = NULL
	 ,@Type             NVARCHAR(50) = NULL
	 ,@MinPrice			NVARCHAR(50) = NULL
	 ,@MaxPrice			NVARCHAR(50) = NULL
	 ,@MinYear			NVARCHAR(50) = NULL
	 ,@MaxYear			NVARCHAR(50) = NULL
)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @SQL							NVARCHAR(MAX)
	DECLARE @ParameterDef					NVARCHAR(500)
    SET @ParameterDef =      '@QuickSearch	NVARCHAR(50),
							 @Type			NVARCHAR(50),
							 @MinPrice		NVARCHAR(50) ,
							 @MaxPrice		NVARCHAR(50) ,
							 @MinYear		NVARCHAR(50) ,
							 @MaxYear		NVARCHAR(50)
							'
							



			SET @SQL = 'SELECT TOP 20 VehicleID,Veh.MakeID,Make.MakeName,Model.ModelID,ModelName,[Year],VINNumber,MSRP,SalePrice,Mileage , BS.StyleID, BS.BodyStyleName, T.TransmissionID,T.TransmissionName, Veh.TypeID,TypeName
					,C.ColorID,C.ColorName,IC.InteriorColorID,IC.InteriorColorName, Veh.ImageFileName
					FROM GuildCars.dbo.Vehicles as Veh
					INNER JOIN dbo.Makes as Make ON Veh.MakeID = Make.MakeID
					INNER JOIN dbo.Models as Model ON Veh.ModelID = Model.ModelID
					INNER JOIN dbo.BodyStyles as BS ON BS.StyleID = Veh.StyleID
					INNER JOIN dbo.Transmission as T ON T.TransmissionID = Veh.TransmissionID
					INNER JOIN dbo.Colors as C ON C.ColorID = Veh.ColorID
					INNER JOIN dbo.InteriorColors as IC ON IC.InteriorColorID = Veh.InteriorColorID
					INNER JOIN dbo.[Types] as Typ on Typ.TypeID = Veh.TypeID
					WHERE -1=-1'

			IF @Type IS NOT NULL 
			SET @SQL = @SQL+ ' AND TypeName = @Type '
					
			IF @QuickSearch IS NOT NULL 
			SET @SQL = @SQL+ ' AND (MakeName like ''%'' + @QuickSearch + ''%'' OR ModelName like ''%'' + @QuickSearch + ''%'' OR Year like ''%'' + @QuickSearch + ''%'')'

			IF @MinPrice IS NOT NULL AND @MinPrice <> 0
			SET @SQL = @SQL+ ' AND SalePrice >=' + @MinPrice

			IF @MaxPrice IS NOT NULL AND @MaxPrice <> 0
			SET @SQL = @SQL+ ' AND SalePrice <=' + @MaxPrice

			IF @MinYear IS NOT NULL AND @MinYear <> 0
			SET @SQL = @SQL+ ' AND Year >=' + @MinYear

			IF @MaxYear IS NOT NULL AND @MaxYear <> 0
			SET @SQL = @SQL+ ' AND Year <=' + @MaxYear

			SET @SQL = @SQL + ' AND NOT EXISTS(SELECT * FROM dbo.Purchases WHERE VehicleID = Veh.VehicleID) ORDER BY MSRP DESC'

			EXEC sp_Executesql     @SQL,  @ParameterDef, @QuickSearch=@QuickSearch,@Type=@Type,@MinPrice=@MinPrice,@MaxPrice=@MaxPrice,@MinYear=@MinYear,@MaxYear=@MaxYear
			
 
END
 
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SalesReport')
      DROP PROCEDURE SalesReport
GO

CREATE PROCEDURE SalesReport
(
	  @UserName			NVARCHAR(256) = NULL	
	 ,@FromDate			DATE = NULL
	 ,@ToDate			DATE = NULL
)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @SQL							NVARCHAR(MAX)
	DECLARE @ParameterDef					NVARCHAR(500)
    SET @ParameterDef =      '@UserName		NVARCHAR(256),
							 @FromDate			NVARCHAR(50),
							 @ToDate			NVARCHAR(50)
							'
							



			SET @SQL = 'SELECT UserID,UserName,SUM(PurchasePrice) AS TotalSales,COUNT(VehicleID) as TotalVehicles
						FROM dbo.Purchases AS P 
						INNER JOIN dbo.AspNetUsers AS U ON U.Id = P.UserID
						WHERE -1=-1
						'

			IF @UserName IS NOT NULL 
			SET @SQL = @SQL+ ' AND UserName = @UserName '
					
			IF @FromDate IS NOT NULL AND @FromDate <> ''
			SET @SQL = @SQL+ ' AND PurchaseDate >=''' + CAST(@FromDate as varchar(100)) + ''''
 
			IF @ToDate IS NOT NULL AND @ToDate <> ''
			SET @SQL = @SQL+ ' AND PurchaseDate <=''' + CAST(@ToDate as varchar(100)) + ''''
 

			SET @SQL = @SQL + ' GROUP BY UserID,UserName'

			EXEC sp_Executesql     @SQL,  @ParameterDef, @UserName=@UserName,@FromDate=@FromDate,@ToDate=@ToDate 
END
 
GO





IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ContactsInsert')
      DROP PROCEDURE ContactsInsert
GO

CREATE PROCEDURE ContactsInsert (
	@ContactName varchar(50),
	@EmailAddress varchar(150),
	@PhoneNumber nvarchar(100),
	@Message nvarchar(300)
) AS
BEGIN
	INSERT INTO dbo.Contacts(ContactName, EmailAddress,PhoneNumber, Message)
	VALUES (@ContactName, @EmailAddress,@PhoneNumber,@Message )
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'MakeInsert')
      DROP PROCEDURE MakeInsert
GO

CREATE PROCEDURE MakeInsert (
	@MakeName varchar(15),
	@UserID nvarchar(128)
) AS
BEGIN
	INSERT INTO dbo.Makes(MakeName,UserID)
	VALUES (@MakeName,@UserID)
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ModelInsert')
      DROP PROCEDURE ModelInsert
GO

CREATE PROCEDURE ModelInsert (
	@ModelName varchar(15),
	@MakeID int,
	@UserID nvarchar(128)
) AS
BEGIN
	INSERT INTO dbo.Models(ModelName,MakeID,UserID)
	VALUES (@ModelName,@MakeID,@UserID)
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SpecialInsert')
      DROP PROCEDURE SpecialInsert
GO

CREATE PROCEDURE SpecialInsert (
	@SpecialTitle varchar(50),
	@SpecialDescription varchar(150)
) AS
BEGIN
	INSERT INTO dbo.Specials(SpecialTitle,SpecialDescription)
	VALUES (@SpecialTitle,@SpecialDescription)
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SpecialDelete')
      DROP PROCEDURE SpecialDelete
GO

CREATE PROCEDURE SpecialDelete (
	@SpecialID int
) AS
BEGIN
	DELETE FROM dbo.Specials WHERE SpecialID = @SpecialID
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleDelete')
      DROP PROCEDURE VehicleDelete
GO

CREATE PROCEDURE VehicleDelete (
	@VehicleID int
) AS
BEGIN
	DELETE FROM dbo.Purchases WHERE VehicleID = @VehicleID
	DELETE FROM dbo.Vehicles WHERE VehicleID = @VehicleID
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'PurchaseInsert')
      DROP PROCEDURE PurchaseInsert
GO

CREATE PROCEDURE PurchaseInsert (
	@VehicleID int,
	@UserID nvarchar(128),
	@Name varchar(100),
	@EmailAddress varchar(150) = null,
	@PhoneNumber nvarchar(100) = null,
	@StreetAddress1 nvarchar(100),
	@StreetAddress2 nvarchar(100) = null,
	@City varchar(15),
	@StateID char(2),
	@ZipCode int,
	@PurchasePrice decimal(15,2),
	@PurchaseTypeID int

) AS
BEGIN
      INSERT INTO dbo.Purchases([VehicleID]
           ,[UserID]
           ,[Name]
           ,[EmailAddress]
           ,[PhoneNumber]
           ,[StreetAddress1]
           ,[StreetAddress2]
           ,[City]
           ,[StateID]
           ,[Zipcode]
           ,[PurchasePrice]
           ,[PurchaseTypeID]
		   )
	VALUES (@VehicleID ,
	@UserID ,
	@Name ,
	@EmailAddress ,
	@PhoneNumber ,
	@StreetAddress1 ,
	@StreetAddress2 ,
	@City ,
	@StateID ,
	@ZipCode ,
	@PurchasePrice,
	@PurchaseTypeID 
	)

END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleInsert')
      DROP PROCEDURE VehicleInsert
GO

CREATE PROCEDURE VehicleInsert (
	@VehicleID int output,
	@MakeID int,
	@ModelID int,
	@TypeID int,
	@StyleID int,
	@Year int,
	@TransmissionID int,
	@ColorID int,
	@InteriorColorID int,
	@Mileage int,
	@VINNumber nvarchar(17),
	@MSRP decimal(15,2),
	@SalePrice decimal(15,2),
	@Description varchar(500),
	@ImageFileName varchar(50)
	

) AS
BEGIN
      INSERT INTO dbo.Vehicles([MakeID]
           ,[ModelID]
           ,[TypeID]
           ,[StyleID]
           ,[Year]
           ,[TransmissionID]
           ,[ColorID]
           ,[InteriorColorID]
           ,[Mileage]
           ,[VINNumber]
           ,[MSRP]
           ,[SalePrice]
           ,[Description]
           ,[ImageFileName]
			)
	VALUES (
			@MakeID ,
			@ModelID ,
			@TypeID ,
			@StyleID ,
			@Year ,
			@TransmissionID ,
			@ColorID ,
			@InteriorColorID ,
			@Mileage ,
			@VINNumber ,
			@MSRP ,
			@SalePrice ,
			@Description ,
			@ImageFileName 
			)
	SET @VehicleID = SCOPE_IDENTITY();
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleUpdate')
      DROP PROCEDURE VehicleUpdate
GO


CREATE PROCEDURE VehicleUpdate (
	@VehicleID int,
	@MakeID int,
	@ModelID int,
	@TypeID int,
	@StyleID int,
	@Year int,
	@TransmissionID int,
	@ColorID int,
	@InteriorColorID int,
	@Mileage int,
	@VINNumber nvarchar(17),
	@MSRP decimal(15,2),
	@SalePrice decimal(15,2),
	@Description varchar(500),
	@ImageFileName varchar(50) = null,
	@Featured bit

) AS
BEGIN
	UPDATE Vehicles SET 
		MakeID = @MakeID, 
		ModelID = @ModelID, 
		TypeID = @TypeID, 
		StyleID = @StyleID, 
		[Year] = @Year, 
		TransmissionID = @TransmissionID, 
		ColorID = ColorID, 
		InteriorColorID = @InteriorColorID,
		Mileage = @Mileage, 
		VINNumber = @VINNumber,
		MSRP = @MSRP,
		SalePrice = @SalePrice,
		[Description] = @Description, 
		ImageFileName = @ImageFileName,
		Featured = @Featured
	WHERE VehicleID = @VehicleID

END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectUsersAll')
      DROP PROCEDURE SelectUsersAll
GO

CREATE PROCEDURE SelectUsersAll AS 
BEGIN
	SELECT U.id,LastName,FirstName,Email,R.RoleId,NR.Name FROM dbo.AspNetUsers as U 
	INNER JOIN dbo.AspNetUserRoles as R ON R.UserId = U.Id
	INNER JOIN dbo.AspNetRoles as NR ON NR.Id = R.RoleId

END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'MakesSelectAll')
      DROP PROCEDURE MakesSelectAll
GO

CREATE PROCEDURE MakesSelectAll AS
BEGIN
	SELECT MakeID, MakeName, id, Email,M.CreatedDate
	FROM Makes as M 
	INNER JOIN dbo.AspNetUsers as U ON U.Id = M.UserID
	ORDER BY MakeName
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ModelsSelectAll')
      DROP PROCEDURE ModelsSelectAll
GO

CREATE PROCEDURE ModelsSelectAll AS
BEGIN
	SELECT ModelId, ModelName, id, Email, M.MakeID, MakeName,M.CreatedDate
	FROM Models as M 
	INNER JOIN dbo.AspNetUsers as U ON U.Id = M.UserID
	INNER JOIN dbo.Makes as Make ON M.MakeID = Make.MakeID
	ORDER BY MakeName,ModelName
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ModelsSelectByMake')
      DROP PROCEDURE ModelsSelectByMake
GO

CREATE PROCEDURE ModelsSelectByMake (@MakeID int) AS 
BEGIN
	SELECT ModelId, ModelName, id, Email, M.MakeID, MakeName
	FROM Models as M 
	INNER JOIN dbo.AspNetUsers as U ON U.Id = M.UserID
	INNER JOIN dbo.Makes as Make ON M.MakeID = Make.MakeID
	WHERE M.MakeID = @MakeID
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectTypeAll')
      DROP PROCEDURE SelectTypeAll
GO

CREATE PROCEDURE SelectTypeAll AS
BEGIN
	SELECT TypeID, TypeName
	FROM [Types] 
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectPurchaseTypesAll')
      DROP PROCEDURE SelectPurchaseTypesAll
GO

CREATE PROCEDURE SelectPurchaseTypesAll AS
BEGIN
	SELECT PurchaseTypeID, PurchaseTypeName
	FROM dbo.PurchaseTypes 
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectRolesAll')
      DROP PROCEDURE SelectRolesAll
GO

CREATE PROCEDURE SelectRolesAll AS
BEGIN
	SELECT Id, Name
	FROM dbo.AspNetRoles 
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectContactsAll')
      DROP PROCEDURE SelectContactsAll
GO

CREATE PROCEDURE SelectContactsAll AS
BEGIN
	SELECT ContactID,ContactName,EmailAddress,PhoneNumber,Message
	FROM dbo.Contacts 
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectBodyStylesAll')
      DROP PROCEDURE SelectBodyStylesAll
GO

CREATE PROCEDURE SelectBodyStylesAll AS
BEGIN
	SELECT StyleID, BodyStyleName
	FROM BodyStyles 
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectTransmissionsAll')
      DROP PROCEDURE SelectTransmissionsAll
GO

CREATE PROCEDURE SelectTransmissionsAll AS
BEGIN
	SELECT TransmissionID, TransmissionName
	FROM Transmission 
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectColorsAll')
      DROP PROCEDURE SelectColorsAll
GO

CREATE PROCEDURE SelectColorsAll AS
BEGIN
	SELECT ColorID, ColorName
	FROM Colors 
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectInteriorColorsAll')
      DROP PROCEDURE SelectInteriorColorsAll
GO

CREATE PROCEDURE SelectInteriorColorsAll AS
BEGIN
	SELECT InteriorColorID, InteriorColorName
	FROM InteriorColors 
END

GO

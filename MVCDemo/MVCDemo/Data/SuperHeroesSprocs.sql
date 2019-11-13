
-------------------Citizen-----------------
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Add a new  Citizen to the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_CitizenAdd
@CitizenID int OUTPUT,
@FirstName nvarchar(60),
@LastName nvarchar(60),
@DateOfBirth datetime,
@EyeColor tinyint,
@HeightInInches decimal
AS
     INSERT INTO Citizens(FirstName,LastName,DateOfBirth,EyeColor,HeightInInches)
               VALUES(@FirstName,@LastName,@DateOfBirth,@EyeColor,
               @HeightInInches)
     SET @CitizenID = @@IDENTITY
GO

--GRANT EXECUTE ON dbo.sproc_CitizenAdd TO sprocEditUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Update Citizen in the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_CitizenUpdate
@CitizenID int,
@FirstName nvarchar(60),
@LastName nvarchar(60),
@DateOfBirth datetime,
@EyeColor tinyint,
@HeightInInches decimal
AS
     UPDATE Citizens
          SET
               FirstName = @FirstName,
               LastName = @LastName,
               DateOfBirth = @DateOfBirth,
               EyeColor = @EyeColor,
               HeightInInches = @HeightInInches
          WHERE CitizenID = @CitizenID
GO

--GRANT EXECUTE ON dbo.sproc_CitizenUpdate TO sprocEditUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve specific Citizen from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocCitizenGet
@CitizenID int
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM Citizens
     WHERE CitizenID = @CitizenID
END
GO

--GRANT EXECUTE ON dbo.sprocCitizenGet TO sprocReadUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve all Citizens from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocCitizensGetAll
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM Citizens
END
GO

--GRANT EXECUTE ON dbo.sprocCitizensGetAll TO sprocReadUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Remove specific Citizen from the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_CitizenRemove
@CitizenID int
AS
BEGIN
     DELETE FROM Citizens
          WHERE CitizenID = @CitizenID

     -- Return -1 if we had an error
     IF @@ERROR > 0
     BEGIN
          RETURN -1
     END
     ELSE
     BEGIN
          RETURN 1
     END
END
GO

--GRANT EXECUTE ON dbo.sproc_CitizenRemove TO sprocEditUser
GO
-------------------Costume-----------------
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Add a new  Costume to the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_CostumeAdd
@CostumeID int OUTPUT,
@ColorMain int,
@ColorSecondary int,
@ColorTertiary int,
@HasCape bit,
@HasMask bit
AS
     INSERT INTO Costumes(ColorMain,ColorSecondary,ColorTertiary,HasCape,HasMask)
               VALUES(@ColorMain,@ColorSecondary,@ColorTertiary,@HasCape,
               @HasMask)
     SET @CostumeID = @@IDENTITY
GO

--GRANT EXECUTE ON dbo.sproc_CostumeAdd TO sprocEditUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Update Costume in the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_CostumeUpdate
@CostumeID int,
@ColorMain int,
@ColorSecondary int,
@ColorTertiary int,
@HasCape bit,
@HasMask bit
AS
     UPDATE Costumes
          SET
               ColorMain = @ColorMain,
               ColorSecondary = @ColorSecondary,
               ColorTertiary = @ColorTertiary,
               HasCape = @HasCape,
               HasMask = @HasMask
          WHERE CostumeID = @CostumeID
GO

--GRANT EXECUTE ON dbo.sproc_CostumeUpdate TO sprocEditUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve specific Costume from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocCostumeGet
@CostumeID int
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM Costumes
     WHERE CostumeID = @CostumeID
END
GO

--GRANT EXECUTE ON dbo.sprocCostumeGet TO sprocReadUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve all Costumes from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocCostumesGetAll
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM Costumes
END
GO

--GRANT EXECUTE ON dbo.sprocCostumesGetAll TO sprocReadUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Remove specific Costume from the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_CostumeRemove
@CostumeID int
AS
BEGIN
     DELETE FROM Costumes
          WHERE CostumeID = @CostumeID

     -- Return -1 if we had an error
     IF @@ERROR > 0
     BEGIN
          RETURN -1
     END
     ELSE
     BEGIN
          RETURN 1
     END
END
GO

--GRANT EXECUTE ON dbo.sproc_CostumeRemove TO sprocEditUser
GO
-------------------SuperHero-----------------
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Add a new  SuperHero to the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_SuperHeroAdd
@SuperHeroID int OUTPUT,
@FirstName nvarchar(60),
@LastName nvarchar(60),
@DateOfBirth datetime,
@EyeColor tinyint,
@HeightInInches decimal,
@AlterEgoID int,
@SideKickID int,
@CostumeID int
AS
     INSERT INTO SuperHeroes(FirstName,LastName,DateOfBirth,EyeColor,HeightInInches,AlterEgoID,SideKickID,CostumeID)
               VALUES(@FirstName,@LastName,@DateOfBirth,@EyeColor,
               @HeightInInches,@AlterEgoID,@SideKickID,@CostumeID)
     SET @SuperHeroID = @@IDENTITY
GO

--GRANT EXECUTE ON dbo.sproc_SuperHeroAdd TO sprocEditUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Update SuperHero in the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_SuperHeroUpdate
@SuperHeroID int,
@FirstName nvarchar(60),
@LastName nvarchar(60),
@DateOfBirth datetime,
@EyeColor tinyint,
@HeightInInches decimal,
@AlterEgoID int,
@SideKickID int,
@CostumeID int
AS
     UPDATE SuperHeroes
          SET
               FirstName = @FirstName,
               LastName = @LastName,
               DateOfBirth = @DateOfBirth,
               EyeColor = @EyeColor,
               HeightInInches = @HeightInInches,
               AlterEgoID = @AlterEgoID,
               SideKickID = @SideKickID,
               CostumeID = @CostumeID
          WHERE SuperHeroID = @SuperHeroID
GO

--GRANT EXECUTE ON dbo.sproc_SuperHeroUpdate TO sprocEditUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve specific SuperHero from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocSuperHeroGet
@SuperHeroID int
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM SuperHeroes
     WHERE SuperHeroID = @SuperHeroID
END
GO

--GRANT EXECUTE ON dbo.sprocSuperHeroGet TO sprocReadUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve all SuperHeroes from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocSuperHeroesGetAll
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM SuperHeroes
END
GO

--GRANT EXECUTE ON dbo.sprocSuperHeroesGetAll TO sprocReadUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Remove specific SuperHero from the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_SuperHeroRemove
@SuperHeroID int
AS
BEGIN
     DELETE FROM SuperHeroes
          WHERE SuperHeroID = @SuperHeroID

     -- Return -1 if we had an error
     IF @@ERROR > 0
     BEGIN
          RETURN -1
     END
     ELSE
     BEGIN
          RETURN 1
     END
END
GO

--GRANT EXECUTE ON dbo.sproc_SuperHeroRemove TO sprocEditUser
GO
-------------------Villian-----------------
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Add a new  Villian to the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_VillianAdd
@VillianID int OUTPUT,
@FirstName nvarchar(60),
@LastName nvarchar(60),
@DateOfBirth datetime,
@EyeColor tinyint,
@HeightInInches decimal,
@SideKickID int,
@CostumeID int
AS
     INSERT INTO Villians(FirstName,LastName,DateOfBirth,EyeColor,HeightInInches,SideKickID,CostumeID)
               VALUES(@FirstName,@LastName,@DateOfBirth,@EyeColor,
               @HeightInInches,@SideKickID,@CostumeID)
     SET @VillianID = @@IDENTITY
GO

--GRANT EXECUTE ON dbo.sproc_VillianAdd TO sprocEditUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Update Villian in the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_VillianUpdate
@VillianID int,
@FirstName nvarchar(60),
@LastName nvarchar(60),
@DateOfBirth datetime,
@EyeColor tinyint,
@HeightInInches decimal,
@SideKickID int,
@CostumeID int
AS
     UPDATE Villians
          SET
               FirstName = @FirstName,
               LastName = @LastName,
               DateOfBirth = @DateOfBirth,
               EyeColor = @EyeColor,
               HeightInInches = @HeightInInches,
               SideKickID = @SideKickID,
               CostumeID = @CostumeID
          WHERE VillianID = @VillianID
GO

--GRANT EXECUTE ON dbo.sproc_VillianUpdate TO sprocEditUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve specific Villian from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocVillianGet
@VillianID int
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM Villians
     WHERE VillianID = @VillianID
END
GO

--GRANT EXECUTE ON dbo.sprocVillianGet TO sprocReadUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve all Villians from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocVilliansGetAll
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM Villians
END
GO

--GRANT EXECUTE ON dbo.sprocVilliansGetAll TO sprocReadUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Remove specific Villian from the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_VillianRemove
@VillianID int
AS
BEGIN
     DELETE FROM Villians
          WHERE VillianID = @VillianID

     -- Return -1 if we had an error
     IF @@ERROR > 0
     BEGIN
          RETURN -1
     END
     ELSE
     BEGIN
          RETURN 1
     END
END
GO

--GRANT EXECUTE ON dbo.sproc_VillianRemove TO sprocEditUser
GO
-------------------City-----------------
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Add a new  City to the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_CityAdd
@CityID int OUTPUT,
@Name nvarchar(60)
AS
     INSERT INTO Cities(Name)
               VALUES(@Name)
     SET @CityID = @@IDENTITY
GO

--GRANT EXECUTE ON dbo.sproc_CityAdd TO sprocEditUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Update City in the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_CityUpdate
@CityID int,
@Name nvarchar(60)
AS
     UPDATE Cities
          SET
               Name = @Name
          WHERE CityID = @CityID
GO

--GRANT EXECUTE ON dbo.sproc_CityUpdate TO sprocEditUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve specific City from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocCityGet
@CityID int
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM Cities
     WHERE CityID = @CityID
END
GO

--GRANT EXECUTE ON dbo.sprocCityGet TO sprocReadUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve all Cities from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocCitiesGetAll
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM Cities
END
GO

--GRANT EXECUTE ON dbo.sprocCitiesGetAll TO sprocReadUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Remove specific City from the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_CityRemove
@CityID int
AS
BEGIN
     DELETE FROM Cities
          WHERE CityID = @CityID

     -- Return -1 if we had an error
     IF @@ERROR > 0
     BEGIN
          RETURN -1
     END
     ELSE
     BEGIN
          RETURN 1
     END
END
GO

--GRANT EXECUTE ON dbo.sproc_CityRemove TO sprocEditUser
GO
-------------------Hideout-----------------
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Add a new  Hideout to the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_HideoutAdd
@HideoutID int OUTPUT,
@Name nvarchar(60),
@IsHeroBase bit
AS
     INSERT INTO Hideouts(Name,IsHeroBase)
               VALUES(@Name,@IsHeroBase)
     SET @HideoutID = @@IDENTITY
GO

--GRANT EXECUTE ON dbo.sproc_HideoutAdd TO sprocEditUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Update Hideout in the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_HideoutUpdate
@HideoutID int,
@Name nvarchar(60),
@IsHeroBase bit
AS
     UPDATE Hideouts
          SET
               Name = @Name,
               IsHeroBase = @IsHeroBase
          WHERE HideoutID = @HideoutID
GO

--GRANT EXECUTE ON dbo.sproc_HideoutUpdate TO sprocEditUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve specific Hideout from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocHideoutGet
@HideoutID int
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM Hideouts
     WHERE HideoutID = @HideoutID
END
GO

--GRANT EXECUTE ON dbo.sprocHideoutGet TO sprocReadUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve all Hideouts from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocHideoutsGetAll
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM Hideouts
END
GO

--GRANT EXECUTE ON dbo.sprocHideoutsGetAll TO sprocReadUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Remove specific Hideout from the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_HideoutRemove
@HideoutID int
AS
BEGIN
     DELETE FROM Hideouts
          WHERE HideoutID = @HideoutID

     -- Return -1 if we had an error
     IF @@ERROR > 0
     BEGIN
          RETURN -1
     END
     ELSE
     BEGIN
          RETURN 1
     END
END
GO

--GRANT EXECUTE ON dbo.sproc_HideoutRemove TO sprocEditUser
GO
-------------------Universe-----------------
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Add a new  Universe to the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_UniverseAdd
@SuperHeroID int OUTPUT,
@Name nvarchar(60)
AS
     INSERT INTO Universes(Name)
               VALUES(@Name)
     SET @SuperHeroID = @@IDENTITY
GO

--GRANT EXECUTE ON dbo.sproc_UniverseAdd TO sprocEditUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Update Universe in the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_UniverseUpdate
@UniverseID int,
@Name nvarchar(60)
AS
     UPDATE Universes
          SET
               Name = @Name
          WHERE UniverseID = @UniverseID
GO

--GRANT EXECUTE ON dbo.sproc_UniverseUpdate TO sprocEditUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve specific Universe from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocUniverseGet
@UniverseID int
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM Universes
     WHERE UniverseID = @UniverseID
END
GO

--GRANT EXECUTE ON dbo.sprocUniverseGet TO sprocReadUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve all Universes from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocUniversesGetAll
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM Universes
END
GO

--GRANT EXECUTE ON dbo.sprocUniversesGetAll TO sprocReadUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Remove specific Universe from the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_UniverseRemove
@UniverseID int
AS
BEGIN
     DELETE FROM Universes
          WHERE UniverseID = @UniverseID

     -- Return -1 if we had an error
     IF @@ERROR > 0
     BEGIN
          RETURN -1
     END
     ELSE
     BEGIN
          RETURN 1
     END
END
GO

--GRANT EXECUTE ON dbo.sproc_UniverseRemove TO sprocEditUser
GO
-------------------HideoutMember-----------------
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Add a new  HideoutMember to the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_HideoutMemberAdd
@HideoutMemberID int OUTPUT,
@HideoutID int,
@MemberID int
AS
     INSERT INTO HideoutMembers(HideoutID,MemberID)
               VALUES(@HideoutID,@MemberID)
     SET @HideoutMemberID = @@IDENTITY
GO

--GRANT EXECUTE ON dbo.sproc_HideoutMemberAdd TO sprocEditUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Update HideoutMember in the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_HideoutMemberUpdate
@HideoutMemberID int,
@HideoutID int,
@MemberID int
AS
     UPDATE HideoutMembers
          SET
               HideoutID = @HideoutID,
               MemberID = @MemberID
          WHERE HideoutMemberID = @HideoutMemberID
GO

--GRANT EXECUTE ON dbo.sproc_HideoutMemberUpdate TO sprocEditUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve specific HideoutMember from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocHideoutMemberGet
@HideoutMemberID int
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM HideoutMembers
     WHERE HideoutMemberID = @HideoutMemberID
END
GO

--GRANT EXECUTE ON dbo.sprocHideoutMemberGet TO sprocReadUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve all HideoutMembers from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocHideoutMembersGetAll
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM HideoutMembers
END
GO

--GRANT EXECUTE ON dbo.sprocHideoutMembersGetAll TO sprocReadUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Remove specific HideoutMember from the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_HideoutMemberRemove
@HideoutMemberID int
AS
BEGIN
     DELETE FROM HideoutMembers
          WHERE HideoutMemberID = @HideoutMemberID

     -- Return -1 if we had an error
     IF @@ERROR > 0
     BEGIN
          RETURN -1
     END
     ELSE
     BEGIN
          RETURN 1
     END
END
GO

--GRANT EXECUTE ON dbo.sproc_HideoutMemberRemove TO sprocEditUser
GO
-------------------PetType-----------------
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Add a new  PetType to the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_PetTypeAdd
@PetTypeID int OUTPUT,
@Name nvarchar(30)
AS
     INSERT INTO PetTypes(Name)
               VALUES(@Name)
     SET @PetTypeID = @@IDENTITY
GO

--GRANT EXECUTE ON dbo.sproc_PetTypeAdd TO sprocEditUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Update PetType in the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_PetTypeUpdate
@PetTypeID int,
@Name nvarchar(30)
AS
     UPDATE PetTypes
          SET
               Name = @Name
          WHERE PetTypeID = @PetTypeID
GO

--GRANT EXECUTE ON dbo.sproc_PetTypeUpdate TO sprocEditUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve specific PetType from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocPetTypeGet
@PetTypeID int
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM PetTypes
     WHERE PetTypeID = @PetTypeID
END
GO

--GRANT EXECUTE ON dbo.sprocPetTypeGet TO sprocReadUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve all PetTypes from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocPetTypesGetAll
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM PetTypes
END
GO

--GRANT EXECUTE ON dbo.sprocPetTypesGetAll TO sprocReadUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Remove specific PetType from the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_PetTypeRemove
@PetTypeID int
AS
BEGIN
     DELETE FROM PetTypes
          WHERE PetTypeID = @PetTypeID

     -- Return -1 if we had an error
     IF @@ERROR > 0
     BEGIN
          RETURN -1
     END
     ELSE
     BEGIN
          RETURN 1
     END
END
GO

--GRANT EXECUTE ON dbo.sproc_PetTypeRemove TO sprocEditUser
GO
-------------------SuperPet-----------------
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Add a new  SuperPet to the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_SuperPetAdd
@SuperPetID int OUTPUT,
@Name nvarchar(30),
@PetTypeID int,
@SuperHeroID int
AS
     INSERT INTO SuperPets(Name,PetTypeID,SuperHeroID)
               VALUES(@Name,@PetTypeID,@SuperHeroID)
     SET @SuperPetID = @@IDENTITY
GO

--GRANT EXECUTE ON dbo.sproc_SuperPetAdd TO sprocEditUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Update SuperPet in the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_SuperPetUpdate
@SuperPetID int,
@Name nvarchar(30),
@PetTypeID int,
@SuperHeroID int
AS
     UPDATE SuperPets
          SET
               Name = @Name,
               PetTypeID = @PetTypeID,
               SuperHeroID = @SuperHeroID
          WHERE SuperPetID = @SuperPetID
GO

--GRANT EXECUTE ON dbo.sproc_SuperPetUpdate TO sprocEditUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve specific SuperPet from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocSuperPetGet
@SuperPetID int
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM SuperPets
     WHERE SuperPetID = @SuperPetID
END
GO

--GRANT EXECUTE ON dbo.sprocSuperPetGet TO sprocReadUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve all SuperPets from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocSuperPetsGetAll
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM SuperPets
END
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Retrieve all SuperPets from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocSuperPetsGetForSuperHero
@SuperHeroID int
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM SuperPets
		WHERE SuperHeroID = @SuperHeroID
END
GO
--GRANT EXECUTE ON dbo.sprocSuperPetsGetAll TO sprocReadUser
GO
-- =============================================
-- Author:		COB\holmjona
-- Create date:	12 Nov 2019
-- Description:	Remove specific SuperPet from the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_SuperPetRemove
@SuperPetID int
AS
BEGIN
     DELETE FROM SuperPets
          WHERE SuperPetID = @SuperPetID

     -- Return -1 if we had an error
     IF @@ERROR > 0
     BEGIN
          RETURN -1
     END
     ELSE
     BEGIN
          RETURN 1
     END
END
GO

--GRANT EXECUTE ON dbo.sproc_SuperPetRemove TO sprocEditUser
GO


CREATE TABLE dbo.Citizens(
	CitizenID int IDENTITY(1,1) PRIMARY KEY
	,FirstName nvarchar(60) NOT NULL
	,LastName nvarchar(60) NOT NULL
	,DateOfBirth datetime NOT NULL
	,EyeColor tinyint NOT NULL
	,HeightInInches decimal(5,2) NOT NULL
)
GO

CREATE TABLE dbo.Costumes(
	CostumeID int IDENTITY(1,1) PRIMARY KEY
	,ColorMain int NOT NULL
	,ColorSecondary int NOT NULL
	,ColorTertiary int NOT NULL
	,HasCape bit NOT NULL
	,HasMask bit NOT NULL
)
GO

CREATE TABLE dbo.SuperHeroes(
	SuperHeroID int IDENTITY(1,1) PRIMARY KEY
	,FirstName nvarchar(60) NOT NULL
	,LastName nvarchar(60) NOT NULL
	,DateOfBirth datetime NOT NULL
	,EyeColor tinyint NOT NULL
	,HeightInInches decimal(6,2) NOT NULL
	,AlterEgoID int NOT NULL
	,SideKickID int NOT NULL 
		CONSTRAINT SuperHero_SideKick FOREIGN KEY
		REFERENCES SuperHeroes(SuperHeroID)
	,CostumeID int NOT NULL 
		CONSTRAINT SuperHero_Costume FOREIGN KEY
		REFERENCES Costumes(CostumeID)
)
GO

ALTER TABLE dbo.SuperHeroes
	NOCHECK CONSTRAINT SuperHero_SideKick 
GO

CREATE TABLE dbo.Villians(
	VillianID int IDENTITY(1,1) PRIMARY KEY
	,FirstName nvarchar(60) NOT NULL
	,LastName nvarchar(60) NOT NULL
	,DateOfBirth datetime NOT NULL
	,EyeColor tinyint NOT NULL
	,HeightInInches decimal(6,2) NOT NULL
	,SideKickID int NOT NULL 
		CONSTRAINT Villian_SideKick FOREIGN KEY
		REFERENCES Villians(VillianID)
	,CostumeID int 
		CONSTRAINT Villian_Costume FOREIGN KEY
		REFERENCES Costumes(CostumeID)
)
GO

ALTER TABLE dbo.Villians
	NOCHECK CONSTRAINT Villian_SideKick 
GO

CREATE TABLE dbo.Cities(
	CityID int IDENTITY(1,1) PRIMARY KEY
	,Name nvarchar(60) NOT NULL
)
GO

CREATE TABLE dbo.Hideouts(
	HideoutID int IDENTITY(1,1) PRIMARY KEY
	,Name nvarchar(60) NOT NULL
	,IsHeroBase bit NOT NULL
)
GO

CREATE TABLE dbo.Universes(
	UniverseID int IDENTITY(1,1) PRIMARY KEY
	,Name nvarchar(60) NOT NULL
)
GO

CREATE TABLE dbo.HideoutMembers(
	HideoutMemberID int IDENTITY(1,1) PRIMARY KEY
	,HideoutID int NOT NULL
		CONSTRAINT Member_Hideout FOREIGN KEY
		REFERENCES Hideouts(HideoutID)
	,MemberID int NOT NULL
)

GO

CREATE TABLE dbo.PetTypes(
	PetTypeID int IDENTITY(1,1) PRIMARY KEY
	,Name nvarchar(30) NOT NULL
)
GO

CREATE TABLE dbo.SuperPets(
	SuperPetID int IDENTITY(1,1) PRIMARY KEY
	,Name nvarchar(30) NOT NULL
	,PetTypeID int REFERENCES PetTypes(PetTypeID)
	,SuperHeroID int REFERENCES SuperHeroes(SuperHeroID)
)
GO

SET IDENTITY_INSERT dbo.Costumes ON
INSERT INTO Costumes (CostumeID,ColorMain,ColorSecondary,ColorTertiary,HasCape,HasMask)
			VALUES	(1,255,16711680,16776960,1,0) -- Blue, Red, Yellow
					,(2,0,335543,11184810,1,1) -- Black, Grey, Silver
					,(3,16711680,16711680,16776960,0,1) -- Red, Red, Yellow
					,(4,16711680,255,16776960,0,0) -- Red, Blue, Yellow
SET IDENTITY_INSERT dbo.Costumes OFF


SET IDENTITY_INSERT dbo.SuperHeroes ON

INSERT INTO SuperHeroes 
			(SuperHeroID,FirstName,LastName,DateOfBirth,EyeColor,HeightInInches,AlterEgoID,SideKickID,CostumeID)
			VALUES (1,'Super','Man','12/24/1955',0,73,-1,-1,1)
				,(2,'Bat','Man','4/5/1960',0,69,-1,-1,2)
				,(3,'The','Flash','4/5/1980',0,65,-1,-1,3)
				,(4,'Wonder','Woman','4/5/1960',0,68,-1,-1,4)
SET IDENTITY_INSERT dbo.SuperHeroes OFF


SET IDENTITY_INSERT dbo.PetTypes ON
INSERT INTO PetTypes (PetTypeID, Name)
			VALUES (1,'Dog'),(2,'Cat'),(3,'Squirrel')
SET IDENTITY_INSERT dbo.PetTypes OFF

SET IDENTITY_INSERT dbo.SuperPets ON
INSERT INTO SuperPets (SuperPetID,Name,PetTypeID,SuperHeroID)
			VALUES (1,'Fluffy',2,1)
			,(2,'Ruff',1,2)
			,(3,'Arco',3,3)
			,(4,'Larry',2,3)
			,(5,'Mildred',3,1)
			,(6,'Mighty',2,2)
SET IDENTITY_INSERT dbo.SuperPets OFF
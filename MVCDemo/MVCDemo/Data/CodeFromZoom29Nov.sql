--EXEC sprocSuperHeroGet 1

SELECT * FROM SuperPets p 
	JOIN SuperHeroToPets sp ON sp.SuperPetID = p.SuperPetID
WHERE p.SuperHeroID = 1


CREATE TABLE SuperHeroToPets (
	SuperHeroPetID int IDENTITY(1,1) PRIMARY KEY
	,SuperHeroID int NOT NULL
	,SuperPetID int NOT NULL

	)

INSERT INTO SuperHeroToPets (SuperHeroID,SuperPetID) VALUES 
(1,	1),
(2,	2),
(3,	3),
(3,	4),
(1,	5),
(2,	6)


CREATE TABLE Roles(
	RoleID int IDENTITY(1,1) PRIMARY KEY
	,Name nvarchar(53) NOT NULL
	,SuperHeroAdd bit NOT NULL
    ,SuperHeroEdit bit NOT NULL
    ,SuperHeroDelete bit NOT NULL
    ,SuperPetAdd bit NOT NULL
    ,SuperPetEdit bit NOT NULL
    ,SuperPetDelete bit NOT NULL
)

SET INSERT_IDENTITY ON
INSERT INTO Roles 
		(RoleID	,Name	
		,SuperHeroAdd    ,SuperHeroEdit    ,SuperHeroDelete
		,SuperPetAdd     ,SuperPetEdit     ,SuperPetDelete)
	VALUES	(1, 'Anonymous',0,0,0,0,0,0)
	,(2, 'Data Entry',1,0,0,1,0,0)
	,(3, 'Admin',1,1,1,1,1,1)
	,(4, 'Power User',1,1,0,1,1,0)
	
SET INSERT_IDENTITY OFF
	
	INSERT INTO USErs(Name,ROleID,Salt,Password)
	VALUES ("bob",3,'NU7OqacPNlFCzwfgytskAg==','HbfdqFOoL2qXmOs2QVcC0DwyAHk4GAGzzWlhR/BilPo=')
	,("Smidt",2)
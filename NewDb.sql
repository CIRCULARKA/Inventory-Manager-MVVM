USE NewDb;

CREATE TABLE DeviceType
(
	ID INT PRIMARY KEY IDENTITY,
	Name VARCHAR(max)
);

CREATE TABLE Housing
(
	ID INT PRIMARY KEY IDENTITY,
	[Name] TEXT NOT NULL
);

CREATE TABLE Cabinet
(
	ID INTEGER PRIMARY KEY IDENTITY,
	HousingID INT NOT NULL,
	[Name] VARCHAR(max) NOT NULL,
	FOREIGN KEY (HousingID) REFERENCES Housing (ID)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	UNIQUE (HousingID, [Name])
);

CREATE TABLE [Location]
(
	ID INT PRIMARY KEY IDENTITY,
	HousingID INT REFERENCES Housing (ID),
	CabinetID INT REFERENCES Cabinet (ID),
	UNIQUE (HousingID, CabinetID)
);

CREATE TABLE Device
(
	ID INT IDENTITY PRIMARY KEY,
	InventoryNumber VARCHAR(max) NOT NULL UNIQUE,
	TypeID INT NOT NULL,
	NetworkName VARCHAR(max) NOT NULL,
	LocationID INT NOT NULL REFERENCES [Location] (ID),
	FOREIGN KEY (TypeID) REFERENCES DeviceType (ID)
		ON UPDATE CASCADE
		ON DELETE CASCADE
);

CREATE TABLE UserGroup
(
	ID INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(max) UNIQUE
);

CREATE TABLE [User]
(
	ID INT PRIMARY KEY IDENTITY,
	LastName NVARCHAR(max) NOT NULL,
	FirstName NVARCHAR(max) NOT NULL,
	MiddleName NVARCHAR(max) NOT NULL,
	[Login] VARCHAR(max) NOT NULL UNIQUE,
	[Password] VARCHAR(max) NOT NULL,
	UserGroupID INT NOT NULL,
	FOREIGN KEY (UserGroupID) REFERENCES [UserGroup] (ID)
);

CREATE TABLE DeviceAccount
(
	ID INT PRIMARY KEY IDENTITY,
	[Login] VARCHAR(max) UNIQUE,
	[Password] VARCHAR(max),
);
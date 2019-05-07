use master

if db_id(N'ČeličniProfili') is NULL CREATE DATABASE ČeličniProfili;

IF DB_ID(N'ČeličniProfili') IS NOT NULL DROP DATABASE ČeličniProfili;

use ČeličniProfili

---------
--profil
---------
CREATE TABLE profil (
	ID					int		PRIMARY key,
	ID_monoblok	int		NOT Null,
	ID_ojačanje int		NOT null,
)

-----------
--Monoblok
-----------
CREATE TABLE Monoblok (
	ID					INT						PRIMARY key,
	ID_standard	INT						NOT Null,
	Naziv				NVARCHAR(30)	NOT NULL,
	ID_tip			int						NOT null,
)

ALTER TABLE profil ADD FOREIGN KEY (ID_monoblok) REFERENCES Monoblok (ID)

SELECT * FROM Monoblok

-------------
--tip profila
-------------
CREATE TABLE [tip profila] (
	ID_tip			INT						PRIMARY key,
	tehnologija	NVARCHAR(30)	NOT null,
	oznaka			NVARCHAR(10)	NOT NULL
)

ALTER TABLE Monoblok ADD FOREIGN KEY (ID_tip) REFERENCES [tip profila] (ID_tip)

------------
--standard
-------------
CREATE TABLE standard (
	Id_standard	int						PRIMARY key,
	Naziv				NVARCHAR(10)	NOT NULL
)

ALTER TABLE Monoblok ADD FOREIGN KEY (Id_standard) REFERENCES standard (Id_standard)

ALTER TABLE standard ADD šifra NVARCHAR(20) NOT  NULL

SELECT * FROM standard

---------------
--I_geometrija
---------------
CREATE TABLE [I-geometrija](
	ID	int		PRIMARY key,
	b		FLOAT NOT null,
	h		FLOAT NOT null,
	s		FLOAT NOT NULL,
	t		FLOAT NOT NULL,
	r1  float					,
	r2	float
)

ALTER TABLE Monoblok ADD FOREIGN KEY (ID) REFERENCES [I-geometrija] (ID)

-------------------
-- I-karakteristike
-------------------
CREATE TABLE [I-karakteristike](
	ID			INT		PRIMARY KEY,
	A				FLOAT	NOT NULL,
	G				FLOAT	NOT NULL,
	Ix			FLOAT	NOT NULL,
	Wx			FLOAT	NOT NULL,
	ix_jez	FLOAT	NOT NULL,
	Iy			FLOAT	NOT NULL,
	Wy			FLOAT	NOT NULL,
	iy_jez	FLOAT	NOT NULL,
	Sx			FLOAT	NOT NULL,
	s_x			FLOAT	NOT NULL,
	I_tor		FLOAT	NOT NULL
)

ALTER TABLE Monoblok ADD FOREIGN KEY (ID) REFERENCES [I-karakteristike] (ID)

-----------------------------
-- monoblok_pozicije_ojačanja
-----------------------------
CREATE TABLE monoblok_pozicije_ojačanja (
	ID_mono			INT						NOT null,
	X_pos				float					NOT NULL,
	Y_pos				FLOAT					NOT null,
	br_segment  int						NOT null,
	pravac			NVARCHAR(10)	NOT null
)

ALTER TABLE monoblok_pozicije_ojačanja ADD index_pr INT IDENTITY (1,1);
ALTER TABLE monoblok_pozicije_ojačanja ADD PRIMARY KEY (index_pr)

SELECT * FROM monoblok_pozicije_ojačanja

-----------------
-- monoblok_opis
-----------------
CREATE TABLE mon_opis (
	ID_mono		INT		NOT null,
	No_node		int NOT null,
	Node_x		float	NOT null,
	Node_y		float	NOT NULL
)

ALTER TABLE mon_opis ADD index_pr INT IDENTITY (1,1);
ALTER TABLE mon_opis ADD PRIMARY KEY (index_pr);

SELECT * FROM mon_opis;

----------------
-- ojačanje_opis
----------------
CREATE TABLE [ojačanje_opis] (
	index_pr	INT		PRIMARY KEY IDENTITY (1,1),
	Id_ojač		INT		NOT NULL,
	b					FLOAT NOT null,
	h					FLOAT NOT null,
	pol_x			FLOAT NOT null,
	pol_y			FLOAT NOT null
)

SELECT * FROM [ojačanje_opis];

--------------------------
-- profil_I_karakteristike
--------------------------
CREATE TABLE [Profil_I-karakteristike](
	ID			INT		PRIMARY KEY,
	A				FLOAT	NOT NULL,
	G				FLOAT	NOT NULL,
	Ix			FLOAT	NOT NULL,
	Wx			FLOAT	NOT NULL,
	ix_jez	FLOAT	NOT NULL,
	Iy			FLOAT	NOT NULL,
	Wy			FLOAT	NOT NULL,
	iy_jez	FLOAT	NOT NULL,
	Sx			FLOAT	NOT NULL,
	s_x			FLOAT	NOT NULL,
	I_tor		FLOAT	NOT NULL
)

ALTER TABLE profil ADD Foreign KEY (ID) REFERENCES [Profil_I-karakteristike] (ID);


ALTER TABLE profil ADD pos_x FLOAT, pos_y FLOAT

----------
-- users
----------
create TABLE Users (
	id				INT							PRIMARY KEY IDENTITY(1,1),
	Name			NVARCHAR(20)		NOT null,
	pass			NVARCHAR(30)		NOT null,
	level			smallint				NOT null,
	email			NVARCHAR (320)	NOT null
)

CREATE TABLE UserActivation (
	UserId					INT								PRIMARY KEY CLUSTERED,
	ActivationCode	uniqueidentifier	NOT NULL)


INSERT INTO mon_opis (ID_mono, No_node, Node_x,	Node_y) VALUES
	(1, 1, -21, -40), (1, 2, -21, -38.29), (1, 3, -20.49, -36.85),
	(1, 4, -19.2, -36.05), (1, 5, -5, -32.87), (1, 6, -2.81, -31.5), (1, 7, -1.95, -29.06);

INSERT INTO mon_opis (ID_mono, No_node, Node_x,	Node_y) VALUES
	(2, 1, -25, -50), (2, 2, -25, -48.16),
	(2, 3, -24.41, -46.47), (2, 4, -22.89, -45.52),
	(2, 5, -5.3, -41.59), (2, 6, -3.11, -40.22),
	(2, 7, -2.25, -37.78);

SELECT * FROM mon_opis


INSERT INTO [monoblok_pozicije_ojačanja] (ID_mono, X_pos, Y_pos, br_segment,	pravac) VALUES
	(1, 0, -40, 1, "dole"),
	(1, 0, 40, 17, "gore"),
	(2, 0, -50, 1, "dole"),
	(2, 0, -50, 17, "dole");

UPDATE [monoblok_pozicije_ojačanja] SET pravac = "gore" WHERE ID_mono =2 AND br_segment= 17

SELECT * FROM [monoblok_pozicije_ojačanja];

INSERT INTO [tip profila] VALUES
(1, "toplo-valjani", "I");

INSERT INTO standard VALUES
(1, "JUS", "C.B3.131");

INSERT INTO [I-geometrija] VALUES
(1, 42, 80, 3.9, 5.9, 3.9, 2.3),
(2, 50, 100, 4.5, 6.8, 4.5, 2.7);

INSERT INTO [I-karakteristike] VALUES
(1, 7.57, 5.94, 77.8, 19.5, 3.2, 6.92, 3, 0.91, 11.4, 6.84, 0.869),
(2, 10.6, 8.34, 170, 34.2, 4.01, 12.2, 4.88, 1.07, 19.9, 8.57, 1.6);

SELECT * FROM Users

SELECT * FROM UserActivation

INSERT INTO Monoblok VALUES
(1, 1, "I-80", 1),
(2, 1, "I-100", 1);

insert INTO Users (Name, pass, level, email) VALUES  ("fradd", "lockonlatch", 0, "dragg_s@ymail.com")


DELETE FROM Users WHERE id = 18

DELETE FROM UserActivation WHERE Userid = 18
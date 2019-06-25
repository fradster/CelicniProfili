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
	pos_x				FLOAT					,
	pos_y				FLOAT
)

-----------
--Monoblok
-----------
CREATE TABLE Monoblok (
	ID					INT						PRIMARY key,
	ID_standard	INT						NOT Null,
	Naziv				NVARCHAR(30)	NOT NULL,
	ID_tip			int						NOT NULL,
	id_tehn			INT						NOT Null
)

SELECT * FROM [tehnologija monoblok]

-----------------------
--tehnologija monoblok
-----------------------
CREATE TABLE [tehnologija monoblok] (
	ID_tehn			INT						PRIMARY key,
	tehnologija	NVARCHAR(30)	NOT null,
)

---------------
--tip monoblok
--------------
create TABLE [dbo].[tip monoblok] (
	id_tip	INT						PRIMARY KEY,
	tip			NVARCHAR(30)	NOT NULL,
	id_tehn INT						NOT NULL
)

------------
--standard
-------------
CREATE TABLE standard (
	Id_standard	int						PRIMARY key,
	Naziv				NVARCHAR(10)	NOT NULL,
	šifra				NVARCHAR(20)	NOT  NULL
)

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

-----------------
-- UserActivation
-----------------
CREATE TABLE UserActivation (
	UserId					INT								PRIMARY KEY CLUSTERED,
	ActivationCode	uniqueidentifier	NOT NULL,
)

CREATE TABLE [Admin SMTP parameteres] (
	Ind					int						PRIMARY KEY IDENTITY(1,1),
	UserName		NVARCHAR(50)	NOT NULL,
	password		NVARCHAR(30)	NOT NULL,
	host				NVARCHAR(30)	NOT NULL
)


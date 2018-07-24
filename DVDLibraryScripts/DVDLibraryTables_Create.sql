-- Tables
-- First Drop if present

USE DVDLibrary
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='DVD')
	DROP TABLE DVD
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Rating')
	DROP TABLE Rating
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Director')
	DROP TABLE Director
GO

CREATE TABLE Director (
	DirectorId int identity(1,1) primary key not null,
	[Name] varchar(100) not null 
)

CREATE TABLE Rating (
	RatingId int identity(1,1) primary key not null,
	[Name] varchar(10) not null
)

CREATE TABLE DVD (
	DVDId int identity(1,1) primary key not null,
	Title varchar(150) not null,
	ReleaseYear int not null,
	DirectorId int foreign key references Director(DirectorId) not null,
	RatingId int foreign key references Rating(RatingId) not null,
	Notes varchar(150) 
)
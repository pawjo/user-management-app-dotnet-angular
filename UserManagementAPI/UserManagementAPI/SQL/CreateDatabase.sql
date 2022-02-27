CREATE TABLE [dbo].[User]
(
	[Id] int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Email] varchar(50) NOT NULL,
	[Name] nvarchar(30) NOT NULL,
	[Surname] nvarchar(30) NOT NULL,
	[Age] int NOT NULL,
	[ImageName] varchar(40) NULL
)
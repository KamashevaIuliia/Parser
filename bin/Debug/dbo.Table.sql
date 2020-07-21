CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(MAX) NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Source] NVARCHAR(MAX) NULL, 
    [Obyect] NVARCHAR(MAX) NULL, 
    [Konf] CHAR(10) NULL, 
    [Cel] CHAR(10) NULL, 
    [Dostup] CHAR(10) NULL, 
    [On] DATETIME NULL, 
    [Change] DATETIME NULL
)

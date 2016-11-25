IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL IDENTITY(1,1) Primary Key, 
    [Name] VARCHAR(50) NOT NULL Unique,
	[Sku] VARCHAR(50) NOT NULL Unique,
	[Description] VARCHAR(250),
	[Price] INT NOT NULL,
	[CreateTime] DATETIME NOT NULL Default GETDATE()
)
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductReview]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductReview]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [ProductName] VARCHAR(50) NOT NULL,
	[UserName] VARCHAR(10) NOT NULL,
	[Score] INT NOT NULL
	check(Score >= 0 and Score <= 5),
	[Comment] varchar(max),
	[CreateTime] DATETIME NOT NULL Default GETDATE()
)
END
GO
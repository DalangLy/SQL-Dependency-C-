# SQL-Dependency-C-
this use to get notification from sql server when data change

# Introduction
- enable broker in sql server "ALTER DATABASE DataName SET ENABLE_BROKER;"
- create customer table "CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Surname] [nvarchar](50) NULL
) ON [PRIMARY]
GO"

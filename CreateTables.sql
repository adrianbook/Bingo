use master

IF NOT EXISTS (
        SELECT *
        FROM sys.databases
        WHERE name = 'BingoAdministration'
        )
BEGIN
    CREATE DATABASE [BingoAdministration]
END
GO

use [BingoAdministration]

create table [dbo].[PasswordHash](
	[Id] [uniqueidentifier] default NEWID(),
	[Hash] [nvarchar](256) NOT NULL,
	constraint [PK_PasswordHash] primary key ([Id]),
	constraint [Unique_PasswordHash_Hash] UNIQUE ([Hash],[Id])
)

create table [dbo].[BingoUser](
	[Id] [uniqueidentifier] default NEWID(),
	[FirstName] [nvarchar](64),
	[LastName] [nvarchar](64),
	[Email] [nvarchar](64) NOT NULL,
	[PasswordHash] [nvarchar](256) NOT NULL,
	constraint [PK_BingoUser] primary key (Id),
)

create table [dbo].[OldPassword](
	[BingoUserId] [uniqueidentifier] NOT NULL,
	[PasswordHash] [nvarchar](256) NOT NULL,
	constraint [PK_OldPasswords] primary key (PasswordHash, BingoUserId)
)

create table [dbo].[Role](
	[Id] [uniqueidentifier] default NEWID(),
	[Label] [nvarchar](16),
	constraint [PK_Role] primary key (Id)
)

create table [dbo].[BingoUserRole](
	[BingoUserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	constraint [FK_BingoUserRole_BingoUser] foreign key (BingoUserId)
	references [BingoUser](Id),
	constraint [FK_BingoUserRole_Role] foreign key (RoleId)
	references [Role](Id),
	constraint [PK_BingoUserRole] primary key (BingoUserId, RoleId)
)


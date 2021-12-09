USE master
GO
IF DB_ID('ZupreeDB') is not null
DROP database ZupreeDB
GO
CREATE DATABASE ZupreeDB
GO
USE ZupreeDB
CREATE TABLE [dbo].[Suppliers] (
    [SupplierId]  INT           IDENTITY (1, 1) NOT NULL,
    [CompanyName] VARCHAR (50)  NOT NULL,
    [ContactName] VARCHAR (50)  NOT NULL,
    [Address]     VARCHAR (100) NOT NULL,
    [Phone]       VARCHAR (20)  NOT NULL,
    [Email]       VARCHAR (50)  NOT NULL,
    [ImageName]   VARCHAR (100) NULL,
    [ImageUrl]    VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([SupplierId] ASC)
);
GO
CREATE TABLE [dbo].[Category] (
    [CategoryId]   INT          IDENTITY (1, 1) NOT NULL,
    [CategoryName] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([CategoryId] ASC)
);
GO
CREATE TABLE [dbo].[Products] (
    [ProductId]    INT             IDENTITY (1, 1) NOT NULL,
    [ProductName]  VARCHAR (50)    NOT NULL,
    [PurchaseDate] DATETIME        NOT NULL,
    [SupplierId]   INT             NOT NULL,
    [CategoryId]   INT             NOT NULL,
    [Quantity]     INT             NOT NULL,
    [UnitPrice]    DECIMAL (18, 2) NOT NULL,
    [MSRP]         DECIMAL (18, 2) NOT NULL,
    [ImageName]    VARCHAR (100)   NULL,
    [ImageUrl]     VARCHAR (MAX)   NULL,
    PRIMARY KEY CLUSTERED ([ProductId] ASC),
    CONSTRAINT [FK_Products_ToTable] FOREIGN KEY ([SupplierId]) REFERENCES [dbo].[Suppliers] ([SupplierId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Products_ToTable_1] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([CategoryId]) ON DELETE CASCADE ON UPDATE CASCADE
);
GO
CREATE TABLE [dbo].[Payment] (
    [PaymentId]   INT          IDENTITY (1, 1) NOT NULL,
    [PaymentType] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([PaymentId] ASC)
);
GO
CREATE TABLE [dbo].[Sale] (
    [SalesNumber]   INT             IDENTITY (1, 1) NOT NULL,
    [CustomerName]  VARCHAR (50)    NOT NULL,
    [CustomerPhone] VARCHAR (50)    NOT NULL,
    [CustomerEmail] VARCHAR (50)    NOT NULL,
    [ProductId]     INT             NOT NULL,
    [PaymentId]     INT             NOT NULL,
    [Quantity]      INT             NOT NULL,
    [SalesUnitPrice]     DECIMAL (18, 4) NOT NULL,
    PRIMARY KEY CLUSTERED ([SalesNumber] ASC),
    CONSTRAINT [FK_Orders_ToTable_2] FOREIGN KEY ([PaymentId]) REFERENCES [dbo].[Payment] ([PaymentId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Orders_ToTable] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([ProductId]) ON DELETE CASCADE ON UPDATE CASCADE
);
GO
CREATE TABLE [dbo].[tblRole]
(
	[RoleId] INT             IDENTITY (1, 1) NOT NULL, 
    [RoleName] VARCHAR(50) NULL
	PRIMARY KEY CLUSTERED ([RoleId] ASC),
	
)
GO
CREATE TABLE [dbo].[tblUser] (
    [UserId]    INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] VARCHAR (30)  NOT NULL,
    [LastName]  VARCHAR (30)  NOT NULL,
    [UserName]  VARCHAR (50)  NOT NULL,
    [Phone]     VARCHAR (20)  NOT NULL,
    [Email]     VARCHAR (50)  NOT NULL,
    [Password]  VARCHAR (MAX) NOT NULL,
    [ImageName] VARCHAR (100) NULL,
    [ImageUrl]  VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);
GO
CREATE TABLE [dbo].[UserWiseRole]
(
	[uwrId] INT             IDENTITY (1, 1) NOT NULL, 
    [RoleId] INT  NOT NULL, 
    [UserId] INT  NOT NULL, 
    PRIMARY KEY CLUSTERED ([uwrId] ASC),
    CONSTRAINT [FK_UserWiseRole_ToTable] FOREIGN KEY ([RoleId]) REFERENCES [tblRole]([RoleId]), 
    CONSTRAINT [FK_UserWiseRole_ToTable_1] FOREIGN KEY ([UserId]) REFERENCES [tblUser]([UserId])
)
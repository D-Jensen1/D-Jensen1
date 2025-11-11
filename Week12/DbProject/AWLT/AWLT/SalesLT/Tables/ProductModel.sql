CREATE TABLE [SalesLT].[ProductModel] (
    [ProductModelID]     INT              IDENTITY (1, 1) NOT NULL,
    [Name]               [dbo].[Name]     NOT NULL,
    [CatalogDescription] XML              NULL,
    [rowguid]            UNIQUEIDENTIFIER NOT NULL,
    [ModifiedDate]       DATETIME         NOT NULL
);
GO

ALTER TABLE [SalesLT].[ProductModel]
    ADD CONSTRAINT [DF_ProductModel_rowguid] DEFAULT (newid()) FOR [rowguid];
GO

ALTER TABLE [SalesLT].[ProductModel]
    ADD CONSTRAINT [DF_ProductModel_ModifiedDate] DEFAULT (getdate()) FOR [ModifiedDate];
GO

ALTER TABLE [SalesLT].[ProductModel]
    ADD CONSTRAINT [AK_ProductModel_Name] UNIQUE NONCLUSTERED ([Name] ASC);
GO

ALTER TABLE [SalesLT].[ProductModel]
    ADD CONSTRAINT [AK_ProductModel_rowguid] UNIQUE NONCLUSTERED ([rowguid] ASC);
GO

ALTER TABLE [SalesLT].[ProductModel]
    ADD CONSTRAINT [PK_ProductModel_ProductModelID] PRIMARY KEY CLUSTERED ([ProductModelID] ASC);
GO


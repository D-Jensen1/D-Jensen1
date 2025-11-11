CREATE TABLE [SalesLT].[CustomerAddress] (
    [CustomerID]   INT              NOT NULL,
    [AddressID]    INT              NOT NULL,
    [AddressType]  [dbo].[Name]     NOT NULL,
    [rowguid]      UNIQUEIDENTIFIER NOT NULL,
    [ModifiedDate] DATETIME         NOT NULL
);
GO

ALTER TABLE [SalesLT].[CustomerAddress]
    ADD CONSTRAINT [FK_CustomerAddress_Customer_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [SalesLT].[Customer] ([CustomerID]);
GO

ALTER TABLE [SalesLT].[CustomerAddress]
    ADD CONSTRAINT [FK_CustomerAddress_Address_AddressID] FOREIGN KEY ([AddressID]) REFERENCES [SalesLT].[Address] ([AddressID]);
GO

ALTER TABLE [SalesLT].[CustomerAddress]
    ADD CONSTRAINT [AK_CustomerAddress_rowguid] UNIQUE NONCLUSTERED ([rowguid] ASC);
GO

ALTER TABLE [SalesLT].[CustomerAddress]
    ADD CONSTRAINT [PK_CustomerAddress_CustomerID_AddressID] PRIMARY KEY CLUSTERED ([CustomerID] ASC, [AddressID] ASC);
GO

ALTER TABLE [SalesLT].[CustomerAddress]
    ADD CONSTRAINT [DF_CustomerAddress_ModifiedDate] DEFAULT (getdate()) FOR [ModifiedDate];
GO

ALTER TABLE [SalesLT].[CustomerAddress]
    ADD CONSTRAINT [DF_CustomerAddress_rowguid] DEFAULT (newid()) FOR [rowguid];
GO


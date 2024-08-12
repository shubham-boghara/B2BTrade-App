CREATE TABLE [dbo].[Products] (
    [ProductID]   INT             IDENTITY (1, 1) NOT NULL,
    [ProductName] NVARCHAR (255)  NULL,
    [Description] NVARCHAR (MAX)  NULL,
    [Price]       DECIMAL (18, 2) NULL,
    [Quantity]    INT             NULL,
    [CategoryID]  INT             NULL,
    [SellerID]    INT             NULL,
    [CreatedAt]   DATETIME        DEFAULT (getdate()) NULL,
    [UpdatedAt]   DATETIME        DEFAULT (getdate()) NULL,
    [CreatedBy]   NVARCHAR (450)  NULL,
    [UpdatedBy]   NVARCHAR (450)  NULL,
    PRIMARY KEY CLUSTERED ([ProductID] ASC),
    FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[ProductCategories] ([CategoryID])
);


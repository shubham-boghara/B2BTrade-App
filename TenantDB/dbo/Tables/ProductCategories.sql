CREATE TABLE [dbo].[ProductCategories] (
    [CategoryID]       INT            IDENTITY (1, 1) NOT NULL,
    [CategoryName]     NVARCHAR (255) NULL,
    [ParentCategoryID] INT            NULL,
    [CreatedAt]        DATETIME       DEFAULT (getdate()) NULL,
    [UpdatedAt]        DATETIME       DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([CategoryID] ASC),
    FOREIGN KEY ([ParentCategoryID]) REFERENCES [dbo].[ProductCategories] ([CategoryID])
);


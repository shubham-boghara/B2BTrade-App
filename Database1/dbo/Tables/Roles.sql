CREATE TABLE [dbo].[Roles] (
    [RoleID]   INT           IDENTITY (1, 1) NOT NULL,
    [TenantID] INT           NULL,
    [RoleName] NVARCHAR (50) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([RoleID] ASC)
);


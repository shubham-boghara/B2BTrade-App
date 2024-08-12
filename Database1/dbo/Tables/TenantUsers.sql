CREATE TABLE [dbo].[TenantUsers] (
    [PkID]            INT            IDENTITY (1, 1) NOT NULL,
    [TenantID]        INT            NULL,
    [AspUserID]       NVARCHAR (450) NULL,
    [AspRoleID]       NVARCHAR (50)  NULL,
    [CreatedAt]       DATETIME       DEFAULT (getdate()) NULL,
    [UpdatedAt]       DATETIME       DEFAULT (getdate()) NULL,
    [RoleID]          INT            NULL,
    [TenantUserName ] NVARCHAR (50)  NULL,
    CONSTRAINT [PK_TenantUsers_PkID] PRIMARY KEY CLUSTERED ([PkID] ASC),
    FOREIGN KEY ([TenantID]) REFERENCES [dbo].[Tenants] ([TenantID])
);


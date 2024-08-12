CREATE TABLE [dbo].[Tenants] (
    [TenantID]    INT            IDENTITY (1, 1) NOT NULL,
    [TenantName]  NVARCHAR (100) NULL,
    [Subdomain]   NVARCHAR (100) NULL,
    [CreatedAt]   DATETIME       DEFAULT (getdate()) NULL,
    [UpdatedAt]   DATETIME       DEFAULT (getdate()) NULL,
    [CompanyName] NVARCHAR (100) NULL,
    [TenantSeqID] INT            NULL,
    PRIMARY KEY CLUSTERED ([TenantID] ASC),
    UNIQUE NONCLUSTERED ([Subdomain] ASC)
);


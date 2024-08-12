CREATE TABLE [dbo].[DataAccessPermissions] (
    [AccessPermissionID] INT           IDENTITY (1, 1) NOT NULL,
    [AccessType]         NVARCHAR (50) NULL,
    CONSTRAINT [PK_DataAccessPermission] PRIMARY KEY CLUSTERED ([AccessPermissionID] ASC)
);


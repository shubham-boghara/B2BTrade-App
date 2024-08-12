CREATE TABLE [dbo].[Permissions] (
    [PermissionID]       INT IDENTITY (1, 1) NOT NULL,
    [RoleID]             INT NULL,
    [FormID]             INT NULL,
    [CanView]            BIT NULL,
    [CanEdit]            BIT NULL,
    [CanAdd]             BIT NULL,
    [CanDelete]          BIT NULL,
    [AccessPermissionID] INT NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED ([PermissionID] ASC)
);


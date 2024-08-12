CREATE TABLE [dbo].[Forms] (
    [FormID]   INT            IDENTITY (1, 1) NOT NULL,
    [FormName] NVARCHAR (50)  NULL,
    [FormUrl]  NVARCHAR (500) NULL,
    CONSTRAINT [PK_Forms] PRIMARY KEY CLUSTERED ([FormID] ASC)
);


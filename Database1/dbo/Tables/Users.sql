CREATE TABLE [dbo].[Users] (
    [UserID]      INT            IDENTITY (1, 1) NOT NULL,
    [Username]    NVARCHAR (255) NULL,
    [PhoneNumber] NVARCHAR (20)  NULL,
    [CreatedAt]   DATETIME       DEFAULT (getdate()) NULL,
    [UpdatedAt]   DATETIME       DEFAULT (getdate()) NULL,
    [AspUserID]   NVARCHAR (450) NULL,
    [FirstName]   NVARCHAR (256) NULL,
    [MiddleName]  NVARCHAR (256) NULL,
    [LastName]    NVARCHAR (256) NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Users_AspUserID_idx]
    ON [dbo].[Users]([AspUserID] ASC);


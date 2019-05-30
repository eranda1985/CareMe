CREATE TABLE [dbo].[UserData] (
    [Id]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [SecretKey] NVARCHAR (50) NULL,
    [UserName]  NVARCHAR (50) NULL,
    CONSTRAINT [PK_UserData] PRIMARY KEY CLUSTERED ([Id] ASC)
);


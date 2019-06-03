CREATE TABLE [dbo].[UserData] (
    [Id]        BIGINT        NOT NULL,
    [SecretKey] NVARCHAR (70) NULL,
    [UserName]  NVARCHAR (50) NULL,
    CONSTRAINT [PK_UserData] PRIMARY KEY CLUSTERED ([Id] ASC)
);




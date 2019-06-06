CREATE TABLE [dbo].[UserDetail] (
    [Id]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [Username]      NVARCHAR (50) NULL,
    [Password]      NVARCHAR (50) NULL,
    [Secret]        NVARCHAR (70) NULL,
    [DeviceType]    NVARCHAR (50) NULL,
    [LastLoginDate] DATETIME      NULL,
    CONSTRAINT [PK_UserDetail] PRIMARY KEY CLUSTERED ([Id] ASC)
);




CREATE TABLE [dbo].[UserProfile] (
    [Id]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [Username]          NVARCHAR (100) NOT NULL,
    [FirstName]         NVARCHAR (150) NULL,
    [LastName]          NVARCHAR (150) NULL,
    [MobilePhoneNumber] NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


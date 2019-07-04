CREATE TABLE [dbo].[AppVersion] (
    [Id]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [VersionHash]   NVARCHAR (70) NOT NULL,
    [VersionNumber] NVARCHAR (50) NULL,
    [Enabled]       BIT           NULL,
    [DeviceType]    NVARCHAR (50) NULL
);


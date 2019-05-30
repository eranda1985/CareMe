CREATE TABLE [dbo].[VehicleUsers] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [VehicleId] BIGINT         NOT NULL,
    [Username]  NVARCHAR (150) NOT NULL,
    [IsDefault] BIT            NOT NULL
);


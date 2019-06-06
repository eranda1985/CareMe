CREATE TABLE [dbo].[FuelData] (
    [Id]        BIGINT     IDENTITY (1, 1) NOT NULL,
    [VehicleId] BIGINT     NULL,
    [Date]      DATETIME   NULL,
    [Litres]    FLOAT (53) NULL,
    [Price]     FLOAT (53) NULL,
    [Mileage]   FLOAT (53) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


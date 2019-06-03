CREATE TABLE [dbo].[FuelConsumption] (
    [Id]        BIGINT     NOT NULL,
    [VehicleId] BIGINT     NULL,
    [Litres]    FLOAT (53) NULL,
    [Date]      DATETIME   NULL,
    [Amount]    FLOAT (53) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[VehicleData] (
    [Type]      NVARCHAR (50) NULL,
    [Brand]     NVARCHAR (50) NULL,
    [Model]     NVARCHAR (50) NULL,
    [FuelType]  NVARCHAR (50) NULL,
    [RegoPlate] NVARCHAR (50) NULL,
    [Date]      DATETIME      NULL,
    [ODOMeter]  FLOAT (53)    NULL,
    [Id]        BIGINT        IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_VehicleData] PRIMARY KEY CLUSTERED ([Id] ASC)
);


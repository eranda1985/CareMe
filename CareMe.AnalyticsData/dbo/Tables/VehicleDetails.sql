﻿CREATE TABLE [dbo].[VehicleDetails] (
    [Id]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [VehicleId]    BIGINT        NOT NULL,
    [Rego]         NVARCHAR (50) NULL,
    [LastOdometer] FLOAT (53)    NULL,
    [LastUpdated]  DATETIME      NULL,
    CONSTRAINT [PK_VehicleDetails] PRIMARY KEY CLUSTERED ([VehicleId] ASC)
);






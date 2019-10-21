CREATE TABLE [dbo].[Weather] (
[WeatherId]        INT    IDENTITY (1, 1) NOT NULL,
[Temperature] DECIMAL (4,1) NOT NULL,
[Rainfall]   DECIMAL(4,1) NOT NULL,
[SeaLevel]   DECIMAL (4,1) NOT NULL,
PRIMARY KEY CLUSTERED ([WeatherId] ASC)
);
GO
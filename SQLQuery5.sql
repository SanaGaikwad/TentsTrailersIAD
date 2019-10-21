CREATE TABLE [dbo].[Precipitate] (
[PrecipitateId]        INT            IDENTITY (1, 1) NOT NULL,
[MONTHS] nvarchar (max) NOT NULL,
[Precipitation]       DECIMAL(4,1) NOT NULL,
PRIMARY KEY CLUSTERED ([PrecipitateId] ASC)
);

CREATE TABLE [dbo].[PrecipitateMIN] (
[PrecipitateMINId]        INT            IDENTITY (1, 1) NOT NULL,
[MONTHS] nvarchar (max) NOT NULL,
[PrecipitationMin]       DECIMAL(4,1) NOT NULL,
PRIMARY KEY CLUSTERED ([PrecipitateMINId] ASC)
);

CREATE TABLE [dbo].[PrecipitateMAX] (
[PrecipitateMAXId]        INT            IDENTITY (1, 1) NOT NULL,
[MONTHS] nvarchar (max) NOT NULL,
[PrecipitationMAX]       DECIMAL(4,1) NOT NULL,
PRIMARY KEY CLUSTERED ([PrecipitateMAXId] ASC)
);
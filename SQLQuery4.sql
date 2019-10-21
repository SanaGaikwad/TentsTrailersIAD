CREATE TABLE [dbo].[Precipitate] (
[WeatherId]        INT            IDENTITY (1, 1) NOT NULL,
[Months] nvarchar (MAX) NOT NULL,
[Precipitation]       DECIMAL(4,1) NOT NULL,
[Min]   DECIMAL(4,1) NOT NULL,
[Max]   DECIMAL(4,1) NOT NULL,
PRIMARY KEY CLUSTERED ([WeatherId] ASC)
);

drop table [dbo].[Weather]
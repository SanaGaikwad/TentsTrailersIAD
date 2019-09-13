CREATE TABLE [dbo].[Member] (
[MemberId]        INT      IDENTITY (1, 1)      NOT NULL,
[FirstName] NVARCHAR (max)  NOT NULL,
[LastName]  NVARCHAR (max)  NOT NULL,
[ContactNo] NVARCHAR (max) NOT NULL,
[Email]     NVARCHAR (max) NOT NULL,
[UserId]    NVARCHAR (128) NOT NULL,
PRIMARY KEY CLUSTERED ([MemberId] ASC),
CONSTRAINT [FK_UserId_Member] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers]([Id]));
GO


CREATE TABLE [dbo].[Registration] (
[Id]        INT      IDENTITY (1, 1)      NOT NULL,
[MemberId] INT not null,
[BookingId] INT not null,
Primary Key([BookingId],[MemberId]),
CONSTRAINT FK_Member_Registration Foreign Key(MemberId) references Member(MemberId),
CONSTRAINT FK_Booking_Registration Foreign Key([BookingId]) references Booking([BookingId])
);
GO


CREATE TABLE [dbo].[Booking] (
[BookingId]        INT            IDENTITY (1, 1) NOT NULL,
[CampId]      INT NOT NULL,
[BookingDate]      DATE           NOT NULL,
[BookingStartDate] DATE       NOT NULL,
[BookingEnddate]   DATE        NOT NULL,
[BookingStatus] NVARCHAR (max)  NOT NULL,
PRIMARY KEY CLUSTERED ([BookingId] ASC),
CONSTRAINT FK_Booking_Campsite Foreign Key([CampId]) references Campsite([CampId])
);
GO

CREATE TABLE [dbo].[Campsite] (
[CampId]        INT            IDENTITY (1, 1) NOT NULL,
[Description]       NVARCHAR (MAX) NULL,
[Price]   INT NOT NULL,
[Type] NVARCHAR (MAX) NULL,
[Accomodates] INT NOT NULL,
[Location]  NVARCHAR (MAX) NOT NULL,
PRIMARY KEY CLUSTERED ([CampId] ASC)
);
GO

CREATE TABLE [dbo].[Rating] (
    [RatingId]  INT IDENTITY (1, 1) NOT NULL,
    [Comment] NVARCHAR (max)  NOT NULL,
    [Rating]  INT   NOT NULL,
    [UserId]    NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([RatingId] ASC),
    CONSTRAINT [FK_UserId_Rating] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);
GO


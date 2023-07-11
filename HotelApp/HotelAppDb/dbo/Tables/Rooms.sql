CREATE TABLE [dbo].[Rooms] (
    [Id]         INT          IDENTITY (1, 1) NOT NULL,
    [RoomNumber] VARCHAR (10) NOT NULL,
    [RoomTypeId] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Rooms_RoomTypes] FOREIGN KEY ([RoomTypeId]) REFERENCES [dbo].[RoomTypes] ([Id])
);


CREATE TABLE [dbo].[RoomTypes] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (50)   NOT NULL,
    [Description] NVARCHAR (2000) NOT NULL,
    [Price]       MONEY           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


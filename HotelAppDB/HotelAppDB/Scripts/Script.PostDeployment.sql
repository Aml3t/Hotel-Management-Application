/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

-- Generate sample data for Guests table
if not exists (select 1 from dbo.RoomTypes)
begin
    insert into dbo.RoomTypes(Title, Description)
    Values
        ('Junior Suite', 'The junior suite of the hotel'),
        ('Suite','The standar suite of the hotel'),
        ('Executive Suite','The best suite of the hotel');
end

if not exists (select 1 from dbo.Rooms)
begin
    declare @roomId1 int;
    declare @roomId2 int;
    declare @roomId3 int;

    select @roomId1 = Id from dbo.RoomTypes where Title = 'Junior Suite';
    select @roomId2 = Id from dbo.RoomTypes where Title = 'Suite';
    select @roomId3 = Id from dbo.RoomTypes where Title = 'Executive Suite';

    insert into dbo.Rooms(RoomNumber, RoomTypeId)
    values
    ('101',@roomId1),
    ('102',@roomId1),
    ('103',@roomId1),
    ('201',@roomId2),
    ('202',@roomId2),
    ('301',@roomId3);
end
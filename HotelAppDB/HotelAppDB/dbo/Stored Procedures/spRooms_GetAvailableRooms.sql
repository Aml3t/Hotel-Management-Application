﻿CREATE PROCEDURE [dbo].[spRooms_GetAvailableRooms]
	@startDate date,
	@endDate date,
	@roomTypeId int
AS
begin
	set nocount on;


	select r.*
	from dbo.Rooms r
	inner join dbo.RoomTypes t on t.id = r.RoomTypeId
	where r.RoomTypeId = @roomTypeId
	and	r.id not in(
	select b.RoomId
	from dbo.Bookings b
	where (@startDate < b.StartDate and @endDate > b.EndDate)
	or (b.StartDate <= @endDate and @endDate < b.EndDate)
	or (b.StartDate <= @startDate and @startDate < b.EndDate)
	);

end

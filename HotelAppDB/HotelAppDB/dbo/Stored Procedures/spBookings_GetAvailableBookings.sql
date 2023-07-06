CREATE PROCEDURE [dbo].[spBookings_GetAvailableBookings]
	 @startDate date,
	 @endDate date,
	 @roomTypeId int
AS
begin
	set nocount on;


	select b.*
	from dbo.Bookings b
	inner join dbo.Rooms r on r.RoomTypeId = b.RoomId
	where r.RoomTypeId = @roomTypeId
	and r.RoomTypeId not in(
	select b.RoomId
	from dbo.Bookings b
	where (@startDate < b.StartDate and @endDate > b.EndDate)
	or (b.StartDate <= @endDate and @endDate < b.EndDate)
	or (b.StartDate <= @startDate and @startDate < b.EndDate)
	);
end

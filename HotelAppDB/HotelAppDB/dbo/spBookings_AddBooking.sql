CREATE PROCEDURE [dbo].[spBookings_AddBooking]
	@roomId int,
	@guestId int,
	@startDate date,
	@endDate date,
	@checkIn bit,
	@totalCost money,
	@id int = 0 output

AS
begin
	set nocount on;
	insert into dbo.Bookings (RoomId, GuestId, StartDate, EndDate, CheckedIn, TotalCost)
	values (@roomId, @guestId, @startDate, @endDate, @checkIn, @totalCost)

	select @id = SCOPE_IDENTITY();
end


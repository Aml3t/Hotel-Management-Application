using HotelAppLibrary.Databases;
using HotelAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Text;
using Dapper;
using System.Linq;

namespace HotelAppLibrary.Data
{
    public class SqliteData : IDatabaseData
    {
        private const string connectionStringName = "SqliteDb";

        private readonly ISqliteDataAccess _db;

        public SqliteData(ISqliteDataAccess db)
        {
            _db = db;
        }
        public List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
        {
            string sql = @" select t.Id, t.Title, t.Description ,t.Price
	                    from Rooms r
	                    inner join RoomTypes t on t.Id = r.RoomTypeId
	                    where r.Id not in(
	                    select b.RoomId
	                    from Bookings b
	                    where (@startDate < b.StartDate and @endDate > b.EndDate)
	                       or (b.StartDate <= @endDate and @endDate < b.EndDate)
	                       or (b.StartDate <= @startDate and @startDate < b.EndDate)
	                    )
                        group by t.Id, t.Title, t.Description ,t.Price";


            var output = _db.LoadData<RoomTypeModel, dynamic>(sql,
                                                        new { startDate, endDate },
                                                        connectionStringName).AsList();

            output.ForEach(x => x.Price = x.Price / 100);
            return output;

        }
        public void BookGuest(string FirstName, string LastName, DateTime startDate, DateTime endDate, int roomTypeId)
        {
            string sqlGuestInsert = @" if not exists (select 1 from dbo.Guests where FirstName = @firstName and LastName = @lastName)
	                            begin
		                            insert into Guests(FirstName, LastName)
		                            values (@firstName, @lastName);
	                            end
	                              select top 1 [Id], [FirstName], [LastName] 
	                                from Guests
	                                where FirstName = @firstName and LastName = @lastName";


            GuestModel guest = _db.LoadData<GuestModel, dynamic>(sqlGuestInsert,
                                                     new { FirstName, LastName },
                                                     connectionStringName).FirstOrDefault();

            RoomTypeModel roomType = _db.LoadData<RoomTypeModel, dynamic>("select * from dbo.RoomTypes where Id = @Id",
                                                                          new { Id = roomTypeId },
                                                                          connectionStringName).First();

            TimeSpan timeStaying = endDate.Date.Subtract(startDate.Date);

            string sqlRoomsAvailable = @"   select r.*
	                                       from dbo.Rooms r
	                                       inner join RoomTypes t on t.Id = r.RoomTypeId
	                                       where r.RoomTypeId = @roomTypeId
	                                       and	r.Id not in(
	                                       select b.RoomId
	                                       from Bookings b
	                                       where (@startDate < b.StartDate and @endDate > b.EndDate)
	                                       or (b.StartDate <= @endDate and @endDate < b.EndDate)
	                                       or (b.StartDate <= @startDate and @startDate < b.EndDate)
	                                       )";

            List<RoomModel> availableRooms = _db.LoadData<RoomModel, dynamic>(sqlRoomsAvailable,
                                                                              new { startDate, endDate, roomTypeId },
                                                                              connectionStringName);

            _db.SaveData("dbo.spBookings_Insert",
                         new
                         {
                             roomId = availableRooms.First().Id,
                             guestId = guest.Id,
                             startDate = startDate,
                             endDate = endDate,
                             totalCost = timeStaying.Days * roomType.Price
                         },
                         connectionStringName);

        }

        public void CheckInGuest(int bookingId)
        {
            string sql = @"update Bookings
	                        set CheckedIn = 1
	                        where Id = @Id;";

            _db.SaveData(sql, new { Id = bookingId }, connectionStringName);
        }



        public RoomTypeModel GetRoomTypeById(int id)
        {
            string sql = @"select [Id], [Title], [Description], [Price]
	                        from RoomTypes
	                        where Id = @id;";

            return _db.LoadData<RoomTypeModel, dynamic>(sql,
                                            new { id },
                                            connectionStringName).FirstOrDefault();
        }

        public List<BookingFullModel> SearchBookings(string lastName)
        {
            throw new NotImplementedException();
        }
    }
}

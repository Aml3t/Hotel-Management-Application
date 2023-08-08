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
            string sqlInsert = @"select top 1 [Id], [FirstName], [LastName] 
	                            from dbo.Guests
	                            where FirstName = @firstName and LastName = @lastName";


            GuestModel guest = _db.LoadData<GuestModel, dynamic>(sqlInsert,
                                                     new { FirstName, LastName },
                                                     connectionStringName).FirstOrDefault();

            RoomTypeModel roomType = _db.LoadData<RoomTypeModel, dynamic>("select * from dbo.RoomTypes where Id = @Id",
                                                                          new { Id = roomTypeId },
                                                                          connectionStringName,
                                                                          false).First();

            TimeSpan timeStaying = endDate.Date.Subtract(startDate.Date);



            List<RoomModel> availableRooms = _db.LoadData<RoomModel, dynamic>("dbo.spRooms_GetAvailableRooms",
                                                                              new { startDate, endDate, roomTypeId },
                                                                              connectionStringName,
                                                                              true);

            _db.SaveData("dbo.spBookings_Insert",
                         new
                         {
                             roomId = availableRooms.First().Id,
                             guestId = guest.Id,
                             startDate = startDate,
                             endDate = endDate,
                             totalCost = timeStaying.Days * roomType.Price
                         },
                         connectionStringName,
                         true);

        }

        public void CheckInGuest(int bookingId)
        {
            throw new NotImplementedException();
        }



        public RoomTypeModel GetRoomTypeById(int id)
        {
            throw new NotImplementedException();
        }

        public List<BookingFullModel> SearchBookings(string lastName)
        {
            throw new NotImplementedException();
        }
    }
}

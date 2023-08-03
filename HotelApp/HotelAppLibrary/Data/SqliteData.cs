using HotelAppLibrary.Databases;
using HotelAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Text;
using Dapper;

namespace HotelAppLibrary.Data
{
    public class SqliteData : IDatabaseData
    {
        private const string connectionStringName = "SQLiteDb";

        private readonly ISqliteDataAccess _db;

        public SqliteData(ISqliteDataAccess db)
        {
            _db = db;
        }
        public List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
        {
            string sql = @"	select r.*
	                    from dbo.Rooms r
	                    inner join dbo.RoomTypes t on t.Id = r.RoomTypeId
	                    where r.RoomTypeId = @roomTypeId
	                    and	r.Id not in(
	                    select b.RoomId
	                    from dbo.Bookings b
	                    where (@startDate < b.StartDate and @endDate > b.EndDate)
	                    or (b.StartDate <= @endDate and @endDate < b.EndDate)
	                    or (b.StartDate <= @startDate and @startDate < b.EndDate)
	                    );";

            using (IDbConnection dbConnection = new SQLiteConnection(connectionStringName))
            {
                dbConnection.Open();

                string query = sql;
                var parameters = new { startDate, endDate };

                return dbConnection.Query<RoomTypeModel>(query, parameters).AsList();
            }
        }
        public void BookGuest(string FirstName, string LastName, DateTime startDate, DateTime endDate, int roomTypeId)
        {
            throw new NotImplementedException();
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

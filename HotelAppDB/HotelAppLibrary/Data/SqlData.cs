using HotelAppLibrary.Databases;
using HotelAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAppLibrary.Data
{
    public class SqlData
    {
        private readonly ISqlDataAccess _db;

        private const string connectionStringName = "SqlDb";

        public SqlData(ISqlDataAccess db)
        {
            _db = db;
        }

        public List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
        {
            return _db.LoadData<RoomTypeModel, dynamic>("dbo.spRoomTypes_GetAvailableTypes",
                                                new { startDate, endDate },
                                                connectionStringName,
                                                true);
        }

        public List<BookingModel> SaveBooking(int roomId,
                                int guestId,
                                DateTime startDate,
                                DateTime endDate,
                                bool checkIn,
                                decimal totalCost)
        {
          return _db.SaveData<BookingModel>("dbo.spBookings_Insert",
                               (new BookingModel { roomId, guestId, startDate, endDate, checkIn, totalCost }).ToString(),
                               connectionStringName,
                               true
                               );
        }
    }
}

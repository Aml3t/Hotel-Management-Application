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

        public void SaveBooking(int roomId, int guestId, DateTime startDate, DateTime endDate)
        {
           return _db.SaveData<BookingModel, dynamic>("dbo.spBookings_AddBooking",
                               new { roomId, guestId, startDate, endDate },
                               connectionStringName,
                               true
                               );
        }
    }
}

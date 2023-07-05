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

        public void BookGuest(string FirstName,
                              string LastName,
                              DateTime startDate,
                              DateTime endDate,
                              int roomType)
        {
            // Added the ? in case of null value//
            GuestModel? guest = _db.LoadData<GuestModel, dynamic>("dbo.spGuests_Insert",
                                                                 new { FirstName, LastName },
                                                                 connectionStringName,
                                                                 true).FirstOrDefault();


        }
    }
}

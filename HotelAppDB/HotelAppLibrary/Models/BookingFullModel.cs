using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAppLibrary.Models
{
    public class BookingFullModel
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public bool CheckedIn { get; set; }
        public decimal TotalCost { get; set; }

    }
}

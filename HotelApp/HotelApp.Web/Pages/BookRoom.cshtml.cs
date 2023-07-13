using HotelAppLibrary.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace HotelApp.Web.Pages
{
    public class BookRoomModel : PageModel
    {
        private readonly IDatabaseData _db;

        public string LastName { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public int RoomTypeId { get; set; }

        public BookRoomModel(IDatabaseData db)
        {
            _db = db;
        }
        public void OnGet()
        {

        }
    }
}


//BookGuest(string FirstName,
//                              string LastName,
//                              DateTime startDate,
//                              DateTime endDate,
//                              int roomTypeId)
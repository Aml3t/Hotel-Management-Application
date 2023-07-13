using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace HotelApp.Web.Pages
{
    public class BookRoomModel : PageModel
    {
        private readonly IDatabaseData _db;

        GuestModel guest = new GuestModel();

        RoomTypeModel roomType = new RoomTypeModel();

        RoomModel room = new RoomModel();

        BookingFullModel booking = new BookingFullModel();

        public BookRoomModel(IDatabaseData db)
        {
            _db = db;
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            return RedirectToPage(new {
                roomId = "",
                guestId = guest.Id,
                startData = DateTime.Now,
                endDate = DateTime.Now,
                totalCost = ""
            });
        }
    }
}


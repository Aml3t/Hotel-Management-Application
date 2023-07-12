using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace HotelApp.Web.Pages
{
    public class RoomSearchModel : PageModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            return Page();
        }
    }
}

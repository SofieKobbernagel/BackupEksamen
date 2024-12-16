using HSLibrary.Interfaces;
using HSLibrary.Models.Dinghy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HilleroedSejlklubExam.Pages.Bookings
{
    public class RemoveBookingModel : PageModel
    {
        private IBookingRepository _repo;
        public Booking Booking { get; set; }
        public RemoveBookingModel(IBookingRepository bookingRepository)
        {
            _repo = bookingRepository;
        }
        public void OnGet(int deleteId)
        {
            Booking = _repo.Get(deleteId);
        }
        public IActionResult OnPost(int deleteID)
        {
            _repo.Remove(deleteID);
            return RedirectToPage("ShowBookings");
        }
    }
}
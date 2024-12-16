using HSLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

using HSLibrary.Models;
using HSLibrary.Interfaces;

using HSLibrary.Models.Dinghy;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HilleroedSejlklubExam.Pages.Bookings
{
    public class ShowBookingModel : PageModel
    {
        private IBookingRepository _bookingRepository;


        public List<Booking> BookingList { get; private set; }

        public ShowBookingModel(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public List<Booking> Bookings { get; private set; }
        //public Show
        public void OnGet()
        {
            BookingList = _bookingRepository.GetAll() ?? new List<Booking>();
            Bookings = BookingList;
        }
    }

}

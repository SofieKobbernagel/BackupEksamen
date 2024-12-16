using HSLibrary.Interfaces;
using HSLibrary.Models;
using HSLibrary.Models.Dinghy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSLibrary.Data;

namespace HSLibrary.Services
{
    public class BookingRepository : IBookingRepository
    {
        private Dictionary<int, Booking> _bookings;
        public int Count { get { return _bookings.Count; } }

        public BookingRepository()
        {
            _bookings = new Dictionary<int, Booking>();
            _bookings = MockData.BookingData;
        }

        public void Add(Booking booking)
        {
            _bookings.Add(Count, booking);
        }

        public Booking Get(int id)
        {
            //if (!_bookings.ContainsKey(id)) return null;
            return _bookings[id];
        }

        public List<Booking> GetAll()
        {
            return _bookings.Values.ToList();
        }

        public List<Booking> GetAllByDinghy(Dinghy dinghy)
        {
            List<Booking> temp = new List<Booking>();
            foreach (Booking booking in _bookings.Values)
            {
                if (booking.BookedDinghy == dinghy)
                    temp.Add(booking);
            }
            return temp;
        }

        public List<Booking> GetAllByMember(Member member)
        {
            List<Booking> temp = new List<Booking>();
            foreach (Booking booking in _bookings.Values)
            {
                if (booking.BookedBy == member)
                    temp.Add(booking);
            }
            return temp;
        }

        public List<Booking> GetAllOnDate(DateOnly date)
        {
            List<Booking> temp = new List<Booking>();
            foreach (Booking booking in _bookings.Values)
            {
                if (DateOnly.FromDateTime(booking.Date) == date)
                    temp.Add(booking);
            }
            return temp;
        }

        public void Remove(int id)
        {
            _bookings.Remove(id);
        }

        public override string ToString()
        {
            string result = $"Der er i alt {Count} udlegninger";
            foreach (Booking booking in _bookings.Values)
            {
                result += $"\n\t{booking.ToString()}";
            }
            return result;
        }
    }
}

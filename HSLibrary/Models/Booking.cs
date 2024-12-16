using HSLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSLibrary.Models.Dinghy
{
    public class Booking
    {
        public int Id { get; }
        private static int _count = 0;
        public DateTime Date;
        public TimeSpan Duration;
        public DateTime End
        {
            get
            {
                return Date + Duration;
            }
        }
        public Member BookedBy;
        public Dinghy BookedDinghy;

        public Booking(DateTime date, TimeSpan duration, Member bookedBy, Dinghy bookedDinghy)
        {
            Id = _count++;
            Date = date;
            Duration = duration;
            BookedBy = bookedBy;
            BookedDinghy = bookedDinghy;
        }

        public override string ToString()
        {
            return $"Booking ID: {Id} | Dato: {Date} | Tid booked: {Duration} | Jolle #{BookedDinghy.Id} booked af: {BookedBy.Name}";
        }
    }
}

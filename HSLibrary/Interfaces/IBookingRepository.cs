using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSLibrary.Models;
using HSLibrary.Models.Dinghy;

namespace HSLibrary.Interfaces
{
    public interface IBookingRepository
    {
        int Count { get; }
        void Add(Booking booking);
        void Remove(int id);
        Booking Get(int id);
        List<Booking> GetAll();
        List<Booking> GetAllOnDate(DateOnly date);
        List<Booking> GetAllByMember(Member member);
        List<Booking> GetAllByDinghy(Dinghy dinghy);
    }
}

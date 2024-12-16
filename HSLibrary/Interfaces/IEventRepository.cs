using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSLibrary.Models;

namespace HSLibrary.Interfaces
{
    public interface IEventRepository
    {
        int Count { get; }
        void Add(Event Event);
        void Remove(int id);
        Event Get(int id);
        List<Event> GetAll();
        List<Event> GetAllOnDate(DateOnly date);
        List<Event> GetAllByMember(Member member);
    }
}

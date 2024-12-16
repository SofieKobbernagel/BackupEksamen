using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSLibrary.Models;

namespace HSLibrary.Interfaces
{
    public interface IMemberRepository
    {
        int Count { get; }
        void Add(Member member);
        void Remove(int id);
        Member Get(int id);
        List<Member> GetAll();
        List<Member> GetAllTrainers();
        List<Member> GetAllMinors();
        List<Member> GetAllAdults();
        List<Member> GetAllSeniors();
        List<Member> GetAllAdmins();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSLibrary.Models;

namespace HSLibrary.Interfaces
{
    public interface ITeamRepository
    {
        int Count { get; }
        void Add(Team team);
        void Remove(int id);
        Team Get(int id);
        List<Team> GetAll();
        List<Team> GetAllByTrainer(Member member);
    }
}

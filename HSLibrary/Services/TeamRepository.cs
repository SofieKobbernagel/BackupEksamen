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
    public class TeamRepository : ITeamRepository
    {
        Dictionary<int, Team> _teams;
        public int Count
        {
            get
            {
                return _teams.Count;
            }
        }

        public TeamRepository()
        {
            _teams = new Dictionary<int, Team>();
            _teams = MockData.TeamData;
        }

        public void Add(Team team)
        {
            _teams.Add(team.Id, team);
        }

        public Team Get(int id)
        {
            return _teams[id];
        }

        public List<Team> GetAll()
        {
            return _teams.Values.ToList();
        }

        public List<Team> GetAllByTrainer(Member member)
        {
            List<Team> list = new List<Team>();
            foreach (Team team in _teams.Values)
            {
                if (team.Trainer == member) list.Add(team);
            }
            return list;
        }

        public void Remove(int id)
        {
            _teams.Remove(id);
        }

        public override string ToString()
        {
            string result = $"Der er et total af {Count} hold";
            foreach (Team team in _teams.Values)
            {
                result += $"\n\t{team}";
            }
            return result;
        }
    }
}

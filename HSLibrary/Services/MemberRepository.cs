using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSLibrary.Models;
using HSLibrary.Interfaces;

using HSLibrary.Data;

namespace HSLibrary.Services
{
    public class MemberRepository : IMemberRepository
    {
        private Dictionary<int, Member> _members;
        public int Count { get { return _members.Count; } }

        public MemberRepository()
        {
            _members = new Dictionary<int, Member>();
            _members = MockData.MemberData;
        }

        public void Add(Member member)
        {
            if (!_members.ContainsKey(member.Id))
            {
                _members.Add(member.Id, member);
            }
        }

        public void Remove(int id)
        {
            if (_members.ContainsKey(id))
            {
                _members.Remove(id);
            }
        }

        public Member Get(int id)
        {
            return _members[id];
        }

        public List<Member> GetAll()
        {
            return _members.Values.ToList();
        }

        public List<Member> GetAllTrainers()
        {
            List<Member> trainers = new List<Member>();
            foreach (Member member in _members.Values)
            {
                if (member.IsActiveTrainer)
                {
                    trainers.Add(member);
                }
            }
            return trainers;
        }

        public List<Member> GetAllMinors()
        {
            List<Member> minors = new List<Member>();
            foreach (Member member in _members.Values)
            {
                if (member.Age < 18)
                {
                    minors.Add(member);
                }
            }
            return minors;
        }

        public List<Member> GetAllAdults()
        {
            List<Member> adults = new List<Member>();
            foreach (Member member in _members.Values)
            {
                if (member.Age >= 18 && member.Age < 65)
                {
                    adults.Add(member);
                }
            }
            return adults;
        }

        public List<Member> GetAllSeniors()
        {
            List<Member> seniors = new List<Member>();
            foreach (Member member in _members.Values)
            {
                if (member.Age >= 65)
                {
                    seniors.Add(member);
                }
            }
            return seniors;
        }

        public List<Member> GetAllAdmins()
        {
            List<Member> admins = new List<Member>();
            foreach (Member member in _members.Values)
            {
                if (member.IsAdmin)
                {
                    admins.Add(member);
                }
            }
            return admins;
        }

        public override string ToString()
        {
            string result = $"Der er i alt {Count} medlemmer";
            foreach (Member member in _members.Values)
            {
                result += $"\n\t{member.ToString()}";
            }
            return result;
        }
    }
}

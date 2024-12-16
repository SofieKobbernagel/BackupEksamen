using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSLibrary.Models
{
    public class Participants
    {
        public int? Max { get; }
        public DateTime? ExpirationDate { get; }
        public List<Member> Members { get; }

        public Participants(DateTime? expirationDate, int? max)
        {
            Max = max;
            ExpirationDate = expirationDate;
            Members = new List<Member>();
        }
        public Participants(DateTime? expirationDate)
        {
            Max = null;
            ExpirationDate = expirationDate;
            Members = new List<Member>();
        }
        public Participants(int? max)
        {
            Max = max;
            ExpirationDate = null;
            Members = new List<Member>();
        }
        public Participants()
        {
            Max = null;
            ExpirationDate = null;
            Members = new List<Member>();
        }

        public List<Member> GetAll()
        {
            return Members;
        }
        public void AddMember(Member member)
        {
            if (!Members.Contains(member) && UnderLimit() && NotExpired())
            {
                Members.Add(member);
            }
        }
        public void RemoveMember(Member member)
        {
            if (Members.Contains(member) && NotExpired())
            {
                Members.Remove(member);
            }
        }
        private bool NotExpired()
        {
            return ExpirationDate == null || ExpirationDate > DateTime.Now;
        }
        private bool UnderLimit()
        {
            return Max == null || Members.Count < Max;
        }
    }
}

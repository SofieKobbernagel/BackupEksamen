using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using HSLibrary.Interfaces;

namespace HSLibrary.Models
{
    public class Team
    {
        private static int _count = 0;

        public int Id { get; }
        public Member Trainer;
        public Participants Participants { get; }

        public Team(Member trainer, int max)
        {
            Id = _count++;
            Trainer = trainer;
            Participants = new Participants(max);
        }
        public override string ToString()
        {
            return $"Hold ID: {Id} | Træner: {Trainer.Name} | Holdmedlemmer: {Participants.Members.Count}/{Participants.Max}";
        }
    }
}

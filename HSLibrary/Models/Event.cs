using HSLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSLibrary.Models
{
    public class Event
    {
        public int Id { get; }
        private static int _count = 0;
        public DateTime Date { get; }
        public string Title { get; }
        public string Description { get; }
        public Participants Participants { get; }
        public Member Organiser { get; }
        public Event(DateTime date, string title, string description, Member organiser, int? maxParticipants = null)
        {
            Id = _count++;
            Date = date;
            Title = title;
            Description = description;
            Participants = new Participants(date, maxParticipants);
            Organiser = organiser;
        }
        public override string ToString()
        {
            return $"ID: {Id} | Dag & Tid: {Date} | Deltagere: {Participants.Members.Count} | Organisator: {Organiser.Name} | Titel: {Title}";
        }
    }
}

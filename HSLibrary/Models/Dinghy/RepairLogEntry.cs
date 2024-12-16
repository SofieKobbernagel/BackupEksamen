using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSLibrary.Models.Dinghy
{
    public class RepairLogEntry
    {
        public DateOnly Date { get; }
        public string Notes { get; }
        public string Summary { get; }
        public RepairLogEntry(string summary, string notes)
        {
            Summary = summary;
            Notes = notes;
            Date = DateOnly.FromDateTime(DateTime.Now);
        }
        public override string ToString()
        {
            return $"Dato af Reperation: {Date} | Summering: {Summary}";
        }
    }
}

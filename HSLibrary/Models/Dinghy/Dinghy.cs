using HSLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSLibrary.Models.Dinghy
{
    public class Dinghy
    {
        public int Id { get; }
        private static int _count = 0;
        public DinghyModel Model { get; }
        public string Components { get; }
        public bool NeedsRepair
        {
            get
            {
                return RepairComment != null;
            }
        }
        public string RepairComment { get; private set; }
        public List<RepairLogEntry> RepairLog { get; }

        public Dinghy(DinghyModel model, string components)
        {
            Id = _count++;
            Model = model;
            Components = components;
            RepairLog = new List<RepairLogEntry>();
        }


        public void RepairDinghy(string summary, string notes)
        {
            RepairLog.Add(new RepairLogEntry(summary, notes));
            RepairComment = null;
        }
        public void NeedRepair(string repairComment)
        {
            RepairComment = repairComment;
        }
        public override string ToString()
        {
            return $"ID: {Id} | Model: {Model} | Komponenter: {Components}" + (NeedsRepair ? $" | Defect: {RepairComment}" : "");
        }
    }
}

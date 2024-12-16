using HSLibrary.Interfaces;
using HSLibrary.Models.Dinghy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSLibrary.Data;

namespace HSLibrary.Services
{
    public class DinghyRepository : IDinghyRepository
    {
        private Dictionary<int, Dinghy> _dinghies;
        public int Count
        {
            get
            {
                return _dinghies.Count;
            }
        }

        public DinghyRepository()
        {
            _dinghies = new Dictionary<int, Dinghy>();
            _dinghies = MockData.DinghyData;
        }

        public void Add(Dinghy dinghy)
        {
            _dinghies.Add(dinghy.Id, dinghy);
        }
        public void Remove(int id)
        {
            _dinghies.Remove(id);
        }
        public Dinghy Get(int id)
        {
            return _dinghies[id];
        }
        public List<Dinghy> GetAll()
        {
            return _dinghies.Values.ToList();
        }
        public List<Dinghy> GetAllNeedingRepairs()
        {
            List<Dinghy> list = new List<Dinghy>();
            foreach (Dinghy dinghy in _dinghies.Values)
            {
                if (dinghy.NeedsRepair) list.Add(dinghy);
            }
            return list;
        }
        public List<Dinghy> GetAllSeaWorthy()
        {
            List<Dinghy> list = new List<Dinghy>();
            foreach (Dinghy dinghy in _dinghies.Values)
            {
                if (!dinghy.NeedsRepair) list.Add(dinghy);
            }
            return list;
        }
        public List<Dinghy> GetAllOfModel(DinghyModel model)
        {
            List<Dinghy> list = new List<Dinghy>();
            foreach (Dinghy dinghy in _dinghies.Values)
            {
                if (dinghy.Model == model) list.Add(dinghy);
            }
            return list;
        }
        public override string ToString()
        {
            string result = $"Der er et total af {Count} joller";
            foreach (Dinghy dinghy in _dinghies.Values)
            {
                result += $"\n\t{dinghy}";
            }
            return result;
        }
    }
}

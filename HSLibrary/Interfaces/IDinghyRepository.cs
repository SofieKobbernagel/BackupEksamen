using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSLibrary.Models.Dinghy;

namespace HSLibrary.Interfaces
{
    public interface IDinghyRepository
    {
        int Count { get; }
        void Add(Dinghy dinghy);
        void Remove(int id);
        Dinghy Get(int id);
        List<Dinghy> GetAll();
        List<Dinghy> GetAllNeedingRepairs();
        List<Dinghy> GetAllSeaWorthy();
        List<Dinghy> GetAllOfModel(DinghyModel model);
    }
}

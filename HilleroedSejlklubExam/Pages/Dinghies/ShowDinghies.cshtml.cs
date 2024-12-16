using HSLibrary.Interfaces;
using HSLibrary.Models.Dinghy;
using HSLibrary.Models.Dinghy.Motorized;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HilleroedSejlklubExam.Pages.Dinghies
{
    public class ShowDinghyModel : PageModel
    {
        IDinghyRepository _dinghyRepository;
        public List<HSLibrary.Models.Dinghy.Dinghy> DinghyList;
        public ShowDinghyModel(IDinghyRepository dinghyRepository)
        {
            _dinghyRepository = dinghyRepository;
            DinghyList = _dinghyRepository.GetAll();
        }
        public void OnGet()
        {
        }
    }
}

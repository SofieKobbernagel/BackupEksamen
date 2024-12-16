using HSLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HSLibrary.Models.Dinghy;

namespace HilleroedSejlklubExam.Pages.Dinghies
{
    public class ShowSpecificDinghyModel : PageModel
    {
        IDinghyRepository _dinghyRepository;
        public HSLibrary.Models.Dinghy.Dinghy Dinghy;
        public ShowSpecificDinghyModel(IDinghyRepository dinghyRepository)
        {
            _dinghyRepository = dinghyRepository;
        }
        public void OnGet(int showId)
        {
            Dinghy = _dinghyRepository.Get(showId);
        }
    }
}

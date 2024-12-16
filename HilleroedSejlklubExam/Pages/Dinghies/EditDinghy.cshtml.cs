using HSLibrary.Interfaces;
using HSLibrary.Models.Dinghy;
using HSLibrary.Models.Dinghy.Motorized;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HilleroedSejlklubExam.Pages.Dinghies
{
    public class EditDinghyModel : PageModel
    {
        IDinghyRepository _dinghyRepository;
        public EditDinghyModel(IDinghyRepository dinghyRepository)
        {
            _dinghyRepository = dinghyRepository;
        }
        public void OnGet()
        {
        }

    }
}

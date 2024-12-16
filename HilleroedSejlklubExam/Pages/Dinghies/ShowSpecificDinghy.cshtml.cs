using HSLibrary.Interfaces;
using HSLibrary.Models.Dinghy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HilleroedSejlklubExam.Pages.Dinghies
{
    public class ShowSpecificDinghyModel : PageModel
    {
        IDinghyRepository _dinghyRepository;
        public Dinghy Dinghy;
        #region NeedRepair
        [BindProperty]
        public string RepairComment { get; set; } = "";
        #endregion
        #region NeedRepair
        [BindProperty]
        public string Summary { get; set; } = "";
        [BindProperty]
        public string Notes { get; set; } = "";
        #endregion
        public ShowSpecificDinghyModel(IDinghyRepository dinghyRepository)
        {
            _dinghyRepository = dinghyRepository;
        }
        public void OnGet(int showId)
        {
            Dinghy = _dinghyRepository.Get(showId);
        }
        public IActionResult OnPostDelete(int deleteId)
        {
            _dinghyRepository.Remove(deleteId);
            return RedirectToPage("ShowDinghies");
        }
        public void OnPostNeedRepair(int dinghyId)
        {
            Dinghy = _dinghyRepository.Get(dinghyId);
            Dinghy.NeedRepair(RepairComment);
        }
        public void OnPostMakeRepair(int dinghyId)
        {
            Dinghy = _dinghyRepository.Get(dinghyId);
            Dinghy.RepairDinghy(Summary, Notes);
        }
    }
}

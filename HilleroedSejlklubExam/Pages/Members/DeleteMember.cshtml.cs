using HSLibrary.Interfaces;
using HSLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HilleroedSejlklubExam.Pages.Members
{
    public class DeleteMemberModel : PageModel
    {
        private IMemberRepository _repo;
        private int _id;

        [BindProperty]
        public Member Member { get; set; }

        public DeleteMemberModel(IMemberRepository memberRepository)
        {
            _repo = memberRepository;
        }

        public void OnGet(int deleteId)
        {
            Member = _repo.Get(deleteId);
            _id = deleteId;
        }

        public IActionResult OnPost(int deleteID)
        {
            _repo.Remove(deleteID);
            return RedirectToPage("/Members/ShowMembers");
        }
    }
}

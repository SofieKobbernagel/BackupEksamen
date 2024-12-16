using HSLibrary.Interfaces;
using HSLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HilleroedSejlklubExam.Pages.Members
{
    public class ShowMembersModel : PageModel
    {
        private IMemberRepository _memberRepository;

        public List<Member> MemberList { get; private set; }

        public int MemberCount => _memberRepository.Count;

        public ShowMembersModel(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public void OnGet()
        {
            MemberList = _memberRepository.GetAll();
        }
    }

}

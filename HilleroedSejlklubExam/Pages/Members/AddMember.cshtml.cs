using HSLibrary.Interfaces;
using HSLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HilleroedSejlklubExam.Pages.Members
{
    public class AddMemberModel : PageModel
    {
        private IMemberRepository _repo;

        private IWebHostEnvironment webHostEnvironment;

        [BindProperty]
        public IFormFile? Photo { get; set; }


        [BindProperty] //Two way binding
        public Member Member { get; set; }



        public AddMemberModel(IMemberRepository memberRepository, IWebHostEnvironment webHost)
        {
            _repo = memberRepository;
            webHostEnvironment = webHost;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Photo != null)
            {
                if (Member.MemberImage != null && Member.MemberImage != "default.jpeg")
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "/Images/MemImages", Member.MemberImage);
                    System.IO.File.Delete(filePath);
                }

                Member.MemberImage = ProcessUploadedFile();
            }
            _repo.Add(Member);
            return RedirectToPage("/Members/ShowMembers");
        }
        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images/MemImages");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}

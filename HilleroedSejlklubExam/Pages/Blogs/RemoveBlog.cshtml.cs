using HSLibrary.Interfaces;
using HSLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HilleroedSejlklubExam.Pages.Blogs
{
    public class RemoveBlogModel : PageModel
    {
        private IBlogRepository _repo;

        public Blog Blog { get; set; }
        public RemoveBlogModel(IBlogRepository blogRepo)
        {
            _repo = blogRepo;
        }
        public void OnGet(int deleteID)
        {
            Blog = _repo.Get(deleteID);
        }
        public IActionResult OnPost(int deleteId)
        {
            _repo.Remove(deleteId);
            return RedirectToPage("ShowBlog");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using HSLibrary.Models;
using HSLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata;

namespace HilleroedSejlklubExam.Pages.Blogs
{
    public class ShowBlogModel : PageModel
    {
        private IBlogRepository _blogRepository;
        public List<Blog> Blogs { get; private set; }
        public ShowBlogModel(IBlogRepository blogRepo)
        {
            _blogRepository = blogRepo;
        }
        public void OnGet()
        {
            Blogs = _blogRepository.GetAll();
        }
    }
}

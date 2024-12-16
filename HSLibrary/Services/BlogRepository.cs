using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using HSLibrary.Interfaces;
using HSLibrary.Models;
using HSLibrary.Models.Dinghy;

using HSLibrary.Data;

namespace HSLibrary.Services
{
    public class BlogRepository : IBlogRepository
    {
        private Dictionary<int, Blog> _blogs;
        public int Count { get { return _blogs.Count; } }

        public BlogRepository()
        {
            _blogs = new Dictionary<int, Blog>();
            _blogs = MockData.BlogData;
        }

        public void Add(Blog blog)
        {
            _blogs.Add(Count, blog);
        }

        public Blog Get(int id)
        {
            return _blogs[id];
        }

        public List<Blog> GetAll()
        {
            return _blogs.Values.ToList();
        }

        public List<Blog> GetAllByMember(Member member)
        {
            List<Blog> temp = new List<Blog>();
            foreach (Blog blog in _blogs.Values)
            {
                if (blog.PostedBy == member)
                    temp.Add(blog);
            }
            return temp;
        }

        public List<Blog> GetAllOnDate(DateOnly date)
        {
            List<Blog> temp = new List<Blog>();
            foreach (Blog blog in _blogs.Values)
            {
                if (DateOnly.FromDateTime(blog.PostedOn) == date)
                    temp.Add(blog);
            }
            return temp;
        }

        public void Remove(int id)
        {
            _blogs.Remove(id);
        }

        public override string ToString()
        {
            string result = $"Der er et total af {Count} blogs";
            foreach (Blog blog in _blogs.Values)
            {
                result += $"\n\t{blog.ToString()}";
            }
            return result;
        }
    }
}

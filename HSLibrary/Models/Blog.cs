using HSLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HSLibrary.Models
{
    public class Blog
    {
        #region Instance Field
        private static int _count = 0;
        #endregion

        #region Propeties
        public int Id { get; }
        public Member PostedBy { get; }
        public DateTime PostedOn { get; }
        public DateTime? LastUpdate { get; private set; }
        public string Text { get; private set; }
        public string Title { get; }

        // public ImageFileMachine Pictures { get; set; }
        #endregion

        #region Constructors
        public Blog(Member poster, string title, string text)
        {
            Id = _count++;
            PostedBy = poster;
            Title = title;
            Text = text;
            PostedOn = DateTime.Now;
            LastUpdate = null;
        }
        #endregion

        #region Methods
        public void EditText(String text)
        {
            Text = text;
            LastUpdate = DateTime.Now;
        }

        public void InsertPhoto()
        {
            throw new NotImplementedException("RazerPage function, not implemented");
        }

        public void EditPhoto()
        {
            throw new NotImplementedException("RazerPage function, not implemented");
        }

        public override string ToString()
        {
            return $"Blog ID: {Id} | Blog: {Title} | Oplagt af: {PostedBy.Name} | Posted: {PostedOn}" + (LastUpdate == null ? "" : $" | Sidst opdateret: {LastUpdate}");
        }
        #endregion
    }
}

using HSLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSLibrary.Exceptions
{
    public class WrongUserException : Exception
    {
        private Member _member;
        public WrongUserException(Member member)
        {
            _member = member;
        }
        public override string ToString()
        {
            return $"Brugeren {_member.Name} har ikke tilladelse for denne handling";
        }
    }
}

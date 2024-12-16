using HSLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSLibrary.Exceptions
{
    public class BookingOverlapException : Exception
    {
        public BookingOverlapException()
        {
        }
        public override string ToString()
        {
            return $"";
        }
    }
}

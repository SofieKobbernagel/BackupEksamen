using HSLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSConsoleApp.IO
{
    public class Output
    {
        public void Write(string text)
        {
            Console.WriteLine(text);
        }

        public void DisplayList<T>(List<T> list)
        {
            Write($"Der er {list.Count} objekter:");
            for (int i = 0; i < list.Count; i++)
            {
                Write($"\t{i}# {list[i]}");
            }
        }
    }
}

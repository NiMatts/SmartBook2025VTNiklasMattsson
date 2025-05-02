using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBook.Core.BookTypes
{
    internal class Hardcover : Book, IPhysical
    {
        private const int MEDIATYPEID = 2; //Hardcover id
        public string Section { get; set; }

        public Hardcover(string title, string author, string genre) : base(title, author, genre, MEDIATYPEID)
        {
            Section = $"{genre.ToUpper()[0]}{author.ToUpper()[0]}";
        }

        public Hardcover(string title, string author, string genre, bool isavailable) : base(title, author, genre, MEDIATYPEID, isavailable)
        {
            Section = $"{genre.ToUpper()[0]}{author.ToUpper()[0]}";
        }
    }
}

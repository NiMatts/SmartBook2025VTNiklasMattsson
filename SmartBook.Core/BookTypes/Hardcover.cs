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
        public char Shelf { get; set; }
        public Hardcover(string title, string author, string genre) : base(title, author, genre, MEDIATYPEID)
        {
            Section = genre;
            Shelf = author[0];
            //Shelf = shelf;
        }
    }
}

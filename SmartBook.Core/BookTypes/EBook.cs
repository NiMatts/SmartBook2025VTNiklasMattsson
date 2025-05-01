using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace SmartBook.Core.BookTypes
{
    internal class EBook : Book, IDigital
    {
        private const int MEDIATYPEID = 3; //Ebook id
        public string FilePath { get; set; }

        public EBook(string title, string author, string genre) : base(title, author, genre, MEDIATYPEID)
        {
            FilePath = $"Library/example/book/{title}";
            //Shelf = shelf;
        }
    }
}

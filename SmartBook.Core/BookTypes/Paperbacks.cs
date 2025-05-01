using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartBook.Core.BookTypes
{
    internal class Paperbacks : Book, IPhysical
    {
        private const int MEDIATYPEID = 1; //Paperback id
        public string Section { get; set; }

        public Paperbacks(string title, string author, string genre) : base(title, author, genre, MEDIATYPEID)
        { 
            Section = $"{genre.ToUpper()[0]}{author.ToUpper()[0]}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBook.Core.BookTypes
{
    internal class AudioBook : Book, IDigital
    {
        private const int MEDIATYPEID = 4; //AudioBook id
        public string FilePath { get; set; }

        public AudioBook(string title, string author, string genre) : base(title, author, genre, MEDIATYPEID)
        {
            FilePath = $"Library/example/audio/{author}/{title}";
        }
        public AudioBook(string title, string author, string genre, bool isavailable) : base(title, author, genre, MEDIATYPEID, isavailable)
        {
            FilePath = $"Library/example/audio/{author}/{title}";
        }
    }
}

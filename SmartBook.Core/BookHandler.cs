using SmartBook.Core.BookTypes;
using static System.Reflection.Metadata.BlobBuilder;

namespace SmartBook.Core
{
    public static class BookHandler
    {
        public static bool changes = false;
        public static List<Book> Books = new List<Book>(); 

        public static bool GenerateBook()
        {
            Book book = null;
            Console.Write("Enter title: ");
            string title = Console.ReadLine();
            Console.Write("Enter author: ");
            string author = Console.ReadLine();
            Console.Write("Enter genre: ");
            string genre = Console.ReadLine();

            Console.WriteLine("Categories: 1=Hardcover|2=Paperback|3=Ebook|4=AudioBook|Q.exit");
            Console.Write("Enter category: ");
            int category = MenuOptions("1234");
            try
            {
                switch (category)
                {
                    case 1:
                        book = new Paperbacks(title, author, genre);
                        break;
                    case 2:
                        book = new Hardcover(title, author, genre);
                        break;
                    case 3:
                        book = new EBook(title, author, genre);
                        break;
                    case 4:
                        book = new AudioBook(title, author, genre);
                        break;
                    case 10:
                        return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
            if (book != null)
            {
                if (CheckForCopy(book))
                {
                    AddBook(book);
                }
            }

            return true;
        }
        private static bool CheckForCopy(Book book)
        {
            if(Books.Any(b => b.Title == book.Title)) return false;
            if(Books.Any(b => b.ISBN == book.ISBN)) return false;
            return true;
        }
        public static void AddBook(Book book)
        {
            Books.Add(book);
        }
        public static bool RemoveBook()
        {
            Console.WriteLine("Get book by: 1=Title|2=ISBN|Q.exit");
            Console.Write("Enter category: ");
            int category = MenuOptions("14");//1,4 is BookDataTypes
            Console.Write($"Enter {(BookDataTypes)category}: ");
            string input = Console.ReadLine();
            Book foundbook = null;
            
            if (category == 1)
            {
                if (input != null)
                    foundbook = Books.FirstOrDefault(b => b.Title == input);
            }
            else if (category == 4)
            {
                if (input != null)
                    foundbook = Books.FirstOrDefault(b => b.ISBN == input);
            }
            else if (category == 10)
            {
                Console.WriteLine("exiting menu.");
                return false;
            }
            else
            {
                Console.WriteLine("Menu error.");
                return false;
            }

            if (foundbook != null)
            {
                foundbook.ToString();
                Console.Write(" :has been removed");
                Books.Remove(foundbook); return true;
            }
            else
            {
                Console.WriteLine("The book couldn't be found");
            }
            return false;
        }
        
        public static void ListBooks()
        {
            Console.WriteLine("List books by: 1=Title|2=Author|3=Genre|4=IsAvailable|Q.exit");
            Console.Write("Enter category: ");
            int category = MenuOptions("1235");//1,2,3,5 is BookDataTypes
            Console.Write($"Enter {(BookDataTypes)category}: ");
            string input = "";
            if(category != 5)input = Console.ReadLine();
            List<Book> foundbooks = null;
            if (category == 1)
            {
                if (input != null)
                    foundbooks = Books.Where(b => b.Title == input).ToList();
            }
            else if (category == 2)
            {
                if (input != null)
                    foundbooks = Books.Where(b => b.Author == input).ToList();
            }
            else if (category == 3)
            {
                if (input != null)
                    foundbooks = Books.Where(b => b.Genre == input).ToList();
            }
            else if (category == 5)
            {
                if (input != null)
                    foundbooks = Books.Where(b => b.IsAvailable).ToList();
            }
            else if (category == 10)
            {
                Console.WriteLine("exiting menu.");
            }
            else
            {
                Console.WriteLine("Menu error.");
            }
        }
        private static int MenuOptions(string options)
        {
            int category = 0;
            while (!options.Contains(category.ToString()) && category != 10)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1: category = 1; break;
                    case ConsoleKey.D2: category = 2; break;
                    case ConsoleKey.D3: category = 3; break;
                    case ConsoleKey.D4: category = 4; break;
                    case ConsoleKey.D5: category = 5; break;
                    case ConsoleKey.Q: category = 10; break;
                }
            }
            return category;
        }


    }
}

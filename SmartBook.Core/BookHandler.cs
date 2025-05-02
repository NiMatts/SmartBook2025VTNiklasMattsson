using SmartBook.Core.BookTypes;
using static System.Reflection.Metadata.BlobBuilder;

namespace SmartBook.Core
{
    public static class BookHandler
    {
        public static bool changes = false;
        public static List<Book> Books = new List<Book>();
        public static bool GenerateBookFromMenu()
        {

            Console.Write("Enter title: ");
            string title = Console.ReadLine();
            Console.Write("Enter author: ");
            string author = Console.ReadLine();
            Console.Write("Enter genre: ");
            string genre = Console.ReadLine();

            Console.WriteLine("Categories: 1=Hardcover|2=Paperback|3=Ebook|4=AudioBook|Q.exit");
            Console.WriteLine("Enter category: ");
            int category = MenuOptions("1234");
            if (CreateBook(category, title, author, genre, true, false))
                Console.WriteLine("Book was generated succesfully");

            return true;
        }
        public static void GenerateBookFromFile(int category, string title, string author, string genre, bool isavailable)
        {
            if (CreateBook(category, title, author, genre,isavailable, true))
                Console.WriteLine("Book was generated succesfully");
        }
        private static bool CreateBook(int category, string title, string author, string genre,bool isavailable, bool fromFile)
        {
            Book book = null;
            try
            {
                switch (category)
                {
                    case 1:
                        book = new Paperbacks(title, author, genre, isavailable);
                        break;
                    case 2:
                        book = new Hardcover(title, author, genre, isavailable);
                        break;
                    case 3:
                        book = new EBook(title, author, genre, isavailable);
                        break;
                    case 4:
                        book = new AudioBook(title, author, genre, isavailable);
                        break;
                    case 10:
                        Console.WriteLine("exiting to main menu");
                        return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}. returning to main menu.");
                return false;
            }
            if (book != null)
            {
                if (CheckForCopy(book))
                {
                    AddBook(book);
                    if(!fromFile)changes = true;// for saving changes to txt.file
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
        private static void AddBook(Book book)
        {
            Books.Add(book);
        }
        public static bool RemoveBook()
        {
            Console.WriteLine("Get book by: 1=Title|2=ISBN|Q.exit");
            Console.Write("Enter category: ");
            int category = MenuOptions("14");//1,4 is BookDataTypes
            Console.Write($"Enter {(BookDataFields)category}: ");
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
                Console.Write($"{foundbook.ToString()}");
                Console.WriteLine(" :has been removed");
                Books.Remove(foundbook);
                changes = true;// for saving changes to txt.file
                return true;
            }
            else
            {
                Console.WriteLine("The book couldn't be found");
            }
            return false;
        }

        public static void FindBook()
        {
            var foundbooks = FindListBooks("Find");
            if (foundbooks != null)
            {
                string menueoptions = "";
                Console.WriteLine($"Found {foundbooks.Count} books 5 available for update");
                for (int i = 0; i < foundbooks.Count; i++)
                {
                    
                    if (i <= 4)
                    {
                        menueoptions = menueoptions + (i+1).ToString();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{i + 1}. {foundbooks[i].ToString()}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{foundbooks[i].ToString()}");
                    }
                }
                Console.ResetColor();
                Console.WriteLine("Toggle isAvailable on green book by: number or exit: Q");
                Console.WriteLine("Enter number or Q: ");
                int category = MenuOptions(menueoptions);//menuoptions is max 5 found books.
                if (category != 10)
                {
                    foundbooks[category - 1].IsAvailable = !foundbooks[category - 1].IsAvailable;
                    changes = true;// for saving changes to txt.file
                    Console.WriteLine($"updated {foundbooks[category - 1].ToString()}");
                }
                else
                {
                    Console.WriteLine("exiting");
                }
            }
            else
            {
                Console.WriteLine("No books could be found");
            }
        }

        public static void ListBooks()
        {
            var foundbooks = FindListBooks("List");
            if (foundbooks != null)
            {
                foreach (var book in foundbooks)
                {
                    Console.WriteLine(book.ToString());
                }
            }
            else
            {
                Console.WriteLine("No books could be found");
            }
        }
        private static List<Book> FindListBooks(string type)
        {
            Console.WriteLine($"{type} books by: 1=Title|2=Author|3=Genre|Q.exit");
            Console.Write("Enter category: ");
            int category = MenuOptions("123");//1,2,3 is BookDataFields
            
            string input = null;
            if (category != 5 && category != 10)
            { 
                Console.Write($"Enter {(BookDataFields)category} or leave empty for list by category: "); input = Console.ReadLine(); 
            }
            List<Book> foundbooks = null;
            if (category == 1)
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    foundbooks = Books.Where(b => b.Title == input).ToList();
                }
                else
                {
                    foundbooks = Books.OrderBy(b => b.Title).ToList();
                }
            }
            else if (category == 2)
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    foundbooks = Books.Where(b => b.Author == input).ToList();
                }
                else
                {
                    foundbooks = Books.OrderBy(b => b.Author).ToList();
                }
            }
            else if (category == 3)
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    foundbooks = Books.Where(b => b.Genre == input).ToList();
                }
                else
                {
                    foundbooks = Books.OrderBy(b => b.Genre).ToList();
                }    
            }
            else if (category == 10)
            {
                Console.WriteLine("exiting menu.");
            }
            else
            {
                Console.WriteLine("Menu error.");
            }

            return foundbooks;
        }
        private static int MenuOptions(string options)
        {
            int category = 0;
            while ((category>options.Length || category == 0) && category != 10)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (char.IsDigit(keyInfo.KeyChar))
                {
                    category = int.Parse(keyInfo.KeyChar.ToString());
                }
                else if (keyInfo.Key == ConsoleKey.Q)
                {
                    category = 10; // exit
                }             
            }
            if (category != 10)
            {
                category = int.Parse(options[category-1].ToString());
            }
            return category;
        }


    }
}

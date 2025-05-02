using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SmartBook.Core;
using SmartBook.Core.BookTypes;
using static System.Reflection.Metadata.BlobBuilder;

namespace SmartBook2025VTNiklasMattsson;

internal class Library
{
    public void LibraryLoop()
    {
        //setups
        bool isAlive = true;
        GenerateLibraryFromFile();
        //live loop
        do
        {
            isAlive = LibraryMenu();
            if (BookHandler.changes) SaveLibraryToFile(); BookHandler.changes = false;
        } while (isAlive);
    }
    private bool LibraryMenu()
    {
        Console.WriteLine("Bibblan");
        bool isAlive = false;
        do
        {
            Console.WriteLine("1. Add book");
            Console.WriteLine("2. Remove book");
            Console.WriteLine("3. List books");
            Console.WriteLine("4. Find book/change availabilty");
            Console.WriteLine("5. exit program.");
            string menuChoice = Console.ReadLine();
            if (menuChoice != null)
            {
                switch (menuChoice)
                {
                    case "1":
                        BookHandler.GenerateBookFromMenu();
                        break;
                    case "2":
                        BookHandler.RemoveBook();
                        break;
                    case "3":
                        BookHandler.ListBooks();
                        break;
                    case "4":
                        BookHandler.FindBook();
                        break;
                    case "5":
                        isAlive = false;
                        return false;
                    default:
                        Console.WriteLine("felaktig input.(1-5)");
                        break;
                }
            }
            if (BookHandler.changes) return true;
            Console.WriteLine();
        } while (isAlive);
        return true;
    }
    private class TemporaryBook
    {
        public string title { get; set; }
        public string author { get; set; }
        public string genre { get; set; }
        public int mediatypeid { get; set; }
        public bool isAvailable { get; set; }
    }
    private void GenerateLibraryFromFile()
    {
        try
        {
            string filePath = "C:\\Users\\Nikla\\Desktop\\ProgC#\\SmartBook2025VTNiklasMattsson\\SmartBook2025VTNiklasMattsson\\books.json"; // adjust path as needed

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            string json = File.ReadAllText(filePath);
            List<TemporaryBook> books = JsonSerializer.Deserialize<List<TemporaryBook>>(json);

            foreach (var book in books)
            {
                BookHandler.GenerateBookFromFile(book.mediatypeid,book.title,book.author,book.genre,book.isAvailable);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading library: {ex.Message}");
        }
    }
    private void SaveLibraryToFile()
    {
        try
        {
            string filePath = "C:\\Users\\Nikla\\Desktop\\ProgC#\\SmartBook2025VTNiklasMattsson\\SmartBook2025VTNiklasMattsson\\books.json"; // adjust path as needed

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return;
            }
            List<TemporaryBook> books = new List<TemporaryBook>();
            foreach (var book in BookHandler.Books)
            {
                books.Add(new TemporaryBook()
                {
                    title = book.Title,
                    author = book.Author,
                    genre = book.Genre,
                    mediatypeid = book.MediaTypeId,
                    isAvailable = book.IsAvailable
                });
            }
            string updatedJson = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, updatedJson);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error Saving library: {ex.Message}");
        }

    }
}

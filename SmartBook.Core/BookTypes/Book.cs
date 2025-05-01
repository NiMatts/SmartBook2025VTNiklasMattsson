using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartBook.Core.BookTypes
{
    public abstract class Book
    {
        private string _title;
        private string _author;
        private string _genre;
        public int MediaTypeId { get; }
        public string ISBN { get; }
        public bool IsAvailable { get; set; } = true;
        public Book(string title, string author, string genre, int mediatypeid) { 
            Title = title;
            Author = author;
            Genre = genre;
            MediaTypeId = mediatypeid;
            ISBN = GenerateISBN(title, author, genre);
        }
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Title cannot be null or whitespace.");
                _title = value;
            }
        }
        public string Author
        {
            get => _author;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Author cannot be null or whitespace.");
                _author = value;
            }
        }
        public string Genre
        {
            get => _genre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Genre cannot be null or whitespace.");
                _genre = value;
            }
        }

        public static string GenerateISBN(string title, string author, string genre)
        {
            string input = $"{title}|{author}|{genre}".ToLowerInvariant(); // normalize input

            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);

                // Convert first 13 digits of hash to mimic an ISBN
                StringBuilder sb = new StringBuilder();
                foreach (var b in hash)
                {
                    sb.Append((b % 10)); // Use only digits
                    if (sb.Length == 13) break; // Stop at 13 digits
                }

                return sb.ToString();
            }
        }
        public override string ToString()
        {
            return $"\"{Title}\" by {Author} | Genre: {Genre} | MediaType: {(MediaTypes)MediaTypeId} | Available: {IsAvailable} | ISBN: {ISBN}";
        }
    }
}

using SmartBook.Core;
namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void TestGenerateBookFromFile()
        {
            int size = BookHandler.Books.Count;
            BookHandler.GenerateBookFromFile(4, "test", "testAuthor", "testGenre", false);
            Assert.NotEqual(size, BookHandler.Books.Count);
        }

        [Fact]
        public void TestGenerateBookFromFile2()
        {
            int size = BookHandler.Books.Count;
            BookHandler.GenerateBookFromFile(4, "test", "testAuthor", "testGenre", false);
            //Assert.Equal(expected: MediaTypes.EBook, (MediaTypes)BookHandler.Books.Last().MediaTypeId);
            Assert.Equal(expected: MediaTypes.AudioBook, (MediaTypes)BookHandler.Books.Last().MediaTypeId);
        }
    }
}

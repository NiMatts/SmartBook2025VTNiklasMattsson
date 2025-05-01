namespace SmartBook.Data
{
    public class LibraryDataHandler
    {
        public static string GenerateISBN()
        {
            Random random = new Random();

            // ISBN Format: 978-0-XXXXXX-XX-X (13-digit ISBN)

            // Prefix for ISBN-13 (fixed to 978)
            string prefix = "978";

            // Registration group and publisher (can vary depending on the region or publisher)
            string registrationGroup = random.Next(0, 10).ToString();  // Simplified, real-world data would be more complex
            string publisher = random.Next(100000, 999999).ToString();  // Publisher part (6 digits)

            // Title or item number (should be unique for each book)
            string itemNumber = random.Next(10, 99).ToString(); // Simplified for example (2 digits)

            // Check digit (the last digit of ISBN-13, calculated using a weighted sum formula)
            int checkDigit = CalculateISBNCheckDigit(prefix + registrationGroup + publisher + itemNumber);

            // Combine all parts to form the ISBN
            return $"{prefix}-{registrationGroup}-{publisher}-{itemNumber}-{checkDigit}";
        }

        private static int CalculateISBNCheckDigit(string isbnWithoutCheckDigit)
        {
            // ISBN-13 check digit calculation (based on weighted sum of 13 digits)
            int sum = 0;

            for (int i = 0; i < isbnWithoutCheckDigit.Length; i++)
            {
                if (char.IsDigit(isbnWithoutCheckDigit[i]))
                {
                    int digit = int.Parse(isbnWithoutCheckDigit[i].ToString());
                    if (i % 2 == 0)
                    {
                        sum += digit; // Odd positions (0-based) are multiplied by 1
                    }
                    else
                    {
                        sum += digit * 3; // Even positions (0-based) are multiplied by 3
                    }
                }
            }

            // Calculate the check digit
            int mod = sum % 10;
            return (mod == 0) ? 0 : 10 - mod; // ISBN check digit is the value that makes the sum a multiple of 10
        }
    }
}

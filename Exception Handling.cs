using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Exception_Handling
{
    public static class ErrorHandling
    {
        public static string ReadLetterInput(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            if (Regex.IsMatch(input, @"^[a-zA-Z\s]+$"))
            {
                return input;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            throw new ArgumentException("Invalid input: only letters are allowed.");
        }

        public static string ReadYesNoInput(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine().Trim().ToLower();
            if (input.ToLower() == "yes" || input.ToLower() == "no")
            {
                return input;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new ArgumentException("Invalid input: Please enter 'YES' or 'NO' only.");
            }
        }
    }


    public class IncidentException
    {
        public int MedicalRelation(string medic)
        {
            string relationPath = @"C:\Users\Shan Michael\OneDrive\文档\2nd Year 1st Sem\OOP\swift aid\Exception\relation.txt";
            try
            {
                using (StreamReader sr = new StreamReader(relationPath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (medic.IndexOf(line, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            return 0;
                        }
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
                return 1;
            }
        }
    }
}

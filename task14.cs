using System;
using System.Text;

namespace StringExtensions
{
    public static class StringExtensions
    {
   
        public static string RemoveExtraSpaces(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            StringBuilder result = new StringBuilder();
            bool previousWasSpace = false;

            foreach (char c in str)
            {
                if (char.IsWhiteSpace(c))
                {
                    if (!previousWasSpace)
                    {
                        result.Append(' ');
                        previousWasSpace = true;
                    }
                }
                else
                {
                    result.Append(c);
                    previousWasSpace = false;
                }
            }

            return result.ToString().Trim();
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== РОЗШИРЕННЯ КЛАСУ STRING ===\n");

 

            // Демонстрація роботи методу
            Console.WriteLine("ДЕМОНСТРАЦІЯ РОБОТИ МЕТОДУ RemoveExtraSpaces:\n");

           
            

            // Робота з введенням від користувача
            Console.WriteLine("=== ВВЕДЕННЯ ВІД КОРИСТУВАЧА ===");

            while (true)
            {
                Console.Write("\nВведіть рядок для обробки (або 'exit' для виходу): ");
                string input = Console.ReadLine() ?? "";

                if (input.ToLower() == "exit")
                    break;

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Введено порожній рядок!");
                    continue;
                }

                string processed = input.RemoveExtraSpaces();
                Console.WriteLine($"Результат: '{processed}'");

                // Додаткова інформація
                Console.WriteLine($"Довжина оригіналу: {input.Length} символів");
                Console.WriteLine($"Довжина після обробки: {processed.Length} символів");
                Console.WriteLine($"Видалено {input.Length - processed.Length} зайвих символів");
            }

            // Демонстрація використання в реальних сценаріях
            Console.WriteLine("\n=== ПРАКТИЧНІ ПРИКЛАДИ ===");

            string userInput = "   Іван    Петрович    Коваленко   ";
            string cleanedName = userInput.RemoveExtraSpaces();
            Console.WriteLine($"Введені дані: '{userInput}'");
            Console.WriteLine($"Для бази даних: '{cleanedName}'");

            string address = "м.   Київ,   вул.   Хрещатик,   буд.   25";
            string cleanedAddress = address.RemoveExtraSpaces();
            Console.WriteLine($"\nАдреса: '{address}'");
            Console.WriteLine($"Нормалізована адреса: '{cleanedAddress}'");

            string textWithTabs = "Текст\tз\tтабуляціями\t\tта\tпропусками";
            string cleanedText = textWithTabs.RemoveExtraSpaces();
            Console.WriteLine($"\nТекст з табуляціями: '{textWithTabs}'");
            Console.WriteLine($"Після обробки: '{cleanedText}'");
        }
    }
}
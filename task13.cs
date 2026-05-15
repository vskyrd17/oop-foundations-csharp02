using System;

namespace ComplexNumbers
{
    public class Complex
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public Complex() : this(0, 0) { }

        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        // Перевантаження оператора додавання
        public static Complex operator +(Complex c1, Complex c2)
        {
            return new Complex(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
        }

        // Перевантаження оператора віднімання
        public static Complex operator -(Complex c1, Complex c2)
        {
            return new Complex(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);
        }

        // Перевантаження оператора множення
        public static Complex operator *(Complex c1, Complex c2)
        {
            return new Complex(
                c1.Real * c2.Real - c1.Imaginary * c2.Imaginary,
                c1.Real * c2.Imaginary + c1.Imaginary * c2.Real
            );
        }

        // Перевантаження оператора ділення
        public static Complex operator /(Complex c1, Complex c2)
        {
            double denominator = c2.Real * c2.Real + c2.Imaginary * c2.Imaginary;
            return new Complex(
                (c1.Real * c2.Real + c1.Imaginary * c2.Imaginary) / denominator,
                (c1.Imaginary * c2.Real - c1.Real * c2.Imaginary) / denominator
            );
        }

        // Неявне перетворення до string
        public static implicit operator string(Complex c)
        {
            if (c.Imaginary == 0) return $"{c.Real}";
            if (c.Real == 0) return $"{c.Imaginary}i";

            string sign = c.Imaginary > 0 ? " + " : " - ";
            return $"{c.Real}{sign}{Math.Abs(c.Imaginary)}i";
        }

        public override string ToString() => (string)this;
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== КАЛЬКУЛЯТОР КОМПЛЕКСНИХ ЧИСЕЛ ===\n");

            // Введення першого комплексного числа
            Complex c1 = InputComplexNumber("перше");
            Complex c2 = InputComplexNumber("друге");

            Console.WriteLine($"\nПерше число: {c1}");
            Console.WriteLine($"Друге число: {c2}");

            // Виконання операцій
            Console.WriteLine("\n=== РЕЗУЛЬТАТИ ОПЕРАЦІЙ ===");
            Console.WriteLine($"Додавання: {c1} + {c2} = {c1 + c2}");
            Console.WriteLine($"Віднімання: {c1} - {c2} = {c1 - c2}");
            Console.WriteLine($"Множення: {c1} * {c2} = {c1 * c2}");

            if (c2.Real != 0 || c2.Imaginary != 0)
                Console.WriteLine($"Ділення: {c1} / {c2} = {c1 / c2}");
            else
                Console.WriteLine("Ділення: неможливо (ділення на нуль)");

            // Додаткові операції
            Console.WriteLine("\n=== ДОДАТКОВІ ОПЕРАЦІЇ ===");

            // Множення на дійсне число
            Console.Write("Введіть дійсне число для множення: ");
            double realMultiplier = double.Parse(Console.ReadLine() ?? "1");
            Console.WriteLine($"{c1} * {realMultiplier} = {c1 * new Complex(realMultiplier, 0)}");

            // Введення третього числа для комбінованих операцій
            Console.Write("\nБажаєте ввести третє число для комбінованих операцій? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                Complex c3 = InputComplexNumber("третє");
                Console.WriteLine($"\nКомбінована операція: ({c1} + {c2}) * {c3} = {(c1 + c2) * c3}");
            }
        }

        static Complex InputComplexNumber(string numberName)
        {
            Console.WriteLine($"\nВведіть {numberName} комплексне число:");

            Console.Write("Дійсна частина: ");
            double real = double.Parse(Console.ReadLine() ?? "0");

            Console.Write("Уявна частина: ");
            double imaginary = double.Parse(Console.ReadLine() ?? "0");

            return new Complex(real, imaginary);
        }
    }
}
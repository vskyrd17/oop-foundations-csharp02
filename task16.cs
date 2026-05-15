using System;

namespace QuadraticEquationClass
{
    /// <summary>
    /// Клас для представлення квадратного рівняння
    /// </summary>
    public class QuadraticEquation
    {
        public double A { get; }
        public double B { get; }
        public double C { get; }

        private double? _root1;
        private double? _root2;
        private int _rootsCount = -2; // -2 - ще не розв'язувалось

        /// <summary>
        /// Кількість коренів (-1 для безлічі розв'язків)
        /// </summary>
        public int RootsCount
        {
            get
            {
                if (_rootsCount == -2) Solve();
                return _rootsCount;
            }
        }

        /// <summary>
        /// Перший корінь (якщо існує)
        /// </summary>
        public double? Root1
        {
            get
            {
                if (_rootsCount == -2) Solve();
                return _root1;
            }
        }

        /// <summary>
        /// Другий корінь (якщо існує)
        /// </summary>
        public double? Root2
        {
            get
            {
                if (_rootsCount == -2) Solve();
                return _root2;
            }
        }

        /// <summary>
        /// Дискримінант рівняння
        /// </summary>
        public double Discriminant
        {
            get
            {
                if (A == 0) return double.NaN;
                return B * B - 4 * A * C;
            }
        }

        /// <summary>
        /// Індексатор для доступу до коренів за індексом
        /// </summary>
        public double? this[int index]
        {
            get
            {
                if (_rootsCount == -2) Solve();

                return index switch
                {
                    0 => _root1,
                    1 => _root2,
                    _ => throw new IndexOutOfRangeException("Індекс має бути 0 або 1")
                };
            }
        }

        public QuadraticEquation(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        private void Solve()
        {
            if (A == 0)
            {
                if (B == 0)
                {
                    _rootsCount = C == 0 ? -1 : 0;
                    return;
                }
                _rootsCount = 1;
                _root1 = -C / B;
                return;
            }

            double discriminant = B * B - 4 * A * C;

            if (discriminant < 0)
            {
                _rootsCount = 0;
            }
            else if (discriminant == 0)
            {
                _rootsCount = 1;
                _root1 = -B / (2 * A);
            }
            else
            {
                _rootsCount = 2;
                _root1 = (-B - Math.Sqrt(discriminant)) / (2 * A);
                _root2 = (-B + Math.Sqrt(discriminant)) / (2 * A);
            }
        }

        /// <summary>
        /// Обчислює значення рівняння для заданого x
        /// </summary>
        public double Calculate(double x)
        {
            return A * x * x + B * x + C;
        }

        /// <summary>
        /// Перевіряє, чи є число коренем рівняння
        /// </summary>
        public bool IsRoot(double x)
        {
            return Math.Abs(Calculate(x)) < 1e-10;
        }

        public override string ToString()
        {
            string equation = "";

            if (A != 0) equation += $"{A}x²";
            if (B != 0) equation += $" {(B > 0 ? '+' : '-')} {Math.Abs(B)}x";
            if (C != 0) equation += $" {(C > 0 ? '+' : '-')} {Math.Abs(C)}";

            if (string.IsNullOrEmpty(equation)) equation = "0";

            return equation + " = 0";
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== КЛАС КВАДРАТНОГО РІВНЯННЯ ===\n");

            // Демонстрація роботи з фіксованими рівняннями
            TestFixedEquations();

            // Робота з введенням від користувача
            TestUserInput();

            // Додаткові можливості
            TestAdditionalFeatures();
        }

        static void TestFixedEquations()
        {
            Console.WriteLine("1. ТЕСТУВАННЯ З ФІКСОВАНИМИ РІВНЯННЯМИ:\n");

            QuadraticEquation[] equations = {
                new QuadraticEquation(1, -3, 2),    // x² - 3x + 2 = 0
                new QuadraticEquation(1, -2, 1),    // x² - 2x + 1 = 0  
                new QuadraticEquation(1, 1, 1),     // x² + x + 1 = 0
                new QuadraticEquation(0, 2, -4),    // 2x - 4 = 0
                new QuadraticEquation(0, 0, 0),     // 0 = 0
                new QuadraticEquation(0, 0, 5),     // 5 = 0
                new QuadraticEquation(2, -5, -3)    // 2x² - 5x - 3 = 0
            };

            foreach (var eq in equations)
            {
                PrintEquationInfo(eq);
                Console.WriteLine();
            }
        }

        static void TestUserInput()
        {
            Console.WriteLine("2. ВВЕДЕННЯ ВІД КОРИСТУВАЧА:\n");

            while (true)
            {
                try
                {
                    Console.WriteLine("Введіть коефіцієнти квадратного рівняння ax² + bx + c = 0");

                    Console.Write("a: ");
                    double a = double.Parse(Console.ReadLine() ?? "0");

                    Console.Write("b: ");
                    double b = double.Parse(Console.ReadLine() ?? "0");

                    Console.Write("c: ");
                    double c = double.Parse(Console.ReadLine() ?? "0");

                    QuadraticEquation userEq = new QuadraticEquation(a, b, c);

                    Console.WriteLine("\nРезультат:");
                    PrintEquationInfo(userEq);

                    Console.Write("\nБажаєте продовжити? (y/n): ");
                    if (Console.ReadLine()?.ToLower() != "y")
                        break;

                    Console.WriteLine();
                }
                catch (FormatException)
                {
                    Console.WriteLine("Помилка: введіть коректне число!\n");
                }
            }
        }

        static void TestAdditionalFeatures()
        {
            Console.WriteLine("\n3. ДОДАТКОВІ МОЖЛИВОСТІ:\n");

            // Перевірка коренів
            QuadraticEquation eq = new QuadraticEquation(1, -5, 6); // x² - 5x + 6 = 0

            Console.WriteLine($"Рівняння: {eq}");
            Console.WriteLine($"Корені: {eq.Root1}, {eq.Root2}");

            Console.WriteLine("\nПеревірка чисел на корені:");
            double[] testValues = { 2, 3, 4, 0, 1 };
            foreach (double x in testValues)
            {
                bool isRoot = eq.IsRoot(x);
                double value = eq.Calculate(x);
                Console.WriteLine($"x = {x}: {value:F4} - {(isRoot ? "корінь" : "не корінь")}");
            }

            // Використання індексатора
            Console.WriteLine("\nДоступ через індексатор:");
            Console.WriteLine($"eq[0] = {eq[0]}");
            Console.WriteLine($"eq[1] = {eq[1]}");

            try
            {
                Console.WriteLine($"eq[2] = {eq[2]}");
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            // "Ліниве" обчислення
            Console.WriteLine("\n4. 'ЛІНИВЕ' ОБЧИСЛЕННЯ:");
            QuadraticEquation lazyEq = new QuadraticEquation(1, -4, 4);
            Console.WriteLine($"Створено рівняння: {lazyEq}");
            Console.WriteLine("Корені ще не обчислювались...");
            Console.WriteLine($"Звертаємось до властивості RootsCount: {lazyEq.RootsCount}");
            Console.WriteLine($"Тепер корені обчислені: {lazyEq.Root1}, {lazyEq.Root2}");
        }

        static void PrintEquationInfo(QuadraticEquation eq)
        {
            Console.WriteLine($"Рівняння: {eq}");
            Console.WriteLine($"Дискримінант: {(double.IsNaN(eq.Discriminant) ? "не визначений" : eq.Discriminant.ToString("F2"))}");
            Console.WriteLine($"Кількість коренів: {GetRootsCountDescription(eq.RootsCount)}");

            if (eq.RootsCount == 1)
            {
                Console.WriteLine($"Корінь: x = {eq.Root1:F2}");
            }
            else if (eq.RootsCount == 2)
            {
                Console.WriteLine($"Корені: x₁ = {eq.Root1:F2}, x₂ = {eq.Root2:F2}");
            }

            // Демонстрація індексатора
            if (eq.RootsCount > 0)
            {
                Console.WriteLine($"Через індексатор: [0] = {eq[0]:F2}" +
                                 (eq.RootsCount == 2 ? $", [1] = {eq[1]:F2}" : ""));
            }
        }

        static string GetRootsCountDescription(int count)
        {
            return count switch
            {
                -1 => "безліч розв'язків",
                0 => "немає дійсних коренів",
                1 => "один корінь",
                2 => "два корені",
                _ => "невідомо"
            };
        }
    }
}
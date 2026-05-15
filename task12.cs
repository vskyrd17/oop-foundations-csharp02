using System;

namespace QuadraticEquation
{
    // Структура для зберігання результатів
    public struct QuadraticSolution
    {
        public int RootsCount;
        public double? Root1;
        public double? Root2;
    }

    public static class QuadraticSolver
    {
        // Спосіб 1: з out параметрами
        public static int SolveWithOut(double a, double b, double c, out double? x1, out double? x2)
        {
            x1 = null;
            x2 = null;

            if (a == 0)
            {
                if (b == 0)
                    return c == 0 ? -1 : 0; // безліч розв'язків або жодного

                x1 = -c / b;
                return 1;
            }

            double discriminant = b * b - 4 * a * c;

            if (discriminant < 0) return 0;

            if (discriminant == 0)
            {
                x1 = -b / (2 * a);
                return 1;
            }

            x1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            x2 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            return 2;
        }

        // Спосіб 2: з структурою
        public static QuadraticSolution SolveWithStruct(double a, double b, double c)
        {
            var solution = new QuadraticSolution();

            if (a == 0)
            {
                if (b == 0)
                {
                    solution.RootsCount = c == 0 ? -1 : 0;
                    return solution;
                }
                solution.RootsCount = 1;
                solution.Root1 = -c / b;
                return solution;
            }

            double discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                solution.RootsCount = 0;
            }
            else if (discriminant == 0)
            {
                solution.RootsCount = 1;
                solution.Root1 = -b / (2 * a);
            }
            else
            {
                solution.RootsCount = 2;
                solution.Root1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                solution.Root2 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            }

            return solution;
        }

        // Спосіб 3: з кортежем
        public static (int rootsCount, double? root1, double? root2) SolveWithTuple(double a, double b, double c)
        {
            if (a == 0)
            {
                if (b == 0)
                    return (c == 0 ? -1 : 0, null, null);

                return (1, -c / b, null);
            }

            double discriminant = b * b - 4 * a * c;

            if (discriminant < 0) return (0, null, null);

            if (discriminant == 0) return (1, -b / (2 * a), null);

            double root1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            double root2 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            return (2, root1, root2);
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Тестування різних рівнянь
            double[,] equations = {
                {1, -3, 2},    // x² - 3x + 2 = 0 (2 корені)
                {1, -2, 1},    // x² - 2x + 1 = 0 (1 корінь)
                {1, 1, 1},     // x² + x + 1 = 0 (немає коренів)
                {0, 2, -4},    // 2x - 4 = 0 (лінійне)
                {0, 0, 0},     // 0 = 0 (безліч розв'язків)
                {0, 0, 5}      // 5 = 0 (немає розв'язків)
            };

            for (int i = 0; i < equations.GetLength(0); i++)
            {
                double a = equations[i, 0];
                double b = equations[i, 1];
                double c = equations[i, 2];

                Console.WriteLine($"\nРівняння {i + 1}: {a}x² + {b}x + {c} = 0");

                // Спосіб 1: out параметри
                int count1 = QuadraticSolver.SolveWithOut(a, b, c, out double? x1, out double? x2);
                PrintResults("Метод 1 (out)", count1, x1, x2);

                // Спосіб 2: структура
                var solution = QuadraticSolver.SolveWithStruct(a, b, c);
                PrintResults("Метод 2 (struct)", solution.RootsCount, solution.Root1, solution.Root2);

                // Спосіб 3: кортеж
                var (count3, root1, root2) = QuadraticSolver.SolveWithTuple(a, b, c);
                PrintResults("Метод 3 (tuple)", count3, root1, root2);

                Console.WriteLine(new string('-', 50));
            }

            // Демонстрація з введенням від користувача
            Console.WriteLine("\n=== РОЗВ'ЯЗАННЯ ВАШОГО РІВНЯННЯ ===");

            Console.Write("Введіть a: ");
            double userA = double.Parse(Console.ReadLine() ?? "0");

            Console.Write("Введіть b: ");
            double userB = double.Parse(Console.ReadLine() ?? "0");

            Console.Write("Введіть c: ");
            double userC = double.Parse(Console.ReadLine() ?? "0");

            var userResult = QuadraticSolver.SolveWithTuple(userA, userB, userC);
            PrintResults("Ваше рівняння", userResult.rootsCount, userResult.root1, userResult.root2);
        }

        static void PrintResults(string method, int count, double? x1, double? x2)
        {
            string result = count switch
            {
                -1 => "Безліч розв'язків",
                0 => "Немає дійсних коренів",
                1 => $"Один корінь: x = {x1:F2}",
                2 => $"Два корені: x₁ = {x1:F2}, x₂ = {x2:F2}",
                _ => "Невідомий результат"
            };

            Console.WriteLine($"{method}: {result}");
        }
    }
}
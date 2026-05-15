using System;

namespace Geometry3D
{

    public struct Point3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        /// Обчислює відстань від точки до початку координат
        public double DistanceToOrigin()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }
        /// Обчислює відстань між двома точками
        public double DistanceTo(Point3D other)
        {
            double dx = X - other.X;
            double dy = Y - other.Y;
            double dz = Z - other.Z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        /// <summary>
        /// Переміщує точку на заданий вектор
        /// </summary>
        public void Move(double dx, double dy, double dz)
        {
            X += dx;
            Y += dy;
            Z += dz;
        }

        /// <summary>
        /// Повертає точку, симетричну відносно початку координат
        /// </summary>
        public Point3D Symmetric()
        {
            return new Point3D(-X, -Y, -Z);
        }

        public override string ToString() => $"({X:F2}, {Y:F2}, {Z:F2})";
    }
    public static class Point3DTester
    {
        public static void Test()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== ТЕСТУВАННЯ 3D-ТОЧКИ ===\n");

            // Тестування з фіксованими точками
            TestFixedPoints();

            // Робота з введенням від користувача
            TestUserInput();

            // Додаткові тести
            TestAdditionalOperations();
        }

        static void TestFixedPoints()
        {
            Console.WriteLine("1. ТЕСТУВАННЯ З ФІКСОВАНИМИ ТОЧКАМИ:\n");

            Point3D point1 = new Point3D(3, 4, 5);
            Point3D point2 = new Point3D(1, 2, 3);
            Point3D origin = new Point3D(0, 0, 0);

            Console.WriteLine($"Точка 1: {point1}");
            Console.WriteLine($"Точка 2: {point2}");
            Console.WriteLine($"Початок координат: {origin}");

            Console.WriteLine($"\nВідстань від точки 1 до початку: {point1.DistanceToOrigin():F2}");
            Console.WriteLine($"Відстань від точки 2 до початку: {point2.DistanceToOrigin():F2}");
            Console.WriteLine($"Відстань між точками 1 і 2: {point1.DistanceTo(point2):F2}");

            // Симетричні точки
            Point3D symmetric1 = point1.Symmetric();
            Console.WriteLine($"\nСиметрична точка до {point1}: {symmetric1}");
            Console.WriteLine($"Відстань симетричної точки до початку: {symmetric1.DistanceToOrigin():F2}");

            Point3D movedPoint = point2;
            movedPoint.Move(2, -1, 3);
            Console.WriteLine($"\nТочка 2 після переміщення на (2, -1, 3): {movedPoint}");
            Console.WriteLine($"Нова відстань між точками: {point1.DistanceTo(movedPoint):F2}");
        }

        static void TestUserInput()
        {
            Console.WriteLine("\n2. ВВЕДЕННЯ ВІД КОРИСТУВАЧА:\n");

            Point3D point1 = InputPoint("першу");
            Point3D point2 = InputPoint("другу");

            Console.WriteLine($"\nРезультати для введених точок:");
            Console.WriteLine($"Точка 1: {point1}");
            Console.WriteLine($"Точка 2: {point2}");
            Console.WriteLine($"Відстань між точками: {point1.DistanceTo(point2):F2}");
            Console.WriteLine($"Відстань точки 1 до початку: {point1.DistanceToOrigin():F2}");
            Console.WriteLine($"Відстань точки 2 до початку: {point2.DistanceToOrigin():F2}");

            // Додаткові операції
            Console.Write($"\nБажаєте перемістити точку 1? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                Console.Write("Введіть dx: ");
                double dx = double.Parse(Console.ReadLine() ?? "0");
                Console.Write("Введіть dy: ");
                double dy = double.Parse(Console.ReadLine() ?? "0");
                Console.Write("Введіть dz: ");
                double dz = double.Parse(Console.ReadLine() ?? "0");

                Point3D moved = point1;
                moved.Move(dx, dy, dz);
                Console.WriteLine($"Точка після переміщення: {moved}");
                Console.WriteLine($"Нова відстань до точки 2: {moved.DistanceTo(point2):F2}");
            }
        }

        static void TestAdditionalOperations()
        {
            Console.WriteLine("\n3. ДОДАТКОВІ ТЕСТИ:\n");

            // Точки для утворення трикутника
            Point3D A = new Point3D(0, 0, 0);
            Point3D B = new Point3D(4, 0, 0);
            Point3D C = new Point3D(0, 3, 0);

            Console.WriteLine($"Трикутник ABC:");
            Console.WriteLine($"A{A}, B{B}, C{C}");

            double ab = A.DistanceTo(B);
            double bc = B.DistanceTo(C);
            double ca = C.DistanceTo(A);

            Console.WriteLine($"Сторони: AB = {ab:F2}, BC = {bc:F2}, CA = {ca:F2}");
            Console.WriteLine($"Периметр: {ab + bc + ca:F2}");

            // Точка в просторі
            Point3D spacePoint = new Point3D(1, 2, 3);
            Console.WriteLine($"\nТочка в просторі: {spacePoint}");
            Console.WriteLine($"Її відстань до кожної вершини трикутника:");
            Console.WriteLine($"До A: {spacePoint.DistanceTo(A):F2}");
            Console.WriteLine($"До B: {spacePoint.DistanceTo(B):F2}");
            Console.WriteLine($"До C: {spacePoint.DistanceTo(C):F2}");
        }

        static Point3D InputPoint(string pointName)
        {
            Console.WriteLine($"Введіть {pointName} точку:");

            Console.Write("Координата X: ");
            double x = double.Parse(Console.ReadLine() ?? "0");

            Console.Write("Координата Y: ");
            double y = double.Parse(Console.ReadLine() ?? "0");

            Console.Write("Координата Z: ");
            double z = double.Parse(Console.ReadLine() ?? "0");

            return new Point3D(x, y, z);
        }
    }

    class Program
    {
        static void Main()
        {
            Point3DTester.Test();
        }
    }
}
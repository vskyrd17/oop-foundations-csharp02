using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentGroupApp
{
    public class Address
    {
        public string City { get; set; } = "";
        public string Street { get; set; } = "";
        public string HouseNumber { get; set; } = "";
    }

    public class Student
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public int Age { get; set; }
        public string StudentId { get; set; } = "";
        public double AverageGrade { get; set; }
        public Address Address { get; set; } = new Address();
    }

    public class Group
    {
        public string GroupName { get; set; } = "";
        public List<Student> Students { get; set; } = new List<Student>();

        public void AddStudent(Student student) => Students.Add(student);

        public List<Student> FindStudentsByName(string name)
        {
            return Students.Where(s =>
                s.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                s.LastName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void SortByGrade() => Students.Sort((s1, s2) => s2.AverageGrade.CompareTo(s1.AverageGrade));

        public double GetAverageGrade() => Students.Count > 0 ? Students.Average(s => s.AverageGrade) : 0;
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Створення групи
            var group = new Group { GroupName = "КН-401" };

            // Додавання студентів
            group.AddStudent(new Student
            {
                FirstName = "Іван",
                LastName = "Петренко",
                Age = 20,
                StudentId = "KN40101",
                AverageGrade = 85.5,
                Address = new Address { City = "Київ", Street = "Хрещатик", HouseNumber = "25" }
            });

            group.AddStudent(new Student
            {
                FirstName = "Марія",
                LastName = "Коваленко",
                Age = 19,
                StudentId = "KN40102",
                AverageGrade = 92.0,
                Address = new Address { City = "Львів", Street = "Шевченка", HouseNumber = "15" }
            });

            group.AddStudent(new Student
            {
                FirstName = "Олександр",
                LastName = "Мельник",
                Age = 21,
                StudentId = "KN40103",
                AverageGrade = 78.3,
                Address = new Address { City = "Київ", Street = "Лісова", HouseNumber = "8" }
            });

            // Виведення інформації
            Console.WriteLine($"Група: {group.GroupName}");
            Console.WriteLine($"Кількість студентів: {group.Students.Count}");
            Console.WriteLine($"Середній бал: {group.GetAverageGrade():F2}\n");

            // Пошук студентів з Києва
            Console.WriteLine("Студенти з Києва:");
            foreach (var student in group.Students.Where(s => s.Address.City == "Київ"))
            {
                Console.WriteLine($"- {student.LastName} {student.FirstName}");
            }

            // Сортування за балом
            Console.WriteLine("\nТоп студенти:");
            group.SortByGrade();
            foreach (var student in group.Students.Take(3))
            {
                Console.WriteLine($"- {student.LastName} {student.FirstName}: {student.AverageGrade}");
            }

            // Пошук за іменем (введення з клавіатури)
            Console.Write("\nВведіть ім'я для пошуку: ");
            string searchName = Console.ReadLine() ?? "";

            var found = group.FindStudentsByName(searchName);
            Console.WriteLine($"Знайдено: {found.Count} студент(ів)");
            foreach (var student in found)
            {
                Console.WriteLine($"- {student.LastName} {student.FirstName}");
            }
        }
    }
}
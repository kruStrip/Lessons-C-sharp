using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentPerformanceAnalysis
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Group { get; set; }
        public int[] Grades { get; set; }

        public Student(string firstName, string lastName, string group, int[] grades)
        {
            FirstName = firstName;
            LastName = lastName;
            Group = group;
            Grades = grades;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>
            {
                new Student("Иван", "Иванов", "A1", new []{5, 4, 3, 5}),
                new Student("Пётр", "Петров", "A1", new []{4, 4, 4, 5}),
                new Student("Светлана", "Сидорова", "B1", new []{5, 5, 5, 5}),
                new Student("Мария", "Кузнецова", "B1", new []{3, 3, 4, 4}),
                new Student("Алексей", "Смирнов", "A2", new []{2, 3, 3, 4}),
                new Student("Ольга", "Попова", "A2", new []{5, 4, 5, 4}),
                new Student("Дмитрий", "Васильев", "C1", new []{4, 4, 4, 4}),
                new Student("Елена", "Морозова", "C1", new []{5, 3, 4, 5}),
                new Student("Николай", "Лебедев", "B2", new []{3, 4, 2, 3}),
                new Student("Татьяна", "Новикова", "B2", new []{5, 5, 4, 5}),
                new Student("Сергей", "Козлов", "C2", new []{4, 2, 3, 4}),
                new Student("Анна", "Павлова", "C2", new []{5, 5, 5, 4}),
                new Student("Владимир", "Соколов", "A1", new []{3, 4, 5, 4}),
                new Student("Ирина", "Мельникова", "A2", new []{4, 4, 3, 2}),
                new Student("Григорий", "Орлов", "B1", new []{5, 5, 5, 5})
            };
            Console.WriteLine("Приветстую!!!");
            bool status = true;

            while (status)
            {
                Console.Write(
                    "Выберите действие:\n" +
                    "1. Студенты со средним баллом выше 4\n" +
                    "2. Сортировать студентов по фамилии и имени\n" +
                    "3. Имена студентов с оценкой 5\n" +
                    "4. Средний балл по группам\n" +
                    "5. Студенты из группы с средним > 4 (только фамилии)\n" +
                    "6. Выход\n" +
                    "Выбор: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.\n");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        var highAvg = students.Where(s => s.Grades.Average() > 4);
                        Console.WriteLine("Студенты со средним баллом > 4:");
                        foreach (var s in highAvg)
                            Console.WriteLine($"{s.FirstName} {s.LastName} - Ср.балл: {s.Grades.Average():F2}");
                        break;
                    case 2:
                        var sorted = students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName);
                        Console.WriteLine("Студенты, отсортированные по фамилии и имени:");
                        foreach (var s in sorted)
                            Console.WriteLine($"{s.LastName} {s.FirstName}");
                        break;
                    case 3:
                        var withFive = students.Where(s => s.Grades.Contains(5)).Select(s => s.FirstName);
                        Console.WriteLine("Имена студентов, имеющих оценку 5:");
                        foreach (var name in withFive)
                            Console.WriteLine(name);
                        break;
                    case 4:
                        var groupAvg = students.GroupBy(s => s.Group)
                            .Select(g => new { Group = g.Key, Avg = g.Average(s => s.Grades.Average()) });
                        Console.WriteLine("Средний балл по группам:");
                        foreach (var g in groupAvg)
                            Console.WriteLine($"Группа {g.Group}: {g.Avg:F2}");
                        break;
                    case 5:
                        Console.Write("Введите группу: ");
                        var grp = Console.ReadLine();
                        var filtered = students
                            .Where(s => s.Group.Equals(grp, StringComparison.OrdinalIgnoreCase)
                                        && s.Grades.Average() > 4)
                            .OrderBy(s => s.FirstName)
                            .Select(s => s.LastName);
                        Console.WriteLine($"Студенты группы {grp} со ср.баллом > 4 (фамилии):");
                        foreach (var last in filtered)
                            Console.WriteLine(last);
                        break;
                    case 6:
                        status = false;
                        Console.WriteLine("До свидания!!!");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}

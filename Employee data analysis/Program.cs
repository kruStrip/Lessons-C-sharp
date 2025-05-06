using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EmployeeAnalysis
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public List<string> Projects { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {FirstName} {LastName} | Dept: {Department} | Pos: {Position} | Salary: {Salary} | Projects: {string.Join(", ", Projects)}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string xmlPath = "C:\\dop.files\\Employees.xml";

            XDocument xdoc = XDocument.Load(xmlPath);

            // Сотрудники отдела IT
            var itEmployees = xdoc
                .Descendants("Employee")
                .Where(e => (string)e.Element("Department") == "IT");
            Console.WriteLine("Сотрудники отдела IT:");
            foreach (var e in itEmployees)
                Console.WriteLine($"- {e.Element("FirstName").Value} {e.Element("LastName").Value}");
            Console.WriteLine();

            // Сотрудники, работающие более чем над одним проектом
            var multiProjectEmployees = xdoc
                .Descendants("Employee")
                .Where(e => e.Element("Projects").Elements("Project").Count() > 1);
            Console.WriteLine("Сотрудники с более чем одним проектом:");
            foreach (var e in multiProjectEmployees)
                Console.WriteLine($"- {e.Element("FirstName").Value} {e.Element("LastName").Value} ({e.Element("Projects").Elements("Project").Count()} проектов)");
            Console.WriteLine();

            // Добавление нового сотрудника
            var newEmp = new XElement("Employee",
                new XAttribute("Id", 16),
                new XElement("FirstName", "Ivan"),
                new XElement("LastName", "Petrov"),
                new XElement("Department", "IT"),
                new XElement("Position", "Intern"),
                new XElement("Salary", 2000),
                new XElement("Projects",
                    new XElement("Project", "Helpdesk")
                )
            );
            xdoc.Root.Add(newEmp);

            // Обновление должности существующего сотрудника
            var empToUpdate = xdoc
                .Descendants("Employee")
                .FirstOrDefault(e => (int)e.Attribute("Id") == 2);
            if (empToUpdate != null)
                empToUpdate.Element("Position").Value = "Senior Developer";

            // Удаление сотрудника
            var empToDelete = xdoc
                .Descendants("Employee")
                .FirstOrDefault(e => (int)e.Attribute("Id") == 4);
            empToDelete?.Remove();

            // Сохранение изменений
            xdoc.Save(xmlPath);
            Console.WriteLine($"Измененный XML сохранен");
            Console.WriteLine();

            // Преобразование XML
            List<Employee> employees = xdoc
                .Descendants("Employee")
                .Select(e => new Employee
                {
                    Id = (int)e.Attribute("Id"),
                    FirstName = (string)e.Element("FirstName"),
                    LastName = (string)e.Element("LastName"),
                    Department = (string)e.Element("Department"),
                    Position = (string)e.Element("Position"),
                    Salary = (decimal)e.Element("Salary"),
                    Projects = e.Element("Projects").Elements("Project").Select(p => (string)p).ToList()
                })
                .ToList();

            Console.WriteLine("Список объектов Employee:");
            foreach (var emp in employees)
                Console.WriteLine(emp);
        }
    }
}

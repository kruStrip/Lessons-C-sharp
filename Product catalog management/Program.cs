using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ProductCatalogManagement
{
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var catalog = XDocument.Load("C:\\dop.files\\Catalog.xml");

            // Из категории "Electronics"
            var electronics = catalog
                .Descendants("Product")
                .Where(p => (string)p.Element("Category") == "Electronics");
            Console.WriteLine("Продукты в категории Electronics:");
            foreach (var p in electronics)
                Console.WriteLine($"- {(string)p.Element("Name")} ({(decimal)p.Element("Price")} у.е.)");
            Console.WriteLine();

            // Доступные продукты
            var available = catalog
                .Descendants("Product")
                .Where(p => (bool)p.Element("Available"));
            Console.WriteLine("Доступные для покупки продукты:");
            foreach (var p in available)
                Console.WriteLine($"- {(string)p.Element("Name")} [{(string)p.Element("Category")}]");
            Console.WriteLine();

            // Дешевле 1000
            var cheap = catalog
                .Descendants("Product")
                .Where(p => (decimal)p.Element("Price") < 1000m);
            Console.WriteLine("Товары дешевле 1000:");
            foreach (var p in cheap)
                Console.WriteLine($"- {(string)p.Element("Name")} - {(decimal)p.Element("Price")} у.е.");
            Console.WriteLine();

            // Добавление нового товара
            var nextId = catalog.Descendants("Product")
                .Select(p => (int)p.Attribute("Id")).DefaultIfEmpty(0).Max() + 1;
            var newProduct = new XElement("Product",
                new XAttribute("Id", nextId),
                new XElement("Name", "Bluetooth Speaker"),
                new XElement("Category", "Electronics"),
                new XElement("Price", 150m),
                new XElement("Available", true),
                new XElement("Attributes",
                    new XElement("Color", "Black"),
                    new XElement("BatteryLife", "10h")
                )
            );
            catalog.Root.Add(newProduct);
            Console.WriteLine("Добавлен новый продукт Bluetooth Speaker.");

            // Обновление цены существующего товара
            var laptop = catalog.Descendants("Product")
                .FirstOrDefault(p => (string)p.Element("Name") == "Laptop");
            if (laptop != null)
            {
                laptop.Element("Price").Value = "1100";
                Console.WriteLine("Цена товара Laptop обновлена на 1100.");
            }

            // Удаление товара
            var blender = catalog.Descendants("Product")
                .FirstOrDefault(p => (string)p.Element("Name") == "Blender");
            if (blender != null)
            {
                blender.Remove();
                Console.WriteLine("Товар Blender удалён из каталога.");
            }

            catalog.Save("C:\\dop.files\\Catalog.xml");
            Console.WriteLine();

            // Преобразование XML в C#
            var products = catalog.Descendants("Product")
                .Select(p => new Product
                {
                    Name = (string)p.Element("Name"),
                    Category = (string)p.Element("Category"),
                    Price = (decimal)p.Element("Price"),
                    Available = (bool)p.Element("Available"),
                    Attributes = p.Element("Attributes").Elements()
                        .ToDictionary(a => a.Name.LocalName, a => (string)a)
                })
                .ToList();

            Console.WriteLine($"Преобразовано в объекты Product. Всего продуктов: {products.Count}");
        }
    }
}

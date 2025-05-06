using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
}

// Класс AppDbContext наследуется от DbContext.
// DbContext — это основной класс в Entity Framework Core, который управляет взаимодействием с базой данных.
// Он предоставляет методы для работы с таблицами и записями в базе данных.
public class AppDbContext : DbContext
{
    // DbSet<Product> Products — это свойство, которое представляет таблицу "Products" в базе данных.
    // DbSet<T> — это коллекция объектов типа T, которая соответствует строкам в таблице базы данных.
    // В данном случае, Product — это класс, который описывает структуру данных для каждой строки в таблице.
    public DbSet<Product> Products { get; set; }

    // Метод OnConfiguring вызывается автоматически при создании экземпляра класса AppDbContext.
    // Он используется для настройки подключения к базе данных.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseNpgsql(...) — этот метод указывает, что мы будем использовать PostgreSQL (через библиотеку Npgsql).
        // Строка подключения содержит информацию, необходимую для подключения к базе данных:
        // - Host=localhost: указывает, что база данных находится на локальном компьютере (localhost).
        //   Если база данных находится на удаленном сервере, здесь нужно указать IP-адрес или доменное имя сервера.
        // - Database=postgres: указывает имя базы данных, к которой мы хотим подключиться.
        //   В данном случае база данных называется "postgres".
        // - Username=postgres: указывает имя пользователя базы данных.
        //   По умолчанию в PostgreSQL часто используется пользователь "postgres".
        // - Password=12345: указывает пароль для пользователя "postgres".
        //   Убедитесь, что пароль совпадает с тем, который вы установили при настройке PostgreSQL.
        optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=z0m4g");
    }
}

class Program
{
    static void Main()
    {
        // Создаем экземпляр класса AppDbContext, который управляет подключением к базе данных.
        var context = new AppDbContext();

        // Метод EnsureCreated() проверяет, существует ли уже таблица Products в базе данных.
        // Если таблица еще не создана, она будет автоматически создана на основе модели Product.
        context.Database.EnsureCreated();

        // Добавляем новые записи в таблицу Products с помощью метода AddRange().
        context.Products.AddRange(
            new Product { Id = 1, Name = "Phone", Price = 800 },
            new Product { Id = 2, Name = "Laptop", Price = 1200 },
            new Product { Id = 3, Name = "Tablet", Price = 600 }
        );

        // Метод SaveChanges() сохраняет все изменения, которые мы сделали в контексте базы данных (например, добавление новых записей).
        // До вызова этого метода все изменения существуют только в памяти программы, но не в самой базе данных.
        context.SaveChanges();

        Console.WriteLine("Продукты добавлены.");

        // Используем метод ToList(), чтобы получить все строки таблицы в виде списка объектов типа Product.
        Console.WriteLine("Все продукты:");
        var products = context.Products.ToList();

        foreach (var pr in products)
        {
            Console.WriteLine($"{pr.Id}: {pr.Name} - {pr.Price}");
        }

        Console.WriteLine("\nПоиск продукта по имени:");

        // Используем метод FirstOrDefault() для поиска первого продукта с именем "Phone".
        var product = context.Products.FirstOrDefault(p => p.Name == "Phone");

        if (product != null)
        {
            Console.WriteLine($"Найден продукт: {product.Name} - {product.Price}");
        }
        else
        {
            Console.WriteLine("Продукт не найден.");
        }

        Console.WriteLine("\nПродукты дороже 700:");

        // Используем метод Where() для фильтрации продуктов.
        var filteredProducts = context.Products.Where(p => p.Price > 700).ToList();

        foreach (var pr in filteredProducts)
        {
            Console.WriteLine($"{pr.Name} - {pr.Price}");
        }

        Console.WriteLine("\nОбновление цены продукта:");

        // Ищем продукт с Id = 1 с помощью метода Find().
        // Этот метод ищет объект в базе данных по его первичному ключу (в данном случае Id).
        product = context.Products.Find(1);

        if (product != null)
        {
            product.Price = 900;

            // Сохраняем изменения в базе данных с помощью метода SaveChanges().
            // Без этого метода изменения останутся только в памяти программы, но не будут записаны в базу данных.
            context.SaveChanges();

            Console.WriteLine($"Цена продукта {product.Name} обновлена до {900}.");
        }
        else
        {
            Console.WriteLine("Продукт для обновления не найден.");
        }

        products = context.Products.ToList();

        foreach (var pr in products)
        {
            Console.WriteLine($"{pr.Id}: {pr.Name} - {pr.Price}");
        }

        Console.WriteLine("\nУдаление продукта:");

        // Ищем продукт с Id = 2 с помощью метода Find().
        product = context.Products.Find(2);

        if (product != null)
        {
            // Если продукт найден, удаляем его из базы данных с помощью метода Remove().
            context.Products.Remove(product);

            // Сохраняем изменения в базе данных с помощью метода SaveChanges().
            context.SaveChanges();

            Console.WriteLine($"Продукт {product.Name} удален.");
        }
        else
        {
            Console.WriteLine("Продукт для удаления не найден.");
        }

        products = context.Products.ToList();
        foreach (var pr in products)
        {
            Console.WriteLine($"{pr.Id}: {pr.Name} - {pr.Price}");
        }

        Console.WriteLine("\nСредняя цена всех продуктов:");

        // Используем метод Average() для вычисления средней цены всех продуктов.
        var averagePrice = context.Products.Average(p => p.Price);
        Console.WriteLine($"Средняя цена: {averagePrice:F2}");

        Console.WriteLine("\nОчистка таблицы:");

        // Используем метод RemoveRange(), чтобы удалить все продукты из таблицы.
        // context.Products — это коллекция всех продуктов в базе данных.
        context.Products.RemoveRange(context.Products);

        // Сохраняем изменения в базе данных с помощью метода SaveChanges().
        context.SaveChanges();

        Console.WriteLine("Таблица очищена.");

        products = context.Products.ToList();
        foreach (var pr in products)
        {
            Console.WriteLine($"{pr.Id}: {pr.Name} - {pr.Price}");
        }
    }
}

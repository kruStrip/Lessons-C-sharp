using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string Genre { get; set; } = string.Empty;

    public int PublicationYear { get; set; }

    public int Pages { get; set; }

    public double Price { get; set; }

    public bool IsAvailable { get; set; }

    public double Rating { get; set; }

    public string? Description { get; set; }
}

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=z0m4g");
    }
}

class Program
{
    static void Main()
    {
        var context = new AppDbContext();
        context.Database.EnsureCreated();

        Console.WriteLine("\nОчистка таблицы:");

        // Используем метод RemoveRange(), чтобы удалить все продукты из таблицы.
        // context.Products — это коллекция всех продуктов в базе данных.
        context.Books.RemoveRange(context.Books);

        // Сохраняем изменения в базе данных с помощью метода SaveChanges().
        context.SaveChanges();

        Console.WriteLine("Таблица очищена.");

        // добавление записей
        context.Books.AddRange(
            new Book
            {
                Id = 1,
                Title = "1984",
                Author = "George Orwell",
                Genre = "Dystopian",
                PublicationYear = 1949,
                Pages = 328,
                IsAvailable = true,
                Price = 1000,
                Rating = 4.8,
                Description = "A classic novel about surveillance and totalitarianism."
            },
            new Book
            {
                Id = 2,
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                Genre = "Fiction",
                PublicationYear = 1960,
                Pages = 281,
                IsAvailable = true,
                Price = 1500,
                Rating = 4.7,
                Description = "A powerful story of race and justice in the American South."
            },
            new Book
            {
                Id = 3,
                Title = "The Hobbit",
                Author = "J.R.R. Tolkien",
                Genre = "Fantasy",
                PublicationYear = 1937,
                Pages = 310,
                IsAvailable = false,
                Price = 2000,
                Rating = 4.9,
                Description = "The prelude to The Lord of the Rings."
            }
        );
        context.SaveChanges();
        Console.WriteLine("Книги добавлены.");

        // вывод всех книг
        Console.WriteLine("\nВсе книги:");
        var books = context.Books.ToList();
        foreach (var bk in books)
        {
            Console.WriteLine($"ID: {bk.Id} | \"{bk.Title}\" — {bk.Author} ({bk.PublicationYear})");
            Console.WriteLine($"Жанр: {bk.Genre}, Страниц: {bk.Pages}, Доступна: {bk.IsAvailable}, Цена: {bk.Price}, Рейтинг: {bk.Rating}");
            if (!string.IsNullOrWhiteSpace(bk.Description))
                Console.WriteLine($"Описание: {bk.Description}");
            Console.WriteLine();
        }

        // поиск по Id
        Console.WriteLine("\nПоиск книги по Id:");
        if (int.TryParse(Console.ReadLine(), out int findId))
        {
            var bookById = context.Books.FirstOrDefault(p => p.Id == findId);
            if (bookById != null)
            {
                Console.WriteLine($"Найденная книга: «{bookById.Title}» — {bookById.Author}");
            }
            else
            {
                Console.WriteLine("Книга не найдена.");
            }
                
        }
            

        // поиск по автору, жанру или году
        Console.WriteLine("\nВведите автора, жанр или год для поиска:");
        var text = Console.ReadLine();
        int year;
        bool isYear = int.TryParse(text, out year);
        var filteredBooks = context.Books
            .Where(p =>
                p.Author == text ||
                p.Genre == text ||
                (isYear && p.PublicationYear == year));
        Console.WriteLine("Результаты поиска:");
        if (!filteredBooks.Any())
        {
            Console.WriteLine("Книги не найдены.");
        }
        else
        {
            foreach (var pr in filteredBooks)
                Console.WriteLine($"«{pr.Title}» — {pr.Price}");
        }


        // фильтрация по цене
        Console.WriteLine("\nВведите максимальную цену:");
        var priceInput = Console.ReadLine();
        if (double.TryParse(priceInput, out double maxPrice))
        {
            var booksByPrice = context.Books
                .Where(p => p.Price <= maxPrice)
                .OrderByDescending(p => p.Price)
                .ToList();
            Console.WriteLine("Книги по заданной цене:");
            foreach (var pr in booksByPrice)
                Console.WriteLine($"«{pr.Title}» — {pr.Price}");
        }

        // обновление книги
        Console.WriteLine("\nВведите Id книги для обновления:");
        if (int.TryParse(Console.ReadLine(), out int updateId))
        {
            var bookToUpdate = context.Books.Find(updateId);
            if (bookToUpdate != null)
            {
                bookToUpdate.Price = 900;
                context.SaveChanges();
                Console.WriteLine($"Цена книги «{bookToUpdate.Title}» обновлена до {bookToUpdate.Price}.");
            }
            else
                Console.WriteLine("Книга для обновления не найдена.");
        }

        // вывод после обновления
        Console.WriteLine("\nСписок книг после обновления:");
        var afterUpdate = context.Books.ToList();
        foreach (var pr in afterUpdate)
            Console.WriteLine($"ID: {pr.Id} | «{pr.Title}» — {pr.Price}");

        // удаление книги
        Console.WriteLine("\nВведите Id книги для удаления:");
        if (int.TryParse(Console.ReadLine(), out int deleteId))
        {
            var bookToDelete = context.Books.Find(deleteId);
            if (bookToDelete != null)
            {
                context.Books.Remove(bookToDelete);
                context.SaveChanges();
                Console.WriteLine($"Книга «{bookToDelete.Title}» удалена.");
            }
            else
                Console.WriteLine("Книга для удаления не найдена.");
        }

        // вывод после удаления
        Console.WriteLine("\nСписок книг после удаления:");
        var afterDelete = context.Books.ToList();
        foreach (var pr in afterDelete)
            Console.WriteLine($"ID: {pr.Id} | «{pr.Title}» — Страниц: {pr.Pages}");

        // вычисление средней цены
        var averagePrice = context.Books.Average(p => p.Price);
        Console.WriteLine($"\nСредняя цена всех книг: {averagePrice:F2}");

        // поиск самой дорогой и самой дешевой книги
        var cheapest = context.Books.OrderBy(b => b.Price).FirstOrDefault();
        var mostExpensive = context.Books.OrderByDescending(b => b.Price).FirstOrDefault();
        Console.WriteLine($"\nСамая дешевая книга: «{cheapest?.Title}» — {cheapest?.Price}");
        Console.WriteLine($"Самая дорогая книга: «{mostExpensive?.Title}» — {mostExpensive?.Price}");

        // вычисление среднего рейтинга по жанрам
        var avgRatingByGenre = context.Books
            .GroupBy(b => b.Genre)
            .Select(g => new { Genre = g.Key, AvgRating = g.Average(b => b.Rating) })
            .ToList();
        Console.WriteLine("\nСредний рейтинг по жанрам:");
        foreach (var gr in avgRatingByGenre)
            Console.WriteLine($"Жанр «{gr.Genre}»: {gr.AvgRating:F2}");
    }
}

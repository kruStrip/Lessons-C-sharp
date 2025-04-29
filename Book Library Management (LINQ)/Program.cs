using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public int Pages { get; set; }
        public string Genre { get; set; }

        public Book(string title, string author, int year, int pages, string genre)
        {
            Title = title;
            Author = author;
            Year = year;
            Pages = pages;
            Genre = genre;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Инициализация первого списка книг
            List<Book> books = new List<Book>
            {
                new Book("Война и мир", "Лев Толстой", 1869, 1225, "Роман"),
                new Book("Преступление и наказание", "Фёдор Достоевский", 1866, 671, "Роман"),
                new Book("Мастер и Маргарита", "Михаил Булгаков", 1966, 384, "Мистика"),
                new Book("1984", "Джордж Оруэлл", 1949, 328, "Антиутопия"),
                new Book("Улисс", "Джеймс Джойс", 1922, 730, "Модернизм"),
                new Book("Гарри Поттер и философский камень", "Дж. К. Роулинг", 1997, 223, "Фэнтези"),
                new Book("Над пропастью во ржи", "Дж. Д. Сэлинджер", 1951, 277, "Современная проза"),
                new Book("Хоббит, или Туда и обратно", "Дж. Р. Р. Толкин", 1937, 310, "Фэнтези"),
                new Book("451 градус по Фаренгейту", "Рэй Брэдбери", 1953, 194, "Антиутопия"),
                new Book("Маленький принц", "Антуан де Сент-Экзюпери", 1943, 96, "Сказка")
            };

            // Инициализация второго списка книг
            List<Book> newBooks = new List<Book>
            {
                new Book("Война и мир", "Лев Толстой", 1869, 1225, "Роман"),
                new Book("Новый роман", "Современный Автор", 2020, 310, "Роман"),
                new Book("Гипербук", "Техно Писатель", 2021, 450, "Научная фантастика"),
                new Book("Маленький принц", "Антуан де Сент-Экзюпери", 1943, 96, "Сказка"),
                new Book("Звёздный путь", "Футуролог", 2019, 389, "Фантастика")
            };
            Console.WriteLine("Приветстую!!!");
            bool status = true;

            while (status)
            {
                Console.Write(
                    "Выберите действие:\n" +
                    "1. Фильтрация: Книги после определённого года\n" +
                    "2. Сортировка: По страницам\n" +
                    "3. Выборка: Названия и авторы жанра\n" +
                    "4. Count: Количество книг\n" +
                    "5. Sum: Общее число страниц\n" +
                    "6. Min: Мин. страниц\n" +
                    "7. Max: Макс. страниц\n" +
                    "8. Average: Среднее страниц\n" +
                    "9. Группировка: По жанрам\n" +
                    "10. Комбинация: Книги автора по году\n" +
                    "11. Первые 3 книги\n" +
                    "12. Последние 3 книги\n" +
                    "13. Уникальные книги первого списка\n" +
                    "14. Уникальные жанры первого списка\n" +
                    "15. Объединить списки (все)\n" +
                    "16. Объединить списки (уникальные)\n" +
                    "17. Пересечение списков\n" +
                    "18. Книги из первого, отсутствующие во втором\n" +
                    "19. Добавить книгу в первый список\n" +
                    "20. Обновить информацию о книге в первом списке\n" +
                    "21. Выход\n" +
                    "Выбор: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.\n");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("Введите год: ");
                        if (int.TryParse(Console.ReadLine(), out int year))
                        {
                            var filtered = books.Where(b => b.Year > year);
                            Console.WriteLine($"Книги после {year}:");
                            foreach (var b in filtered)
                                Console.WriteLine($"{b.Title} ({b.Year})");
                        }
                        break;
                    case 2:
                        var sortedByPages = books.OrderBy(b => b.Pages);
                        Console.WriteLine("По количеству страниц:");
                        foreach (var b in sortedByPages)
                            Console.WriteLine($"{b.Title} - {b.Pages}");
                        break;
                    case 3:
                        Console.Write("Жанр: ");
                        var genre = Console.ReadLine();
                        var byGenre = books.Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
                                            .Select(b => new { b.Title, b.Author });
                        foreach (var b in byGenre)
                            Console.WriteLine($"{b.Title} — {b.Author}");
                        break;
                    case 4:
                        Console.WriteLine($"Всего книг: {books.Count}");
                        break;
                    case 5:
                        Console.WriteLine($"Страниц всего: {books.Sum(b => b.Pages)}");
                        break;
                    case 6:
                        Console.WriteLine($"Мин. страниц: {books.Min(b => b.Pages)}");
                        break;
                    case 7:
                        Console.WriteLine($"Макс. страниц: {books.Max(b => b.Pages)}");
                        break;
                    case 8:
                        Console.WriteLine($"Сред. страниц: {books.Average(b => b.Pages):F2}");
                        break;
                    case 9:
                        var groupByGenre = books.GroupBy(b => b.Genre)
                                                .Select(g => new { g.Key, Count = g.Count() });
                        foreach (var g in groupByGenre)
                            Console.WriteLine($"{g.Key}: {g.Count}");
                        break;
                    case 10:
                        Console.Write("Автор: ");
                        var author = Console.ReadLine();
                        var byAuthor = books.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase))
                                           .OrderBy(b => b.Year)
                                           .Select(b => b.Title);
                        foreach (var t in byAuthor)
                            Console.WriteLine(t);
                        break;
                    case 11:
                        foreach (var b in books.Take(3))
                            Console.WriteLine(b.Title);
                        break;
                    case 12:
                        foreach (var b in books.Skip(Math.Max(0, books.Count - 3)))
                            Console.WriteLine(b.Title);
                        break;
                    case 13:
                        var uniqueBooks = books.GroupBy(b => new { b.Title, b.Author }).Select(g => g.First());
                        foreach (var b in uniqueBooks)
                            Console.WriteLine($"{b.Title} — {b.Author}");
                        break;
                    case 14:
                        foreach (var g in books.Select(b => b.Genre).Distinct())
                            Console.WriteLine(g);
                        break;
                    case 15:
                        foreach (var b in books.Concat(newBooks))
                            Console.WriteLine($"{b.Title} — {b.Author}");
                        break;
                    case 16:
                        var unionDistinct = books.Concat(newBooks)
                            .GroupBy(b => new { b.Title, b.Author }).Select(g => g.First());
                        foreach (var b in unionDistinct)
                            Console.WriteLine($"{b.Title} — {b.Author}");
                        break;
                    case 17:
                        var intersect = books.Where(b => newBooks.Any(nb => nb.Title == b.Title && nb.Author == b.Author));
                        foreach (var b in intersect)
                            Console.WriteLine($"{b.Title} — {b.Author}");
                        break;
                    case 18:
                        var exceptList = books.Where(b => !newBooks.Any(nb => nb.Title == b.Title && nb.Author == b.Author));
                        foreach (var b in exceptList)
                            Console.WriteLine($"{b.Title} — {b.Author}");
                        break;
                    case 19:
                        // Добавление новой книги
                        Console.Write("Название: ");
                        var newTitle = Console.ReadLine();
                        Console.Write("Автор: ");
                        var newAuthor = Console.ReadLine();
                        Console.Write("Год: "); int.TryParse(Console.ReadLine(), out int newYear);
                        Console.Write("Страницы: "); int.TryParse(Console.ReadLine(), out int newPages);
                        Console.Write("Жанр: ");
                        var newGenre = Console.ReadLine();
                        books.Add(new Book(newTitle, newAuthor, newYear, newPages, newGenre));
                        Console.WriteLine("Книга добавлена.");
                        break;
                    case 20:
                        // Обновление существующей книги
                        Console.WriteLine("Выберите номер книги для обновления:");
                        for (int i = 0; i < books.Count; i++)
                            Console.WriteLine($"{i + 1}. {books[i].Title} — {books[i].Author}");
                        Console.Write("Номер: ");
                        if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= books.Count)
                        {
                            var book = books[idx - 1];
                            Console.WriteLine($"Текущее название: {book.Title}");
                            Console.Write("Новое (Enter, чтобы пропустить): ");
                            var upd = Console.ReadLine(); if (!string.IsNullOrEmpty(upd)) book.Title = upd;

                            Console.WriteLine($"Текущий автор: {book.Author}");
                            Console.Write("Новый: ");
                            upd = Console.ReadLine(); if (!string.IsNullOrEmpty(upd)) book.Author = upd;

                            Console.WriteLine($"Текущий год: {book.Year}");
                            Console.Write("Новый: ");
                            if (int.TryParse(Console.ReadLine(), out int y)) book.Year = y;

                            Console.WriteLine($"Текущие страницы: {book.Pages}");
                            Console.Write("Новые: ");
                            if (int.TryParse(Console.ReadLine(), out int p)) book.Pages = p;

                            Console.WriteLine($"Текущий жанр: {book.Genre}");
                            Console.Write("Новый: ");
                            upd = Console.ReadLine(); if (!string.IsNullOrEmpty(upd)) book.Genre = upd;

                            Console.WriteLine("Книга обновлена.");
                        }
                        else
                        {
                            Console.WriteLine("Неверный выбор.");
                        }
                        break;
                    case 21:
                        Console.WriteLine("До свидания!!!");
                        status = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}

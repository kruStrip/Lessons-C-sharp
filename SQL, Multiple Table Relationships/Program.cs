using System;
using Npgsql;

class Program
{
    static void Main()
    {
        // строка подключения
        var connString = "Host=localhost;Port=5432;Username=postgres;Password=z0m4g;Database=postgres;";
        var conn = new NpgsqlConnection(connString);
        conn.Open();

        while (true)
        {
            Console.Write(
                "Выберите действие:\n" +
                "1 – Показать всё\n" +
                "2 – Добавить\n" +
                "3 – Обновить\n" +
                "4 – Удалить\n" +
                "5 – Поиск заказов по email\n" +
                "6 – Фильтр продуктов по цене\n" +
                "7 – Средняя цена по категориям\n" +
                "0 – Выход\n" +
                "Ваш выбор: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "0":
                    conn.Close();
                    return;
                case "1":
                    ShowAll(conn);
                    break;
                case "2":
                    Add(conn);
                    break;
                case "3":
                    Update(conn);
                    break;
                case "4":
                    Delete(conn);
                    break;
                case "5":
                    SearchOrders(conn);
                    break;
                case "6":
                    FilterProducts(conn);
                    break;
                case "7":
                    AvgPrices(conn);
                    break;
                default:
                    Console.WriteLine("Неверный ввод");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void ShowAll(NpgsqlConnection conn)
    {
        Console.Write("Таблица (1-Категории,2-Продукты,3-Клиенты,4-Заказы): ");
        var t = Console.ReadLine();
        string sql = "";
        if (t == "1") sql = "SELECT * FROM Categories";
        else if (t == "2") sql = "SELECT * FROM Products";
        else if (t == "3") sql = "SELECT * FROM Customers";
        else if (t == "4") sql = "SELECT * FROM Orders";
        else { Console.WriteLine("Неправильно"); return; }

        var cmd = new NpgsqlCommand(sql, conn);
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
                Console.Write(reader.GetName(i) + "=" + reader[i] + " ");
            Console.WriteLine();
        }
        reader.Close();
    }

    static void Add(NpgsqlConnection conn)
    {
        Console.Write("Таблица (1-Категории,2-Продукты,3-Клиенты,4-Заказы): ");
        var t = Console.ReadLine();
        string sql = "";

        if (t == "1")
        {
            Console.Write("Название категории: ");
            var n = Console.ReadLine();
            sql = $"INSERT INTO Categories(Name) VALUES('{n}')";
        }
        else if (t == "2")
        {
            Console.Write("Название продукта: "); var n = Console.ReadLine();
            Console.Write("Цена: "); var p = Console.ReadLine();
            Console.Write("КатегорияId: "); var c = Console.ReadLine();
            sql = $"INSERT INTO Products(Name,Price,CategoryId) VALUES('{n}',{p},{c})";
        }
        else if (t == "3")
        {
            Console.Write("Имя: "); var f = Console.ReadLine();
            Console.Write("Фамилия: "); var l = Console.ReadLine();
            Console.Write("Email: "); var e = Console.ReadLine();
            sql = $"INSERT INTO Customers(FirstName,LastName,Email) VALUES('{f}','{l}','{e}')";
        }
        else if (t == "4")
        {
            Console.Write("КлиентId: "); var cid = Console.ReadLine();
            Console.Write("Статус: "); var st = Console.ReadLine();
            sql = $"INSERT INTO Orders(CustomerId,Status,Date) VALUES({cid},'{st}',CURRENT_TIMESTAMP)";
        }
        else
        {
            Console.WriteLine("Неверно");
            return;
        }

        new NpgsqlCommand(sql, conn).ExecuteNonQuery();
        Console.WriteLine("Добавлено");
    }

    static void Update(NpgsqlConnection conn)
    {
        Console.Write("Таблица (1-Категории,2-Продукты,3-Клиенты,4-Заказы): ");
        var t = Console.ReadLine();
        Console.Write("Id: ");
        var id = Console.ReadLine();
        string sql = "";

        if (t == "1")
        {
            Console.Write("Новое название: "); var n = Console.ReadLine();
            sql = $"UPDATE Categories SET Name='{n}' WHERE Id={id}";
        }
        else if (t == "2")
        {
            Console.Write("Имя: "); var n = Console.ReadLine();
            Console.Write("Цена: "); var p = Console.ReadLine();
            Console.Write("КатегорияId: "); var c = Console.ReadLine();
            sql = $"UPDATE Products SET Name='{n}',Price={p},CategoryId={c} WHERE Id={id}";
        }
        else if (t == "3")
        {
            Console.Write("Имя: "); var f = Console.ReadLine();
            Console.Write("Фамилия: "); var l = Console.ReadLine();
            Console.Write("Email: "); var e = Console.ReadLine();
            sql = $"UPDATE Customers SET FirstName='{f}',LastName='{l}',Email='{e}' WHERE Id={id}";
        }
        else if (t == "4")
        {
            Console.Write("Статус: "); var s = Console.ReadLine();
            sql = $"UPDATE Orders SET Status='{s}' WHERE Id={id}";
        }
        else
        {
            Console.WriteLine("Неверно");
            return;
        }

        new NpgsqlCommand(sql, conn).ExecuteNonQuery();
        Console.WriteLine("Обновлено");
    }

    static void Delete(NpgsqlConnection conn)
    {
        Console.Write("Таблица (1-Категории,2-Продукты,3-Клиенты,4-Заказы): ");
        var t = Console.ReadLine();
        Console.Write("Id: ");
        var id = Console.ReadLine();

        string table = "";
        if (t == "1") table = "Categories";
        else if (t == "2") table = "Products";
        else if (t == "3") table = "Customers";
        else if (t == "4") table = "Orders";
        else { Console.WriteLine("Неверно"); return; }

        var sql = $"DELETE FROM {table} WHERE Id={id}";
        new NpgsqlCommand(sql, conn).ExecuteNonQuery();
        Console.WriteLine("Удалено");
    }

    static void SearchOrders(NpgsqlConnection conn)
    {
        Console.Write("Email клиента: ");
        var em = Console.ReadLine();
        var sql = $"SELECT o.Id, o.Date, o.Status FROM Orders o JOIN Customers c ON o.CustomerId=c.Id WHERE c.Email='{em}'";
        var cmd = new NpgsqlCommand(sql, conn);
        var r = cmd.ExecuteReader();
        Console.WriteLine("Заказы:");
        while (r.Read())
        {
            Console.WriteLine(r[0] + " | " + r[1] + " | " + r[2]);
        }
        r.Close();
    }

    static void FilterProducts(NpgsqlConnection conn)
    {
        Console.Write("Макс. цена: ");
        var mp = Console.ReadLine();
        var sql = $"SELECT Id,Name,Price FROM Products WHERE Price < {mp}";
        var cmd = new NpgsqlCommand(sql, conn);
        var r = cmd.ExecuteReader();
        Console.WriteLine("Продукты:");
        while (r.Read())
        {
            Console.WriteLine(r[0] + " | " + r[1] + " | " + r[2]);
        }
        r.Close();
    }

    static void AvgPrices(NpgsqlConnection conn)
    {
        var sql = "SELECT c.Name, AVG(p.Price) FROM Products p JOIN Categories c ON p.CategoryId=c.Id GROUP BY c.Name";
        var cmd = new NpgsqlCommand(sql, conn);
        var r = cmd.ExecuteReader();
        Console.WriteLine("Средняя цена по категориям:");
        while (r.Read())
        {
            Console.WriteLine(r[0] + " | " + r[1]);
        }
        r.Close();
    }
}

using System;
using Npgsql;

// Классы моделей для таблиц
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class Order
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int CustomerId { get; set; }
    public string Status { get; set; } = string.Empty;
}

class Program
{
    // Параметры подключения к PostgreSQL
    private const string ConnString =
        "Host=localhost;Port=5432;Username=postgres;Password=z0m4g;Database=postgres;";

    static void Main()
    {
        using var conn = new NpgsqlConnection(ConnString);
        conn.Open();

        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Показать все записи");
            Console.WriteLine("2 - Добавить запись");
            Console.WriteLine("3 - Обновить запись");
            Console.WriteLine("4 - Удалить запись");
            Console.WriteLine("0 - Выход");
            Console.Write("Ваш выбор: ");
            var action = Console.ReadLine();

            if (action == "0") break;

            Console.WriteLine("Выберите сущность:");
            Console.WriteLine("1 - Категория, 2 - Продукт, 3 - Клиент, 4 - Заказ");
            Console.Write("Сущность: ");
            var entity = Console.ReadLine();

            switch (action)
            {
                case "1": ViewAll(conn, entity); break;
                case "2": AddRecord(conn, entity); break;
                case "3": UpdateRecord(conn, entity); break;
                case "4": DeleteRecord(conn, entity); break;
                default: Console.WriteLine("Неверный выбор действия"); break;
            }

            Console.WriteLine();
        }

        conn.Close();
    }

    static void ViewAll(NpgsqlConnection conn, string entity)
    {
        string sql;
        if (entity == "1") sql = "SELECT * FROM Categories";
        else if (entity == "2") sql = "SELECT * FROM Products";
        else if (entity == "3") sql = "SELECT * FROM Customers";
        else if (entity == "4") sql = "SELECT * FROM Orders";
        else { Console.WriteLine("Сущность не найдена"); return; }

        using var cmd = new NpgsqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write($"{reader.GetName(i)}: {reader[i]}  ");
            }
            Console.WriteLine();
        }
    }

    static void AddRecord(NpgsqlConnection conn, string entity)
    {
        var cmd = conn.CreateCommand();
        if (entity == "1")
        {
            Console.Write("Название категории: ");
            var catName = Console.ReadLine();
            cmd.CommandText = $"INSERT INTO Categories(Name) VALUES('{catName}')";
        }
        else if (entity == "2")
        {
            Console.Write("Название продукта: "); var pName = Console.ReadLine();
            Console.Write("Цена: "); var pPrice = Console.ReadLine();
            Console.Write("Id категории: "); var pCat = Console.ReadLine();

            // Проверяем, существует ли категория с таким Id
            var checkCat = conn.CreateCommand();
            checkCat.CommandText = $"SELECT COUNT(*) FROM Categories WHERE Id = {pCat}";
            var exists = Convert.ToInt32(checkCat.ExecuteScalar());
            if (exists == 0)
            {
                Console.WriteLine("Ошибка: категория с указанным Id не найдена.");
                return;
            }

            cmd.CommandText = $"INSERT INTO Products(Name,Price,CategoryId) VALUES('{pName}',{pPrice},{pCat})";
        }
        else if (entity == "3")
        {
            Console.Write("Имя: "); var fn = Console.ReadLine();
            Console.Write("Фамилия: "); var ln = Console.ReadLine();
            Console.Write("Email: "); var em = Console.ReadLine();
            cmd.CommandText = $"INSERT INTO Customers(FirstName,LastName,Email) VALUES('{fn}','{ln}','{em}')";
        }
        else if (entity == "4")
        {
            Console.Write("Id клиента: "); var oCust = Console.ReadLine();
            Console.Write("Статус: "); var oSt = Console.ReadLine();

            // Проверяем, существует ли клиент с таким Id
            var checkCus = conn.CreateCommand();
            checkCus.CommandText = $"SELECT COUNT(*) FROM Customers WHERE Id = {oCust}";
            var existsCus = Convert.ToInt32(checkCus.ExecuteScalar());
            if (existsCus == 0)
            {
                Console.WriteLine("Ошибка: клиент с указанным Id не найден.");
                return;
            }

            cmd.CommandText = $"INSERT INTO Orders(CustomerId,Status,Date) VALUES({oCust},'{oSt}',CURRENT_TIMESTAMP)";
        }
        else
        {
            Console.WriteLine("Сущность не найдена");
            return;
        }
        cmd.ExecuteNonQuery();
        Console.WriteLine("Запись добавлена.");
    }

    static void UpdateRecord(NpgsqlConnection conn, string entity)
    {
        Console.Write("Введите Id: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) return;
        var cmd = conn.CreateCommand();

        if (entity == "1")
        {
            Console.Write("Новое название категории: "); var catNew = Console.ReadLine();
            cmd.CommandText = $"UPDATE Categories SET Name='{catNew}' WHERE Id={id}";
        }
        else if (entity == "2")
        {
            Console.Write("Новое название продукта: "); var pNewName = Console.ReadLine();
            Console.Write("Новая цена: "); var pNewPrice = Console.ReadLine();
            Console.Write("Новый Id категории: "); var pNewCat = Console.ReadLine();
            cmd.CommandText = $"UPDATE Products SET Name='{pNewName}',Price={pNewPrice},CategoryId={pNewCat} WHERE Id={id}";
        }
        else if (entity == "3")
        {
            Console.Write("Новое имя: "); var fnNew = Console.ReadLine();
            Console.Write("Новая фамилия: "); var lnNew = Console.ReadLine();
            Console.Write("Новый Email: "); var emNew = Console.ReadLine();
            cmd.CommandText = $"UPDATE Customers SET FirstName='{fnNew}',LastName='{lnNew}',Email='{emNew}' WHERE Id={id}";
        }
        else if (entity == "4")
        {
            Console.Write("Новый статус: "); var stNew = Console.ReadLine();
            cmd.CommandText = $"UPDATE Orders SET Status='{stNew}' WHERE Id={id}";
        }
        else
        {
            Console.WriteLine("Сущность не найдена");
            return;
        }
        cmd.ExecuteNonQuery();
        Console.WriteLine("Запись обновлена.");
    }

    static void DeleteRecord(NpgsqlConnection conn, string entity)
    {
        Console.Write("Введите Id: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) return;

        string table;
        if (entity == "1") table = "Categories";
        else if (entity == "2") table = "Products";
        else if (entity == "3") table = "Customers";
        else if (entity == "4") table = "Orders";
        else
        {
            Console.WriteLine("Сущность не найдена");
            return;
        }

        var cmd = conn.CreateCommand();
        cmd.CommandText = $"DELETE FROM {table} WHERE Id={id}";
        cmd.ExecuteNonQuery();
        Console.WriteLine("Запись удалена.");
    }
}

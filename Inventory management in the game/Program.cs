class Program
{
    static void Main()
    {
        Console.WriteLine("Приветствую!!!!!");

        Dictionary<string, int> Inventory = new Dictionary<string, int>();
        string name;
        int count;

        bool exit = false;
        while (!exit)
        {
            Console.Write(
                "Выберите действие:\n" +
                "1. Добавление предмета в инвентарь\n" +
                "2. Удаление предмета из инвентаря\n" +
                "3. Просмотр текущего содержимого инвентаря\n" +
                "4. Поиск предмета по названию и вывод его количества\n" +
                "5. Выход\n" +
                "Выбор: "
            );

            double choice = Convert.ToDouble(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Введите название предмета: ");
                    name = Console.ReadLine();
                    Console.Write("Введите его кол-во: ");
                    count = Convert.ToInt32(Console.ReadLine());
                    Inventory[name] = count;
                    Console.WriteLine("Предмет добавлен!");
                    break;

                case 2:
                    Console.Write("Введите название предмета: ");
                    name = Console.ReadLine();
                    if (Inventory.ContainsKey(name))
                    {
                        if (Inventory[name] == 1)
                        {
                            Inventory.Remove(name);
                            Console.WriteLine($"Предмет '{name}, удален!'");
                        }
                        else
                        {
                            Inventory[name] -= 1;
                            Console.WriteLine($"Предмет '{name}, удален!', текущее кол-во {Inventory[name]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Данного предмета на существует(");
                    }
                    break;

                case 3:
                    Console.WriteLine("Список предметов:");
                    foreach (var (key, val) in Inventory) Console.WriteLine($"Предмет: {key}, {val}");
                    break;

                case 4:
                    Console.Write("Введите название предмета: ");
                    name = Console.ReadLine();
                    if (Inventory.ContainsKey(name))
                    {
                        Console.WriteLine($"Его кол-во: {Inventory[name]}");
                    }
                    else
                    {
                        Console.WriteLine("Данного предмета на существует(");
                    }
                    break;

                case 5:
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Неверный пункт меню. Напишите цифру от 1 до 5.");
                    break;
            }
            Console.WriteLine();
        }

        Console.WriteLine("До свидания!!!!");
    }
}

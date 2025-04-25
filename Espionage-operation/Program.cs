using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<string> suspects = new List<string> { "Алекс", "Джон", "Майк" };
        List<string> suspects2 = new List<string> { "Джон", "Сара", "Эми" };

        bool exit = false;
        while (!exit)
        {
            Console.Write(
                "Выберите действие:\n" +
                "1. Добавить подозреваемого\n" +
                "2. Удалить подозреваемого\n" +
                "3. Проверить наличие подозреваемого\n" +
                "4. Объединить два списка и показать\n" +
                "5. Выход\n" +
                "Выбор: "
            );
            double choice = Convert.ToDouble(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Введите имя нового подозреваемого: ");
                    string nameToAdd = Console.ReadLine();
                    if (nameToAdd != "")
                    {
                        suspects.Add(nameToAdd);
                        Console.WriteLine("Добавлено: " + nameToAdd);
                    }
                    else
                    {
                        Console.WriteLine("Имя не может быть пустым.");
                    }
                    break;

                case 2:
                    Console.Write("Введите имя подозреваемого для удаления: ");
                    string nameToRemove = Console.ReadLine();
                    bool removed = suspects.Remove(nameToRemove);
                    if (removed)
                        Console.WriteLine("Удалён: " + nameToRemove);
                    else
                        Console.WriteLine("Не найден в основном списке: " + nameToRemove);
                    break;

                case 3:
                    Console.Write("Введите имя подозреваемого для проверки: ");
                    string nameToCheck = Console.ReadLine();
                    bool exists = suspects.Contains(nameToCheck);
                    if (exists)
                        Console.WriteLine("Подозреваемый есть в списке: " + nameToCheck);
                    else
                        Console.WriteLine("Подозреваемый НЕ найден: " + nameToCheck);
                    break;

                case 4:
                    List<string> merged = new List<string>();
                    for (int i = 0; i < suspects.Count; i++)
                    {
                        merged.Add(suspects[i]);
                    }
                    // Затем из второго, только если ещё нет
                    for (int i = 0; i < suspects2.Count; i++)
                    {
                        string n = suspects2[i];
                        if (!merged.Contains(n))
                        {
                            merged.Add(n);
                        }
                    }
                    // Выводим объединённый список
                    Console.WriteLine("Объединённый список подозреваемых:");
                    for (int i = 0; i < merged.Count; i++)
                    {
                        Console.Write(merged[i]);
                        if (i < merged.Count - 1)
                            Console.Write(", ");
                    }
                    Console.WriteLine();
                    break;

                case 5:
                    // Выход
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Неправильный пункт меню.");
                    break;
            }
        }

        Console.WriteLine("Операция завершена.");
    }
}

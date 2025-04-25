using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Приветствую!!!!!");

        List<string> animals = new List<string>();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1. Добавить существо");
            Console.WriteLine("2. Удалить существо");
            Console.WriteLine("3. Найти по первой букве");
            Console.WriteLine("4. Показать всех по алфавиту");
            Console.WriteLine("5. Выход");
            Console.Write("Ваш выбор: ");

            double choice = Convert.ToDouble(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    // Добавить
                    Console.Write("Введите название существа: ");
                    string nameToAdd = Console.ReadLine();
                    if (nameToAdd != "")
                    {
                        animals.Add(nameToAdd);
                        Console.WriteLine("Добавлено: " + nameToAdd);
                    }
                    else
                    {
                        Console.WriteLine("Нельзя добавить пустое название.");
                    }
                    break;

                case 2:
                    // Удалить
                    Console.Write("Введите название для удаления: ");
                    string nameToRemove = Console.ReadLine();
                    bool removed = animals.Remove(nameToRemove);
                    if (removed)
                        Console.WriteLine("Удалено: " + nameToRemove);
                    else
                        Console.WriteLine("Не найдено: " + nameToRemove);
                    break;

                case 3:
                    Console.Write("Введите букву: ");
                    string s = Console.ReadLine();
                    if (s == "")
                    {
                        Console.WriteLine("Нужно ввести букву.");
                        break;
                    }
                    char letter = Char.ToUpper(s[0]);
                    List<string> found = new List<string>();
                    for (int i = 0; i < animals.Count; i++)
                    {
                        string a = animals[i];
                        if (Char.ToUpper(a[0]) == letter)
                        {
                            found.Add(a);
                        }
                    }
                    if (found.Count > 0)
                    {
                        Console.Write("Результат: ");
                        for (int i = 0; i < found.Count; i++)
                        {
                            Console.Write(found[i]);
                            if (i < found.Count - 1) Console.Write(", ");
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Ничего не найдено на букву " + letter);
                    }
                    break;

                case 4:
                    animals.Sort();
                    Console.WriteLine("Существа в алфавитном порядке:");
                    for (int i = 0; i < animals.Count; i++)
                    {
                        Console.WriteLine(" - " + animals[i]);
                    }
                    break;

                case 5:
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Неверный пункт меню.");
                    break;
            }
        }

        Console.WriteLine("До свидания!!!!");
    }
}

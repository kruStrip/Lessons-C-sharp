using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        List<int> ints1 = new List<int>();
        List<int> ints2 = new List<int> { 1, 2 };

        ints1.Add(1);
        ints1.AddRange(new int[] { 1, 2 });

        // ints1 = {1,2,3}
        ints1.Insert(0, 2); // {2,1,2,3}
        ints2.InsertRange(1, new List<int> { 4, 5}); // {1,4,5,2,3}

        ints1.Remove(2); // Удаление числа 2 (первого которое находит в массиве)
        ints1.RemoveAt(0); // Удаление по индексу
        ints1.RemoveRange(1, 3); //Удаляет промежуток указанных индексов
        ints1.RemoveAll(x => x % 2 == 0); // Удаление всех четных чисел
        ints1.Clear();

        bool res = ints1.Contains(1);
        int num = ints1.Find(x => x % 2 == 0);
        List<int> ints3 = ints1.FindAll(x => x % 2 == 0);

        List<int> ints4 = ints1.GetRange(1,3);

        ints1.Reverse(); //Переворачивает элементы



        //Множества
        HashSet<int> list = new HashSet<int>();

        list.Add(1);
        list.Add(2);
        list.Add(1);

        list.Remove(2); // Удаление элемента

        list.Clear();

        bool res = list.Contains(1); // Проверка на наличие элемента

        list.Count(); // Кол-во элементов


        //Стэк
        Stack<int> stack = new Stack<int>();

        stack.Push(1); // Добавление элемента
        stack.Pop(); // Удаление элемента
        int r = stack.Pop(); // Запись удаленного элемента в переменную
        int g = stack.Peek(); // Возвращение элемента

        stack.Clear();
        stack.Contains(1); // Проверка на наличие элемента (true\false)
        int b = stack.Count();



        // Словарь
        Dictionary<string, int> dic = new Dictionary<string, int>();
        dic.Add("a", 1);
        dic.Add("b", 2);    
        dic.Add("c", 3);
        dic["a"] = 33; // Тут значение измениться
        dic["f"] = 33; // Тут значение добавиться

        dic.Remove("g");
        dic.Clear(); // Все очищает

        dic.ContainsKey("g");
        dic.ContainsValue(20);

        bool res = dic.TryGetValue("b", out var age);

        dic.Count();


    }
}
class Program
{
    static void Main()
    {
        Console.WriteLine("Приветствую!!!!!");

        Stack<string> books = new Stack<string>();

        bool exit = false;
        while (!exit)
        {
            Console.Write(
                "Выберите действие:\n" +
                "1. Положить на верх стопки\n" +
                "2. Убрать книгу сверху\n" +
                "3. Просмотреть название книги сверху без её удаления\n" +
                "4. Вывести текущую стопку книг\n" +
                "5. Выход\n" +
                "Выбор: "
            );

            double choice = Convert.ToDouble(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Введите название книги: ");
                    books.Push(Console.ReadLine());
                    break;

                case 2:
                    string name = books.Pop();
                    Console.WriteLine($"Книга '{name}' удалена!");
                    break;

                case 3:
                    Console.WriteLine($"Книга: {books.Peek()}");
                    break;

                case 4:
                    Console.WriteLine("Список книг: ");
                    foreach (var book in books) Console.WriteLine(book);
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

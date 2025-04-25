Console.WriteLine("Приветствую!!!!!");

while (true) {
    Console.Write("Введи 1-ое число: ");
    double num1 = Convert.ToDouble(Console.ReadLine());
    Console.Write("Введи 2-ое число: ");
    double num2 = Convert.ToDouble(Console.ReadLine());
    Console.Write("Выберите тип операции:\n" +
        "1. Сложение\n" +
        "2. Вычитание\n" +
        "3. Умножение\n" +
        "4. Деление\n" +
        "5. Вычислить площадь круга\n" +
        "Выбор: "
        );
    double? choice = Convert.ToDouble(Console.ReadLine());

    switch (choice)
    {
        case 1:
            Console.WriteLine($"Результат: {num1} + {num2} = {num1 + num2}");
            break;
        case 2:
            if (num1 != 0 || num2 != 0)
            {
                Console.WriteLine($"Результат: {num1} - {num2} = {num1 - num2}");
            }
            else
            {
                Console.WriteLine("Одно из значений равно 0");
            }
            break;
        case 3:
            Console.WriteLine($"Результат: {num1} * {num2} = {num1 * num2}");
            break;
        case 4:
            Console.WriteLine($"Результат: {num1} / {num2} = {num1 / num2}");
            break;
        case 5:
            Console.Write("Введи радиус: ");
            double? a = Convert.ToDouble(Console.ReadLine());
            if (a > 0)
            {
                Console.WriteLine($"Плошадь круга, равна: {3.14 * a * a}");
            }
            else
            {
                Console.WriteLine("Радиус должен быть больше 0!");
            }
                break;
        default:
            Console.WriteLine("Неверный ввод, попробуйте еще раз");
            break;
    }
    Console.WriteLine("Нажмите любую клавишу");
    Console.ReadKey();
    Console.Clear();
}
Console.WriteLine("Приветствую!!!!!");
bool status = true;

while (status)
{
    Console.Write("Выберите действие:\n" +
        "1. Определить тип осадков\n" +
        "2. Определить комфортность температуры\n" +
        "3. Выход\n" +
        "Выбор: "
        );
    double? choice = Convert.ToDouble(Console.ReadLine());

    switch (choice)
    {
        case 1:
            Console.Write("Введите уровень осадков: ");
            double rainfall = Convert.ToDouble(Console.ReadLine());

            Console.Write("Ответ: ");
            if (rainfall < 0.1)
            {
                Console.WriteLine("Без осадков");
            }
            else if (rainfall >= 0.1 && rainfall <= 2.5)
            {
                Console.WriteLine("Небольшой дождь");
            }
            else if (rainfall >= 2.6 && rainfall <= 17)
            {
                Console.WriteLine("Умеренный дождь");
            }
            else
            {
                Console.WriteLine("Сильный дождь");
            }
            break;
        case 2:
            Console.Write("Введите температуру (в градусах Цельсия): ");
            double temp = Convert.ToDouble(Console.ReadLine());
            Console.Write("Ответ: ");
            string res = temp > 25
            ? "Жарко"
            : temp < 10
            ? "Холодно"
            : "Комфортно";

            Console.WriteLine(res);

            break;
        case 3:
            status = false;
            break;
        default:
            Console.WriteLine("Неверный ввод, попробуйте еще раз");
            break;
    }
    Console.WriteLine();
}
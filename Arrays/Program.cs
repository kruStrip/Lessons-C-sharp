Console.WriteLine("Приветствую!!!!!");
bool status;
int[] marks = { 85, 92, 78, 90, 67 };
int[] sortMarks = marks;
double average = 0;
bool logout = true;

// Расчет средней успеваемости
for (int i = 0; i < marks.Length; i++)
{
    average += marks[i];
}
average /= marks.Length;

// Кол-во студентов получивших оценку выше средней
int count = 0;
for (int i = 0; i < marks.Length; i++)
{
    if (marks[i] >= average)
    {
        count++;
    }
}

// Сортировка оценок, в порядке возрастания
for (int i = 0; i < sortMarks.Length - 1; i++)
{
    status = false;

    for (int j = 0; j < sortMarks.Length - 1 - i; j++)
    {
        if (sortMarks[j] > sortMarks[j + 1])
        {
            int temp = sortMarks[j];
            sortMarks[j] = sortMarks[j + 1];
            sortMarks[j + 1] = temp;

            status = true;
        }
    }

    if (!status)
    {
        break;
    }
}

while (logout)
{
    Console.Write("Выберите действие:\n" +
        "1. Найти среднюю оценку\n" +
        "2. Определить кол-во студентов, получивших оценку выше средней\n" +
        "3. Посмотреть список оценок (в порядке возрастания)\n" +
        "4. Выход\n" +
        "Выбор: "
        );
    double? choice = Convert.ToDouble(Console.ReadLine());

    switch (choice)
    {
        case 1:
            Console.WriteLine($"Средняя успеваемость учеников: {average}");
            break;
        case 2:
            Console.WriteLine($"Кол-во учеников, получивших оценку выше средней: {count}");
            break;
        case 3:
            Console.WriteLine($"Список оценок:");
            for (int i = 0; i < sortMarks.Length; i++)
            {
                Console.WriteLine(sortMarks[i]);
            }

            break;
        case 4:
            logout = false;
            break;
        default:
            Console.WriteLine("Неверный ввод, попробуйте еще раз");
            break;
    }
    Console.WriteLine();
}
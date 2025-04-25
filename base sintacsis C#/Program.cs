// See https://aka.ms/new-console-template for more information
//Console.Write("Hello, World!"); // без переноса
//Console.WriteLine("Hello, World!"); // с переносом на новую строку (/n)

class Program
{
    static void Main()
    {
        string greeting = "Meow";
        Console.WriteLine(greeting);

        Console.ReadKey(); // Ожидает нажатие на любую клавишу 

        //Интерполяция строк
        Console.WriteLine($"Hello - {greeting}");
        Console.WriteLine($"Hello - {0}, {1}", 5, 10);


        string? enter = Console.ReadLine(); // Заполнение переменной, через консоль
        //знак ? после типа данных означает, что в переменной может храниться тип NULL

        double num = Convert.ToDouble( Console.ReadLine() ); // Метод  Convert.ToDouble, переводит в double

        Console.ReadKey();
        Console.Clear(); // Очищает консоль 

        /* Условия

        if(n < 10 && n = 2 || n = 3)
        {

        } else if ()
        {

        }
        else
        {

        }
        */

        //Тернарный оператор
        //string res = (n % 2 == 0) ? "Четное" : "Нечетное"; 

        /* 
        switch (n)
        {
            case 0:
                break;
            case 1:
                break;
            default:
                break;
        }
        */


    }

    //static void Main(string[] args)
    //static int Main()

}
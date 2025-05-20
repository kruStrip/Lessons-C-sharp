// Исключение для деления на ноль
public class DivisionByZeroException : Exception
{
    public DivisionByZeroException(string message) : base(message) { }
}

// Исключение для неверной операции
public class InvalidOperationException : Exception
{
    public InvalidOperationException(string message) : base(message) { }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Приветствую!!!");

        while (true)
        {
            try
            {
                Console.Write("Введите 1-е число: ");
                string input1 = Console.ReadLine();
                double number1 = double.Parse(input1);  

                Console.Write("Введите 2-е число: ");
                string input2 = Console.ReadLine();
                double number2 = double.Parse(input2);  

                Console.Write("Введите операцию (+, -, *, /): ");
                string operation = Console.ReadLine();

                double result;

                switch (operation)
                {
                    case "+":
                        result = number1 + number2;
                        break;
                    case "-":
                        result = number1 - number2;
                        break;
                    case "*":
                        result = number1 * number2;
                        break;
                    case "/":
                        if (number2 == 0)
                            throw new DivisionByZeroException("Попытка деления на ноль");
                        result = number1 / number2;
                        break;
                    default:
                        throw new InvalidOperationException("Операция не распознана");
                }

                Console.WriteLine($"Результат: {result}");
                break;

            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: введено не число. Пожалуйста, введите корректное число.");
            }
            catch (DivisionByZeroException)
            {
                Console.WriteLine("Ошибка: деление на ноль недопустимо. Попробуйте снова.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Ошибка: неверная операция. Используйте только +, -, * или /.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
            }

            Console.WriteLine("Попробуйте ещё раз.\n");
        }

        Console.WriteLine("Досвидание!!!");
        Console.ReadKey();
    }
}



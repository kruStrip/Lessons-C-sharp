string[][] movies = {
    new string[] { "Матрица", "Интерстеллар", "Время" }, // Фантастика
    new string[] { "Крёстный отец", "Казино", "Славные парни" }, // Криминал
    new string[] { "Аватар", "Человек-паук", "Железный человек" } // Экшн
};for  (int i = 0; i < movies.Length; i++)
{
    switch (i) 
    { 
        case 0:
            Console.WriteLine("Фантастика:");
            for (int j = 0; j < 3; j++)
            {
                Console.WriteLine(movies[i][j]);
            }
            break;
        case 1:
            Console.WriteLine("Криминал:");
            for (int j = 0; j < 3; j++)
            {
                Console.WriteLine(movies[i][j]);
            }
            break;
        case 2:
            Console.WriteLine("Экшн:");
            for (int j = 0; j < 3; j++)
            {
                Console.WriteLine(movies[i][j]);
            }
            break;
    }
    Console.WriteLine();
}
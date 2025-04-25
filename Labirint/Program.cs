using System;

class Program
{
    const int N = 5;
    static int[,] maze = new int[N, N];
    static bool[,] visited = new bool[N, N];
    static bool pathFound = false;

    static void Main()
    {
        // Ввод лабиринта
        Console.WriteLine("Введите лабиринт 5×5 (0 — пустая клетка, 1 — стена):");
        for (int i = 0; i < N; i++)
        {
            Console.Write($"Строка {i + 1} (5 чисел 0 или 1 через пробел): ");
            string[] parts = Console.ReadLine()
                                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < N; j++)
            {
                maze[i, j] = int.Parse(parts[j]);
            }
        }

        // Вывод лабиринта
        Console.WriteLine("\nВаш лабиринт:");
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
                Console.Write(maze[i, j] + " ");
            Console.WriteLine();
        }

        // Поиск пути из [0,0] в [4,4]
        DFS(0, 0);

        // Результат
        Console.WriteLine(
            pathFound
              ? "\nПуть из [0,0] в [4,4] существует!"
              : "\nПути из [0,0] в [4,4] нет."
        );
    }
    static void DFS(int i, int j)
    {
        if (i < 0 || i >= N || j < 0 || j >= N) return;
        if (maze[i, j] == 1 || visited[i, j] || pathFound) return;

        if (i == N - 1 && j == N - 1)
        {
            pathFound = true;
            return;
        }


        visited[i, j] = true;


        DFS(i + 1, j);
        DFS(i - 1, j);
        DFS(i, j + 1);
        DFS(i, j - 1);
    }
}

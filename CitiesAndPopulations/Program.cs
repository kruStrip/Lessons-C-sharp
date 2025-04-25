int[][] cities = {
    new int[] { 10000, 15000, 20000 },      // Город 1
    new int[] {  5000,  7000       },      // Город 2
    new int[] { 30000, 40000, 50000, 60000 } // Город 3
};

int maxCityIndex = 0;   // Индекс города с максимальным населением
int maxPopulation = 0;  // Самое большое общее население

for (int i = 0; i < cities.Length; i++)
{
    int sum = 0;
    for (int j = 0; j < cities[i].Length; j++)
    {
        sum += cities[i][j];
    }

    Console.WriteLine($"Город {i + 1}: Общее население = {sum}");

    if (sum > maxPopulation)
    {
        maxPopulation = sum;
        maxCityIndex = i;
    }
}

Console.WriteLine($"\nСамый большой город: Город {maxCityIndex + 1}");
using System.Diagnostics;
var line = FileParser.ParseFile("/Advent of Code 2024/Day 11/data.txt")[0];

void partOne()
{
    List<string> stones = line.Split(' ').ToList();

    var blinks = 25;
    var time = Stopwatch.StartNew();
    for (int i = 0; i < blinks; i++)
    {
        for (int j = 0; j < stones.Count; j++)
        {
            if (stones[j] == "0")
            {
                stones[j] = "1";
            }
            else if (stones[j].Length % 2 == 0)
            {
                int middle = stones[j].Length / 2;
                string leftStone = stones[j].Substring(0, middle);
                string rightStone = stones[j].Substring(middle, middle);
                stones.RemoveAt(j);
                stones.Insert(j, int.Parse(rightStone).ToString());
                stones.Insert(j, leftStone);
                j++;
            }
            else
            {
                stones[j] = (long.Parse(stones[j]) * 2024).ToString();
            }

        }
    }
    time.Stop();
    Console.WriteLine($"{time.ElapsedMilliseconds}ms");
    Console.WriteLine(stones.Count);
}

void partTwo()
{
    var line = FileParser.ParseFile("/Advent of Code 2024/Day 11/data.txt")[0];
    var stoneCounts = new Dictionary<long, long>();

    foreach (var stone in line.Split(' '))
    {
        long value = long.Parse(stone);
        if (!stoneCounts.ContainsKey(value))
            stoneCounts[value] = 0;
        stoneCounts[value]++;
    }

    int blinks = 75;
    var time = Stopwatch.StartNew();

    for (int i = 0; i < blinks; i++)
    {
        var newCounts = new Dictionary<long, long>();

        foreach (var kvp in stoneCounts)
        {
            long stone = kvp.Key;
            long count = kvp.Value;

            if (stone == 0)
            {
                AddToDictionary(newCounts, 1, count);
            }
            else if (stone.ToString().Length % 2 == 0)
            {
                string stoneString = stone.ToString();
                int middle = stoneString.Length / 2;

                long leftStone = long.Parse(stoneString.Substring(0, middle));
                long rightStone = long.Parse(stoneString.Substring(middle, middle));

                AddToDictionary(newCounts, leftStone, count);
                AddToDictionary(newCounts, rightStone, count);
            }
            else
            {
                AddToDictionary(newCounts, stone * 2024, count);
            }
        }

        stoneCounts = newCounts;
    }

    time.Stop();
    Console.WriteLine($"{time.ElapsedMilliseconds}ms");
    long totalStones = 0;
    foreach (var count in stoneCounts.Values)
        totalStones += count;
    Console.WriteLine(totalStones);

    void AddToDictionary(Dictionary<long, long> dict, long key, long count)
    {
        if (!dict.ContainsKey(key))
            dict[key] = 0;
        dict[key] += count;
    }
}

partOne();
partTwo();
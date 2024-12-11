void part1()
{
    var lines = FileParser.ParseFile("/Advent of Code 2024/Day 10/data.txt");
    var sum = 0; var visited = new HashSet<(int, int)>();
    for (int i = 0; i < lines.Length; i++)
    {
        for (int j = 0; j < lines[i].Length; j++)
        {
            if (lines[i][j] == '0')
            {
                int count = BlazeTheTrail(i, j, lines);
                sum += count;
                visited.Clear();
            }
        }
    }
    Console.WriteLine(sum);
    int BlazeTheTrail(int i, int j, string[] lines)
    {
        if (i < 0 || i >= lines.Length) return 0;
        if (j < 0 || j >= lines[0].Length) return 0;
        if (visited.Contains((i, j))) return 0;
        if (lines[i][j] == '9')
        {
            visited.Add((i, j));
            return 1;

        }
        visited.Add((i, j));
        int currentValue = int.Parse(lines[i][j].ToString());

        var accumulated = 0;
        // Check top
        if (i >= 1 && int.Parse(lines[i - 1][j].ToString()) - 1 == currentValue)
        {
            accumulated += BlazeTheTrail(i - 1, j, lines);
        }

        // Check bot
        if (i < lines.Length - 1 && int.Parse(lines[i + 1][j].ToString()) - 1 == currentValue)
        {
            accumulated += BlazeTheTrail(i + 1, j, lines);
        }

        // Check left
        if (j >= 1 && int.Parse(lines[i][j - 1].ToString()) - 1 == currentValue)
        {
            accumulated += BlazeTheTrail(i, j - 1, lines);
        }

        // Check right
        if (j < lines[0].Length - 1 && int.Parse(lines[i][j + 1].ToString()) - 1 == currentValue)
        {
            accumulated += BlazeTheTrail(i, j + 1, lines);
        }

        return accumulated;
    }
}
void part2()
{
    var lines = FileParser.ParseFile("/Advent of Code 2024/Day 10/data.txt");
    var sum = 0;
    for (int i = 0; i < lines.Length; i++)
    {
        for (int j = 0; j < lines[i].Length; j++)
        {
            if (lines[i][j] == '0')
            {
                sum += BlazeTheTrail(i, j, lines);
            }
        }
    }
    Console.WriteLine(sum);
    int BlazeTheTrail(int i, int j, string[] lines)
    {
        if (i < 0 || i >= lines.Length) return 0;
        if (j < 0 || j >= lines[0].Length) return 0;
        if (lines[i][j] == '9')
        {
            return 1;
        }
        int currentValue = int.Parse(lines[i][j].ToString());
        var accumulated = 0;
        // Check top
        if (i >= 1 && int.Parse(lines[i - 1][j].ToString()) - 1 == currentValue)
        {
            accumulated += BlazeTheTrail(i - 1, j, lines);
        }

        // Check bot
        if (i < lines.Length - 1 && int.Parse(lines[i + 1][j].ToString()) - 1 == currentValue)
        {
            accumulated += BlazeTheTrail(i + 1, j, lines);
        }

        // Check left
        if (j >= 1 && int.Parse(lines[i][j - 1].ToString()) - 1 == currentValue)
        {
            accumulated += BlazeTheTrail(i, j - 1, lines);
        }

        // Check right
        if (j < lines[0].Length - 1 && int.Parse(lines[i][j + 1].ToString()) - 1 == currentValue)
        {
            accumulated += BlazeTheTrail(i, j + 1, lines);
        }

        return accumulated;
    }
}

part1();
part2();
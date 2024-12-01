class Program
{
    public static void Main(string[] args)
    {
        var lines = FileParser.ParseFile("/Advent of Code 2024/Day 1/data.txt");

        List<int> column1 = new List<int>();
        List<int> column2 = new List<int>();

        foreach (var line in lines)
        {
            var rowValues = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            column1.Add(int.Parse(rowValues[0]));
            column2.Add(int.Parse(rowValues[1]));
        }

        column1.Sort();
        column2.Sort();

        Part1(column1, column2);
        Part2(column1, column2);
    }

    public static void Part1(List<int> column1, List<int> column2)
    {
        var sum = 0;
        for (int i = 0; i < column1.Count; i++)
        {
            sum += Math.Abs(column1[i] - column2[i]);
        }

        Console.WriteLine(sum);

    }
    public static void Part2(List<int> column1, List<int> column2)
    {
        Dictionary<int, int> counts = new Dictionary<int, int>();

        foreach (int number in column2)
        {
            if (counts.ContainsKey(number))
            {
                counts[number]++;
            }
            else
            {
                counts[number] = 1;
            }
        }

        var sum2 = 0;

        foreach (int number in column1)
        {
            if (counts.ContainsKey(number))
            {
                sum2 += number * counts[number];
            }
        }

        Console.WriteLine(sum2);
    }
}
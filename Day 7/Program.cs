var lines = FileParser.ParseFile("/Advent of Code 2024/Day 7/data.txt");

List<KeyValuePair<long, List<long>>> data = [];

foreach (var line in lines)
{
    var rowValues = line.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

    var testValue = long.Parse(rowValues[0]);
    var combinations = rowValues[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

    data.Add(new KeyValuePair<long, List<long>>(testValue, combinations));
}

bool part1 = false;
long sum = 0;
var watch = System.Diagnostics.Stopwatch.StartNew();
foreach (var row in data)
{

    if (FindCombination(row.Key, row.Value, part1))
    {
        sum += row.Key;
    }
}
watch.Stop();
Console.WriteLine(watch.ElapsedMilliseconds.ToString() + "ms");
Console.WriteLine(sum);

bool FindCombination(long target, List<long> options, bool part1)
{
    List<long> calculateNewPerms(List<long> perms, long nextVal)
    {
        return perms
            .SelectMany(perm => new[]
            {
                perm + nextVal,
                perm * nextVal,
                (!part1) ? long.Parse(perm.ToString() + nextVal.ToString()) : 0
            })
            .ToList();
    }

    if (options.Count == 0)
    {
        return false;
    }

    if (options.Count == 1)
    {
        return options[0] == sum;
    }

    List<long> permutations = new List<long> { options[0] + options[1], options[0] * options[1] };
    if (!part1)
    {
        permutations.Add(long.Parse(options[0].ToString() + options[1].ToString()));
    }
    for (int i = 2; i < options.Count(); i++)
    {
        permutations = calculateNewPerms(permutations, options[i]);
    }

    return permutations.Contains(target);
}
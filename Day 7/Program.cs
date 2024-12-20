﻿var lines = FileParser.ParseFile("/Advent of Code 2024/Day 7/data.txt");

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
        List<long> newPerms = [];

        foreach (var perm in perms)
        {
            newPerms.Add(perm + nextVal);
            newPerms.Add(perm * nextVal);
            if (!part1)
            {
                newPerms.Add(long.Parse(perm.ToString() + nextVal.ToString()));
            }
        }

        return newPerms;
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
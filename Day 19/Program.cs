var lines = File.ReadAllLines("/Advent of Code 2024/Day 19/data.txt");

var towels = lines[0].Split(", ").ToList();

var designs = lines.Skip(2).ToArray();

var memo = new Dictionary<string, bool>();

var memo2 = new Dictionary<string, long>();

Dictionary<string, List<List<string>>> allWays = [];

long possibleDesigns2 = 0;

foreach (var design in designs)
{
    var ways = CanConstruct(design, towels.ToArray(), memo2);

    possibleDesigns2 += ways;
}

Console.WriteLine(possibleDesigns2);

//var possibleDesigns = 0;

//foreach (var design in designs)
//{
//    if (CanConstruct(design, towels, memo)) possibleDesigns++;
//}

//Console.WriteLine(possibleDesigns);

//bool CanConstruct(string design, List<string> towels, Dictionary<string, bool> memo)
//{
//    if (memo.ContainsKey(design))
//    {
//        return memo[design];
//    }

//    if (string.IsNullOrEmpty(design)) return true;

//    foreach (var towel in towels)
//    {
//        if (design.StartsWith(towel))
//        {
//            string remaining = design.Substring(towel.Length);
//            if (CanConstruct(remaining, towels, memo))
//            {
//                memo[design] = true;
//                return true;
//            }
//        }
//    }

//    memo[design] = false;
//    return false;
//}


long CanConstruct(string target, string[] patterns, Dictionary<string, long> memo)
{
    // Check memoization
    if (memo.ContainsKey(target)) return memo[target];

    // Base case: empty target
    if (string.IsNullOrEmpty(target)) return 1;

    long totalWays = 0;

    // Try each pattern
    foreach (string pattern in patterns)
    {
        if (target.StartsWith(pattern))
        {
            string remaining = target.Substring(pattern.Length);
            totalWays += CanConstruct(remaining, patterns, memo);
        }
    }

    // Cache the result
    memo[target] = totalWays;
    return totalWays;
}
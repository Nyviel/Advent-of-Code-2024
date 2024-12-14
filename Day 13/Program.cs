using System.Numerics;
using System.Text.RegularExpressions;

var lines = FileParser.ParseFile("/Advent of Code 2024/Day 13/data.txt");

var pattern = @"Button A: X\+(\d+), Y\+(\d+)\s*Button B: X\+(\d+), Y\+(\d+)\s*Prize: X=(\d+), Y=(\d+)";
var machines = new List<Machine>();

for (int i = 0; i < lines.Length; i += 4)
{
    var block = string.Join(" ", lines, i, 3);
    var match = Regex.Match(block, pattern);

    if (match.Success)
    {
        // Add 10000000000000 to the prize X and Y positions
        machines.Add(new Machine
        {
            ButtonA = (long.Parse(match.Groups[1].Value), long.Parse(match.Groups[2].Value)),
            ButtonB = (long.Parse(match.Groups[3].Value), long.Parse(match.Groups[4].Value)),
            Prize = (long.Parse(match.Groups[5].Value) + 10000000000000, long.Parse(match.Groups[6].Value) + 10000000000000)
        });
    }
}

BigInteger totalTokens = 0;
foreach (var machine in machines)
{
    // Check if it's possible to reach the prize
    if (CanReachPrize(
        machine.ButtonA.X, machine.ButtonA.Y,
        machine.ButtonB.X, machine.ButtonB.Y,
        machine.Prize.X, machine.Prize.Y,
        out long buttonAPresses, out long buttonBPresses))
    {
        Console.WriteLine($"{machine.ButtonA} | {machine.ButtonB} | {machine.Prize} | {buttonAPresses} {buttonBPresses}");
        // Calculate the total cost in tokens
        totalTokens += buttonAPresses * 3 + buttonBPresses;
    }
}

Console.WriteLine(totalTokens);

bool CanReachPrize(long ax, long ay, long bx, long by, long prizeX, long prizeY, out long na, out long nb)
{
    // Calculate determinant
    long delta = ax * by - ay * bx;

    if (delta == 0)
    {
        // No unique solution exists
        na = 0;
        nb = 0;
        return false;
    }

    // Calculate determinants for na and nb
    long deltaNa = prizeX * by - prizeY * bx;
    long deltaNb = ax * prizeY - ay * prizeX;

    // Check divisibility to ensure solutions are integers
    if (deltaNa % delta != 0 || deltaNb % delta != 0)
    {
        na = 0;
        nb = 0;
        return false;
    }

    // Compute na and nb
    na = deltaNa / delta;
    nb = deltaNb / delta;

    return true;
}

class Machine
{
    public (long X, long Y) ButtonA { get; set; }
    public (long X, long Y) ButtonB { get; set; }
    public (long X, long Y) Prize { get; set; }
}

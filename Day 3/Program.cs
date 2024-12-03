using System.Text.RegularExpressions;

var lines = FileParser.ParseFile("/Advent of Code 2024/Day 3/data.txt");

string oneLine = "";

foreach (var line in lines)
{
    oneLine += line;
}

string pattern = @"(?:mul|do|don't)\((\d*),?(\d*)\)";

Regex regex = new Regex(pattern);

MatchCollection matches = regex.Matches(oneLine);

int total = 0;
bool canMultiply = true;
foreach (Match match in matches)
{
    string fullMatch = match.Value;

    if (fullMatch.Contains("do()"))
        canMultiply = true;

    if (fullMatch.Contains("don't()"))
        canMultiply = false;

    if (fullMatch.Contains("mul("))
    {
        int num1 = int.Parse(match.Groups[1].Value);
        int num2 = int.Parse(match.Groups[2].Value);
        if (canMultiply)
            total += num1 * num2;
    }
}

Console.WriteLine(total);



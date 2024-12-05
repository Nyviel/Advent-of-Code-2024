var lines = FileParser.ParseFile("/Advent of Code 2024/Day 5/data.txt");

Dictionary<int, int[]> rules = new Dictionary<int, int[]>();
List<List<int>> pages = new List<List<int>>();
bool parsingRules = true;
foreach (var line in lines)
{
    if (line.Length == 0)
    {
        parsingRules = false; continue;
    }

    if (parsingRules)
    {
        var data = line.Split('|');
        var key = int.Parse(data[0]);
        var value = int.Parse(data[1]);
        if (rules.ContainsKey(key))
        {
            rules[key] = rules[key].Concat([value]).ToArray();
        }
        else
        {
            rules.Add(key, [value]);
        }
    }
    else
    {
        var data = line.Split(',');
        pages.Add(data.Select(int.Parse).ToList());
    }
}

List<List<int>> sortedPagesCollection = [];
foreach (var pageList in pages)
{
    List<int> list = new List<int>(pageList);
    list.Sort((a, b) => compareValues(a, b));
    sortedPagesCollection.Add(list);
}

List<List<int>> initialIncorrectPageLines = [];
for (int i = 0; i < sortedPagesCollection.Count; i++)
{
    if (!sortedPagesCollection[i].SequenceEqual(pages[i]))
    {
        initialIncorrectPageLines.Add(sortedPagesCollection[i].ToList());
    }
}

Console.WriteLine(initialIncorrectPageLines.Sum(page => page[page.Count / 2]));

int compareValues(int a, int b)
{
    if (rules.ContainsKey(a))
    {
        if (rules[a].Contains(b))
        {
            return -1;
        }
    }

    return 1;
}
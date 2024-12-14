var lines = FileParser.ParseFile("/Advent of Code 2024/Day 12/data.txt");

List<KeyValuePair<int, int>> visitedPlots = [];
List<(int area, int perimeter, char type, HashSet<(int, int)> plots)> regions = [];
for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        HashSet<(int, int)> plots = [];
        (int area, int perimeter) = Gardener(i, j, lines[i][j], plots);
        if (area == 0) continue;
        regions.Add((area, perimeter, lines[i][j], new HashSet<(int, int)>(plots)));
    }
}

var sum = 0;
foreach (var region in regions)
{
    Console.WriteLine(string.Join(" ", region.plots));
    sum += region.area * region.perimeter;
}
Console.WriteLine(sum);

// [{cols: ['L', 'R'], rows: ['U', 'D']}]
List<RegionPerimeters> newPerimeters = [];
foreach (var region in regions)
{
    var currentRegion = new RegionPerimeters { cols = [], rows = [] };
    foreach (var plot in region.plots)
    {
        int i = plot.Item1;
        int j = plot.Item2;
        // Check for rows above and below if type is different
        if (i == 0 || lines[i - 1][j] != region.type)
        {
            if (!currentRegion.rows.ContainsKey(i))
            {
                currentRegion.rows.Add(i, []);
            }
            if (!currentRegion.rows[i].Contains('U'))
            {
                currentRegion.rows[i].Add('U');
            }
        }

        if (i == lines.Length - 1 || lines[i + 1][j] != region.type)
        {
            if (!currentRegion.rows.ContainsKey(i))
            {
                currentRegion.rows.Add(i, []);
            }
            if (!currentRegion.rows[i].Contains('D'))
            {
                currentRegion.rows[i].Add('D');
            }
        }

        // Check for cols above and below if type is different
        if (j == 0 || lines[i][j - 1] != region.type)
        {
            if (!currentRegion.cols.ContainsKey(j))
            {
                currentRegion.cols.Add(j, []);
            }
            if (!currentRegion.cols[j].Contains('L'))
            {
                currentRegion.cols[j].Add('L');
            }
        }

        if (j == lines[i].Length - 1 || lines[i][j + 1] != region.type)
        {
            if (!currentRegion.cols.ContainsKey(j))
            {
                currentRegion.cols.Add(j, []);
            }
            if (!currentRegion.cols[j].Contains('R'))
            {
                currentRegion.cols[j].Add('R');
            }
        }

    }
    Console.WriteLine($"Type: {region.type}");
    Console.WriteLine("Cols");
    foreach (var kvp in currentRegion.cols)
    {
        int key = kvp.Key;
        List<char> chars = kvp.Value;

        Console.WriteLine($"Key: {key}, Characters: {string.Join(", ", chars)}");
    }
    Console.WriteLine("Rows");
    foreach (var kvp in currentRegion.rows)
    {
        int key = kvp.Key;
        List<char> chars = kvp.Value;

        Console.WriteLine($"Key: {key}, Characters: {string.Join(", ", chars)}");
    }
    newPerimeters.Add(currentRegion);
}

var sum2 = 0;
for (int i = 0; i < regions.Count; i++)
{
    var area = regions[i].area;
    var newR = newPerimeters[i];
    Console.WriteLine(area);

    var newPerimeter = 0;
    foreach (var kvp in newR.cols)
    {
        int key = kvp.Key;
        List<char> chars = kvp.Value;
        newPerimeter += chars.Count;
    }
    foreach (var kvp in newR.rows)
    {
        int key = kvp.Key;
        List<char> chars = kvp.Value;
        newPerimeter += chars.Count;
    }
    Console.WriteLine($"type:{regions[i].type}, perimeter: {newPerimeter}, area: {area}");
    sum2 += area * newPerimeter;
}
Console.WriteLine(sum2);

// 1. Walk the garden on plants of given type
// 2. Mark the visited plots on the list to not re-enter
// 3. Count the area and perimeter as the navigation continues
// 4. Once no more plots are left of said type return area and perimeter
(int, int) Gardener(int i, int j, char type, HashSet<(int, int)> plots)
{
    // Break the loop if already visited this plot before
    if (visitedPlots.Contains(new KeyValuePair<int, int>(i, j)))
    {
        return (0, 0);
    }

    visitedPlots.Add(new KeyValuePair<int, int>(i, j));
    plots.Add((i, j));
    var area = 1;
    var perimeter = 0;

    if (i == 0 || lines[i - 1][j] != type)
        perimeter++;
    if (i == lines.Length - 1 || lines[i + 1][j] != type)
        perimeter++;
    if (j == 0 || lines[i][j - 1] != type)
        perimeter++;
    if (j == lines[i].Length - 1 || lines[i][j + 1] != type)
        perimeter++;

    if (i > 0 && lines[i - 1][j] == type)
    {
        var newAcc = Gardener(i - 1, j, type, plots);
        area += newAcc.Item1;
        perimeter += newAcc.Item2;
    }

    if (i + 1 < lines.Length && lines[i + 1][j] == type)
    {
        var newAcc = Gardener(i + 1, j, type, plots);
        area += newAcc.Item1;
        perimeter += newAcc.Item2;
    }

    if (j > 0 && lines[i][j - 1] == type)
    {
        var newAcc = Gardener(i, j - 1, type, plots);
        area += newAcc.Item1;
        perimeter += newAcc.Item2;
    }

    if (j + 1 < lines[0].Length && lines[i][j + 1] == type)
    {
        var newAcc = Gardener(i, j + 1, type, plots);
        area += newAcc.Item1;
        perimeter += newAcc.Item2;
    }

    return (area, perimeter);
}

class RegionPerimeters
{
    public Dictionary<int, List<char>> cols { get; set; }
    public Dictionary<int, List<char>> rows { get; set; }
}
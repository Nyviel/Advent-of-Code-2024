using System.Text.RegularExpressions;

var lines = File.ReadAllLines("/Advent of Code 2024/Day 14/data.txt");

var robots = new Dictionary<int, (int x, int y, int vX, int vY)>();

var pattern = @"(-?\d+),(-?\d+)\s*v=(-?\d+),(-?\d+)";
for (int i = 0; i < lines.Length; i++)
{
    var match = Regex.Match(lines[i], pattern);
    robots.Add(i,
        (int.Parse(match.Groups[1].Value),
        int.Parse(match.Groups[2].Value),
        int.Parse(match.Groups[3].Value),
        int.Parse(match.Groups[4].Value)));
}

bool partOne = false;
var loops = partOne ? 100 : 15000;
var width = 101;
var height = 103;
char[,] grid = new char[height, width];

for (int i = 0; i < loops; i++)
{
    for (int j = 0; j < robots.Count; j++)
    {
        robots[j] = (
            (robots[j].x + robots[j].vX + width) % width,
            (robots[j].y + robots[j].vY + height) % height,
            robots[j].vX,
            robots[j].vY
        );
    }

    for (int y = 0; y < height; y++)
        for (int x = 0; x < width; x++)
            grid[y, x] = '.';

    foreach (var robot in robots)
    {
        var (x, y, _, _) = robot.Value;
        grid[y, x] = '#';
    }

    // Check if a tree pattern matches
    if (IsTreeInGrid(grid))
    {
        Console.WriteLine($"Tree alignment found at iteration {i}!");
        printGrid();
        break;
    }
}

int midX = width / 2, midY = height / 2;

var quadrants = new Dictionary<string, (int xMin, int xMax, int yMin, int yMax)>
{
    { "Q1", (0, midX - 1, 0, midY - 1) },
    { "Q2", (midX + 1, width - 1, 0, midY - 1) },
    { "Q3", (0, midX - 1, midY + 1, height - 1) },
    { "Q4", (midX + 1, width - 1, midY + 1, height - 1) }
};

var quadrantCounts = new Dictionary<string, int>
{
    { "Q1", 0 },
    { "Q2", 0 },
    { "Q3", 0 },
    { "Q4", 0 }
};

foreach (var robot in robots)
{
    var (x, y, _, _) = robot.Value;

    foreach (var quadrant in quadrants)
    {
        var (xMin, xMax, yMin, yMax) = quadrant.Value;

        if (x >= xMin && x <= xMax && y >= yMin && y <= yMax)
        {
            quadrantCounts[quadrant.Key]++;
            break;
        }
    }
}

var sum = 1;
foreach (var quadrant in quadrantCounts)
{
    sum *= quadrant.Value;
}

Console.WriteLine(sum);

void printGrid()
{
    for (int j = 0; j < height; j++)
    {
        for (int i = 0; i < width; i++)
        {
            bool robotHex = false;
            foreach (var robot in robots)
            {
                if (robot.Value.x == i && robot.Value.y == j)
                {
                    robotHex = true;
                    Console.Write("#");
                }
            }
            if (!robotHex)
            {
                Console.Write(".");
            }
        }
        Console.Write("\n");
    }
}
bool IsTreeInGrid(char[,] grid)
{
    for (int peakY = 0; peakY < height; peakY++)
    {
        for (int peakX = 0; peakX < width; peakX++)
        {
            if (grid[peakY, peakX] != '#')
                continue;

            if (IsSymmetricalTree(grid, peakX, peakY))
            {
                return true;
            }
        }
    }
    return false;
}

// Actually looking for triangular growth
bool IsSymmetricalTree(char[,] grid, int peakX, int peakY)
{
    int currentWidth = 1;

    // Check for pyramid growth downwards
    for (int y = peakY + 1; y < height; y++)
    {
        int robotCount = 0;
        int leftEdge = peakX - currentWidth;
        int rightEdge = peakX + currentWidth;

        // Count # from left to right of expected triangle coords
        for (int x = Math.Max(0, leftEdge); x <= Math.Min(width - 1, rightEdge); x++)
        {
            if (grid[y, x] == '#')
                robotCount++;
        }

        // Check if the row follows the expected width of a tree
        // If the above is for example ### then we expect this one to be ######
        if (robotCount != currentWidth * 2 + 1)
        {
            return currentWidth > 4; // if the current width is 5 and bigger we have found a triangle
        }

        currentWidth++; // Expand the triangle for the next row
    }
    return currentWidth > 4;// if the current width is 5 and bigger we have found a triangle
}
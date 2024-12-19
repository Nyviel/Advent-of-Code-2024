var lines = File.ReadAllLines("/Advent of Code 2024/Day 16/data.txt");
HashSet<(int, int)> visited = [];
var start = FindStart();

if (start == (-1, -1))
{
    throw new Exception("No start found");
}

if (!EndExists())
{
    throw new Exception("No end found");
}

var data = FindShortestPath(start);
Console.WriteLine(data);

(int, int) FindStart()
{
    for (int i = 0; i < lines.Length; i++)
    {
        for (int j = 0; j < lines[i].Length; j++)
        {
            if (lines[i][j] == 'S') return (i, j);
        }
    }

    return (-1, -1);
}

bool EndExists()
{
    for (int i = 0; i < lines.Length; i++)
    {
        for (int j = 0; j < lines[i].Length; j++)
        {
            if (lines[i][j] == 'E') return true;
        }
    }

    return false;
}

(int steps, int directionChanges) FindShortestPath((int x, int y) start)
{
    // Define directions: (dx, dy, directionChar)
    var directions = new (int dx, int dy, char dir)[]
    {
        (-1, 0, '^'), // Up
        (1, 0, 'v'),  // Down
        (0, 1, '>'),  // Right
        (0, -1, '<')  // Left
    };

    // Initialize the queue: (position, steps, previousDirection)
    var queue = new Queue<((int x, int y) pos, int steps, char prevDir)>();
    queue.Enqueue((start, 0, 'x'));
    var visited = new HashSet<(int, int)> { start };

    while (queue.Count > 0)
    {
        var (currentPos, steps, prevDir) = queue.Dequeue();

        // Check if we reached the end
        if (lines[currentPos.x][currentPos.y] == 'E')
        {
            return (steps, 0); // Return steps (no direction changes tracked for simplicity)
        }

        foreach (var (dx, dy, newDir) in directions)
        {
            (int x, int y) nextPos = (currentPos.x + dx, currentPos.y + dy);

            // Check bounds and validity
            if (nextPos.x >= 0 && nextPos.x < lines.Length &&
                nextPos.y >= 0 && nextPos.y < lines[0].Length &&
                lines[nextPos.x][nextPos.y] != '#' && // Wall
                !visited.Contains(nextPos)) // Not visited
            {
                visited.Add(nextPos); // Mark as visited
                int dirChange = prevDir != 'x' && prevDir != newDir ? 1 : 0;
                queue.Enqueue((nextPos, steps + 1, newDir));
            }
        }
    }

    // No path found
    return (-1, -1);
}

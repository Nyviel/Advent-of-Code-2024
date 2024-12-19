var lines = File.ReadAllLines("/Advent of Code 2024/Day 18/data.txt");

var maxHeight = 71;
var maxWidth = 71;
char[][] maze = new char[maxHeight][];

for (int i = 0; i < maxHeight; i++)
{
    maze[i] = new char[maxWidth];
    for (int j = 0; j < maxWidth; j++)
    {
        maze[i][j] = '.'; // Initialize each element with '.'
    }
}

int limiter = 0;
foreach (var line in lines)
{
    Queue<(int, int)> path = [];
    HashSet<(int, int)> visited = [];
    var nodesInLayer = 1;
    var nodesInNextLayer = 0;
    bool endReached = false;
    int x = int.Parse(line.Split(',')[0]);
    var y = int.Parse(line.Split(',')[1]);
    maze[y][x] = '#';
    limiter++;
    var canSolve = solve(path, visited, nodesInLayer, nodesInNextLayer, endReached);
    if (canSolve == -1)
    {
        Console.WriteLine($"{x}, {y}");
        break;
    }
}

//Console.WriteLine(solve());

int solve(Queue<(int, int)> path, HashSet<(int, int)> visited, int nodesInLayer, int nodesInNextLayer, bool endReached)
{
    var moves = 0;

    path.Enqueue((0, 0));
    visited.Add((0, 0));
    while (path.Count > 0)
    {
        var node = path.Dequeue();
        if (IsEnd(node))
        {
            endReached = true;
            break;
        }
        ExploreNeighbours(node, visited, path, nodesInNextLayer);
        nodesInLayer--;
        if (nodesInLayer == 0)
        {
            nodesInLayer = nodesInNextLayer;
            nodesInNextLayer = 0;
            moves++;
        }
    }
    if (endReached)
    {
        return moves;
    }

    return -1;
}

bool IsEnd((int, int) node)
{
    return node.Item1 == maxHeight - 1 && node.Item2 == maxWidth - 1;
}

void ExploreNeighbours((int, int) node, HashSet<(int, int)> visited, Queue<(int, int)> path, int nodesInNextLayer)
{
    (int dx, int dy)[] Directions = { (-1, 0), (0, 1), (1, 0), (0, -1) };
    for (int i = 0; i < 4; i++)
    {
        var x = node.Item1 + Directions[i].dx;
        var y = node.Item2 + Directions[i].dy;

        if (x < 0 || x >= maze.Length) continue;
        if (y < 0 || y >= maze[0].Length) continue;

        if (visited.Contains((x, y))) continue;
        if (maze[x][y] == '#') continue;

        path.Enqueue((x, y));
        visited.Add((x, y));
        nodesInNextLayer++;
    }
}
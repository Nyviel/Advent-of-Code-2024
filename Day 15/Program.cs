var lines = File.ReadAllLines("/Advent of Code 2024/Day 15/data.txt");

List<char[]> map = [];
int i = 0;
for (; i < lines.Length; i++)
{
    if (lines[i].Length == 0) break;
    map.Add(lines[i].ToCharArray());
}

List<char> moves = [];

for (; i < lines.Length; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        moves.Add(lines[i][j]);
    }
}

var robotCoords = findRobot();
if (robotCoords == (-1, -1))
{
    throw new Exception("No robot on the map");
}
foreach (var move in moves)
{
    if (move == '^')
    {
        if (robotCoords.i > 0 && map[robotCoords.i - 1][robotCoords.j] == '.')
        {
            map[robotCoords.i][robotCoords.j] = '.';
            robotCoords.i--;
            map[robotCoords.i][robotCoords.j] = '@';
        }
        else if (robotCoords.i > 0 && map[robotCoords.i - 1][robotCoords.j] == 'O')
        {
            var validCoords = CanMoveBox(robotCoords.i - 1, robotCoords.j, move);
            if (validCoords != (-1, -1))
            {
                MoveBoxes(validCoords.Item1, validCoords.Item2, move);

                map[robotCoords.i][robotCoords.j] = '.';
                robotCoords.i--;
                map[robotCoords.i][robotCoords.j] = '@';

            }
        }

    }
    else if (move == 'v')
    {
        if (robotCoords.i + 1 < map.Count && map[robotCoords.i + 1][robotCoords.j] == '.')
        {
            map[robotCoords.i][robotCoords.j] = '.';
            robotCoords.i++;
            map[robotCoords.i][robotCoords.j] = '@';
        }
        else if (robotCoords.i + 1 < map.Count && map[robotCoords.i + 1][robotCoords.j] == 'O')
        {
            var validCoords = CanMoveBox(robotCoords.i + 1, robotCoords.j, move);
            if (validCoords != (-1, -1))
            {
                MoveBoxes(validCoords.Item1, validCoords.Item2, move);

                map[robotCoords.i][robotCoords.j] = '.';
                robotCoords.i++;
                map[robotCoords.i][robotCoords.j] = '@';
            }
        }
    }
    else if (move == '>')
    {
        if (robotCoords.j + 1 < map[0].Length && map[robotCoords.i][robotCoords.j + 1] == '.')
        {
            map[robotCoords.i][robotCoords.j] = '.';
            robotCoords.j++;
            map[robotCoords.i][robotCoords.j] = '@';
        }
        else if (robotCoords.j + 1 < map[0].Length && map[robotCoords.i][robotCoords.j + 1] == 'O')
        {
            var validCoords = CanMoveBox(robotCoords.i, robotCoords.j + 1, move);
            if (validCoords != (-1, -1))
            {
                MoveBoxes(validCoords.Item1, validCoords.Item2, move);

                map[robotCoords.i][robotCoords.j] = '.';
                robotCoords.j++;
                map[robotCoords.i][robotCoords.j] = '@';
            }
        }
    }
    else if (move == '<')
    {
        if (robotCoords.j > 0 && map[robotCoords.i][robotCoords.j - 1] == '.')
        {
            map[robotCoords.i][robotCoords.j] = '.';
            robotCoords.j--;
            map[robotCoords.i][robotCoords.j] = '@';
        }
        else if (robotCoords.j > 0 && map[robotCoords.i][robotCoords.j - 1] == 'O')
        {
            var validCoords = CanMoveBox(robotCoords.i, robotCoords.j - 1, move);
            if (validCoords != (-1, -1))
            {
                MoveBoxes(validCoords.Item1, validCoords.Item2, move);

                map[robotCoords.i][robotCoords.j] = '.';
                robotCoords.j--;
                map[robotCoords.i][robotCoords.j] = '@';
            }
        }
    }
}

for (int y = 0; y < map.Count; y++)
{
    for (int x = 0; x < map[y].Length; x++)
    {
        Console.Write(map[y][x]);
    }
    Console.WriteLine();
}

var sum = 0;
for (int y = 0; y < map.Count; y++)
{
    for (int x = 0; x < map[y].Length; x++)
    {
        if (map[y][x] == 'O')
        {
            sum += 100 * y + x;
        }
    }
}

Console.WriteLine(sum);

(int i, int j) findRobot()
{
    for (int i = 0; i < map.Count; i++)
    {
        for (int j = 0; j < map[i].Length; j++)
        {
            if (map[i][j] == '@') return (i, j);
        }
    }
    return (-1, -1);
}

// Will return a valid coordinate of an empty spot after going through box/es
// or (-1, -1) if the box is next to a wall or the box line goes up to a wall
(int, int) CanMoveBox(int i, int j, char move)
{
    if (i <= 0 || j <= 0 || i + 1 >= map.Count || j + 1 >= map[0].Length || map[i][j] == '#') return (-1, -1);
    if (map[i][j] == '.') return (i, j);

    if (move == '^')
    { return CanMoveBox(i - 1, j, move); }
    if (move == 'v')
    { return CanMoveBox(i + 1, j, move); }
    if (move == '<')
    { return CanMoveBox(i, j - 1, move); }
    if (move == '>')
    { return CanMoveBox(i, j + 1, move); }

    return (-1, -1);
}

// If move right then look left and start shifting boxes from left to i,j
// Then move j - 1 and shift and so on until @ - Robot is found.
void MoveBoxes(int i, int j, char move)
{
    if (map[i][j] == '@' || map[i][j] == '#') return;

    if (move == '^')
    {
        if (map[i + 1][j] == 'O')
        {
            map[i + 1][j] = '.';
            map[i][j] = 'O';
            MoveBoxes(i + 1, j, move);
        }
    }
    else if (move == 'v')
    {
        if (map[i - 1][j] == 'O')
        {
            map[i - 1][j] = '.';
            map[i][j] = 'O';
            MoveBoxes(i - 1, j, move);
        }
    }
    else if (move == '>')
    {
        if (map[i][j - 1] == 'O')
        {
            map[i][j - 1] = '.';
            map[i][j] = 'O';
            MoveBoxes(i, j - 1, move);
        }
    }
    else if (move == '<')
    {
        if (map[i][j + 1] == 'O')
        {
            map[i][j + 1] = '.';
            map[i][j] = 'O';
            MoveBoxes(i, j + 1, move);
        }
    }
}
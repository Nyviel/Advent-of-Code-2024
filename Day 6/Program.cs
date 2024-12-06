using Day_6;

class Program
{
    static HashSet<int> visited = new HashSet<int>();
    static bool loopOccured = false;
    public static void Main(string[] args)
    {
        string[] lines = FileParser.ParseFile("/Advent of Code 2024/Day 6/data.txt");

        (int startX, int startY, Direction direction) = findGuard(lines);

        if (startX == -1 || startY == -1)
        {
            throw new Exception("No guard present in the lab");
        }

        // Part 1 
        // navigateMap(lines, startX, startY, direction);
        // Console.WriteLine(countX(lines));

        // Part 2
        int count = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                // Place a new blockade at i, j
                char[] lineChars = lines[i].ToCharArray();
                bool changedState = false;
                if (lines[i][j] == '.')
                {
                    changedState = true;
                    lineChars[j] = '#';
                    lines[i] = new string(lineChars);
                }

                visited.Clear();
                navigateMap(lines, startX, startY, direction);

                // Restore previous state
                if (changedState)
                {
                    lineChars[j] = '.';
                    lines[i] = new string(lineChars);
                }

                if (loopOccured)
                {
                    count++;
                    loopOccured = false;
                }
            }
        }
        Console.WriteLine(count);
    }

    public static void navigateMap(string[] lines, int x, int y, Direction direction)
    {
        // Boundary exit statements
        if (x < 0 || x >= lines.Length) return;
        if (y < 0 || y >= lines[0].Length) return;

        // Part 2 modification
        if (visited.Contains(encodeState(x, y, direction, lines.Length)))
        {
            loopOccured = true;
            return;
        }
        visited.Add(encodeState(x, y, direction, lines.Length));

        // Uncomment this for part 1 and comment the part2 modification
        //char[] lineChars = lines[x].ToCharArray();
        //lineChars[y] = 'X';
        //lines[x] = new string(lineChars);

        // Look ahead for #
        if (direction == Direction.left)
        {
            // No point looking ahead if we're at the end
            if (y <= 0) { return; }

            // Check if there's a blockade to the left
            if (lines[x][y - 1] == '#')
            {
                // Change 90 degrees, now going up
                navigateMap(lines, x, y, Direction.up);
            }
            else
            {
                // Continue left
                navigateMap(lines, x, y - 1, direction);
            }
        }
        else if (direction == Direction.right)
        {
            // No point looking ahead if we're at the end
            if (y >= lines[0].Length - 1) { return; }

            // Check if there's a blockade to the right
            if (lines[x][y + 1] == '#')
            {
                // Change 90 degrees, now going down
                navigateMap(lines, x, y, Direction.down);
            }
            else
            {
                // Continue right
                navigateMap(lines, x, y + 1, direction);
            }
        }
        else if (direction == Direction.up)
        {
            // No point looking ahead if we're at the end
            if (x <= 0) { return; }

            // Check if there's a blockade above
            if (lines[x - 1][y] == '#')
            {
                // Change 90 degrees, now going right
                navigateMap(lines, x, y, Direction.right);
            }
            else
            {
                // Continue up
                navigateMap(lines, x - 1, y, direction);
            }
        }
        else
        {
            // No point looking ahead if we're at the end
            if (x >= lines[0].Length - 1) { return; }

            // Check if there's a blockade to the right
            if (lines[x + 1][y] == '#')
            {
                // Change 90 degrees, now going left
                navigateMap(lines, x, y, Direction.left);
            }
            else
            {
                // Continue down
                navigateMap(lines, x + 1, y, direction);
            }
        }
    }

    public static int countX(string[] lines)
    {
        int count = 0;
        foreach (var line in lines)
        {
            foreach (var letter in line)
            {
                if (letter == 'X') count++;
            }
        }
        return count;
    }

    public static (int row, int col, Direction direction) findGuard(string[] lines)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == '<')
                {
                    return (i, j, Direction.left);
                }
                if (lines[i][j] == '>')
                {
                    return (i, j, Direction.right);
                }
                if (lines[i][j] == '^')
                {
                    return (i, j, Direction.up);
                }
                if (lines[i][j] == 'v')
                {
                    return (i, j, Direction.down);
                }

            }
        }

        return (-1, -1, Direction.left);
    }

    public static int encodeState(int x, int y, Direction direction, int maxWidth)
    {
        return x * maxWidth * 4 + y * 4 + (int)direction;
    }
}
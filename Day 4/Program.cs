using Day_4;

var lines = FileParser.ParseFile("/Advent of Code 2024/Day 4/data.txt");

//Console.WriteLine(Part1(lines));
Console.WriteLine(Part2(lines));

Direction GetDirection(int oldRow, int oldCol, int row, int col)
{
    // Diagonal directions
    if (oldRow > row && oldCol > col) return Direction.diagonalTopLeft;
    if (oldRow > row && oldCol < col) return Direction.diagonalTopRight;
    if (oldRow < row && oldCol > col) return Direction.diagonalBottomLeft;
    if (oldRow < row && oldCol < col) return Direction.diagonalBottomRight;

    // Vertical directions
    if (oldRow > row && oldCol == col) return Direction.verticalUp;
    if (oldRow < row && oldCol == col) return Direction.verticalDown;

    // Horizontal directions
    if (oldRow == row && oldCol > col) return Direction.horizontalLeft;
    if (oldRow == row && oldCol < col) return Direction.horizontalRight;

    return Direction.unknown;
}

List<int[]> FindCharNear(char ch, int y, int x, string[] lines)
{
    List<int[]> locatedCoords = [];

    for (int i = -1; i <= 1; i++)
    {
        for (int j = -1; j <= 1; j++)
        {
            var row = y - i;
            var col = x - j;

            if (row >= 0 && col >= 0 && row < lines.Length && col < lines[0].Length)
            {
                if (lines[row][col] == ch)
                {
                    locatedCoords.Add([row, col]);
                }
            }
        }
    }

    return locatedCoords;
}

int Part1(string[] lines)
{
    HashSet<string> registeredWords = new HashSet<string>();
    int occurrences = 0;

    for (int i = 0; i < lines.Length; i++)
    {
        for (int j = 0; j < lines[i].Length; j++)
        {
            var letter = lines[i][j];

            if (letter == 'X')
            {
                var nearbyMCoordinates = FindCharNear('M', i, j, lines);
                foreach (var mCoords in nearbyMCoordinates)
                {
                    var mRow = mCoords[0];
                    var mCol = mCoords[1];

                    // Get direction from current X to current M, next A has to follow this Direction
                    Direction mDirection = GetDirection(i, j, mRow, mCol);

                    var nearbyACoordinates = FindCharNear('A', mRow, mCol, lines);
                    foreach (var aCoords in nearbyACoordinates)
                    {
                        var aRow = aCoords[0];
                        var aCol = aCoords[1];

                        Direction aDirection = GetDirection(mRow, mCol, aRow, aCol);
                        // If the if is correct then direction was preserved
                        if (mDirection == aDirection)
                        {
                            var nearbySCoordinates = FindCharNear('S', aRow, aCol, lines);
                            foreach (var sCoords in nearbySCoordinates)
                            {
                                var sRow = sCoords[0];
                                var sCol = sCoords[1];

                                Direction sDirection = GetDirection(aRow, aCol, sRow, sCol);
                                // If the last direction was preserved then we found XMAS
                                if (sDirection == aDirection)
                                {
                                    // Create a unique key for the sequence
                                    string sequenceKey = $"{i}-{j},{mRow}-{mCol},{aRow}-{aCol},{sRow}-{sCol}-{sDirection}";

                                    // If not already registered, count it
                                    if (!registeredWords.Contains(sequenceKey))
                                    {
                                        registeredWords.Add(sequenceKey);
                                        occurrences++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    return (occurrences);
}

int Part2(string[] lines)
{
    HashSet<string> foundLines = new HashSet<string>();
    int occurrences = 0;

    for (int i = 0; i < lines.Length; i++)
    {
        for (int j = 0; j < lines[i].Length; j++)
        {
            var letter = lines[i][j];

            if (letter == 'A')
            {
                // Check the diagonals

                var nearbyMCoordinates = FindCharNear('M', i, j, lines);
                var nearbySCoordinates = FindCharNear('S', i, j, lines);

                foreach (var mCoords in nearbyMCoordinates)
                {
                    var mRow = mCoords[0];
                    var mCol = mCoords[1];
                    Direction direction = GetDirection(i, j, mRow, mCol);

                    // Check if S exists on the opposite diagonal
                    if (direction == Direction.diagonalTopLeft)
                    {
                        foreach (var sCoords in nearbySCoordinates)
                        {
                            var sRow = sCoords[0];
                            var sCol = sCoords[1];
                            Direction sDirection = GetDirection(i, j, sRow, sCol);

                            if (sDirection == Direction.diagonalBottomRight)
                            {
                                string line = $"{mRow}-{mCol},{i}-{j},{sRow}-{sCol}";
                                foundLines.Add(line);
                            }
                        }
                    }

                    if (direction == Direction.diagonalTopRight)
                    {
                        foreach (var sCoords in nearbySCoordinates)
                        {
                            var sRow = sCoords[0];
                            var sCol = sCoords[1];
                            Direction sDirection = GetDirection(i, j, sRow, sCol);

                            if (sDirection == Direction.diagonalBottomLeft)
                            {
                                string line = $"{mRow}-{mCol},{i}-{j},{sRow}-{sCol}";
                                foundLines.Add(line);
                            }
                        }
                    }

                    if (direction == Direction.diagonalBottomLeft)
                    {
                        foreach (var sCoords in nearbySCoordinates)
                        {
                            var sRow = sCoords[0];
                            var sCol = sCoords[1];
                            Direction sDirection = GetDirection(i, j, sRow, sCol);

                            if (sDirection == Direction.diagonalTopRight)
                            {
                                string line = $"{mRow}-{mCol},{i}-{j},{sRow}-{sCol}";
                                foundLines.Add(line);
                            }
                        }
                    }

                    if (direction == Direction.diagonalBottomRight)
                    {
                        foreach (var sCoords in nearbySCoordinates)
                        {
                            var sRow = sCoords[0];
                            var sCol = sCoords[1];
                            Direction sDirection = GetDirection(i, j, sRow, sCol);

                            if (sDirection == Direction.diagonalTopLeft)
                            {
                                string line = $"{mRow}-{mCol},{i}-{j},{sRow}-{sCol}";
                                foundLines.Add(line);
                            }
                        }
                    }
                }

            }
        }
    }

    foreach (var line1 in foundLines)
    {
        var parts1 = line1.Split(',');
        var coordinates1 = parts1[0].Split('-');
        int mRow1 = int.Parse(coordinates1[0]);
        int mCol1 = int.Parse(coordinates1[1]);
        var coordinates2 = parts1[1].Split('-');
        int i1 = int.Parse(coordinates2[0]);
        int j1 = int.Parse(coordinates2[1]);
        var coordinates3 = parts1[2].Split('-');
        int sRow1 = int.Parse(coordinates3[0]);
        int sCol1 = int.Parse(coordinates3[1]);

        foreach (var line2 in foundLines)
        {
            if (line1 == line2) continue;

            var parts2 = line2.Split(',');
            var coordinates4 = parts2[0].Split('-');
            int mRow2 = int.Parse(coordinates4[0]);
            int mCol2 = int.Parse(coordinates4[1]);
            var coordinates5 = parts2[1].Split('-');
            int i2 = int.Parse(coordinates5[0]);
            int j2 = int.Parse(coordinates5[1]);
            var coordinates6 = parts2[2].Split('-');
            int sRow2 = int.Parse(coordinates6[0]);
            int sCol2 = int.Parse(coordinates6[1]);

            if ((i1 == i2 && j1 == j2))
            {
                occurrences++;
            }
        }
    }


    return (occurrences / 2);
}

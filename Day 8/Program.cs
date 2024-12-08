var lines = FileParser.ParseFile("/Advent of Code 2024/Day 8/data.txt");

Dictionary<(int, int, char), List<(int, int, char)>> antennas = [];
List<KeyValuePair<int, int>> antinodes = [];

for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        if (lines[i][j] != '.')
        {
            var matchingAntennas = FindOtherAntennasWithSameFrequency(i, j, lines[i][j]);
            antennas.Add((i, j, lines[i][j]), matchingAntennas);
        }
    }
}

foreach (var antenna in antennas)
{
    applyAntinodes(antenna);
}

foreach (var antenna in antinodes)
{
    //Console.WriteLine(antenna.ToString());
}

var notPresent = 0;
foreach (var antenna in antennas.Keys)
{
    if (!antinodes.Any(a => a.Key == antenna.Item1 && a.Value == antenna.Item2))
    {
        notPresent++;
    }
}

Console.WriteLine(antinodes.Count + notPresent);

void applyAntinodes(KeyValuePair<(int, int, char), List<(int, int, char)>> antenna)
{
    var antennaRow = antenna.Key.Item1;
    var antennaCol = antenna.Key.Item2;

    foreach (var matchingAntenna in antenna.Value)
    {
        var matchingAntennaRow = matchingAntenna.Item1;
        var matchingAntennaCol = matchingAntenna.Item2;

        var differenceRowOne = antennaRow - matchingAntennaRow;
        var differenceColOne = antennaCol - matchingAntennaCol;

        var differenceRowTwo = matchingAntennaRow - antennaRow;
        var differenceColTwo = matchingAntennaCol - antennaCol;

        var rowToApplyAntinodeOne = antennaRow + differenceRowOne;
        var colToApplyAntinodeOne = antennaCol + differenceColOne;

        var rowToApplyAntinodeTwo = matchingAntennaRow + differenceRowTwo;
        var colToApplyAntinodeTwo = matchingAntennaCol + differenceColTwo;

        List<KeyValuePair<int, int>> coordsOfAntinodeOnes = [];
        List<KeyValuePair<int, int>> coordsOfAntinodeTwos = [];

        coordsOfAntinodeOnes.Add(new KeyValuePair<int, int>(rowToApplyAntinodeOne, colToApplyAntinodeOne));
        coordsOfAntinodeTwos.Add(new KeyValuePair<int, int>(rowToApplyAntinodeTwo, colToApplyAntinodeTwo));

        AddAntinodeCoords(coordsOfAntinodeOnes, differenceRowOne, differenceColOne);
        AddAntinodeCoords(coordsOfAntinodeTwos, differenceRowTwo, differenceColTwo);

        foreach (var antinodeToAdd in coordsOfAntinodeOnes.Concat(coordsOfAntinodeTwos))
        {
            KeyValuePair<int, int> keyValuePair = new KeyValuePair<int, int>(antinodeToAdd.Key, antinodeToAdd.Value);
            addAntinode(keyValuePair);
        }
    }
}

void AddAntinodeCoords(List<KeyValuePair<int, int>> coordsOfAntinodes, int differenceRow, int differenceCol)
{
    int i = 0;
    while (true)
    {
        var currentRow = coordsOfAntinodes[i].Key;
        var currentCol = coordsOfAntinodes[i].Value;

        var nextRow = currentRow + differenceRow;
        var nextCol = currentCol + differenceCol;

        if (nextRow < 0 || nextRow >= lines.Length) break;
        if (nextCol < 0 || nextCol >= lines[0].Length) break;

        coordsOfAntinodes.Add(new KeyValuePair<int, int>(nextRow, nextCol));
        i++;
    }
}

void addAntinode(KeyValuePair<int, int> pair)
{
    if (!antinodes.Contains(pair))
    {
        if ((pair.Value >= 0 && pair.Key >= 0) && (pair.Value < lines[0].Length && pair.Key < lines.Length))
            antinodes.Add(pair);
    }
}

List<(int, int, char)> FindOtherAntennasWithSameFrequency(int row, int col, char frequency)
{
    List<(int, int, char)> antennas = new List<(int, int, char)>();
    for (int i = 0; i < lines.Length; i++)
    {
        for (int j = 0; j < lines[i].Length; j++)
        {
            if (i == row && j == col) continue;
            if (lines[i][j] == frequency)
            {
                antennas.Add((i, j, frequency));
            }
        }
    }
    return antennas;
}
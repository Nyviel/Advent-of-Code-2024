var line = FileParser.ParseFile("/Advent of Code 2024/Day 9/data.txt")[0];
long resultOne = PartOne(line);
Console.WriteLine($"Filesystem checksum: {resultOne}");
var resultTwo = PartTwo(line);
Console.WriteLine($"Filesystem checksum: {resultTwo}");

long PartOne(string input)
{
    var disk = ParseInput(input);

    for (int i = 0; i < disk.Count - 1; i++)
    {
        var right = disk[^1];
        var left = disk[i];

        if (left.free >= right.disk)
        {
            var remainingFree = left.free - right.disk;
            disk.Insert(i + 1, (right.id, right.disk, remainingFree));
            disk.RemoveAt(disk.Count - 1);
        }
        else
        {
            disk.Insert(i + 1, (right.id, left.free, 0));
            disk[^1] = (right.id, right.disk - left.free, 0);
            i++;
        }
    }

    return disk.SelectMany(b => Enumerable.Repeat(b.id, b.disk))
        .Select((l, i) => (long)l * i).Sum();
}
List<(int id, int disk, int free)> ParseInput(string input)
     => input.ToCharArray()
         .Chunk(2)
         .Select((c, i) => (id: i, disk: c[0] - '0', free: c.Length > 1 ? c[1] - '0' : 0))
         .ToList();

long PartTwo(string input)
{
    var disk = ParseInput(input);

    for (int i = disk.Count - 1; i > 0;)
    {
        var right = disk[i];
        var prev = disk[i - 1];
        var left = disk.Take(i)
            .Select((b, i) => (index: i, b.id, b.disk, b.free))
            .FirstOrDefault(b => b.free >= right.disk, (index: -1, id: -1, disk: -1, free: -1));

        if (left.index == -1)
        {
            i--; continue;
        }

        disk[i - 1] = (prev.id, prev.disk, prev.free + right.disk + right.free);
        if (left.index == i - 1)
            left = (left.index, left.id, left.disk, disk[i - 1].free);

        disk.RemoveAt(i);
        disk[left.index] = (left.id, left.disk, 0);
        disk.Insert(left.index + 1, (right.id, right.disk, left.free - right.disk));
    }

    return
        disk.SelectMany(b => Enumerable.Repeat(b.id, b.disk).Concat(Enumerable.Repeat(0, b.free)))
        .Select((l, i) => (long)l * i).Sum();
}

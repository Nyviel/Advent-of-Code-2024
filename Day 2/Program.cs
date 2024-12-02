class Program
{
    public static int minDiff = 1;
    public static int maxDiff = 3;
    public static void Main(string[] args)
    {
        var lines = FileParser.ParseFile("/Advent of Code 2024/Day 2/data.txt");

        List<List<int>> reports = new List<List<int>>();

        foreach (var line in lines)
        {
            var rowValues = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            List<int> levels = rowValues.Select(int.Parse).ToList();

            reports.Add(levels);
        }


        Console.WriteLine(Part1(reports));
        Console.WriteLine(Part2(reports));
    }

    public static int Part2(List<List<int>> reports)
    {
        var validReports = 0;
        foreach (var report in reports)
        {
            if (isReportValid(report))
            {
                validReports++;
            }
            else
            {
                for (int i = 0; i < report.Count; i++)
                {
                    var newReport = new List<int>(report);
                    newReport.RemoveAt(i);
                    if (isReportValid(newReport))
                    {
                        validReports++;
                        break;
                    }
                }
            }
        }
        return validReports;
    }

    public static int Part1(List<List<int>> reports)
    {
        var validReports = 0;
        foreach (var report in reports)
        {
            bool validReport = isReportValid(report);

            validReports += Convert.ToInt32(validReport);
        }
        return validReports;
    }

    public static bool isReportValid(List<int> report)
    {
        string tendency = report[0] - report[1] < 0 ? "asc" : "desc";

        for (int i = 0; i < report.Count - 1; i++)
        {
            int diff = report[i] - report[i + 1];

            if (!IsLevelValid(tendency, diff))
                return false;
        }

        return true;
    }

    public static bool IsLevelValid(string tendency, int diff)
    {
        if (tendency == "asc" && diff > 0)
            return false;
        if (tendency == "desc" && diff < 0)
            return false;

        return Math.Abs(diff) >= minDiff && Math.Abs(diff) <= maxDiff;
    }
}


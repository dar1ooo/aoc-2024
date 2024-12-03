using Aoc.Common;

namespace Aoc.Days;

public class Day02 : ICommand
{
    public int Day => 2;

    public void Execute()
    {
        var safeReportCount = 0;
        const string filePath = "Data/day02.txt";

        var lines = File.ReadAllLines(filePath);
        var parsedData = lines
            .Select(line => line.Split(' '))
            .Select(values => values.Select(int.Parse).ToArray())
            .ToList();

        foreach (var line in parsedData)
        {
            if (IsReportSafe(line))
            {
                safeReportCount++;
            }
            else
            {
                var dampenerMakesSafe = line.Select((_, i) => line.Where((_, index) => index != i).ToArray())
                    .Any(IsReportSafe);

                if (dampenerMakesSafe)
                    safeReportCount++;
            }
        }

        Console.WriteLine($"Number of safe reports: {safeReportCount}");
    }

    private static bool IsReportSafe(int[] report)
    {
        if (report.Length < 2) return true;

        var isIncreasing = report[1] > report[0];

        for (var i = 0; i < report.Length - 1; i++)
        {
            var difference = report[i + 1] - report[i];
            var absDifference = Math.Abs(difference);

            if (absDifference is < 1 or > 3)
                return false;

            if ((isIncreasing && difference < 0) || (!isIncreasing && difference > 0))
                return false;
        }

        return true;
    }
}
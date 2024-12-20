using Aoc.Common;

namespace Aoc.Days;

public class Day01 : ICommand
{
    public int Day => 1;

    public void Execute()
    {
        // Part One
        var filePath = "Data/day01.txt";

        var lines = File.ReadAllLines(filePath);

        var leftColumn = new List<int>();
        var rightColumn = new List<int>();

        foreach (var line in lines)
        {
            var columns = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);

            if (columns.Length == 2)
            {
                leftColumn.Add(int.Parse(columns[0]));
                rightColumn.Add(int.Parse(columns[1]));
            }
        }

        leftColumn = leftColumn.OrderBy(x => x).ToList();
        rightColumn = rightColumn.OrderBy(x => x).ToList();

        var totalDistance = leftColumn.Select((t, i) => Math.Abs(t - rightColumn[i])).Sum();
        Console.WriteLine($"Total Distance: {totalDistance}");

        // Part two
        var similarityScore = leftColumn.Select(t => t * rightColumn.Where(x => x == t).ToList().Count).Sum();
        Console.WriteLine($"Similarity Score: {similarityScore}");
    }
}
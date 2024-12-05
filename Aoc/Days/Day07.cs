using Aoc.Common;

namespace Aoc.Days;

public class Day07 : ICommand
{
    public int Day => 7;
    private const string FilePath = "Data/day07.txt";

    public void Execute()
    {
        var lines = File.ReadAllLines(FilePath);
        long totalCombinationResult = 0;

        foreach (var line in lines)
        {
            var parts = line.Split(":");
            var expectedResult = long.Parse(parts[0].Trim());
            var numbers = parts[1].Trim().Split(" ").Select(long.Parse).ToArray();
            var requiredOperators = numbers.Length - 1;
            var possibleCombinations = (int)Math.Pow(3, requiredOperators);

            for (var i = 0; i < possibleCombinations; i++)
            {
                var currentResult = numbers[0];
                var position = i;

                for (var j = 0; j < requiredOperators; j++)
                {
                    var operatorType = position % 3;
                    position /= 3;

                    switch (operatorType)
                    {
                        case 0:
                            currentResult += numbers[j + 1];
                            break;
                        case 1:
                            currentResult *= numbers[j + 1];
                            break;
                        case 2:
                            currentResult = long.Parse(currentResult + numbers[j + 1].ToString());
                            break;
                    }
                }

                if (currentResult != expectedResult) continue;
                totalCombinationResult += expectedResult;
                break;
            }
        }

        Console.WriteLine($"Total lines with at least one valid combination: {totalCombinationResult}");
    }
}
using System.Text.RegularExpressions;
using Aoc.Common;

namespace Aoc.Days;

public partial class Day03 : ICommand
{
    public int Day => 3;

    public void Execute()
    {
        var total = 0;
        var lines = File.ReadAllText("Data/day03.txt");
        var doMul = true;

        foreach (Match match in MulRegex().Matches(lines))
        {
            switch (match.Value)
            {
                case "do()":
                    doMul = true;
                    continue;

                case "don't()":
                    doMul = false;
                    continue;
            }

            if (doMul)
                total += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        }

        Console.WriteLine($"Multiplications = {total}");
    }


    [GeneratedRegex(@"mul\(\s*(\d{1,3})\s*,\s*(\d{1,3})\s*\)|do\(\)|don\'t\(\)")]
    private static partial Regex MulRegex();
}
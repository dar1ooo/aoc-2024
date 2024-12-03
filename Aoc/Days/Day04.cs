using Aoc.Common;

namespace Aoc.Days;

public class Day04 : ICommand
{
    public int Day => 4;
    private const string Xmas = "XMAS";
    private List<(int, int, string)> _solutions = [];
    private const string FilePath = "Data/day04.txt";

    public void Execute()
    {
        var lines = File.ReadAllLines(FilePath);

        var grid = new char[lines.Length, lines[0].Length];
        for (var x = 0; x < lines.Length; x++)
        {
            for (var y = 0; y < lines[x].Length; y++)
            {
                grid[x, y] = lines[x][y];
            }
        }

        PartOne(grid);
        PartTwo(grid);
    }

    private void PartOne(char[,] grid)
    {
        for (var i = 0; i < grid.GetLength(0); i++)
        {
            for (var j = 0; j < grid.GetLength(1); j++)
            {
                SearchWord(grid, i, j);
            }
        }

        Console.WriteLine($"The word '{Xmas}' occurs {_solutions.Count} times in the grid.");
        foreach (var pos in _solutions)
        {
            Console.WriteLine($"Found at: ({pos.Item1}, {pos.Item2}), {pos.Item3}");
        }
    }

    private void SearchWord(char[,] grid, int row, int column)
    {
        int[] rowDirection = [-1, -1, -1, 0, 0, 1, 1, 1];
        int[] colDirection = [-1, 0, 1, -1, 1, -1, 0, 1];
        var directionNames = new List<string>
            { "Top-Left", "Up", "Top-Right", "Left", "Right", "Bottom-Left", "Down", "Bottom-Right" };

        for (var direction = 0; direction < 8; direction++)
        {
            int x = row, y = column;
            int letterIndex;

            for (letterIndex = 0; letterIndex < Xmas.Length; letterIndex++)
            {
                if (x < 0 || x >= grid.GetLength(0) || y < 0 ||
                    y >= grid.GetLength(1))
                    break;

                if (grid[x, y] != Xmas[letterIndex])
                    break;

                x += rowDirection[direction];
                y += colDirection[direction];
            }

            if (letterIndex == Xmas.Length)
            {
                _solutions.Add((row, column, directionNames.ElementAt(direction)));
            }
        }
    }

    private static void PartTwo(char[,] grid)
    {
        var xMasCount = 0;
        var rows = grid.GetLength(0);
        var cols = grid.GetLength(1);

        for (var x = 0; x < rows - 2; x++)
        {
            for (var y = 0; y < cols - 2; y++)
            {
                if (IsXMas(grid, x, y))
                {
                    xMasCount++;
                }
            }
        }

        Console.WriteLine($"The X-MAS pattern appears {xMasCount} times in the grid.");
    }

    private static int FindXMasPatterns(char[,] grid)
    {
        var count = 0;
        var rows = grid.GetLength(0);
        var cols = grid.GetLength(1);

        for (var i = 0; i < rows - 2; i++)
        {
            for (var j = 0; j < cols - 2; j++)
            {
                if (IsXMas(grid, i, j))
                {
                    count++;
                }
            }
        }

        return count;
    }

    private static bool IsXMas(char[,] grid, int x, int y)
    {
        var diagonal1 = (grid[x, y] == 'M' && grid[x + 1, y + 1] == 'A' && grid[x + 2, y + 2] == 'S') ||
                        (grid[x, y] == 'S' && grid[x + 1, y + 1] == 'A' && grid[x + 2, y + 2] == 'M');

        var diagonal2 = (grid[x + 2, y] == 'M' && grid[x + 1, y + 1] == 'A' && grid[x, y + 2] == 'S') ||
                        (grid[x + 2, y] == 'S' && grid[x + 1, y + 1] == 'A' && grid[x, y + 2] == 'M');

        return diagonal1 && diagonal2;
    }
}
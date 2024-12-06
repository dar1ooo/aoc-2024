using Aoc.Common;

namespace Aoc.Days;

public class Day06 : ICommand
{
    public int Day => 6;
    private const string MapFilePath = "Data/day06.txt";


    public void Execute()
    {
        var lines = File.ReadAllLines(MapFilePath);

        var grid = new char[lines.Length, lines[0].Length];
        for (var x = 0; x < lines.Length; x++)
        {
            for (var y = 0; y < lines[x].Length; y++)
            {
                grid[x, y] = lines[x][y];
            }
        }

        var directions = new (int dx, int dy)[] { (-1, 0), (0, 1), (1, 0), (0, -1) };
        var currentDirection = 0;

        int playerX = 0, playerY = 0;
        var found = false;

        for (var x = 0; x < grid.GetLength(0); x++)
        {
            for (var y = 0; y < grid.GetLength(1); y++)
            {
                if (grid[x, y] != '^') continue;

                playerX = x;
                playerY = y;
                found = true;
                break;
            }

            if (found) break;
        }

        grid[playerX, playerY] = 'X';
        var uniqueVisited = 1;

        while (true)
        {
            int nextX = playerX + directions[currentDirection].dx;
            int nextY = playerY + directions[currentDirection].dy;

            if (nextX < 0 || nextX >= grid.GetLength(0) || nextY < 0 || nextY >= grid.GetLength(1))
            {
                break;
            }

            char nextCell = grid[nextX, nextY];

            if (nextCell is '.' or 'X')
            {
                playerX = nextX;
                playerY = nextY;

                if (nextCell != '.') continue;

                grid[playerX, playerY] = 'X';
                uniqueVisited++;
            }
            else if (nextCell == '#')
                currentDirection = (currentDirection + 1) % 4;
            else
                break;
        }

        PrintGrid(grid);
        Console.Write($"The guard has visited ${uniqueVisited} locations");
    }

    private static void PrintGrid(char[,] grid)
    {
        for (var x = 0; x < grid.GetLength(0); x++)
        {
            for (var y = 0; y < grid.GetLength(1); y++)
            {
                Console.Write(grid[x, y]);
            }

            Console.WriteLine();
        }
    }
}
using Spectre.Console;

namespace Aoc.Commands;

public class ExitCommand : ICommand
{
    public int? Day => null;

    public void Execute()
    {
        AnsiConsole.MarkupLine("[green]Goodbye![/] Press any key to exit.");
        Console.ReadKey();
    }
}
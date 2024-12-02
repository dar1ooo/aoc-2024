using Aoc.Commands;
using Aoc.Common;
using Spectre.Console;

var choice = AnsiConsole.Prompt(
    new SelectionPrompt<Choice>()
        .Title("What do you want to do?")
        .PageSize(10)
        .AddChoices(Choice.DayOne, Choice.DayTwo, Choice.Exit));

switch (choice)
{
    case Choice.DayOne:
        DayOne.Run();
        break;

    case Choice.DayTwo:
        DayTwo.Run();
        break;

    case Choice.Exit:
        AnsiConsole.MarkupLine("[green]Goodbye![/] Press any key to exit.");
        AnsiConsole.MarkupLine("[grey93]Press any key to exit.[/]");
        Console.ReadKey();
        break;
}
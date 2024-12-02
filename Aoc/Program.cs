using Aoc.Commands;
using Spectre.Console;
using System.Reflection;

var commands = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t => typeof(ICommand).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
    .Select(Activator.CreateInstance)
    .Cast<ICommand>()
    .OrderByDescending(x => x.Day)
    .ToList();

var running = true;
while (running)
{
    var choice = AnsiConsole.Prompt(
        new SelectionPrompt<ICommand>()
            .Title("What do you want to do?")
            .PageSize(10)
            .UseConverter(command => command.Day.HasValue ? $"Day {command.Day}" : "Exit")
            .AddChoices(commands));

    choice.Execute();

    if (choice is ExitCommand)
    {
        running = false;
    }


    else
    {
        AnsiConsole.MarkupLine("[grey93]Press any key to return to the menu...[/]");
        Console.ReadKey();
        Console.Clear();
    }
}
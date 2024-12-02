using Spectre.Console;
using System.Reflection;
using Aoc.Common;

var commands = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t => typeof(ICommand).IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false })
    .Select(Activator.CreateInstance)
    .OfType<ICommand>()
    .OrderByDescending(x => x.Day)
    .ToList();

var running = true;
while (running)
{
    var choice = AnsiConsole.Prompt(
        new SelectionPrompt<ICommand>()
            .Title("What do you want to do?")
            .PageSize(10)
            .UseConverter(command => $"Day {command.Day}")
            .AddChoices(commands));

    choice.Execute();

    AnsiConsole.MarkupLine("[grey93]Press any key to return to the menu...[/]");
    Console.ReadKey();
    Console.Clear();
}
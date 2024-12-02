namespace Aoc.Commands;

public interface ICommand
{
    int? Day { get; }
    void Execute();
}
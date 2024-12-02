namespace Aoc.Common;

public interface ICommand
{
    int Day { get; }
    void Execute();
}
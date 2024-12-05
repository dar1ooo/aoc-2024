using Aoc.Common;

namespace Aoc.Days;

public class Day05 : ICommand
{
    public int Day => 5;
    private int _correctMiddlePages;
    private int _incorrectMiddlePages;
    private const string RulesFilePath = "Data/day05_rules.txt";
    private const string SequencesFilePath = "Data/day05_updates.txt";

    public void Execute()
    {
        var rules = ParseRules(File.ReadAllLines(RulesFilePath));
        var printSequences = ParseUpdates(File.ReadAllLines(SequencesFilePath));

        foreach (var sequence in printSequences)
        {
            var isValid = CheckUpdateOrder(rules, sequence);

            if (isValid)
                _correctMiddlePages += sequence[sequence.Count / 2];
            else
            {
                var sortedSequence = TopologicalSort(sequence, rules);
                if (sequence.SequenceEqual(sortedSequence)) continue;

                _incorrectMiddlePages += sortedSequence[sequence.Count / 2];
            }
        }

        Console.WriteLine($"Sum of correct pages: {_correctMiddlePages}");
        Console.WriteLine($"Sum of sorted incorrect pages: {_incorrectMiddlePages}");
    }

    private static List<(int, int)> ParseRules(string[] ruleLines)
    {
        var rules = new List<(int, int)>();

        foreach (var ruleLine in ruleLines)
        {
            var ruleNumbers = ruleLine.Split('|');
            if (ruleNumbers.Length == 2 && int.TryParse(ruleNumbers[0], out var before) &&
                int.TryParse(ruleNumbers[1], out var after))
            {
                rules.Add((before, after));
            }
        }

        return rules;
    }

    private static List<List<int>> ParseUpdates(string[] updates)
    {
        var parsedUpdates = new List<List<int>>();

        foreach (var update in updates)
        {
            var sequence = update.Split(',')
                .Select(x => int.TryParse(x, out var pageNumber) ? pageNumber : (int?)null)
                .Where(x => x.HasValue)
                .Select(x => x.Value)
                .ToList();

            parsedUpdates.Add(sequence);
        }

        return parsedUpdates;
    }

    private static bool CheckUpdateOrder(List<(int, int)> rules, List<int> update)
    {
        var pageIndex = update.Select((page, index) => (page, index)).ToDictionary(p => p.page, p => p.index);

        foreach (var (before, after) in rules)
        {
            if (!pageIndex.TryGetValue(before, out var beforeValue) ||
                !pageIndex.TryGetValue(after, out var afterValue) ||
                beforeValue <= afterValue) continue;

            return false;
        }

        return true;
    }

    private static List<int> TopologicalSort(List<int> update, List<(int, int)> rules)
    {
        var graph = update.ToDictionary(page => page, _ => new List<int>());
        var indegree = update.ToDictionary(page => page, _ => 0);

        foreach (var (before, after) in rules.Where(rule =>
                     graph.ContainsKey(rule.Item1) && graph.ContainsKey(rule.Item2)))
        {
            graph[before].Add(after);
            indegree[after]++;
        }

        var queue = new Queue<int>(update.Where(page => indegree[page] == 0));
        var sortedSequence = new List<int>();

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            sortedSequence.Add(current);

            foreach (var neighbor in graph[current].Where(neighbor => --indegree[neighbor] == 0))
            {
                queue.Enqueue(neighbor);
            }
        }

        return sortedSequence;
    }
}
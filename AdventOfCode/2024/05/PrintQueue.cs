namespace AdventOfCode._2024._05;

public class PrintQueue
{
    // ﻿Input was removed due to the copyright.
    // Create input.txt file and fill it with data from: https://adventofcode.com/2024/day/5/input
    private const string InputFilePath = @"2024\05\input.txt";

    #region Part One
    
    public static int RunPartOne()
    {
        return SumValidMiddleElements(InputFilePath);
    }

    public static int SumValidMiddleElements(string inputFilePath)
    {
        ParseInput(inputFilePath, out var pageOrderingRules, out var updates);

        var sum = 0;
        foreach (var update in updates)
        {
            if (CheckOrder(update, pageOrderingRules))
            {
                sum += update[update.Count / 2];
            }
        }

        return sum;
    }

    private static bool CheckOrder(List<int> update, List<Tuple<int, int>> pageOrderingRules)
    {
        for (var i = 1; i < update.Count; i++)
        {
            var pageNumber = update[i];
            var matchesLeft = pageOrderingRules.Where(p => p.Item1 == pageNumber).Select(p => p.Item2).ToArray();

            for (var j = 0; j < i; j++)
            {
                if (matchesLeft.Any(m => m == update[j])) return false;
            }
        }

        return true;
    }

    #endregion

    #region Part Two

    public static int RunPartTwo()
    {
        return SumInvalidMiddleElements(InputFilePath);
    }

    public static int SumInvalidMiddleElements(string inputFilePath)
    {
        ParseInput(inputFilePath, out var pageOrderingRules, out var updates);

        var sum = 0;
        foreach (var update in updates)
        {
            if (CheckOrder(update, pageOrderingRules)) continue;

            var orderedUpdate = OrderUpdate(update, pageOrderingRules);
            sum += orderedUpdate[update.Count / 2];
        }

        return sum;
    }

    private static List<int> OrderUpdate(List<int> update, List<Tuple<int, int>> pageOrderingRules)
    {
        while (MovePage(update, pageOrderingRules)) {}
        
        return update;
    }

    private static bool MovePage(List<int> update, List<Tuple<int, int>> pageOrderingRules)
    {
        for (var i = 1; i < update.Count; i++)
        {
            var pageNumber = update[i];
            var matchesLeft = pageOrderingRules.Where(p => p.Item1 == pageNumber).ToArray();

            for (var j = 0; j < i; j++)
            {
                var matches = matchesLeft.Where(m => m.Item2 == update[j]);

                foreach (var match in matches)
                {
                    update.RemoveAt(i);
                    update.Insert(j, match.Item1);
                    return true;
                }
            }
        }

        return false;
    }

    #endregion

    private static void ParseInput(string inputFilePath, out List<Tuple<int, int>> pageOrderingRules, out List<List<int>> updates)
    {
        var lines = File.ReadAllLines(inputFilePath);

        pageOrderingRules = [];
        updates = [];
        foreach (var line in lines)
        {
            if (line.Contains('|'))
            {
                var pair = line.Split('|');
                pageOrderingRules.Add(new Tuple<int, int>(int.Parse(pair[0]), int.Parse(pair[1])));
            }

            if (line.Contains(','))
            {
                updates.Add(line.Split(',').Select(int.Parse).ToList());
            }
        }
    }
}

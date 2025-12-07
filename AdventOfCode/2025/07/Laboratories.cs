namespace AdventOfCode._2025._07;

public class Laboratories
{
    // ﻿Input was removed due to the copyright.
    // Create input.txt file and fill it with data from: https://adventofcode.com/2025/day/7
    private const string InputFilePath = @"2025\07\input.txt";

    public static int RunPartOne()
    {
        return CountSplits(InputFilePath);
    }

    public static ulong RunPartTwo()
    {
        return FindAllTimelinesFinal(InputFilePath);
    }

    public static ulong FindAllTimelinesFinal(string inputFilePath)
    {
        var diagram = ParseInputFile(inputFilePath);

        var timelines = new List<ulong[]>();
        for (int i = 0; i < diagram.Count; i++)
            timelines.Add(new ulong[diagram[0].Count]);

        for (int i = 0; i < diagram[0].Count; i++)
            timelines[^1][i] = (ulong)(diagram[^1][i] ? 2 : 1);

        for (var i = diagram.Count - 2; i >= 0; i--)
        {
            for (int j = 0; j < diagram[0].Count; j++)
            {
                if (diagram[i][j])
                    timelines[i][j] = timelines[i + 1][j - 1] + timelines[i + 1][j + 1];
                else
                    timelines[i][j] = timelines[i + 1][j];
            }
        }

        return timelines[0].Max();
    }

    public static int CountSplits(string inputFilePath)
    {
        var diagram = File.ReadAllLines(inputFilePath);

        var splitsCount = 0;

        var beamIndexes = new List<int>() { diagram[0].IndexOf('S') };
        foreach (var line in diagram[1..])
        {
            var splitterIndexes = FindAllSplitters(line);
            foreach (var splitterIndex in splitterIndexes)
            {
                if (beamIndexes.Contains(splitterIndex))
                {
                    splitsCount++;
                    // Remove beam as it enxountered the splitter
                    beamIndexes.Remove(splitterIndex);

                    // Add 2 new indexes representing split beam
                    if (!beamIndexes.Contains(splitterIndex - 1))
                        beamIndexes.Add(splitterIndex - 1);
                    if (!beamIndexes.Contains(splitterIndex + 1))
                        beamIndexes.Add(splitterIndex + 1);
                }
            }
        }

        return splitsCount;
    }

    private static List<int> FindAllSplitters(string line)
    {
        var splitterIndexes = new List<int>();
        for (var i = line.IndexOf('^'); i > -1; i = line.IndexOf('^', i + 1))
        {
            splitterIndexes.Add(i);
        }
        return splitterIndexes;
    }

    private static List<List<bool>> ParseInputFile(string inputFilePath)
    {
        var lines = File.ReadAllLines(inputFilePath).ToList();

        var diagram = new List<List<bool>>();
        for (var i = lines.Count - 1; i >= 0; i--)
        {
            if (i % 2 == 1)
                lines.RemoveAt(i);
            else
                diagram.Add(lines[i].Select(v => v == '^' || v == 'S').ToList());
        }

        diagram.Reverse();
        return diagram;
    }
}

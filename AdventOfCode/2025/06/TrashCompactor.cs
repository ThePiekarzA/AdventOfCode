namespace AdventOfCode._2025._06;

public class TrashCompactor
{
    public class Problem
    {
        public char Operator {  get; set; }
        public List<int> Values { get; set; } = new List<int>();
    }

    // ﻿Input was removed due to the copyright.
    // Create input.txt file and fill it with data from: https://adventofcode.com/2025/day/6
    private const string InputFilePath = @"2025\06\input.txt";

    public static ulong RunPartOne()
    {
        return SumProblemsAnswersForPartOne(InputFilePath);
    }

    public static ulong RunPartTwo()
    {
        return SumProblemsAnswersForPartTwo(InputFilePath);
    }

    public static ulong SumProblemsAnswersForPartTwo(string inputFilePath)
    {
        var problems = ParseInputFile(inputFilePath, false);
        return SolveProblems(problems);
    }

    public static ulong SumProblemsAnswersForPartOne(string inputFilePath)
    {
        var problems = ParseInputFile(inputFilePath, true);
        return SolveProblems(problems);
    }

    private static ulong SolveProblems(List<Problem> problems)
    {
        ulong answersSum = 0;
        foreach (var problem in problems)
        {
            if (problem.Operator == '+')
                answersSum += (ulong)problem.Values.Sum();
            else
            {
                ulong result = (ulong)problem.Values[0];
                for (int i = 1; i < problem.Values.Count; i++)
                {
                    result *= (ulong)problem.Values[i];
                }

                answersSum += result;
            }
        }

        return answersSum;
    }

    private static List<Problem> ParseInputFile(string inputFilePath, bool ordinaryMethod)
    {
        var lines = File.ReadAllLines(inputFilePath);

        var problems = new List<Problem>();
        var previousColumn = -1;
        for (int i = 0; i < lines[0].Length; i++)
        {
            if (CheckIfColumnSeparator(lines, i))
            {
                problems.Add(ParseProblem(lines, previousColumn, i, ordinaryMethod));
                previousColumn = i;
            }
        }

        problems.Add(ParseProblem(lines, previousColumn, lines[0].Length, ordinaryMethod));

        return problems;
    }

    private static Problem ParseProblem(string[] lines, int previousColumn, int currentColumn, bool ordinaryMethod)
    {
        var columnRange = new(previousColumn + 1)..currentColumn;

        var problem = new Problem();
        if (ordinaryMethod)
        {
            for (int j = 0; j < lines.Length - 1; j++)
            {
                var value = lines[j][columnRange];
                problem.Values.Add(int.Parse(value.Trim()));
            }
        }
        else
        {
            for (var columnIndex = columnRange.End.Value - 1; columnIndex >= columnRange.Start.Value; columnIndex--)
            {
                var value = new char[lines.Length - 1];
                for (var j = 0; j < lines.Length - 1; j++)
                {
                    value[j] = lines[j][columnIndex];
                }
                problem.Values.Add(int.Parse(new string(value)));
            }
        }

        problem.Operator = lines[^1][columnRange].Trim()[0];
        return problem;
    }

    private static bool CheckIfColumnSeparator(string[] lines, int i)
    {
        for (int j = 0; j < lines.Length; j++)
        {
            if (lines[j][i] != ' ')
                return false;
        }

        return true;
    }
}

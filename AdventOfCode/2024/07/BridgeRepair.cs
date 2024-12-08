using AdventOfCode.Common;

namespace AdventOfCode._2024._07;

public enum Operator
{
    Multiply,
    Sum,
    Concatenate
};

public class BridgeRepair
{
    // ﻿Input was removed due to the copyright.
    // Create input.txt file and fill it with data from: https://adventofcode.com/2024/day/7/input
    private const string InputFilePath = @"2024\07\input.txt";

    public static ulong RunPartOne()
    {
        return SumValidEquations(InputFilePath, 2);
    }

    public static ulong RunPartTwo()
    {
        return SumValidEquations(InputFilePath, 3);
    }

    public static ulong SumValidEquations(string inputFilePath, int operatorsCount)
    {
        var equations = ParseInput(inputFilePath);

        ulong validEquationsSum = 0;
        foreach (var equation in equations)
        {
            if (AnalyzeEquation(equation, operatorsCount))
            {
                validEquationsSum += equation.TestValue;
            }
        }
        return validEquationsSum;
    }

    private static bool AnalyzeEquation(Equation equation, int operatorsCount)
    {
        var numbers = equation.Numbers;
        foreach (var combination in Combinations.PermutationsWithRepetitions(numbers.Count -1, operatorsCount))
        {
            var i = 0;
            var result = numbers[i];
            foreach (Operator @operator in combination)
            {
                i++;
                switch (@operator)
                {
                    case Operator.Multiply:
                        result *= numbers[i];
                        break;
                    case Operator.Sum:
                        result += numbers[i];
                        break;
                    case Operator.Concatenate:
                        result = ulong.Parse($"{result}{numbers[i]}");
                        break;
                }
            }

            if (result == equation.TestValue)
            {
                return true;
            }
        }

        return false;
    }

    private static List<Equation> ParseInput(string inputFilePath)
    {
        var lines = File.ReadAllLines(inputFilePath);
        var equations = new List<Equation>();

        foreach (var line in lines)
        {
            var equationParts = line.Split(": ");
            var equation = new Equation()
            {
                TestValue = ulong.Parse(equationParts[0]),
                Numbers = equationParts[1].Split(' ').Select(ulong.Parse).ToList()
            };

            equations.Add(equation);
        }

        return equations;
    }
}

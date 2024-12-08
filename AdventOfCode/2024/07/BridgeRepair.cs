using System.Collections;

namespace AdventOfCode._2024._07;

public enum Operator
{
    Multiply,
    Sum
};

public class BridgeRepair
{
    // ﻿Input was removed due to the copyright.
    // Create input.txt file and fill it with data from: https://adventofcode.com/2024/day/7/input
    private const string InputFilePath = @"2024\07\input.txt";

    public static ulong RunPartOne()
    {
        return SumValidEquations(InputFilePath);
    }

    public static ulong SumValidEquations(string inputFilePath)
    {
        var equations = ParseInput(inputFilePath);

        ulong validEquationsSum = 0;
        foreach (var equation in equations)
        {
            if (AnalyzeEquation(equation))
            {
                validEquationsSum += equation.TestValue;
            }
        }
        return validEquationsSum;
    }

    private static bool AnalyzeEquation(Equation equation)
    {
        var numbers = equation.Numbers;
        foreach (var combination in GetAllCombinations(numbers.Count -1))
        {
            var i = 0;
            var result = numbers[i];
            foreach (var @operator in combination)
            {
                i++;
                if (@operator == Operator.Multiply)
                    result *= numbers[i];
                else
                    result += numbers[i];
            }

            if (result == equation.TestValue)
            {
                return true;
            }
        }

        return false;
    }

    private static IEnumerable<Operator[]> GetAllCombinations(int places)
    {
        var combinationsCount = Math.Pow(2, places);
        var combination = new Operator[places];

        for (int i = 0; i < combinationsCount; i++)
        {
            //var bitRepresentation = Convert.ToString((long)i, 2);
            var bitArray = new BitArray([i]);
            //bitArray.CopyTo(boolArray, 0);
            for (var j = 0; j < places; j++)
            {
                combination[j] = bitArray[j] == false ? Operator.Multiply : Operator.Sum;
            }

            yield return combination;
        }
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

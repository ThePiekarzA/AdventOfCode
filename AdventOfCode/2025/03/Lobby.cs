namespace AdventOfCode._2025._03;

public class Lobby
{
    // ﻿Input was removed due to the copyright.
    // Create input.txt file and fill it with data from: https://adventofcode.com/2025/day/3
    private const string InputFilePath = @"2025\03\input.txt";

    public static ulong RunPartOne()
    {
        return FindMaxJoltage(InputFilePath, 2);
    }

    public static ulong RunPartTwo()
    {
        return FindMaxJoltage(InputFilePath, 12);
    }

    public static ulong FindMaxJoltage(string inputFilePath, int joltageLength)
    {
        var banks = ParseInputFile(inputFilePath);

        ulong joltage = 0;
        foreach (var bank in banks)
        {
            joltage += FindMaxJoltageForBankRecursively(bank, joltageLength);
        }

        return joltage;
    }

    private static ulong FindMaxJoltageForBankRecursively(List<int> bank, int joltageLength, int startIndex = 0)
    {
        var bankLength = bank.Count;

        var max = bank[startIndex..^(joltageLength - 1)].Max();
        var maxIndex = bank.IndexOf(max, startIndex);

        var currentJoltage = (ulong)max * (ulong)Math.Pow(10, joltageLength - 1);
        if (joltageLength == 1)
            return currentJoltage;

        return currentJoltage + FindMaxJoltageForBankRecursively(bank, joltageLength - 1, maxIndex + 1);
    }

    private static int FindMaxJoltageForBank(List<int> bank)
    {
        var bankLength = bank.Count;

        var max = bank.Max();
        var maxIndex = bank.IndexOf(max);

        int first, second;
        if (maxIndex == 0)
        {
            first = max;
            second = bank[1..].Max();
        }
        else if (maxIndex == bank.Count - 1)
        {
            first = bank[..^1].Max();
            second = max;
        }
        else
        {
            var leftMax = bank[..maxIndex].Max();
            var rightMax = bank[(maxIndex + 1)..].Max();

            if (leftMax == max)
            {
                first = max;
                second = max;
            }
            else
            {
                first = max;
                second = rightMax;
            }
        }

        var joltage = first * 10 + second;
        return joltage;
    }

    private static List<List<int>> ParseInputFile(string inputFilePath)
    {
        var lines = File.ReadAllLines(inputFilePath);

        var banks = new List<List<int>>();
        foreach (var line in lines)
        {
            banks.Add(line.Select(b => int.Parse(b.ToString())).ToList());
        }

        return banks;
    }
}

using System.Text.RegularExpressions;

namespace AdventOfCode._2024._03;

public class MullItOver
{
    // ﻿Input was removed due to the copyright.
    // Create input.txt file and fill it with data from: https://adventofcode.com/2024/day/3/input
    private const string InputFilePath = @"2024\03\input.txt";
    
    private const string MulRegex = @"(mul\()(?<multiplier>\d{1,3})(,)(?<multiplicand>\d{1,3})(\))";
    private const string DoMulRegex = @"(mul\()(?<multiplier>\d{1,3})(,)(?<multiplicand>\d{1,3})(\))|(?<do>do\(\))|(?<dont>don't\(\))";

    #region Part One

    public static int RunPartOne()
    {
        return ScanAndRunInstructionsBasedOnFile(InputFilePath);
    }

    public static int ScanAndRunInstructionsBasedOnFile(string inputFilePath)
    {
        var instructions = File.ReadAllText(inputFilePath);
        return ScanAndRunInstructions(instructions);
    }

    public static int ScanAndRunInstructions(string instructions)
    {
        var regex = new Regex(MulRegex);
        var matches = regex.Matches(instructions);
        
        var result = 0;
        foreach (Match match in matches)
        {
            result += int.Parse(match.Groups["multiplier"].Value) * int.Parse(match.Groups["multiplicand"].Value);
        }

        return result;
    }

    #endregion

    #region Part One

    public static int RunPartTwo()
    {
        return ScanAndRunInstructionsWithSuspendBasedOnFile(InputFilePath);
    }

    public static int ScanAndRunInstructionsWithSuspendBasedOnFile(string inputFilePath)
    {
        var instructions = File.ReadAllText(inputFilePath);
        return ScanAndRunInstructionsWithSuspend(instructions);
    }

    public static int ScanAndRunInstructionsWithSuspend(string instructions)
    {
        var regex = new Regex(DoMulRegex);
        var matches = regex.Matches(instructions);

        var result = 0;
        var suspend = false;
        foreach (Match match in matches)
        {
            if (match.Groups["do"].Success)
                suspend = false;
            if (match.Groups["dont"].Success)
                suspend = true;

            if (!suspend && 
                match.Groups["multiplier"].Success && 
                match.Groups["multiplicand"].Success)
            {
                result += int.Parse(match.Groups["multiplier"].Value) * int.Parse(match.Groups["multiplicand"].Value);
            }
        }

        return result;
    }

    #endregion
}

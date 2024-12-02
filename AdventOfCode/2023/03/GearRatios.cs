using System.Text.RegularExpressions;

namespace AdventOfCode._2023._03;
public static class GearRatios
{
    private static readonly Regex PartNumberRegex = new(@"\d+|[^.]");
    private const char Gear = '*';
    
    // ﻿Input was removed due to the copyright.
    // Create input.txt file and fill it with data from: https://adventofcode.com/2023/day/3/input
    private const string InputFilePath = @"2023\03\input.txt";

    public static int RunPartOne()
    {
        return SumPartNumbersBaseOnSchematic(InputFilePath);
    }

    public static int RunPartTwo()
    {
        return SumGearRatiosBaseOnSchematic(InputFilePath);
    }

    #region PartOne

    public static int SumPartNumbersBaseOnSchematic(string schematicFilePath)
    {
        var schematic = File.ReadAllLines(schematicFilePath);

        ParseSchematic(schematic, out var parts, out var partNumbers, out int rowCount);
        var sum = 0;
        foreach (var part in parts)
        {
            if (part.Row > 0)
            {
                var previousRowCandidates = partNumbers.Where(pn => pn.Row == part.Row - 1);
                sum += previousRowCandidates.Where(pn => pn.End >= part.Position - 1 && pn.Start <= part.Position + 1)
                    .Sum(n => n.Value);
            }

            var currentRowCandidates = partNumbers.Where(pn => pn.Row == part.Row);
            sum += currentRowCandidates.Where(pn => pn.End == part.Position - 1 || pn.Start == part.Position + 1)
                    .Sum(an => an.Value);

            if (part.Row < rowCount)
            {
                var nextRowCandidates = partNumbers.Where(pn => pn.Row == part.Row + 1);
                sum += nextRowCandidates.Where(pn => pn.End >= part.Position - 1 && pn.Start <= part.Position + 1)
                    .Sum(n => n.Value);
            }
        }

        return sum;
    }

    #endregion

    #region PartTwo

    public static int SumGearRatiosBaseOnSchematic(string schematicFilePath)
    {
        var schematic = File.ReadAllLines(schematicFilePath);

        ParseSchematic(schematic, out var parts, out var partNumbers, out int rowCount);

        var gearCandidates = parts.Where(p => p.Symbol == Gear);
        var sum = 0;
        foreach (var part in gearCandidates)
        {
            var adjacentPartNumbers = new List<PartNumber>();

            if (part.Row > 0)
            {
                var previousRowCandidates = partNumbers.Where(pn => pn.Row == part.Row - 1);
                adjacentPartNumbers.AddRange(previousRowCandidates.Where(pn =>
                    pn.End >= part.Position - 1 && pn.Start <= part.Position + 1));
            }

            var currentRowCandidates = partNumbers.Where(pn => pn.Row == part.Row);
            adjacentPartNumbers.AddRange(currentRowCandidates.Where(pn => 
                pn.End == part.Position - 1 || pn.Start == part.Position + 1));

            if (part.Row < rowCount)
            {
                var nextRowCandidates = partNumbers.Where(pn => pn.Row == part.Row + 1);
                adjacentPartNumbers.AddRange(nextRowCandidates.Where(pn => pn.End >= 
                    part.Position - 1 && pn.Start <= part.Position + 1));
            }

            if (adjacentPartNumbers.Count == 2)
            {
                var gearRatio = adjacentPartNumbers[0].Value * adjacentPartNumbers[1].Value;
                sum += gearRatio;
            }
        }

        return sum;
    }

    #endregion

    private static void ParseSchematic(string[] schematic, out List<Part> parts, out List<PartNumber> partNumbers, out int rowCount)
    {
        parts = new List<Part>();
        partNumbers = new List<PartNumber>();
        var row = 0;
        foreach (var line in schematic)
        {
            var matches = PartNumberRegex.Matches(line);
            foreach (Match match in matches)
            {
                if (int.TryParse(match.Value, out int number))
                {
                    var partNumber = new PartNumber()
                    {
                        Value = number,
                        Row = row,
                        Start = match.Index,
                        Length = match.Length
                    };
                    partNumbers.Add(partNumber);
                }
                else
                {
                    var part = new Part()
                    {
                        Symbol = match.Value.FirstOrDefault(),
                        Row = row,
                        Position = match.Index
                    };
                    parts.Add(part);
                }
            }

            row++;
        }

        rowCount = row;
    }
}

public class Part
{
    public char Symbol { get; set; }
    public int Row { get; set; }
    public int Position { get; set; }
}

public class PartNumber
{
    public int Value { get; set; }
    public int Row { get; set; }
    public int Start { get; set; }
    public int Length { get; set; }
    public int End => Start + Length - 1;
}

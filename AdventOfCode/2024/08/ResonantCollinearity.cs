using System.Drawing;
using AdventOfCode.Common;

namespace AdventOfCode._2024._08;

public class ResonantCollinearity
{
    // ﻿Input was removed due to the copyright.
    // Create input.txt file and fill it with data from: https://adventofcode.com/2024/day/8/input
    private const string InputFilePath = @"2024\08\input.txt";

    #region Part One

    public static int RunPartOne()
    {
        return CountAntinodes(InputFilePath);
    }

    public static int CountAntinodes(string inputFilePath)
    {
        var antennas = ParseInput(inputFilePath, out var width, out var height);

        var validAntinodes = new List<Point>();

        var frequencies = antennas.Select(a => a.Frequency).Distinct();
        foreach (var frequency in frequencies)
        {
            var sameFrequencyAntennas = antennas.Where(a => a.Frequency == frequency).ToArray();
            foreach (var antennaPair in GetAllCombinations(sameFrequencyAntennas))
            {
                var positions = antennaPair.Select(a => a.Location).ToArray();

                if (positions[0].Y < positions[1].Y)
                {
                    CalculateAntinodes(positions, 0, width, height, validAntinodes);
                }
                else
                {
                    CalculateAntinodes(positions, 1, width, height, validAntinodes);
                }
            }
        }

        return validAntinodes.Distinct().Count();
    }

    private static void CalculateAntinodes(Point[] positions, int upperIndex, int width, int height, List<Point> validAntinodes)
    {
        var xDiff = Math.Abs(positions[0].X - positions[1].X);
        var yDiff = Math.Abs(positions[0].Y - positions[1].Y);

        Point firstAntinode, secondAntinode;
        if (positions[upperIndex].X < positions[(upperIndex + 1) % 2].X) // upper-left
        {
            firstAntinode = new()
            {
                X = positions[upperIndex].X - xDiff,
                Y = positions[upperIndex].Y - yDiff
            };
            secondAntinode = new()
            {
                X = positions[upperIndex].X + 2 * xDiff,
                Y = positions[upperIndex].Y + 2 * yDiff,
            };
        }
        else // upper-right
        {
            firstAntinode = new()
            {
                X = positions[upperIndex].X + xDiff,
                Y = positions[upperIndex].Y - yDiff
            };
            secondAntinode = new()
            {
                X = positions[upperIndex].X - 2 * xDiff,
                Y = positions[upperIndex].Y + 2 * yDiff,
            };
        }

        ValidateAntinode(width, height, validAntinodes, firstAntinode);
        ValidateAntinode(width, height, validAntinodes, secondAntinode);
    }

    #endregion

    #region Part Two

    public static int RunPartTwo()
    {
        return CountAntinodesWithRepetitions(InputFilePath);
    }

    public static int CountAntinodesWithRepetitions(string inputFilePath)
    {
        var antennas = ParseInput(inputFilePath, out var width, out var height);

        var validAntinodes = new List<Point>();

        var frequencies = antennas.Select(a => a.Frequency).Distinct();
        foreach (var frequency in frequencies)
        {
            var sameFrequencyAntennas = antennas.Where(a => a.Frequency == frequency).ToArray();
            if (sameFrequencyAntennas.Length < 2)
                continue;

            foreach (var antennaPair in GetAllCombinations(sameFrequencyAntennas))
            {
                var positions = antennaPair.Select(a => a.Location).ToArray();

                validAntinodes.Add(new Point(positions[0].X, positions[0].Y));
                validAntinodes.Add(new Point(positions[1].X, positions[1].Y));

                if (positions[0].Y < positions[1].Y)
                {
                    CalculateAntinodesWithRepetitions(positions, 0, width, height, validAntinodes);
                }
                else
                {
                    CalculateAntinodesWithRepetitions(positions, 1, width, height, validAntinodes);
                }
            }
        }

        return validAntinodes.Distinct().Count();
    }

    private static void CalculateAntinodesWithRepetitions(Point[] positions, int upperIndex, int width, int height, List<Point> validAntinodes)
    {
        var xDiff = Math.Abs(positions[0].X - positions[1].X);
        var yDiff = Math.Abs(positions[0].Y - positions[1].Y);

        Point firstAntinode, secondAntinode;
        int i = 0;
        if (positions[upperIndex].X < positions[(upperIndex + 1) % 2].X) // upper-left
        {
            i = 0;
            do
            {
                i++;
                firstAntinode = new()
                {
                    X = positions[upperIndex].X - i * xDiff,
                    Y = positions[upperIndex].Y - i * yDiff
                };
            } while (ValidateAntinode(width, height, validAntinodes, firstAntinode));


            i = 0;
            do
            {
                i++;
                secondAntinode = new()
                {
                    X = positions[upperIndex].X + (i + 1) * xDiff,
                    Y = positions[upperIndex].Y + (i + 1) * yDiff,
                };
            } while (ValidateAntinode(width, height, validAntinodes, secondAntinode));            
        }
        else // upper-right
        {
            i = 0;
            do
            {
                i++;
                firstAntinode = new()
                {
                    X = positions[upperIndex].X + i * xDiff,
                    Y = positions[upperIndex].Y - i * yDiff
                };
            } while (ValidateAntinode(width, height, validAntinodes, firstAntinode));


            i = 0;
            do
            {
                i++;
                secondAntinode = new()
                {
                    X = positions[upperIndex].X - (i + 1) * xDiff,
                    Y = positions[upperIndex].Y + (i + 1) * yDiff,
                };
            } while (ValidateAntinode(width, height, validAntinodes, secondAntinode));
        }
    }

    #endregion 

    private static bool ValidateAntinode(int width, int height, List<Point> validAntinodes, Point antinode)
    {
        if (antinode.X >= 0 && antinode.Y >= 0 &&
                            antinode.X < width && antinode.Y < height)
        {
            validAntinodes.Add(antinode);
            return true;
        }

        return false;
    }

    private static IEnumerable<Antenna[]> GetAllCombinations(Antenna[] sameFrequencyAntennas)
    {
        foreach (var combination in Combinations.GetCombinations(sameFrequencyAntennas.Length))
        {
            yield return combination.Select(e => sameFrequencyAntennas[e]).ToArray();
        }
    }

    private static List<Antenna> ParseInput(string inputFilePath, out int width, out int height)
    {
        var lines = File.ReadAllLines(inputFilePath);
        width = lines[0].Length;
        height = lines.Length;

        var antennas = new List<Antenna>();
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (lines[y][x] == '.')
                    continue;

                var antenna = new Antenna()
                {
                    Frequency = lines[y][x],
                    Location = new Point()
                    {
                        X = x,
                        Y = y
                    }
                };
                antennas.Add(antenna);
            }
        }
        return antennas;
    }
}

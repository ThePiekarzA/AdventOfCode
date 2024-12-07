using System.Formats.Tar;
using System.Net.Http.Headers;
using File = System.IO.File;

namespace AdventOfCode._2024._06;

public enum Direction
{
    Up,
    Right,
    Down,
    Left
}

public class GuardGallivant
{
    // ﻿Input was removed due to the copyright.
    // Create input.txt file and fill it with data from: https://adventofcode.com/2024/day/6/input
    private const string InputFilePath = @"2024\06\input.txt";

    

    #region Part One
    
    public static int RunPartOne()
    {
        return CalculateRoute(InputFilePath);
    }

    public static int CalculateRoute(string inputFilePath)
    {
        var rawFile = File.ReadAllLines(inputFilePath);
        var map = rawFile.Select(line => line.ToList()).ToList();

        var dimensions = new Position()
        {
            X = map[0].Count,
            Y = map.Count
        };

        var guardPosition = LocateGuard(dimensions, map);
        var guardDirection = Direction.Up;
        var moves = 0;
        do
        {
            if (map[guardPosition.Y][guardPosition.X] != 'X')
            {
                moves++;
                map[guardPosition.Y][guardPosition.X] = 'X';
            }
            
            RotateGuard(ref guardDirection, guardPosition, map);
            MoveGuard(guardDirection, guardPosition);

        } while (CheckBoundariesWithDirection(guardPosition, guardDirection, dimensions));

        return moves + 1;
    }

    #endregion

    #region Part Two

    public static int RunPartTwo()
    {
        return CountPossibleLoops(InputFilePath);
    }

    public static int CountPossibleLoops(string inputFilePath)
    {
        var rawFile = File.ReadAllLines(inputFilePath);

        var possibleBarricades = new List<Position>() { new() { X = -2, Y = -2 } };
        var loopCount = 0;
        bool barricadeLocated;
        do
        {
            barricadeLocated = false;
            if (AnalyzeSingleLoop(rawFile, possibleBarricades, ref barricadeLocated))
                loopCount++;
            if (possibleBarricades.Count % 10 == 0)
            {
                Console.WriteLine($"Analyzed {possibleBarricades.Count} possible barricades, found {loopCount} loops");
            }
        } while (barricadeLocated);

        return loopCount;
    }

    private static bool AnalyzeSingleLoop(string[] rawFile, List<Position> possibleBarricades, ref bool barricadeLocated)
    {
        var map = rawFile.Select(line => line.ToList()).ToList();
        var dimensions = new Position()
        {
            X = map[0].Count,
            Y = map.Count
        };

        var guardPosition = LocateGuard(dimensions, map);
        var guardDirection = Direction.Up;

        var currentBarricade = new Position()
        {
            X = -2,
            Y = -2
        };
        
        do
        {
            if (!barricadeLocated && LocateBarricade2(guardDirection, map, guardPosition, currentBarricade, possibleBarricades))
                barricadeLocated = true;
            
            RotateGuard(ref guardDirection, guardPosition, map);

            if (!barricadeLocated && LocateBarricade2(guardDirection, map, guardPosition, currentBarricade, possibleBarricades))
                barricadeLocated = true;

            MoveGuard(guardDirection, guardPosition);

            if (CheckIfLooped(guardDirection, guardPosition, currentBarricade))
            {
                Console.WriteLine($"{currentBarricade}");
                return true;
            }

        } while (CheckBoundariesWithDirection(guardPosition, guardDirection, dimensions));

        return false;
    }

    private static bool LocateBarricade2(Direction guardDirection, List<List<char>> map, Position guardPosition, Position currentBarricade, List<Position> possibleBarricades)
    {
        switch (guardDirection)
        {
            case Direction.Up:
                if (map[guardPosition.Y].FindIndex(guardPosition.X, o => o == '#') != -1 &&
                    guardPosition.Y > 0 &&
                    map[guardPosition.Y - 1][guardPosition.X] != '#' &&
                    !possibleBarricades.Any(b => b.X == guardPosition.X && b.Y == guardPosition.Y - 1))
                {
                    currentBarricade.X = guardPosition.X;
                    currentBarricade.Y = guardPosition.Y - 1;
                }
                break;
            case Direction.Right:
                if (guardPosition.X >= map[0].Count - 1 ||
                    map[guardPosition.Y][guardPosition.X + 1] == '#' ||
                    possibleBarricades.Any(b => b.X == guardPosition.X + 1 && b.Y == guardPosition.Y))
                    break;
                for (var y = guardPosition.Y + 1; y < map.Count; y++)
                {
                    if (map[y][guardPosition.X] != '#') continue;

                    currentBarricade.X = guardPosition.X + 1;
                    currentBarricade.Y = guardPosition.Y;
                    break;
                }
                break;
            case Direction.Down:
                if (guardPosition.Y >= map.Count - 1 ||
                    map[guardPosition.Y + 1][guardPosition.X] == '#' ||
                    possibleBarricades.Any(b => b.X == guardPosition.X && b.Y == guardPosition.Y + 1))
                    break;
                for (var x = guardPosition.X; x >= 0; x--)
                {
                    if (map[guardPosition.Y][x] != '#') continue;

                    currentBarricade.X = guardPosition.X;
                    currentBarricade.Y = guardPosition.Y + 1;
                    break;
                }
                break;
            case Direction.Left:
                if (guardPosition.X <= 0 ||
                    map[guardPosition.Y][guardPosition.X - 1] == '#' ||
                    possibleBarricades.Any(b => b.X == guardPosition.X - 1 && b.Y == guardPosition.Y))
                    break;
                for (var y = guardPosition.Y; y >= 0; y--)
                {
                    if (map[y][guardPosition.X] != '#') continue;

                    currentBarricade.X = guardPosition.X - 1;
                    currentBarricade.Y = guardPosition.Y;
                    break;
                }
                break;
        }

        if (possibleBarricades.Any(b => b.X == currentBarricade.X && b.Y == currentBarricade.Y)) return false;

        possibleBarricades.Add(currentBarricade);
        map[currentBarricade.Y][currentBarricade.X] = '#';
        return true;
    }

    private static bool LocateBarricade(Direction guardDirection, List<List<char>> map, Position guardPosition, Position currentBarricade, List<Position> possibleBarricades)
    {
        switch (guardDirection)
        {
            case Direction.Up:
                if (map[guardPosition.Y + 1].FindIndex(guardPosition.X, o => o == '#') != -1)
                {
                    currentBarricade.X = guardPosition.X - 1;
                    currentBarricade.Y = guardPosition.Y;
                    break;
                }
                break;
            case Direction.Right:
                for (var y = guardPosition.Y; y < map.Count; y++)
                {
                    if (map[y][guardPosition.X - 1] != '#') continue;

                    currentBarricade.X = guardPosition.X;
                    currentBarricade.Y = guardPosition.Y - 1;
                    break;
                }
                break;
            case Direction.Down:
                for (var x = guardPosition.X; x >= 0; x--)
                {
                    if (map[guardPosition.Y - 1][x] != '#') continue;

                    currentBarricade.X = guardPosition.X + 1;
                    currentBarricade.Y = guardPosition.Y;
                    break;
                }
                break;
            case Direction.Left:
                for (var y = guardPosition.Y; y >= 0; y--)
                {
                    if (map[y][guardPosition.X + 1] != '#') continue;

                    currentBarricade.X = guardPosition.X;
                    currentBarricade.Y = guardPosition.Y + 1;
                    break;
                }
                break;
        }

        if (possibleBarricades.Any(b => b.X == currentBarricade.X && b.Y == currentBarricade.Y)) return false;
        
        possibleBarricades.Add(currentBarricade);
        map[currentBarricade.Y][currentBarricade.X] = '#';
        return true;
    }

    private static bool CheckIfLooped(Direction guardDirection, Position guardPosition, Position currentBarricade)
    {
        switch (guardDirection)
        {
            case Direction.Up:
                return guardPosition.X == currentBarricade.X &&
                       guardPosition.Y - 1 == currentBarricade.Y;
            case Direction.Right:
                return guardPosition.X + 1 == currentBarricade.X &&
                       guardPosition.Y == currentBarricade.Y;
            case Direction.Down:
                return guardPosition.X == currentBarricade.X &&
                       guardPosition.Y + 1 == currentBarricade.Y;
            case Direction.Left:
                return guardPosition.X - 1 == currentBarricade.X &&
                       guardPosition.Y == currentBarricade.Y;
        }

        return false;
    }

    #endregion

    private static bool RotateGuard(ref Direction guardDirection, Position guardPosition, List<List<char>> map)
    {
        switch (guardDirection)
        {
            case Direction.Up:
                if (map[guardPosition.Y - 1][guardPosition.X] == '#')
                {
                    guardDirection = (Direction)(((int)guardDirection + 1) % 4);
                    return true;
                }
                break;
            case Direction.Right:
                if (map[guardPosition.Y][guardPosition.X + 1] == '#')
                {
                    guardDirection = (Direction)(((int)guardDirection + 1) % 4);
                    return true;
                }
                break;
            case Direction.Down:
                if (map[guardPosition.Y + 1][guardPosition.X] == '#')
                {
                    guardDirection = (Direction)(((int)guardDirection + 1) % 4);
                    return true;
                }
                break;
            case Direction.Left:
                if (map[guardPosition.Y][guardPosition.X - 1] == '#')
                {
                    guardDirection = (Direction)(((int)guardDirection + 1) % 4);
                    return true;
                }
                break;
        }

        return false;
    }

    private static void MoveGuard(Direction guardDirection, Position guardPosition)
    {
        switch (guardDirection)
        {
            case Direction.Up:
                guardPosition.Y--;
                break;
            case Direction.Right:
                guardPosition.X++;
                break;
            case Direction.Down:
                guardPosition.Y++;
                break;
            case Direction.Left:
                guardPosition.X--;
                break;
        }
    }

    private static bool CheckBoundariesWithDirection(Position guardPosition, Direction guardDirection, Position dimensions)
    {
        var position = new Position()
        {
            X = guardPosition.X,
            Y = guardPosition.Y
        };
        MoveGuard(guardDirection, position);
        return CheckBoundaries(position, dimensions);
    }

    private static bool CheckBoundaries(Position guardPosition, Position dimensions)
    {
        return guardPosition.X >= 0 && guardPosition.X < dimensions.X &&
               guardPosition.Y >= 0 && guardPosition.Y < dimensions.Y;
    }

    private static Position LocateGuard(Position dimensions, List<List<char>> map)
    {
        var guardPosition = new Position();

        for (var i = 0; i < dimensions.X; i++)
        {
            if (map[i].Contains('^'))
            {
                guardPosition.Y = i;
                guardPosition.X = map[i].IndexOf('^');
            }
        }

        return guardPosition;
    }


    //public static int CountPossibleLoops2(string inputFilePath)
    //{
    //    var rawFile = File.ReadAllLines(inputFilePath);
    //    var map = rawFile.Select(line => line.ToList()).ToList();
    //    var dimensions = new Position()
    //    {
    //        X = map[0].Count,
    //        Y = map.Count
    //    };

    //    var guardPosition = LocateGuard(dimensions, map);
    //    var guardDirection = Direction.Up;

    //    var lastCorners = new List<Position>();
    //    var loopCount = 0;
    //    do
    //    {
    //        if (RotateGuard(ref guardDirection, guardPosition, map))
    //        {
    //            RegisterCorner(lastCorners, guardPosition);
    //            if (lastCorners.Count > 2 && AnalyzeLoop(lastCorners, map)) loopCount++;
    //        }

    //        MoveGuard(guardDirection, guardPosition);
    //    } while (CheckBoundariesWithDirection(guardPosition, guardDirection, dimensions));

    //    return loopCount;
    //}

    //private static bool AnalyzeLoop(List<Position> lastCorners, List<List<char>> map)
    //{
    //    Position leftCorner;
    //    Position rightCorner;
    //    if (lastCorners[0].X < lastCorners[2].X)
    //    {
    //        leftCorner = lastCorners[0];
    //        rightCorner = lastCorners[2];
    //    }
    //    else
    //    {
    //        leftCorner = lastCorners[2];
    //        rightCorner = lastCorners[0];
    //    }

    //    for (var x = leftCorner.X + 1; x < rightCorner.X; x++)
    //    {
    //        if (map[lastCorners[0].Y][x] == '#') return false;
    //        if (map[lastCorners[2].Y][x] == '#') return false;
    //    }

    //    Position upperCorner;
    //    Position lowerCorner;
    //    if (lastCorners[0].Y < lastCorners[2].Y)
    //    {
    //        upperCorner = lastCorners[0];
    //        lowerCorner = lastCorners[2];
    //    }
    //    else
    //    {
    //        upperCorner = lastCorners[2];
    //        lowerCorner = lastCorners[0];
    //    }

    //    for (var y = upperCorner.Y + 1; y < lowerCorner.Y; y++)
    //    {
    //        if (map[y][lastCorners[0].X] == '#') return false;
    //        if (map[y][lastCorners[2].X] == '#') return false;
    //    }

    //    return true;
    //}

    //private static void RegisterCorner(List<Position> lastCorners, Position guardPosition)
    //{
    //    var positionToAdd = new Position()
    //    {
    //        X = guardPosition.X,
    //        Y = guardPosition.Y
    //    };
    //    lastCorners.Add(positionToAdd);
    //    if (lastCorners.Count > 3)
    //    {
    //        lastCorners.RemoveAt(0);
    //    }
    //}
}

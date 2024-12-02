namespace AdventOfCode._2023._01.NotQuiteLisp;

public static class NotQuiteLisp
{
    private const char FloorUp = '(';
    private const char FloorDown = ')';

    // ﻿Input was removed due to the copyright.
    private const string InputFilePath = @"2023\01\NotQuiteLisp\input.txt";

    public static int RunPartOne()
    {
        return FindFloorBasedOnFile(InputFilePath);
    }

    public static int RunPartTwo()
    {
        return CountMovesToReachFloorBasedOnFile(InputFilePath, -1);
    }

    public static int FindFloorBasedOnFile(string inputFilePath)
    {
        var directions = File.ReadAllText(inputFilePath);
        return FindFloor(directions);
    }

    public static int CountMovesToReachFloorBasedOnFile(string inputFilePath, int targetFloor)
    {
        var directions = File.ReadAllText(inputFilePath);
        return CountMovesToReachFloor(directions, targetFloor);
    }

    public static int FindFloor(string directions)
    {
        var currentFloor = 0;
        foreach (var move in directions)
        {
            if (move == FloorUp)
            {
                currentFloor += 1;
            }
            else
            {
                currentFloor -= 1;
            }
        }

        return currentFloor;
    }

    public static int CountMovesToReachFloor(string directions, int targetFloor)
    {
        var currentFloor = 0;
        var movesCount = 0;

        foreach (var move in directions)
        {
            movesCount++;

            if (move == FloorUp)
            {
                currentFloor += 1;
            }
            else
            {
                currentFloor -= 1;
            }

            if (currentFloor == targetFloor)
            {
                return movesCount;
            }
        }

        throw new Exception("Could not reach target floor!");
    }
}

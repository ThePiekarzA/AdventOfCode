using System.Text.RegularExpressions;

namespace AdventOfCode._2023._06;

public static class WaitForIt
{
    private static readonly Regex NumberRegex = new(@"\d+");

    // ﻿Input was removed due to the copyright.
    // Create input.txt file and fill it with data from: https://adventofcode.com/2023/day/6/input
    private const string InputFileName = @"2023\06\input.txt";

    public static ulong RunPartOne()
    {
        return CountWaysToWinAllRaces(InputFileName);
    }

    public static ulong RunPartTwo()
    {
        return CountWaysToWinHugeRace(InputFileName);
    }

    #region PartOne

    public static ulong CountWaysToWinAllRaces(string inputFileName)
    {
        var lines = File.ReadAllLines(inputFileName);

        var races = ParseRaces(lines);

        ulong waysToWin = 1;
        foreach (var race in races)
        {
            waysToWin *= CountWinningStrategiesForRace(race);
        }
        return waysToWin;

    }

    public static ulong CountWinningStrategiesForRace(Race race)
    {
        ulong winningStrategiesCount = 0;
        for (ulong chargingTime = 1; chargingTime < race.TimeLimit; chargingTime++)
        {
            var remainingTime = race.TimeLimit - chargingTime;
            var distance = chargingTime * remainingTime;
            if (distance > race.Distance)
            {
                winningStrategiesCount++;
            }
        }

        return winningStrategiesCount;
    }

    private static List<Race> ParseRaces(string[] lines)
    {
        var raceTimes = NumberRegex.Matches(lines[0]);
        var raceDistances = NumberRegex.Matches(lines[1]);

        var races = new List<Race>();
        for (var i = 0; i < raceTimes.Count; i++)
        {
            var race = new Race()
            {
                TimeLimit = ulong.Parse(raceTimes[i].Value),
                Distance = ulong.Parse(raceDistances[i].Value)
            };
            races.Add(race);
        }

        return races;
    }

    #endregion

    #region PartTwo

    public static ulong CountWaysToWinHugeRace(string inputFileName)
    {
        var lines = File.ReadAllLines(inputFileName);

        var race = ParseHugeRace(lines);

        return CountWinningStrategiesForRaceEfficiently(race);
    }

    public static ulong CountWinningStrategiesForRaceEfficiently(Race race)
    {
        ulong minimalWaitTime = 0;
        ulong maximumWaitTime = 0;
        for (ulong chargingTime = 1; chargingTime < race.TimeLimit; chargingTime++)
        {
            var remainingTime = race.TimeLimit - chargingTime;
            var distance = chargingTime * remainingTime;
            if (distance > race.Distance)
            {
                minimalWaitTime = chargingTime;
                break;
            }
        }

        for (var chargingTime = race.TimeLimit - 1; chargingTime > 0; chargingTime--)
        {
            var remainingTime = race.TimeLimit - chargingTime;
            var distance = chargingTime * remainingTime;
            if (distance > race.Distance)
            {
                maximumWaitTime = chargingTime;
                break;
            }
        }

        return maximumWaitTime - minimalWaitTime + 1;
    }

    private static Race ParseHugeRace(string[] lines)
    {
        var raceTime = NumberRegex.Matches(lines[0]);
        var raceDistance = NumberRegex.Matches(lines[1]);

        var race = new Race()
        {
            TimeLimit = ulong.Parse(string.Join("", raceTime.Select(m => m.Value))),
            Distance = ulong.Parse(string.Join("", raceDistance.Select(m => m.Value)))
        };

        return race;
    }

    #endregion
}
using AdventOfCode._2023._12;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2023._12;

[TestClass]
public class HotSpringsTests
{
    // ﻿Input was removed due to the copyright.
    // Create testInput.txt file and fill it with test data from: https://adventofcode.com/2023/day/12
    private const string InputFilePath = @"2023\12\testInput.txt";

    [DataTestMethod]
    [DataRow("???.###", new[] { 1, 1, 3 }, 1)]
    [DataRow(".??..??...?##.", new [] { 1, 1, 3 }, 4)]
    [DataRow("?#?#?#?#?#?#?#?", new [] { 1, 3, 1, 6 }, 1)]
    [DataRow("????.#...#...", new [] { 4, 1, 1 }, 1)]
    [DataRow("????.######..#####.", new [] { 1, 6, 5 }, 4)]
    [DataRow("?###????????", new [] { 3, 2, 1 }, 10)]
    public void CountPossibleArrangementsTest(string row, int[] damagedConditions, int expectedPossibleArrangements)
    {
        // Act
        var possibleArrangements = HotSprings.CountPossibleArrangements(row, damagedConditions);

        // Assert
        Assert.AreEqual(expectedPossibleArrangements, possibleArrangements);
    }

    [DataTestMethod]
    [DataRow("???.###", "1,1,3", 1)]
    [DataRow(".??..??...?##.", "1,1,3", 16384)]
    [DataRow("?#?#?#?#?#?#?#?", "1,3,1,6", 1)]
    [DataRow("????.#...#...", "4,1,1", 16)]
    [DataRow("????.######..#####.", "1,6,5", 2500)]
    [DataRow("?###????????", "3,2,1", 506250)]
    public void CountPossibleArrangementsWithUnfoldingTest(string row, string damagedConditions, int expectedPossibleArrangements)
    {
        // Act
        HotSprings.UnfoldRecord(row, damagedConditions, out var unfoldedConditionRecord, out var unfoldedConditions);
        var possibleArrangements = HotSprings.CountPossibleArrangements(unfoldedConditionRecord, unfoldedConditions);

        // Assert
        Assert.AreEqual(expectedPossibleArrangements, possibleArrangements);
    }

    [TestMethod]
    public void SumPossibleArrangementsTest()
    {
        // Act
        var possibleArrangements = HotSprings.SumPossibleArrangements(InputFilePath);

        // Assert
        Assert.AreEqual(21, possibleArrangements);
    }
}

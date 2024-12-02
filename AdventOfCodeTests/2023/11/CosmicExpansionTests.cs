using AdventOfCode._2023._11;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2023._11;

[TestClass]
public class CosmicExpansionTests
{
    // ﻿Input was removed due to the copyright.
    // Create testInput.txt file and fill it with test data from: https://adventofcode.com/2023/day/11
    private const string InputFilePath = @"2023\11\testInput.txt";

    [DataTestMethod]
    [DataRow(2, 374)]
    [DataRow(10, 1030)]
    [DataRow(100, 8410)]
    public void SumShortestDistancesBetweenGalaxiesTest(int expansionFaxtor, int expectedDistance)
    {
        // Act
        var shortestDistancesSum = CosmicExpansion.SumShortestDistancesBetweenGalaxiesFast(InputFilePath, expansionFaxtor);

        // Assert
        Assert.AreEqual((ulong)expectedDistance, shortestDistancesSum);
    }
}

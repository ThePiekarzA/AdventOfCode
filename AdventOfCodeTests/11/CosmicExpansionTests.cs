﻿using AdventOfCode._11;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._11;

[TestClass]
public class CosmicExpansionTests
{
    private const string InputFilePath = @"11\testInput.txt";

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

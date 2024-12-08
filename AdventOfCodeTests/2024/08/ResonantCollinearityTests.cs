using AdventOfCode._2024._08;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2024._08;

[TestClass]
public class ResonantCollinearityTests
{
    // ﻿Input was removed due to the copyright.
    // Create testInput.txt file and fill it with test data from: https://adventofcode.com/2024/day/8
    private const string TestFilePath = @"2024\08\testInput.txt";

    [TestMethod]
    public void CalculateAntinodesTest()
    {
        // Act
        var result = ResonantCollinearity.CountAntinodes(TestFilePath);

        // Assert
        Assert.AreEqual(14, result);    
    }

    [TestMethod]
    public void CountAntinodesWithRepetitionsTest()
    {
        // Act
        var result = ResonantCollinearity.CountAntinodesWithRepetitions(TestFilePath);

        // Assert
        Assert.AreEqual(34, result);
    }
}

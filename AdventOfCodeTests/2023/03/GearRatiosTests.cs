using AdventOfCode._2023._03;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2023._03;

[TestClass]
public class GearRatiosTests
{
    // ﻿Input was removed due to the copyright.
    // Create testInput.txt file and fill it with test data from: https://adventofcode.com/2023/day/3
    private const string TestInputFilePath = @"2023\03\testInput.txt";

    [TestMethod]
    public void SumPartNumbersTest()
    {
        // Act
        var sum = GearRatios.SumPartNumbersBaseOnSchematic(TestInputFilePath);

        // Assert
        Assert.AreEqual(4361, sum);
    }

    [TestMethod]
    public void SumGearRatiosTest()
    {
        // Act
        var sum = GearRatios.SumGearRatiosBaseOnSchematic(TestInputFilePath);

        // Assert
        Assert.AreEqual(467835, sum);
    }
}

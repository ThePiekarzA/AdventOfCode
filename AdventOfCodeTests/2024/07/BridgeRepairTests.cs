using AdventOfCode._2024._07;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2024._07;

[TestClass]
public class BridgeRepairTests
{
    // ﻿Input was removed due to the copyright.
    // Create testInput.txt file and fill it with test data from: https://adventofcode.com/2024/day/7
    private const string TestFilePath = @"2024\07\testInput.txt";

    [TestMethod]
    public void SumValidEquationsTest()
    {
        // Act
        var result = BridgeRepair.SumValidEquations(TestFilePath);

        // Assert
        Assert.AreEqual((ulong)3749, result);
    }
}

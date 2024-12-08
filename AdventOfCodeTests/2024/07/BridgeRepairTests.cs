using AdventOfCode._2024._07;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2024._07;

[TestClass]
public class BridgeRepairTests
{
    // ﻿Input was removed due to the copyright.
    // Create testInput.txt file and fill it with test data from: https://adventofcode.com/2024/day/7
    private const string TestFilePath = @"2024\07\testInput.txt";

    [DataTestMethod]
    [DataRow(2, (ulong)3749)]
    [DataRow(3, (ulong)11387)]
    public void SumValidEquationsTest(int operatorsCount, ulong expected)
    {
        // Act
        var result = BridgeRepair.SumValidEquations(TestFilePath, operatorsCount);

        // Assert
        Assert.AreEqual(expected, result);
    }
}

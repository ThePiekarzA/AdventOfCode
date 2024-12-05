using AdventOfCode._2024._05;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2024._05;

[TestClass]
public class PrintQueueTests
{
    // ﻿Input was removed due to the copyright.
    // Create testInput.txt file and fill it with test data from: https://adventofcode.com/2024/day/5
    private const string TestFilePath = @"2024\05\testInput.txt";

    [TestMethod]
    public void SumValidMiddleElementsTest()
    {
        // Act
        var sum = PrintQueue.SumValidMiddleElements(TestFilePath);

        // Assert
        Assert.AreEqual(143, sum);
    }

    [TestMethod]
    public void SumInvalidMiddleElementsTest()
    {
        // Act
        var sum = PrintQueue.SumInvalidMiddleElements(TestFilePath);

        // Assert
        Assert.AreEqual(123, sum);
    }
}

using AdventOfCode._2024._03;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2024._03;

[TestClass]
public class MullItOverTests
{
    [TestMethod]
    public void ScanAndRunInstructionsBasedOnFileTest()
    {
        // Act
        // ﻿Input was removed due to the copyright.
        // Create testInput1.txt file and fill it with first part of test data from: https://adventofcode.com/2024/day/3
        var result = MullItOver.ScanAndRunInstructionsBasedOnFile(@"2024\03\testInput1.txt");

        // Assert
        Assert.AreEqual(161, result);
    }

    [TestMethod]
    public void ScanAndRunInstructionsWithSuspendBasedOnFileTest()
    {
        // Act
        // ﻿Input was removed due to the copyright.
        // Create testInput2.txt file and fill it with second part of test data from: https://adventofcode.com/2024/day/3
        var result = MullItOver.ScanAndRunInstructionsWithSuspendBasedOnFile(@"2024\03\testInput2.txt");

        // Assert
        Assert.AreEqual(48, result);
    }
}

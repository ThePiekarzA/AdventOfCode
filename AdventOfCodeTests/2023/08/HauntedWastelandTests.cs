using AdventOfCode._2023._08;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2023._08;

[TestClass]
public class HauntedWastelandTests
{
    // ﻿Input was removed due to the copyright.
    // Create testInput1.txt and testInput2.txt files and fill them with first and second test data from: https://adventofcode.com/2023/day/8
    [DataTestMethod]
    [DataRow(@"2023\08\testInput1.txt", 2)]
    [DataRow(@"2023\08\testInput2.txt", 6)]
    public void GoToZZZTest(string inputFilePath, int expectedStepsCount)
    {
        // Act
        var stepsCount = HauntedWasteland.GoToZZZ(inputFilePath);

        // Assert
        Assert.AreEqual(expectedStepsCount, stepsCount);
    }


    [TestMethod]
    public void GoToXXZTest()
    {
        // Act
        // ﻿Input was removed due to the copyright.
        // Create testInput3.txt and fill it with third test data from: https://adventofcode.com/2023/day/8
        var stepsCount = HauntedWasteland.GoToXXZ(@"2023\08\testInput3.txt");

        // Assert
        Assert.AreEqual((ulong)6, stepsCount);
    }
}

using AdventOfCode._2024._04;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2024._04;

[TestClass]
public class CeresSearchTests
{
    // ﻿Input was removed due to the copyright.
    // Create testInput.txt file and fill it with test data from: https://adventofcode.com/2024/day/4
    private const string TestFilePath = @"2024\04\testInput.txt";

    [TestMethod]
    public void FindXmasByFileTest()
    {
        // Act
        var occurrences = CeresSearch.FindXmasByFile(TestFilePath);

        // Assert
        Assert.AreEqual(18, occurrences);
    }

    [TestMethod]
    public void FindMasByFileTest()
    {
        // Act
        var occurrences = CeresSearch.FindMasByFile(TestFilePath);

        // Assert
        Assert.AreEqual(9, occurrences);
    }
}

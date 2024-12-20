﻿using AdventOfCode._2023._04;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2023._04;

[TestClass]
public class ScratchcardsTests
{
    // ﻿Input was removed due to the copyright.
    // Create testInput.txt file and fill it with test data from: https://adventofcode.com/2023/day/4
    private const string TestFilePath = @"2023\04\testInput.txt";

    [DataTestMethod]
    [DataRow("Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53", 8)]
    [DataRow("Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19", 2)]
    [DataRow("Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1", 2)]
    [DataRow("Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83", 1)]
    [DataRow("Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36", 0)]
    [DataRow("Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11", 0)]
    public void GetScratchcardPointsTest(string scratchcard, int expectedPoints)
    {
        // Act
        var points = Scratchcards.GetScratchcardPoints(scratchcard);

        // Assert
        Assert.AreEqual(expectedPoints, points);
    }

    [TestMethod]
    public void SumScratchcardsPointsTest()
    {
        // Act
        var points = Scratchcards.SumScratchcardsPoints(TestFilePath);

        // Assert
        Assert.AreEqual(13, points);
    }

    [TestMethod]
    public void SumScratchcardsTest()
    {
        // Act 
        var wonScratchcards = Scratchcards.SumScratchcards(TestFilePath);

        // Assert
        Assert.AreEqual(30, wonScratchcards);
    }
}

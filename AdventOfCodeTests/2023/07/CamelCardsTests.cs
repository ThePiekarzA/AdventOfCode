using AdventOfCode._2023._07;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2023._07;

[TestClass]
public class CamelCardsTests
{
    // ﻿Input was removed due to the copyright.
    // Create testInput.txt file and fill it with test data from: https://adventofcode.com/2023/day/7
    private const string InputFileName = @"2023\07\testInput.txt";

    [TestMethod]
    public void GetTotalWinningsTests()
    {
        // Act
        var winnings = CamelCards.GetTotalWinnings(InputFileName);

        // Assert
        Assert.AreEqual(6440, winnings);
    }

    [TestMethod]
    public void GetTotalWinningsWithJokerTests()
    {
        // Act
        var winnings = CamelCards.GetTotalWinnings(InputFileName, true);

        // Assert
        Assert.AreEqual(5905, winnings);
    }
}

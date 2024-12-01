﻿using AdventOfCode._2023._07;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2023._07;

[TestClass]
public class CamelCardsTests
{
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

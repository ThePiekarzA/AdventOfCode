using AdventOfCode._2024._06;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2024._06;

[TestClass]
public class GuardGallivantTests
{
    // ﻿Input was removed due to the copyright.
    // Create testInput.txt file and fill it with test data from: https://adventofcode.com/2024/day/6
    private const string TestFilePath = @"2024\06\testInput.txt";

    [TestMethod]
    public void CalculateRouteTest()
    {
        // Act
        var moves = GuardGallivant.CalculateRoute(TestFilePath);

        // Assert
        Assert.AreEqual(41, moves);
    }

    [TestMethod]
    public void CountPossibleLoopsTest()
    {
        // Act
        var moves = GuardGallivant.CountPossibleLoops(TestFilePath);

        // Assert
        Assert.AreEqual(6, moves);
    }

    //[TestMethod]
    //public void CountPossibleLoops2Test()
    //{
    //    // Act
    //    var moves = GuardGallivant.CountPossibleLoops2(TestFilePath);

    //    // Assert
    //    Assert.AreEqual(6, moves);
    //}
}

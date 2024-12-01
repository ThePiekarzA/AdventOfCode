using AdventOfCode._2023._02;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2023._02;

[TestClass]
public class CubeConundrumTests
{
    #region PartOne

    [DataTestMethod]
    [DataRow("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", true)]
    [DataRow("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", true)]
    [DataRow("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", false)]
    [DataRow("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", false)]
    [DataRow("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", true)]
    public void IsGamePossibleTest(string gameDescription, bool expected)
    {
        // Arrange
        var redCount = 12;
        var greenCount = 13;
        var blueCount = 14;

        // Act
        var result = CubeConundrum.IsGamePossible(gameDescription, redCount, greenCount, blueCount, out _);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void SumPossibleGameIdsTest()
    {
        // Arrange
        var redCount = 12;
        var greenCount = 13;
        var blueCount = 14;

        // Act
        var result = CubeConundrum.SumPossibleGameIds(@"2023\02\testInput.txt", redCount, greenCount, blueCount);

        // Assert
        Assert.AreEqual(8, result);
    }

    #endregion

    #region PartTwo

    [DataTestMethod]
    [DataRow("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 48)]
    [DataRow("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", 12)]
    [DataRow("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", 1560)]
    [DataRow("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", 630)]
    [DataRow("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", 36)]
    public void GetMinimalSetPowerTest(string gameDescription, int expected)
    {
        // Act
        var result = CubeConundrum.GetMinimalSetPower(gameDescription);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void GetMinimalSetPowerSumTest()
    {
        // Act
        var result = CubeConundrum.GetMinimalSetPowerSum(@"2023\02\testInput.txt");

        // Assert
        Assert.AreEqual(2286, result);
    }

    #endregion
}

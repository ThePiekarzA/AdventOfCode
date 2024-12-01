using AdventOfCode._2023._06;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2023._06;

[TestClass]
public class WaitForItTests
{
    private const string InputFileName = @"2023\06\testInput.txt";
    
    [DataTestMethod]
    [DynamicData(nameof(TestData))]
    public void CountWinningStrategiesForRaceTest(ulong timeLimit, ulong distance, ulong expectedStrategiesCount)
    {
        // Arrange
        var race = new Race()
        {
            TimeLimit = timeLimit,
            Distance = distance
        };

        // Act
        var strategiesCount = WaitForIt.CountWinningStrategiesForRace(race);

        // Assert
        Assert.AreEqual(expectedStrategiesCount, strategiesCount);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestData))]
    public void CountWinningStrategiesForRaceEfficientlyTest(ulong timeLimit, ulong distance, ulong expectedStrategiesCount)
    {
        // Arrange
        var race = new Race()
        {
            TimeLimit = timeLimit,
            Distance = distance
        };

        // Act
        var strategiesCount = WaitForIt.CountWinningStrategiesForRaceEfficiently(race);

        // Assert
        Assert.AreEqual(expectedStrategiesCount, strategiesCount);
    }

    [TestMethod]
    public void CountWaysToWinAllRacesTest()
    {
        // Act
        var waysToWin = WaitForIt.CountWaysToWinAllRaces(InputFileName);

        // Assert
        Assert.AreEqual((ulong)288, waysToWin);
    }

    [TestMethod]
    public void CountWaysToWinHugeRaceTest()
    {
        // Act
        var waysToWin = WaitForIt.CountWaysToWinHugeRace(InputFileName);

        // Assert
        Assert.AreEqual((ulong)71503, waysToWin);
    }

    public static IEnumerable<object[]> TestData
    {
        get
        {
            return new[]
            { 
                new object[] { (ulong)7, (ulong)9, (ulong)4 },
                new object[] { (ulong)15, (ulong)40, (ulong)8 },
                new object[] { (ulong)30, (ulong)200, (ulong)9 },
                new object[] { (ulong)71530, (ulong)940200, (ulong)71503 }
            };
        }
    }
}

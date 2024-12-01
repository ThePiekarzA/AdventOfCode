using AdventOfCode._2023._05;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2023._05;

[TestClass]
public class IfYouGiveASeedAFertilizerTests
{
    private const string InputFilePath = @"2023\05\testInput.txt";

    [TestMethod]
    public void FindNearestLocationForSeed()
    {
        // Act 
        var nearestLocation = IfYouGiveASeedAFertilizer.FindNearestLocationForSeed(InputFilePath);

        // Assert
        Assert.AreEqual((ulong)35, nearestLocation);
    }

    [TestMethod]
    public void FindNearestLocationForSeedRangeTest()
    {
        // Act
        var nearestLocation = IfYouGiveASeedAFertilizer.FindNearestLocationForSeedRange(InputFilePath);

        // Assert
        Assert.AreEqual((ulong)46, nearestLocation);
    }
}

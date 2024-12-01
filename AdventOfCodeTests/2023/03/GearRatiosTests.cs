using AdventOfCode._2023._03;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2023._03;

[TestClass]
public class GearRatiosTests
{
    [TestMethod]
    public void SumPartNumbersTest()
    {
        // Act
        var sum = GearRatios.SumPartNumbersBaseOnSchematic(@"2023\03\testInput.txt");

        // Assert
        Assert.AreEqual(4361, sum);
    }

    [TestMethod]
    public void SumGearRatiosTest()
    {
        // Act
        var sum = GearRatios.SumGearRatiosBaseOnSchematic(@"2023\03\testInput.txt");

        // Assert
        Assert.AreEqual(467835, sum);
    }
}

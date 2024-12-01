using AdventOfCode._09;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._09;

[TestClass]
public class MirageMaintenanceTests
{
    private const string InputFilePath = @"09\testInput.txt";

    [DataTestMethod]
    [DataRow("0 3 6 9 12 15", 18)]
    [DataRow("1 3 6 10 15 21", 28)]
    [DataRow("10 13 16 21 30 45", 68)]
    public void ExtrapolateValueTest(string values, int expected)
    {
        // Arrange
        var valueHistory = values.Split(' ').Select(int.Parse).ToList();

        // Act
        var extrapolatedValue = MirageMaintenance.ExtrapolateValue(valueHistory);

        // Assert
        Assert.AreEqual(expected, extrapolatedValue);
    }

    [DataTestMethod]
    [DataRow("0 3 6 9 12 15", -3)]
    [DataRow("1 3 6 10 15 21", 0)]
    [DataRow("10 13 16 21 30 45", 5)]
    public void ExtrapolateValueBackwardsTest(string values, int expected)
    {
        // Arrange
        var valueHistory = values.Split(' ').Select(int.Parse).ToList();

        // Act
        var extrapolatedValue = MirageMaintenance.ExtrapolateValueBackwards(valueHistory);

        // Assert
        Assert.AreEqual(expected, extrapolatedValue);
    }

    [TestMethod]
    public void SumExtrapolationValuesTest()
    {
        // Act
        var sum = MirageMaintenance.SumExtrapolationValues(InputFilePath);
        
        // Assert
        Assert.AreEqual(114, sum);
    }

    [TestMethod]
    public void SumExtrapolationValuesBackwardsTest()
    {
        // Act
        var sum = MirageMaintenance.SumExtrapolationValuesBackwards(InputFilePath);
    
        // Assert
        Assert.AreEqual(2, sum);
    }
}

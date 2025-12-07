using AdventOfCode._2025._07;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2025._07;

[TestClass]
public class LaboratoriesTests
{
    private const string TestFilePath = @"2025\07\testInput.txt";

    [TestMethod]
    public void CountSplitsTest()
    {
        // Act
        var result = Laboratories.CountSplits(TestFilePath);

        // Assert
        Assert.AreEqual(21, result);
    }

    [TestMethod]
    public void FindAllTimelinesFinalTest()
    {
        // Act
        var result = Laboratories.FindAllTimelinesFinal(TestFilePath);

        // Assert
        Assert.AreEqual((ulong)40, result);
    }
}

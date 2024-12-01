using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode._2024._01;

namespace AdventOfCodeTests._2024._01;

[TestClass]
public class HistorianHysteriaTests
{
    private const string TestFilePath = @"2024\01\testInput.txt";

    [TestMethod]
    public void SumDistancesTest()
    {
        // Act
        var distancesSum = HistorianHysteria.ReconcileListsBasedOnFile(TestFilePath);

        // Assert
        Assert.AreEqual(11, distancesSum);
    }

    [TestMethod]
    public void CalculateSimilarityTest()
    {
        // Act 
        var similarityScore = HistorianHysteria.CalculateSimilarityScoreBasedOnFile(TestFilePath);

        // Assert
        Assert.AreEqual(31, similarityScore);
    }
}

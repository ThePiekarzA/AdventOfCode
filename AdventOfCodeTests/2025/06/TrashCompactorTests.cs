using AdventOfCode._2025._06;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2025._06;

[TestClass]
public class TrashCompactorTests
{
    private const string TestFilePath = @"2025\06\testInput.txt";

    [TestMethod]
    public void SumProblemsAnswersPartOneTest()
    {
        // Act
        var result = TrashCompactor.SumProblemsAnswersForPartOne(TestFilePath);

        // Assert
        Assert.AreEqual((ulong)4277556, result);
    }

    [TestMethod]
    public void SumProblemsAnswersPartTwoTest()
    {
        // Act
        var result = TrashCompactor.SumProblemsAnswersForPartTwo(TestFilePath);

        // Assert
        Assert.AreEqual((ulong)3263827, result);
    }
}

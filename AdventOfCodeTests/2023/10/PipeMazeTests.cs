using AdventOfCode._2023._10;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2023._10;

[TestClass]
public class PipeMazeTests
{
    [DataTestMethod]
    [DataRow(@"2023\10\testInput1.txt", 4)]
    [DataRow(@"2023\10\testInput2.txt", 8)]
    public void FindFarthestDistanceTest(string inputFilePath, int expectedDistance)
    {
        // Act
        var distance = PipeMaze.FindFarthestDistance(inputFilePath, out _);

        // Assert
        Assert.AreEqual(expectedDistance, distance);
    }

    [DataTestMethod]
    [DataRow(@"2023\10\testInput1.txt", 1)]
    [DataRow(@"2023\10\testInput2.txt", 1)]
    [DataRow(@"2023\10\testInput3.txt", 4)]
    [DataRow(@"2023\10\testInput4.txt", 8)]
    [DataRow(@"2023\10\testInput5.txt", 10)]
    public void CalculateLoopAreaTest(string inputFilePath, int expectedArea)
    {
        // Act
        PipeMaze.FindFarthestDistance(inputFilePath, out var area);

        // Assert
        Assert.AreEqual(expectedArea, area);
    }
}

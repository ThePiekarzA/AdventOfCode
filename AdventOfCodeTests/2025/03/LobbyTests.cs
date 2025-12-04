using AdventOfCode._2025._03;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2025._03;

[TestClass]
public class LobbyTests
{
    private const string TestFile1Path = @"2025\03\testInput.txt";

    [DataTestMethod]
    [DataRow(2, (ulong)357)]
    [DataRow(12, (ulong)3121910778619)]
    public void FindMaxJoltageTest(int length, ulong expected)
    {
        // Act
        var result = Lobby.FindMaxJoltage(TestFile1Path, length);

        // Assert
        Assert.AreEqual(expected, result);
    }
}

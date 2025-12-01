using AdventOfCode._2025._01;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2025._01;

[TestClass]
public class SecretEntranceTests
{
    private const string TestFile1Path = @"2025\01\testInput1.txt";

    [TestMethod]
    public void FindPasswordTests()
    {
        // Act
        var result = SecretEntrance.FindPassword(TestFile1Path);

        // Assert
        Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void FindPasswordMethod0x434C49434BTests()
    {
        // Act
        var result = SecretEntrance.FindPasswordMethod0x434C49434B(TestFile1Path);

        // Assert
        Assert.AreEqual(6, result);
    }
}

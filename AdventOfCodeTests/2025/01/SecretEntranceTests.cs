using AdventOfCode._2025._01;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2025._01;

[TestClass]
public class SecretEntranceTests
{
    private const string TestFilePath = @"2025\01\testInput.txt";

    [TestMethod]
    public void FindPasswordTests()
    {
        // Act
        var result = SecretEntrance.FindPassword(TestFilePath);

        // Assert
        Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void FindPasswordMethod0x434C49434BTests()
    {
        // Act
        var result = SecretEntrance.FindPasswordMethod0x434C49434B(TestFilePath);

        // Assert
        Assert.AreEqual(6, result);
    }
}

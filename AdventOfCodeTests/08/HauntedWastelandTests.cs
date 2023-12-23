using AdventOfCode._08;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._08;

[TestClass]
public class HauntedWastelandTests
{
    [DataTestMethod]
    [DataRow(@"08\testInput1.txt", 2)]
    [DataRow(@"08\testInput2.txt", 6)]
    public void GoToZZZTest(string inputFilePath, int expectedStepsCount)
    {
        // Act
        var stepsCount = HauntedWasteland.GoToZZZ(inputFilePath);

        // Assert
        Assert.AreEqual(expectedStepsCount, stepsCount);
    }


    [TestMethod]
    public void GoToXXZTest()
    {
        // Act
        var stepsCount = HauntedWasteland.GoToXXZ(@"08\testInput3.txt");

        // Assert
        Assert.AreEqual((ulong)6, stepsCount);
    }
}

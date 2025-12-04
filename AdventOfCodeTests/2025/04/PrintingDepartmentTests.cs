using AdventOfCode._2025._04;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2025._04;

[TestClass]
public class PrintingDepartmentTests
{
    private const string TestFilePath = @"2025\04\testInput.txt";

    [TestMethod]
    public void CountAccessibleRollsTest()
    {
        // Act
        var result = PrintingDepartment.CountAccessibleRolls(TestFilePath);

        // Assert
        Assert.AreEqual(13, result);
    }

    [TestMethod]
    public void RemoveRollsTest()
    {
        // Act
        var result = PrintingDepartment.RemoveRolls(TestFilePath);

        // Assert
        Assert.AreEqual(43, result);
    }
}

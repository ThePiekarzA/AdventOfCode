using AdventOfCode._01.NotQuiteLisp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._01.NotQuiteLispTests;

[TestClass]
public class NotQuiteLispTests
{
    [DataTestMethod]
    [DataRow("()()", 0)]
    [DataRow("(())", 0)]
    [DataRow("(((", 3)]
    [DataRow("(()(()(", 3)]
    [DataRow("))(((((", 3)]
    [DataRow("())", -1)]
    [DataRow("))(", -1)]
    [DataRow(")))", -3)]
    [DataRow(")())())", -3)]
    public void FindFloorTest(string directions, int expected)
    {
        // Act
        var targetFloor = NotQuiteLisp.FindFloor(directions);

        // Assert
        Assert.AreEqual(expected, targetFloor);
    }

    [DataTestMethod]
    [DataRow(")", -1, 1)]
    [DataRow("()())", -1, 5)]
    public void CountMovesToReachFloorTest(string directions, int targetFloor, int expectedMovesCount)
    {
        // Act 
        var movesCount = NotQuiteLisp.CountMovesToReachFloor(directions, targetFloor);

        // Assert
        Assert.AreEqual(expectedMovesCount, movesCount);
    }

    [TestMethod]
    public void FindFloorBasedOnFileTest()
    {
        // Act
        var targetFloor = NotQuiteLisp.FindFloorBasedOnFile(@"01\NotQuiteLispTests\testInput.txt");

        // Assert
        Assert.AreEqual(1, targetFloor);
    }

    [TestMethod]
    public void CountMovesToReachFloorBasedOnFileTest()
    {
        // Act
        var movesCount = NotQuiteLisp.CountMovesToReachFloorBasedOnFile(@"01\NotQuiteLispTests\testInput.txt", 3);

        // Assert
        Assert.AreEqual(11, movesCount);
    }
}

using System.Security.Cryptography;
using AdventOfCode._2025._05;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2025._05;

[TestClass]
public class CafeteriaTests
{
    private const string TestFilePath = @"2025\05\testInput.txt";

    [TestMethod]
    public void CountFreshIngredientsTest()
    {
        // Act
        var result = Cafeteria.CountFreshIngredients(TestFilePath);

        // Assert
        Assert.AreEqual((ulong)3, result);
    }

    [TestMethod]
    public void CountFreshIngredientIdsTest()
    {
        // Act
        var result = Cafeteria.CountFreshIngredientIds(TestFilePath);

        // Assert
        Assert.AreEqual((ulong)14, result);
    }
}

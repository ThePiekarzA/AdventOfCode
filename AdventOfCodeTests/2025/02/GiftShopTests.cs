using AdventOfCode._2025._02;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2025._02;

[TestClass]
public class GiftShopTests
{
    private const string TestFile1Path = @"2025\02\testInput1.txt";

    [TestMethod]
    public void FindInvalidIdsTest()
    {
        // Act
        var result = GiftShop.FindInvalidIds(TestFile1Path);

        // Assert
        Assert.AreEqual((ulong)1227775554, result);
    }

    [TestMethod]
    public void FindInvalidIdsMultiSequenceTest()
    {
        // Act
        var result = GiftShop.FindInvalidIdsMultiSequence(TestFile1Path);

        // Assert
        Assert.AreEqual((ulong)4174379265, result);
    }
}

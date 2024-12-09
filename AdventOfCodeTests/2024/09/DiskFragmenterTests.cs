using AdventOfCode._2024._09;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2024._09;

[TestClass]
public class DiskFragmenterTests
{
    // ﻿Input was removed due to the copyright.
    // Create testInput.txt file and fill it with test data from: https://adventofcode.com/2024/day/9
    private const string TestFilePath = @"2024\09\testInput.txt";

    [DataTestMethod]
    [DataRow("12345", "022111222")]
    [DataRow("2333133121414131402", "0099811188827773336446555566")]
    public void CompactDiskTest(string map, string expected)
    {
        // Act
        var result = DiskFragmenter.CompactDisk(map);

        // Assert
        Assert.AreEqual(expected, string.Join("", result));
    }

    [TestMethod]
    public void FragmentDiskTest()
    {
        // Act
        var result = DiskFragmenter.FragmentDisk(TestFilePath);

        // Assert
        Assert.AreEqual((ulong)1928, result);
    }

    [TestMethod]
    public void DefragmentDiskInternalTest()
    {
        // Act
        var result = DiskFragmenter.DefragmentDiskInternal("2333133121414131402");

        // Assert
        Assert.AreEqual("009921117770440333000055550666600000888800", string.Join("", result));
    }

    [TestMethod]
    public void DeragmentDiskTest()
    {
        // Act
        var result = DiskFragmenter.DefragmentDisk(TestFilePath);

        // Assert
        Assert.AreEqual((ulong)2858, result);
    }
}

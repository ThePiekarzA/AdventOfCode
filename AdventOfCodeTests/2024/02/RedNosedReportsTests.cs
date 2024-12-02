using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode._2024._02;

namespace AdventOfCodeTests._2024._02;

[TestClass]
public class RedNosedReportsTests
{
    // ﻿Input was removed due to the copyright.
    // Create testInput.txt file and fill it with test data from: https://adventofcode.com/2024/day/2
    private const string TestFilePath = @"2024\02\testInput.txt";

    [TestMethod]
    [DataRow(new[] { 1, 2, 3, 4}, true)]
    [DataRow(new[] { 1, 2, 5, 8}, true)]
    [DataRow(new[] { 1, 2, 5, 9}, false)]
    [DataRow(new[] { 1, 1, 5, 9}, false)]
    [DataRow(new[] { 1, 2, 2, 4}, false)]
    [DataRow(new[] { 1, -1, -3, -5}, true)]
    [DataRow(new[] { 1, -1, -3, -10}, false)]
    [DataRow(new[] { 1, -1, -3, -2}, false)]
    public void AnalyzeReportTest(int[] report, bool expectedResult)
    {
        // Act
        var result = RedNosedReports.AnalyzeReport(report);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    [TestMethod]
    public void CountSafeReportsBasedOnFileTest()
    {
        // Act
        var safeReportsCount = RedNosedReports.CountSafeReportsBasedOnFile(TestFilePath);

        // Assert
        Assert.AreEqual(2, safeReportsCount);
    }

    [TestMethod]
    public void CountSafeReportsWithRiskBasedOnFileTest()
    {
        // Act
        var safeReportsCount = RedNosedReports.CountSafeReportsWithRiskBasedOnFile(TestFilePath);

        // Assert
        Assert.AreEqual(4, safeReportsCount);
    }
}

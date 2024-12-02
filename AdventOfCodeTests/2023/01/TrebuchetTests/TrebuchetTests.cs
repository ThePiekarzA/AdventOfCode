using AdventOfCode._2023._01.Trebuchet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._2023._01.TrebuchetTests;

[TestClass]
public class TrebuchetTests
{
    [DataTestMethod]
    [DataRow("1abc2", 12)]
    [DataRow("pqr3stu8vwx", 38)]
    [DataRow("a1b2c3d4e5f", 15)]
    [DataRow("treb7uchet", 77)]
    public void RecoverCalibrationValueTest(string testText, int expectedValue)
    {
        // Act
        var calibrationValue = Trebuchet.RecoverCalibrationValue(testText);

        // Assert
        Assert.AreEqual(expectedValue, calibrationValue);
    }

    [DataTestMethod]
    [DataRow("two1nine", 29)]
    [DataRow("eightwothree", 83)]
    [DataRow("abcone2threexyz", 13)]
    [DataRow("xtwone3four", 24)]
    [DataRow("4nineeightseven2", 42)]
    [DataRow("zoneight234", 14)]
    [DataRow("7pqrstsixteen", 76)]
    [DataRow("hthphptmmtwo7sixsevenoneightls", 28)]
    public void RecoverCalibrationValue2Test(string testText, int expectedValue)
    {
        // Act
        var calibrationValue = Trebuchet.RecoverCalibrationValue2(testText);

        // Assert
        Assert.AreEqual(expectedValue, calibrationValue);
    }

    [TestMethod]
    public void SumCalibrationValuesTest()
    {
        // Act
        // ﻿Input was removed due to the copyright.
        // Paste here the first test input from: https://adventofcode.com/2023/day/1
        var calibrationValuesSum = Trebuchet.SumCalibrationValues(@"2023\01\TrebuchetTests\testInput.txt");

        // Assert
        Assert.AreEqual(142, calibrationValuesSum);
    }

    [TestMethod]
    public void SumCalibrationValues2Test()
    {
        // Act
        // ﻿Input was removed due to the copyright.
        // Paste here the second test input from: https://adventofcode.com/2023/day/1
        var calibrationValuesSum = Trebuchet.SumCalibrationValues(@"2023\01\TrebuchetTests\testInput2.txt", true);

        // Assert
        Assert.AreEqual(281, calibrationValuesSum);
    }
}

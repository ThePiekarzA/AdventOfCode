namespace AdventOfCode._2024._02;

public class RedNosedReports
{
    // ﻿Input was removed due to the copyright.
    // Create input.txt file and fill it with data from: https://adventofcode.com/2024/day/2/input
    private const string InputFilePath = @"2024\02\input.txt";

    #region Part one

    public static int RunPartOne()
    {
        return CountSafeReportsBasedOnFile(InputFilePath);
    }

    public static int CountSafeReportsBasedOnFile(string inputFilePath)
    {
        var reports = ParseInput(inputFilePath);
        return CountSafeReports(reports);
    }

    private static int CountSafeReports(List<int[]> reports)
    {
        var safeReportsCount = 0;
        foreach (var report in reports)
        {
            if (AnalyzeReport(report))
            {
                safeReportsCount++;
            }
        }
        return safeReportsCount;
    }

    public static bool AnalyzeReport(int[] report)
    {
        var previousDistance = report[0] - report[1];
        if (!ValidateDistance(previousDistance)) return false;
        
        for (var i = 1; i < report.Length - 1; i++)
        {
            var currentDistance = report[i] - report[i + 1];
            if (!ValidateDistance(currentDistance)) return false;

            if (previousDistance * currentDistance < 0) // Check if tendency changed
            {
                return false;
            }
            previousDistance = currentDistance;
        }

        return true;
    }

    #endregion

    #region Part Two

    public static int RunPartTwo()
    {
        return CountSafeReportsWithRiskBasedOnFile(InputFilePath);
    }

    public static int CountSafeReportsWithRiskBasedOnFile(string inputFilePath)
    {
        var reports = ParseInput(inputFilePath);
        return CountSafeReportsWithRisk(reports);
    }

    private static int CountSafeReportsWithRisk(List<int[]> reports)
    {
        var safeReportsCount = 0;
        foreach (var report in reports)
        {
            if (AnalyzeReportWithRiskProperly(report))
            {
                safeReportsCount++;
            }
        }
        return safeReportsCount;
    }

    public static bool AnalyzeReportWithRiskProperly(IList<int> originalReport)
    {
        if (AnalyzeReport(originalReport.ToArray())) 
            return true;

        var distances = CalculateDistances(originalReport);

        for (var i = 0; i < distances.Length; i++)
        {
            if (!ValidateDistance(distances[i]))
            {
                if (AnalyzeAlteredReport(originalReport, i)) 
                    return true;
            }

            if (i < distances.Length - 1 &&
                distances[i] * distances[i + 1] < 0)
            {
                if (AnalyzeAlteredReport(originalReport, i))
                    return true;
                if (AnalyzeAlteredReport(originalReport, i + 1))
                    return true;
            }
        }

        return AnalyzeAlteredReport(originalReport, originalReport.Count - 1);
    }

    private static bool AnalyzeAlteredReport(IList<int> originalReport, int indexToRemove)
    {
        var report = new List<int>(originalReport);
        report.RemoveAt(indexToRemove);
        return AnalyzeReport(report.ToArray());
    }

    private static int[] CalculateDistances(IList<int> report)
    {
        var distances = new int[report.Count - 1];
        distances[0] = report[0] - report[1];
        for (var i = 1; i < report.Count - 1; i++)
        {
            distances[i] = report[i] - report[i + 1];
        }

        return distances;
    }

    #region Other working propositions

    // Once for a while brute force can save lives...
    public static bool BruteForceAnalysis(IList<int> report)
    {
        if (AnalyzeReport(report.ToArray()))
            return true;

        for (var i = 0; i < report.Count; i++)
        {
            var reportCopy = new List<int>(report);
            reportCopy.RemoveAt(i);
            if (AnalyzeReport(reportCopy.ToArray()))
                return true;
        }

        return false;
    }

    public static bool AnalyzeReportWithRisk(int[] report)
    {
        var reportCopy = new List<int>(report);
        if (AnalyzeReportAndRemoveInvalidLevel(reportCopy, true) || AnalyzeReportAndRemoveInvalidLevel(reportCopy, true))
            return true;
        
        reportCopy = new List<int>(report);
        if (AnalyzeReportAndRemoveInvalidLevel(reportCopy, false) || AnalyzeReportAndRemoveInvalidLevel(reportCopy, false))
            return true;

        // Handle possible situation where first element messes up the order
        reportCopy = new List<int>(report);
        reportCopy.RemoveAt(0);
        return AnalyzeReport(reportCopy.ToArray());
    }

    #endregion

    private static bool AnalyzeReportAndRemoveInvalidLevel(IList<int> report, bool removeNext)
    {
        var previousDistance = report[0] - report[1];
        if (!ValidateDistance(previousDistance))
        {
            var indexToRemove = removeNext ? 1 : 0;
            report.RemoveAt(indexToRemove);
            return false;
        }

        for (var i = 1; i < report.Count - 1; i++)
        {
            var currentDistance = report[i] - report[i + 1];
            if (!ValidateDistance(currentDistance))
            {
                var indexToRemove = removeNext ? i + 1 : i;
                report.RemoveAt(indexToRemove);
                return false;
            }

            if (previousDistance * currentDistance < 0) // Check if tendency changed
            {
                var indexToRemove = removeNext ? i + 1 : i;
                report.RemoveAt(indexToRemove);
                return false;
            }
            previousDistance = currentDistance;
        }

        return true;
    }

    #endregion

    #region Common

    private static List<int[]> ParseInput(string inputFilePath)
    {
        var rawReports = File.ReadAllLines(inputFilePath);
        var reports = rawReports.Select(
                rawReport => rawReport.Split(' ').Select(int.Parse).ToArray())
            .ToList();
        return reports;
    }

    private static bool ValidateDistance(int previousDistance)
    {
        return Math.Abs(previousDistance) >= 1 && Math.Abs(previousDistance) <= 3;
    }

    #endregion
}

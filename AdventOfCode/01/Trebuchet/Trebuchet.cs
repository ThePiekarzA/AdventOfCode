using System.Text.RegularExpressions;

namespace AdventOfCode._01.Trebuchet;
public static class Trebuchet
{
    private static readonly Regex DigitsRegex = new(@"\d");
    private static readonly Regex WordsRegex = new(@"\d|(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)");
    private static readonly Regex WordsRegexInverse = new(@"\d|(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)", RegexOptions.RightToLeft);

    private static readonly Dictionary<string, int> WordsToNumbersDictionary = new()
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 }
    };

    private const string InputFilePath = @"01\Trebuchet\input.txt";

    public static int RunPartOne()
    {
        return SumCalibrationValues(InputFilePath);
    }

    public static int RunPartTwo()
    {
        return SumCalibrationValues(InputFilePath, true);
    }

    public static int SumCalibrationValues(string inputFilePath, bool matchWords = false)
    {
        using var outputFile = new StreamWriter(@"01\Trebuchet\output.txt");
        var lines = File.ReadLines(inputFilePath);

        var linesCount = 0;
        var calibrationValuesSum = 0;
        foreach (var line in lines)
        {
            var calibrationValue = matchWords ? RecoverCalibrationValue2(line) : RecoverCalibrationValue(line);
            calibrationValuesSum += calibrationValue;

            outputFile.WriteLine($"{calibrationValue}\t{line}");
            linesCount++;
        }

        return calibrationValuesSum;
    }

    public static int RecoverCalibrationValue(string obfuscatedValue)
    {
        var calibrationValueMatches = DigitsRegex.Matches(obfuscatedValue);

        var firstDigit = Convert.ToInt32(calibrationValueMatches[0].Value);
        var secondDigit = Convert.ToInt32(calibrationValueMatches[^1].Value);

        var calibrationValue = firstDigit * 10 + secondDigit;
        return calibrationValue;
    }

    public static int RecoverCalibrationValue2(string obfuscatedValue)
    {
        var firstDigitMatches = WordsRegex.Matches(obfuscatedValue);
        var firstDigit = ConvertCalibrationValueDigit(firstDigitMatches[0].Value);

        var secondDigitMatches = WordsRegexInverse.Matches(obfuscatedValue);
        var secondDigit = ConvertCalibrationValueDigit(secondDigitMatches[0].Value);

        var calibrationValue = firstDigit * 10 + secondDigit;
        return calibrationValue;
    }

    private static int ConvertCalibrationValueDigit(string rawValue)
    {
        return WordsToNumbersDictionary.TryGetValue(rawValue, out var value) ? value : Convert.ToInt32(rawValue);
    }
}

namespace AdventOfCode._2024._01;

public class HistorianHysteria
{
    private const string InputFilePath = @"2024\01\input.txt";

    public static int RunPartOne()
    {
        return ReconcileListsBasedOnFile(InputFilePath);
    }

    public static int RunPartTwo()
    {
        return CalculateSimilarityScoreBasedOnFile(InputFilePath);
    }

    public static int ReconcileListsBasedOnFile(string inputFilePath)
    {
        ParseInput(inputFilePath, out var list1, out var list2);
        return ReconcileLists(list1, list2);
    }

    public static void ParseInput(string inputFilePath, out IList<int> list1, out IList<int> list2)
    {
        list1 = new List<int>();
        list2 = new List<int>();

        var rawLists = File.ReadLines(inputFilePath);
        foreach (var line in rawLists)
        {
            var values = line.Split(' ');
            list1.Add(int.Parse(values.First()));
            list2.Add(int.Parse(values.Last()));
        }
    }

    public static int ReconcileLists(IEnumerable<int> list1, IEnumerable<int> list2)
    {
        var distancesSum = 0;

        var sortedList1 = list1.Order().ToArray();
        var sortedList2 = list2.Order().ToArray();

        for (var i = 0; i < list1.Count(); i++)
        {
            distancesSum += Math.Abs(sortedList1[i] - sortedList2[i]);
        }

        return distancesSum;
    }

    public static int CalculateSimilarityScoreBasedOnFile(string inputFilePath)
    {
        ParseInput(inputFilePath, out var list1, out var list2);
        return CalculateSimilarityScore(list1, list2);
    }

    public static int CalculateSimilarityScore(IEnumerable<int> list1, IEnumerable<int> list2)
    {
        var similarityScore = 0;

        foreach (var locationId in list1)
        {
            similarityScore += list2.Where(l => l.Equals(locationId)).Sum();
        }

        return similarityScore;
    }
}

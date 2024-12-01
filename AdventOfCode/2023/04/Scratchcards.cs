namespace AdventOfCode._2023._04;

public static class Scratchcards
{
    private const string InputFilePath = @"2023\04\input.txt";

    public static int RunPartOne()
    {
        return SumScratchcardsPoints(InputFilePath);
    }

    public static int RunPartTwo()
    {
        return SumScratchcards(InputFilePath);
    }

    public static int SumScratchcardsPoints(string inputFilePath)
    {
        var scratchcards = File.ReadAllLines(inputFilePath);

        var sum = 0;
        foreach (var scratchcard in scratchcards)
        {
            sum += GetScratchcardPoints(scratchcard);
        }

        return sum;
    }

    public static int SumScratchcards(string inputFilePath)
    {
        var file = File.ReadAllLines(inputFilePath);

        var scratchcards = new List<Scratchcard>();
        foreach (var line in file)
        {
            var scratchcard = ParseScratchcard(line);
            GetScratchcardMatches(scratchcard);

            scratchcards.Add(scratchcard);
        }

        var scratchcardCounts = Enumerable.Repeat(1, scratchcards.Count).ToList();
        for (var i = 0; i < scratchcards.Count; i++)
        {
            if (scratchcards[i].Matches == 0)
            {
                continue;
            }

            var currentCardCount = scratchcardCounts[i];
            for (var j = i + 1; j <= scratchcards[i].Matches + i; j++)
            {
                scratchcardCounts[j] += currentCardCount;
            }
        }

        return scratchcardCounts.Sum();
    }

    public static int GetScratchcardPoints(string rawScratchcard)
    {
        var scratchcard = ParseScratchcard(rawScratchcard);

        GetScratchcardMatches(scratchcard);

        return scratchcard.Matches == 0 ? 0 : Convert.ToInt32(Math.Pow(2, scratchcard.Matches - 1));
    }

    private static void GetScratchcardMatches(Scratchcard scratchcard)
    {
        var matches = scratchcard.NumbersYouHave.Count(number => scratchcard.WinningNumbers.Contains(number));
        scratchcard.Matches = matches;
    }

    private static Scratchcard ParseScratchcard(string rawScratchcard)
    {
        var firstSplit = rawScratchcard.Split(": ");

        var scratchcard = new Scratchcard()
        {
            Id = int.Parse(firstSplit[0].Split(' ')[^1])
        };

        var numbers = firstSplit[1].Split(" | ");
        var winningNumbers = numbers[0].Split(' ');
        var numbersYouHave = numbers[1].Split(' ');

        foreach (var number in winningNumbers)
        {
            if (string.IsNullOrEmpty(number))
            {
                continue;
            }
            scratchcard.WinningNumbers.Add(int.Parse(number));
        }

        foreach (var number in numbersYouHave)
        {
            if (string.IsNullOrEmpty(number))
            {
                continue;
            }
            scratchcard.NumbersYouHave.Add(int.Parse(number));
        }

        return scratchcard;
    }
}

public class Scratchcard
{
    public int Id { get; set; }
    public int Matches { get; set; }
    public List<int> WinningNumbers { get; set; } = new();
    public List<int> NumbersYouHave { get; set; } = new(); // xd
}

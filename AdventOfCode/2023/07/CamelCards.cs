namespace AdventOfCode._07;

public static class CamelCards
{
    private const string InputFilePath = @"07\input.txt";

    public static int RunPartOne()
    {
        return GetTotalWinnings(InputFilePath);
    }

    public static int RunPartTwo()
    {
        return GetTotalWinnings(InputFilePath, true);
    }

    public static int GetTotalWinnings(string inputFileName, bool useJoker = false)
    {
        var file = File.ReadAllLines(inputFileName);

        var hands = ParseHands(file, useJoker);

        hands.Sort();

        var winnings = 0;
        for (var rank = 1; rank <= hands.Count; rank++)
        {
            winnings += hands[rank - 1].Bid * rank;
        }

        return winnings;
    }

    private static List<Hand> ParseHands(string[] file, bool useJoker = false)
    {
        var hands = new List<Hand>();
        foreach (var line in file)
        {
            var split = line.Split(' ');
            
            var hand = new Hand(split[0], useJoker)
            {
                Bid = int.Parse(split[1])
            };
            hands.Add(hand);
        }

        return hands;
    }
}

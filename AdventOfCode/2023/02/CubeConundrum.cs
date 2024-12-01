namespace AdventOfCode._02;

public static class CubeConundrum
{
    private const string InputFilePath = @"02\input.txt";

    public static int RunPartOne()
    {
        var redCount = 12;
        var greenCount = 13;
        var blueCount = 14;

        return SumPossibleGameIds(InputFilePath, redCount, greenCount, blueCount);
    }

    public static int RunPartTwo()
    {
        return GetMinimalSetPowerSum(InputFilePath);
    }

    #region PartOne

    public static int SumPossibleGameIds(string inputFilePath, int redCubeCount, int greenCubeCount, int blueCubeCount)
    {
        var games = File.ReadAllLines(inputFilePath);

        var possibleGameIdsSum = 0;
        foreach (var game in games)
        {
            if (IsGamePossible(game, redCubeCount, greenCubeCount, blueCubeCount, out int gameId))
            {
                possibleGameIdsSum += gameId;
            }
        }

        return possibleGameIdsSum;
    }

    public static bool IsGamePossible(string gameDescription, int redCubeCount, int greenCubeCount, int blueCubeCount, out int gameId)
    {
        var game = ParseGameDescription(gameDescription);
        gameId = game.Id;

        foreach (var set in game.Sets)
        {
            if (set.RedCount > redCubeCount || 
                set.GreenCount > greenCubeCount || 
                set.BlueCount > blueCubeCount)
                return false;
        }

        return true;
    }

    #endregion

    #region PartTwo

    public static int GetMinimalSetPowerSum(string inputFilePath)
    {
        var games = File.ReadAllLines(inputFilePath);

        var powerSum = 0;
        foreach (var game in games)
        {
            powerSum += GetMinimalSetPower(game);
        }

        return powerSum;
    }

    public static int GetMinimalSetPower(string gameDescription)
    {
        var game = ParseGameDescription(gameDescription);

        var minimalSet = GetMinimalSet(game);

        var setPower = minimalSet.RedCount * minimalSet.GreenCount * minimalSet.BlueCount;
        return setPower;
    }

    private static Set GetMinimalSet(Game game)
    {
        var minimalSet = new Set();
        foreach (var set in game.Sets)
        {
            if (set.RedCount > minimalSet.RedCount)
            {
                minimalSet.RedCount = set.RedCount;
            }

            if (set.GreenCount > minimalSet.GreenCount)
            {
                minimalSet.GreenCount = set.GreenCount;
            }

            if (set.BlueCount > minimalSet.BlueCount)
            {
                minimalSet.BlueCount = set.BlueCount;
            }
        }

        return minimalSet;
    }

    #endregion

    // Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
    private static Game ParseGameDescription(string gameDescription)
    {
        var firstSplit = gameDescription.Split(": ");

        var game = new Game()
        {
            Id = Convert.ToInt32(firstSplit[0].Split(' ')[1])
        };

        var setsDescription = firstSplit[1].Split("; ");
        foreach (var setDescription in setsDescription)
        {
            var set = new Set();

            var cubesDescription = setDescription.Split(", ");
            foreach (var cubeDescription in cubesDescription)
            {
                var cubeDetails = cubeDescription.Split(' ');
                var cubeCount = Convert.ToInt32(cubeDetails[0]);
                switch (cubeDetails[1])
                {
                    case "red":
                        set.RedCount = cubeCount;
                        continue;
                    case "green":
                        set.GreenCount = cubeCount;
                        continue;
                    case "blue":
                        set.BlueCount = cubeCount;
                        continue;
                }
            }
            game.Sets.Add(set);
        }

        return game;
    }
}

public class Game 
{
    public int Id { get; set; }
    public List<Set> Sets { get; set; } = new();
}

public class Set
{
    public int RedCount { get; set; }
    public int GreenCount { get; set; }
    public int BlueCount { get; set; }
}

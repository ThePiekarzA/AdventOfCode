using System.Text.RegularExpressions;

namespace AdventOfCode._08;

public static class HauntedWasteland
{
    private static readonly Regex NodeRegex = new(@"\w{3}");

    private const string InputFilePath = @"08\input.txt";

    public static int RunPartOne()
    {
        return GoToZZZ(InputFilePath);
    }

    public static ulong RunPartTwo()
    {
        return GoToXXZ(InputFilePath);
    }

    public static ulong GoToXXZ(string inputFilePath)
    {
        var lines = File.ReadAllLines(inputFilePath);

        ParseInstructions(lines, out var instructions, out var nodesNetwork);

        var currentNodesKeys = nodesNetwork.Keys.Where(k => k.EndsWith('A')).ToArray();
        ulong stepsCount = 0;
        while (!currentNodesKeys.All(n => n.EndsWith('Z')))
        {
            var currentMove = instructions[(int)(stepsCount % (ulong)instructions.Length)];
            currentNodesKeys = currentNodesKeys.Select(cn => currentMove == 'L' ? nodesNetwork[cn].Item1 : nodesNetwork[cn].Item2).ToArray();

            stepsCount++;
            if (stepsCount % 100000 == 0)
            {
                Console.Write($"\rMade {stepsCount} steps.");
            }
        }

        return stepsCount;
    }

    public static int GoToZZZ(string inputFilePath)
    {
        var lines = File.ReadAllLines(inputFilePath);

        ParseInstructions(lines, out var instructions, out var nodesNetwork);

        var stepsCount = 0;
        var currentNodeKey = "AAA";
        while (currentNodeKey != "ZZZ")
        {
            var currentNode = nodesNetwork[currentNodeKey];
            var currentMove = instructions[stepsCount % instructions.Length];
            currentNodeKey = currentMove == 'L' ? currentNode.Item1 : currentNode.Item2;

            stepsCount++;
            if (stepsCount % 100000 == 0)
            {
                Console.Write($"\rMade {stepsCount} steps.");
            }
        }

        return stepsCount;
    }

    private static void ParseInstructions(string[] lines, out string instructions, out Dictionary<string, Tuple<string, string>> nodesNetwork)
    {
        instructions = lines[0];
        nodesNetwork = new Dictionary<string, Tuple<string, string>>();
        for (var i = 2; i < lines.Length; i++)
        {
            var node = NodeRegex.Matches(lines[i]).Select(m => m.Value).ToArray();
            nodesNetwork[node[0]] = new Tuple<string, string>(node[1], node[2]);
        }
    }
}

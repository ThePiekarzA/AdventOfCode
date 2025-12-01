namespace AdventOfCode.Common;

public class Combinations
{
    public static IEnumerable<int[]> GetCombinations(int elements)
    {
        var combination = new int[elements];
        for (var i = 0; i < elements; i++)
        {
            combination[0] = i;
            for (var j = i + 1; j < elements; j++)
            {
                combination[1] = j;
                yield return combination;
            }
        }
    }

    public static IEnumerable<int[]> GetPermutationsWithRepetitions(int places, int elements)
    {
        var permutationsCount = Math.Pow(elements, places);
        var permutation = new int[places];
        yield return permutation;
        for (int i = 1; i < permutationsCount; i++)
        {
            NextPermutation(places, elements, permutation, places - 1);
            yield return permutation;
        }
    }

    private static void NextPermutation(int places, int elements, int[] permutation, int index)
    {
        if (index < 0)
            return;

        permutation[index]++;
        if (permutation[index] == elements)
        {
            permutation[index] = 0;
            NextPermutation(places, elements, permutation, index - 1);
        }
    }
}

namespace AdventOfCode.Common;

public class Combinations
{
    public static IEnumerable<int[]> PermutationsWithRepetitions(int places, int elements)
    {
        var permutationsCount = Math.Pow(elements, places);
        var permutation = new int[places];
        yield return permutation;
        for (int i = 1; i < permutationsCount; i++)
        {
            Increment(places, elements, permutation, places - 1);
            yield return permutation;
        }
    }

    private static void Increment(int places, int elements, int[] permutation, int index)
    {
        if (index < 0)
            return;

        permutation[index]++;
        if (permutation[index] == elements)
        {
            permutation[index] = 0;
            Increment(places, elements, permutation, index - 1);
        }
    }
}

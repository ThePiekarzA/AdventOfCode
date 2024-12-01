namespace AdventOfCode._2023._11;

public static class CosmicExpansion
{
    private const string InputFilePath = @"2023\11\input.txt";

    public static ulong RunPartOne()
    {
        return SumShortestDistancesBetweenGalaxiesFast(InputFilePath, 2);
    }

    public static ulong RunPartTwo()
    {
        return SumShortestDistancesBetweenGalaxiesFast(InputFilePath, 1_000_000);
    }

    public static ulong SumShortestDistancesBetweenGalaxiesFast(string inputFilePath, int expansionFactor)
    {
        var space = File.ReadAllLines(inputFilePath).ToList();

        FindEmptyLines(space, out var rowsWithoutGalaxies, out var columnsWithoutGalaxies);
        var galaxies = ExtractGalaxies(space);

        ulong distanceSum = 0;
        for (var i = 0; i < galaxies.Count; i++)
        {
            for (var j = i + 1; j < galaxies.Count; j++)
            {
                var rowOffset = rowsWithoutGalaxies.Count(r => galaxies[i].Row < r && r < galaxies[j].Row);
                var columnOffset = galaxies[i].Column < galaxies[j].Column ?
                    columnsWithoutGalaxies.Count(c => galaxies[i].Column < c && c < galaxies[j].Column) :
                    columnsWithoutGalaxies.Count(c => galaxies[j].Column < c && c < galaxies[i].Column);
                var totalOffset = (rowOffset + columnOffset) * (expansionFactor - 1);

                int distance = Math.Abs(galaxies[i].Row - galaxies[j].Row) + Math.Abs(galaxies[i].Column - galaxies[j].Column) + totalOffset;
                distanceSum += (ulong)distance;;
            }
        }
        
        return distanceSum;
    }

    public static int SumShortestDistancesBetweenGalaxies(string inputFilePath)
    {
        var space = File.ReadAllLines(inputFilePath).ToList();
        var expandedSpace = ExpandEmptyLines(space);

        var galaxies = ExtractGalaxies(expandedSpace);
        
        var distances = new Dictionary<KeyValuePair<int, int>, int>();

        for (var i = 0; i < galaxies.Count; i++)
        {
            for (var j = i + 1; j < galaxies.Count; j++)
            {
                var distance = Math.Abs(galaxies[i].Row - galaxies[j].Row) + Math.Abs(galaxies[i].Column - galaxies[j].Column);
                distances[new KeyValuePair<int, int> (galaxies[i].Id, galaxies[j].Id)] = distance;;
            }
        }
        
        return distances.Values.Sum();
    }

    private static List<Galaxy> ExtractGalaxies(List<string> expandedSpace)
    {
        var galaxies = new List<Galaxy>();
        for (var rowIndex = 0; rowIndex < expandedSpace.Count; rowIndex++)
        {
            var row = expandedSpace[rowIndex];
            if (!row.Contains('#'))
            {
                continue;
            }

            for (var columnIndex = 0; columnIndex < row.Length; columnIndex++)
            {
                if (row[columnIndex] != '#')
                {
                    continue;
                }

                var galaxy = new Galaxy()
                {
                    Id = galaxies.Count,
                    Row = rowIndex,
                    Column = columnIndex,
                };
                galaxies.Add(galaxy);
            }
        }

        return galaxies;
    }

    private static List<string> ExpandEmptyLines(List<string> space)
    {
        var columnsWithoutGalaxies = Enumerable.Range(0, space[0].Length).ToList();
        var rowsWithoutGalaxies = new List<int>();

        for (var rowIndex = 0; rowIndex < space.Count; rowIndex++)
        {
            var row = space[rowIndex];
            if (!row.Contains('#'))
            {
                rowsWithoutGalaxies.Add(rowIndex);
                continue;
            }

            for (var columnIndex = 0; columnIndex < row.Length; columnIndex++)
            {
                if (row[columnIndex] == '#' && columnsWithoutGalaxies.Contains(columnIndex))
                {
                    columnsWithoutGalaxies.Remove(columnIndex);
                }
            }
        }

        var addedColumns = 0;
        foreach (var column in columnsWithoutGalaxies)
        {
            var insertionIndex = column + addedColumns;
            for (var rowIndex = 0; rowIndex < space.Count; rowIndex++)
            {
                var currentRow = space[rowIndex];
                space[rowIndex] = $"{currentRow[..insertionIndex]}.{currentRow[insertionIndex..]}";
            }

            addedColumns++;
        }

        var rowToAdd = string.Join("", Enumerable.Repeat('.', space[0].Length));
        var addedRows = 0;
        foreach (var row in rowsWithoutGalaxies)
        {
            var insertionIndex = row + addedRows;
            space.Insert(insertionIndex, rowToAdd);
            addedRows++;
        }

        return space;
    }

    private static void FindEmptyLines(List<string> space, out List<int> rowsWithoutGalaxies, out List<int> columnsWithoutGalaxies)
    {
        rowsWithoutGalaxies = new List<int>();
        columnsWithoutGalaxies = Enumerable.Range(0, space[0].Length).ToList();

        for (var rowIndex = 0; rowIndex < space.Count; rowIndex++)
        {
            var row = space[rowIndex];
            if (!row.Contains('#'))
            {
                rowsWithoutGalaxies.Add(rowIndex);
                continue;
            }

            for (var columnIndex = 0; columnIndex < row.Length; columnIndex++)
            {
                if (row[columnIndex] == '#' && columnsWithoutGalaxies.Contains(columnIndex))
                {
                    columnsWithoutGalaxies.Remove(columnIndex);
                }
            }
        }
    }
}

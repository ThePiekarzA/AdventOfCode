namespace AdventOfCode._05;

public class Mapping
{
    public ulong DestinationRangeStart { get; }
    public ulong DestinationRangeEnd => DestinationRangeStart + RangeLength;
    public ulong SourceRangeStart { get; }
    public ulong SourceRangeEnd => SourceRangeStart + RangeLength;
    public ulong RangeLength { get; }

    public Mapping(ulong destinationRangeStart, ulong sourceRangeStart, ulong rangeLength)
    {
        DestinationRangeStart = destinationRangeStart;
        SourceRangeStart = sourceRangeStart;
        RangeLength = rangeLength;
    }

    public ulong MapValue(ulong valueToMap)
    {
        if (valueToMap >= SourceRangeStart && valueToMap <= SourceRangeEnd)
        {
            var valueOffset = valueToMap - SourceRangeStart;
            return DestinationRangeStart + valueOffset;
        }

        return valueToMap;
    }
}
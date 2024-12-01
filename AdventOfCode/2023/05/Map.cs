namespace AdventOfCode._2023._05;

public class Map
{
    public List<Mapping> Mappings { get; set; } = new();

    public ulong MapValue(ulong valueToMap)
    {
        var mappedValue = valueToMap;
        foreach (var mapping in Mappings)
        {
            mappedValue = mapping.MapValue(mappedValue);
            if (mappedValue != valueToMap)
            {
                return mappedValue;
            }
        }

        return valueToMap;
    }
}
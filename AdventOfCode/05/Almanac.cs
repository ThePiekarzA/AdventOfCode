namespace AdventOfCode._05;

public class Almanac
{
    public List<Seed> Seeds { get; set; } = new();
    public Map[] Maps { get; set; } = new Map[7];

    public Almanac()
    {
        for (int i = 0; i < Maps.Length; i++)
        {
            Maps[i] = new Map();
        }
    }

    public ulong MapSeed(ulong seed)
    {
        foreach (var map in Maps)
        {
            seed = map.MapValue(seed);
        }

        return seed;
    }
}
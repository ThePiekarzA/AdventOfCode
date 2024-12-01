namespace AdventOfCode._2023._05;

public class Seed
{
    public ulong Id { get; set; }
    public ulong Location { get; set; }
    public ulong RangeStart { get; set; }
    public ulong RangeEnd => RangeStart + RangeLength;
    public ulong RangeLength { get; set; }
}
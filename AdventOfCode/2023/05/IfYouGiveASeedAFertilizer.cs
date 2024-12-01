namespace AdventOfCode._2023._05;

public static class IfYouGiveASeedAFertilizer
{
    private const string InputFilePath = @"2023\05\input.txt";
    public static ulong RunPartOne()
    {
        return FindNearestLocationForSeed(InputFilePath);
    }

    public static ulong RunPartTwo()
    {
        return FindNearestLocationForSeedRange(InputFilePath); // TODO: Merge map into one to speed up process
    }

    public static ulong FindNearestLocationForSeed(string inputFilePath)
    {
        var file = File.ReadAllLines(inputFilePath);

        var almanac = ParseAlmanac(file);

        foreach (var seed in almanac.Seeds)
        {
            seed.Location = almanac.MapSeed(seed.Id);
        }

        return almanac.Seeds.Min(s => s.Location);
    }

    public static ulong FindNearestLocationForSeedRange(string inputFilePath)
    {
        var file = File.ReadAllLines(inputFilePath);

        var almanac = ParseAlmanac(file, true);

        var nearestLocation = ulong.MaxValue;
        var currentSeed = 1;
        foreach (var seedRange in almanac.Seeds)
        {
            Console.WriteLine($"\nMapping {currentSeed} seed range. {seedRange.RangeLength} seeds to map.");

            for (ulong seedId = 0; seedId < seedRange.RangeLength; seedId++)
            {
                if (seedId % 100000 == 0)
                {
                    var completion = ((double)seedId / (double)seedRange.RangeLength) * 100.0d;
                    Console.Write($"\rMapped {completion, 3:F2}% of seeds in current range");
                }

                var location = almanac.MapSeed(seedId + seedRange.RangeStart);
                if (location < nearestLocation)
                {
                    nearestLocation = location;
                }
            }

            currentSeed++;
        }

        return nearestLocation;
    }

    private static Almanac ParseAlmanac(string[] file, bool useSeedRange = false)
    {
        var almanac = new Almanac();

        var currentlyParsing = string.Empty;
        foreach (var line in file)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            if (line.Contains(':'))
            {
                currentlyParsing = ParseSectionHeader(line, almanac, useSeedRange);
                continue;
            }

            if (string.IsNullOrEmpty(currentlyParsing))
            {
                continue;
            }

            ParseMapping(line, currentlyParsing, almanac);
        }

        return almanac;
    }

    private static void ParseMapping(string line, string currentlyParsing, Almanac almanac)
    {
        var rawMapping = line.Split(' ').Where(v => !string.IsNullOrEmpty(v)).Select(ulong.Parse).ToArray();
        var mapping = new Mapping(rawMapping[0], rawMapping[1], rawMapping[2]);
        switch (currentlyParsing)
        {
            case Constants.SeedToSoilMap:
                almanac.Maps[0].Mappings.Add(mapping);
                break;
            case Constants.SoilToFertilizerMap:
                almanac.Maps[1].Mappings.Add(mapping);
                break;
            case Constants.FertilizerToWaterMap:
                almanac.Maps[2].Mappings.Add(mapping);
                break;
            case Constants.WaterToLightMap:
                almanac.Maps[3].Mappings.Add(mapping);
                break;
            case Constants.LightToTemperatureMap:
                almanac.Maps[4].Mappings.Add(mapping);
                break;
            case Constants.TemperatureToHumidityMap:
                almanac.Maps[5].Mappings.Add(mapping);
                break;
            case Constants.HumidityToLocationMap:
                almanac.Maps[6].Mappings.Add(mapping);
                break;
        }
    }

    private static string ParseSectionHeader(string line, Almanac almanac, bool useSeedRange = false)
    {
        var headerSplit = line.Split(":");
        switch (headerSplit[0])
        {
            case Constants.Seeds:
                var seedsRaw = headerSplit[1].Split(' ');
                ParseSeed(almanac, seedsRaw, useSeedRange);
                return string.Empty;
            case Constants.SeedToSoilMap:
                return Constants.SeedToSoilMap;
            case Constants.SoilToFertilizerMap:
                return Constants.SoilToFertilizerMap;
            case Constants.FertilizerToWaterMap:
                return Constants.FertilizerToWaterMap;
            case Constants.WaterToLightMap:
                return Constants.WaterToLightMap;
            case Constants.LightToTemperatureMap:
                return Constants.LightToTemperatureMap;
            case Constants.TemperatureToHumidityMap:
                return Constants.TemperatureToHumidityMap;
            case Constants.HumidityToLocationMap:
                return Constants.HumidityToLocationMap;
        }

        return string.Empty;
    }

    private static void ParseSeed(Almanac almanac, string[] seedsRaw, bool useSeedRange = false)
    {
        if (useSeedRange)
        {
            var notEmptySeeds = seedsRaw.Where(s => !string.IsNullOrEmpty(s)).Select(ulong.Parse).ToArray();
            for (var i = 0; i < notEmptySeeds.Length; i += 2)
            {
                var seed = new Seed()
                {
                    RangeStart = notEmptySeeds[i],
                    RangeLength = notEmptySeeds[i + 1]
                };
                almanac.Seeds.Add(seed);
            }
        }
        else
        {
            almanac.Seeds.AddRange(
                seedsRaw.Where(s => !string.IsNullOrEmpty(s)).Select(s => new Seed() { Id = ulong.Parse(s) }));   
        }
    }
}
namespace AdventOfCode._2024._09;

public class DiskFragmenter
{
    // ﻿Input was removed due to the copyright.
    // Create input.txt file and fill it with data from: https://adventofcode.com/2024/day/9/input
    private const string InputFilePath = @"2024\09\input.txt";

    #region Part One

    public static ulong RunPartOne()
    {
        return FragmentDisk(InputFilePath);
    }

    public static ulong FragmentDisk(string inputFilePath)
    {
        var input = File.ReadAllText(inputFilePath);
        var compactedDisk = CompactDisk(input);
        return CalculateChecksum(compactedDisk);
    }

    public static ulong CalculateChecksum(List<int> compactedDisk)
    {
        ulong checksum = 0;
        for (var i = 0; i < compactedDisk.Count; i++)
        {
            checksum += (ulong)(i * compactedDisk[i]);
        }

        return checksum;
    }

    public static List<int> CompactDisk(string map)
    {
        var disk = map.Select(e => int.Parse(e.ToString())).ToArray();

        var compactedDisk = new List<int>();

        var freeSpace = FillFromBeginning(disk, compactedDisk);
        FillFromEnd(disk, compactedDisk, freeSpace);

        return compactedDisk;
    }

    private static void FillFromEnd(int[] disk, List<int> compactedDisk, int freeSpace)
    {
        var fileBlock = true;
        var fileIndex = disk.Length / 2;
        var stopWriting = false;
        foreach (var element in disk.Reverse())
        {
            if (!fileBlock)
            {
                fileBlock = !fileBlock;
                continue;
            }

            for (var i = 0; i < element; i++)
            {
                compactedDisk[GetFirstFreeSpace(compactedDisk)] = fileIndex;
                freeSpace--;

                if (freeSpace > 0) continue;

                stopWriting = true;
                break;
            }

            if (stopWriting)
                break;

            if (fileBlock)
                fileIndex--;

            fileBlock = !fileBlock;
        }
    }

    private static int FillFromBeginning(int[] disk, List<int> compactedDisk)
    {
        var dataLeftToWrite = 0;
        for (var i = 0; i < disk.Length; i += 2)
        {
            dataLeftToWrite += disk[i];
        }

        var freeSpace = 0;
        var fileIndex = 0;
        var fileBlock = true;
        var stopWriting = false;
        foreach (var element in disk)
        {
            for (var i = 0; i < element; i++)
            {
                if (fileBlock)
                {
                    compactedDisk.Add(fileIndex);
                    dataLeftToWrite--;
                }
                else
                {
                    compactedDisk.Add(int.MaxValue);
                    freeSpace++;
                }

                if (freeSpace < dataLeftToWrite) continue;

                stopWriting = true;
                break;
            }

            if (stopWriting)
                break;

            if (fileBlock)
                fileIndex++;

            fileBlock = !fileBlock;
        }

        return freeSpace;
    }

    private static int GetFirstFreeSpace(List<int> fragmentedDisk)
    {
        return fragmentedDisk.IndexOf(int.MaxValue);
    }

    #endregion

    #region Part Two

    public static ulong RunPartTwo()
    {
        return DefragmentDisk(InputFilePath);
    }

    public static ulong DefragmentDisk(string inputFilePath)
    {
        var input = File.ReadAllText(inputFilePath);
        var defragmentedDisk = DefragmentDiskInternal(input);
        return CalculateChecksum(defragmentedDisk);
    }

    public static List<int> DefragmentDiskInternal(string map)
    {
        var disk = map.Select(e => int.Parse(e.ToString())).ToArray();

        var compactedDisk = FillDisk(disk, out var fileStartIndexes);
        
        var fileIndex = disk.Length / 2 + 1;
        var fileBlock = true;
        var currentIndex = disk.Sum();
        foreach (var element in disk.Reverse())
        {
            currentIndex -= element;

            if (!fileBlock)
            {
                fileBlock = !fileBlock;
                continue;
            }

            fileIndex--;

            var firstFittingFreeSpaceIndex = FindFirstFittingFreeSpace(compactedDisk, element);
            if (firstFittingFreeSpaceIndex == -1 || firstFittingFreeSpaceIndex >= currentIndex)
            {
                fileBlock = !fileBlock;
                continue;
            }

            // File can be moved
            RemoveFile(currentIndex, element, compactedDisk);
            WriteFile(firstFittingFreeSpaceIndex, element, compactedDisk, fileIndex);

            fileBlock = !fileBlock;
            
        }

        return compactedDisk.Select(v => v == -1 ? 0 : v).ToList();
    }

    private static void WriteFile(int firstFittingFreeSpaceIndex, int element, List<int> compactedDisk, int fileIndex)
    {
        for (var i = firstFittingFreeSpaceIndex; i < firstFittingFreeSpaceIndex + element; i++)
        {
            compactedDisk[i] = fileIndex;
        }
    }

    private static void RemoveFile(int currentIndex, int element, List<int> compactedDisk)
    {
        for (var i = currentIndex; i < currentIndex + element; i++)
        {
            compactedDisk[i] = -1;
        }
    }
    
    private static int FindFirstFittingFreeSpace(List<int> compactedDisk, int element)
    {
        for (var i = 0; i < compactedDisk.Count; i++)
        {
            if (compactedDisk[i] == -1)
            {
                var freeSpaceSize = 1;
                int j;
                for (j = i + 1; j < compactedDisk.Count; j++)
                {
                    if (compactedDisk[j] == -1) 
                        freeSpaceSize++;
                    else
                        break;
                }

                if (freeSpaceSize >= element)
                    return i;

                i = j;
            }
        }

        //var firstFittingFreeSpaceIndex = 0;
        //for (var i = 0; i < disk.Length; i++)
        //{
        //    if (i % 2 == 0 || disk[i] < element) // File
        //    {
        //        firstFittingFreeSpaceIndex += disk[i];
        //    }
        //    else
        //        return firstFittingFreeSpaceIndex;
        //}

        return -1;
    }

    private static List<int> FillDisk(int[] disk, out List<int> fileStartIndexes)
    {
        var compactedDisk = new List<int>();
        fileStartIndexes = [];

        var fileIndex = 0;
        var fileBlock = true;
        foreach (var element in disk)
        {
            if(fileBlock)
                fileStartIndexes.Add(compactedDisk.Count);

            for (var i = 0; i < element; i++)
            {
                compactedDisk.Add(fileBlock ? fileIndex : -1);
            }

            if (fileBlock)
                fileIndex++;

            fileBlock = !fileBlock;
        }

        return compactedDisk;
    }

    #endregion
}

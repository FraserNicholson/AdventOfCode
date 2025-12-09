namespace AOC2025.Day5;

public static class Day5
{
    public static int GetAnswerPart1()
    {
        var lines = File.ReadAllLines("./input.txt");

        var ranges = new List<(long, long)>();
        var breakpoint = 0;
        
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            if (line.Length == 0)
            {
                breakpoint = i;
                break;
            }

            var parts = line.Split('-');

            ranges.Add((long.Parse(parts[0]), long.Parse(parts[1])));
        }

        var freshCount = 0;
        
        for (var i = breakpoint + 1; i < lines.Length; i++)
        {
            var line = lines[i];
            var num = long.Parse(line);

            if (ranges.Any(r => r.Item1 <= num && r.Item2 >= num))
            {
                freshCount++;
            }
        }
        
        return freshCount;
    }
    
    public static long GetAnswerPart2()
    {
        var lines = File.ReadAllLines("./input.txt");

        var ranges = new List<(long, long)>();
        
        foreach (var line in lines)
        {
            if (line.Length == 0)
            {
                break;
            }

            var parts = line.Split('-');
            
            ranges.Add((long.Parse(parts[0]), long.Parse(parts[1])));
        }

        var combinedRanges = CombineRanges(ranges);

        long freshCount = 0;

        foreach (var range in combinedRanges)
        {
            var freshNumberFromRange = range.Item2 - range.Item1 + 1;
            freshCount += freshNumberFromRange;
        }
        
        return freshCount;
    }
    
    private static List<(long, long)> CombineRanges(List<(long, long)> ranges)
    {
        while (true)
        {
            // get first range with overlapping ranges
            var firstOverLapping = GetFirstRangeWithOverlappingRanges(ranges);

            // if none - break
            if (firstOverLapping == null) break;
            
            // if some, combine into new range, add this one onto the existing list
            var from = firstOverLapping.OverlappingRanges.Min(x => x.Range.Item1);
            var to = firstOverLapping.OverlappingRanges.Max(x => x.Range.Item2);

            from = long.Min(from, firstOverLapping.Range.Item1);
            to = long.Max(to, firstOverLapping.Range.Item2);
            
            ranges.Add((from, to));

            // remove old values from list
            ranges.Remove(firstOverLapping.Range);

            foreach (var range in firstOverLapping.OverlappingRanges)
            {
                ranges.Remove(range.Range);
            }

        }
        
        return ranges;
    }

    private static OverlappingRange? GetFirstRangeWithOverlappingRanges(List<(long, long)> ranges)
    {
        for (var i = 0; i < ranges.Count; i++)
        {
            var range = ranges[i];
            
            var overlappingRanges = GetOverlappingRanges(range, i, ranges).ToArray();
            
            if (overlappingRanges.Length != 0)
                return new OverlappingRange(i, range, overlappingRanges);
        }

        return null;
    }
    
    private static IEnumerable<RangeWithIndex> GetOverlappingRanges((long, long) range, int rangeIndex, List<(long, long)> ranges)
    {
        return ranges
            .Select((r, i) => new RangeWithIndex(i, r))
            .Where(x => x.Index != rangeIndex &&
                        ((x.Range.Item1 >= range.Item1 && x.Range.Item1 <= range.Item2) ||
                         (x.Range.Item2 >= range.Item1 && x.Range.Item2 <= range.Item2)));
    }
    
    private record OverlappingRange(int Index, (long, long) Range, IEnumerable<RangeWithIndex> OverlappingRanges);
    private record RangeWithIndex(int Index, (long, long) Range);
}
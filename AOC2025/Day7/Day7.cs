namespace AOC2025.Day7;

public static class Day7
{
    public static int GetAnswerPart1()
    {
        var lines = File.ReadAllLines("./input.txt");

        List<int> beamPositions = [];
        
        var firstPosition = lines.First().Select((c, i) => (c, i)).Single(x => x.c == 'S');
        
        beamPositions.Add(firstPosition.i);

        var splitCount = 0;
        for (var y = 1; y < lines.Length; y++)
        {
            var splitters = lines[y].Select((c, x) => (c, x))
                .Where(x => x.c == '^')
                .Where(x => beamPositions.Any(pos => x.x == pos))
                .ToArray();

            if (splitters.Length != 0)
            {
                foreach (var splitter in splitters)
                {
                    splitCount++;
                    beamPositions.Remove(splitter.x);
                    beamPositions.Add(splitter.x + 1);
                    beamPositions.Add(splitter.x - 1);
                }
            }
            
            beamPositions = beamPositions.Distinct().ToList();
        }
        
        return splitCount;
    }
    
    // Needed ChatGPT to help with part 2 - tbh ChatGPT did the real heavy lifting here 
    public static long GetAnswerPart2()
    {
        var lines = File.ReadAllLines("./input.txt");
        
        var firstPositionX = lines.First().Select((c, i) => (c, i)).Single(x => x.c == 'S').i;
        
        return CountTimelines(lines, firstPositionX, 0);
    }

    private static long CountTimelines(string[] lines, int startX, int startY)
    {
        var width = lines[0].Length;
        var height = lines.Length;
        var ways = new long[height, width];

        ways[startY, startX] = 1;

        long totalTimelines = 0;

        for (var y = startY; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                Console.WriteLine("X: {0}, Y: {1}", x, y);
                
                var count = ways[y, x];
                
                if (count == 0)
                    continue;

                if (y + 1 == height)
                {
                    Console.WriteLine($"Reached Top Total timelines added: {totalTimelines}");
                    totalTimelines += count;
                    continue;
                }

                var nextY = y + 1;
                var nextChar = lines[nextY][x];
                
                if (nextChar == '.')
                {
                    ways[nextY, x] += count;
                } else if (nextChar == '^')
                {
                    if (x - 1  >= 0)
                    {
                        ways[nextY, x - 1] += count;
                    }

                    if (x + 1 < width)
                    {
                        ways[nextY, x + 1] += count;
                    }
                }
                else
                {
                    throw new InvalidOperationException("Invalid character " + nextChar);
                }
            }
        }
        
        return totalTimelines;
    }
}
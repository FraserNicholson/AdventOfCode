namespace AOC2025.Day4;

public static class Day4
{
    public static int Part1Answer()
    {
        var lines = File.ReadAllLines("./input.txt").Select(l => l.ToCharArray()).ToArray();

        var count = 0;
        var maxX = lines.First().Length;
        
        for (var y = 0; y < lines.Length; y++)
        {
            for (var x = 0; x < maxX; x++)
            {
                if (lines[y][x] != '@')
                    continue;
                
                var neighbours = CollectNeighbours(lines, x, y);

                if (neighbours.Count(c => c == '@') < 4)
                {
                    count++;
                }
            }
        }
        
        return count;
    }
    
    public static int Part2Answer()
    {
        var lines = File.ReadAllLines("./input.txt").Select(l => l.ToCharArray()).ToArray();

        var count = 0;

        while (true)
        {
            (lines, var countRemoved) = RemoveRolls(lines);
            
            if (countRemoved > 0)
            {
                count += countRemoved;
            }
            else
            {
                break;
            }
        }

        return count;
    }

    private static (char[][], int) RemoveRolls(char[][] lines)
    {
        var newLines = lines;
        
        var count = 0;
        var maxX = lines.First().Length;
        
        for (var y = 0; y < lines.Length; y++)
        {
            for (var x = 0; x < maxX; x++)
            {
                if (lines[y][x] != '@')
                    continue;
                
                var neighbours = CollectNeighbours(lines, x, y);

                if (neighbours.Count(c => c == '@') < 4)
                {
                    count++;
                    newLines[y][x] = '.';
                }
            }
        }
        
        return (newLines, count);
    }

    private static char[] CollectNeighbours(char[][] lines, int x, int y)
    {
        var coords = new[]
        {
            new { x, y = y - 1 }, // N
            new { x = x + 1, y = y - 1 }, // NE
            new { x = x + 1, y }, // E
            new { x = x + 1, y = y + 1 }, // SE
            new { x, y = y + 1 }, // S
            new { x = x - 1, y = y + 1 }, // SW
            new { x = x - 1, y }, // W
            new { x = x - 1, y = y - 1 }, // NW
        };

        List<char> neighbours = [];

        foreach (var coord in coords)
        {
            if (TryGetFromPosition(lines, coord.x, coord.y, out var neighbour))
                neighbours.Add(neighbour!.Value);
        }
        
        return neighbours.ToArray();
    }

    private static bool TryGetFromPosition(char[][] lines, int x, int y, out char? c)
    {
        c = null;
        
        if (x < 0 || x > lines.First().Length - 1) return false;
        
        if (y < 0 || y > lines.Length - 1) return false;
        
        c = lines[y][x];
        return true;
    }
}
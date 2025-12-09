namespace AOC2025.Day2;

public static class Day2
{
    public static long Answer()
    {
        var lines = File.ReadAllLines("./input.txt").First().Split(',');

        long invalidCount = 0;
        
        foreach (var line in lines)
        {
            var parts = line.Split('-');
            var start = long.Parse(parts[0]);
            var end = long.Parse(parts[1]);

            for (var i = start; i <= end; i++)
            {
                if (IsInvalidPart2(i.ToString()))
                {
                    invalidCount+= i;
                }
            }
        }
        
        return invalidCount;
    }

    private static bool IsInvalid(string id)
    {
        if (id.Length % 2 != 0)
            return false;

        var midPoint = id.Length / 2;

        var first = id[..midPoint];
        var second = id[midPoint..];

        // Console.WriteLine(first);
        // Console.WriteLine(second);
        // Console.WriteLine("");
        
        return first == second;
    }

    private static bool IsInvalidPart2(string id)
    {
        var midPoint = id.Length / 2;

        for (var i = 1; i <= midPoint; i++)
        {
            var test = id[..i];
            var restOfId = id[i..];

            if (RestOfIdEqualsTest(test, restOfId))
            {
                Console.WriteLine("Invalid id: " + id);
                Console.WriteLine("Invalid id test: " + test);
                return true;
            }
        }
        
        return false;
    }

    private static bool RestOfIdEqualsTest(string test, string restOfId)
    {
        if (restOfId.Length % test.Length != 0)
            return false;
        
        var allParts = Split(restOfId, test.Length);
        
        return allParts.All(part => part == test);
    }
    
    private static string[] Split(string str, int chunkSize)
    {
        
        return Enumerable.Range(0, str.Length / chunkSize)
            .Select(i => str.Substring(i * chunkSize, chunkSize)).ToArray();
    }
}
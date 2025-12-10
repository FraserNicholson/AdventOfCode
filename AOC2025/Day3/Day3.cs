namespace AOC2025.Day3;

public static class Day3
{
    // Needed ChatGPT to help with part 2
    public static long Answer()
    {
        var lines = File.ReadAllLines("./input.txt");

        long count = 0;

        foreach (var line in lines)
        {
            var intPair = GetHighestNum(line, 12);
            Console.WriteLine(intPair);
            count += intPair;
        }
        
        return count;
    }

    private static int GetHighestIntPair(string line)
    {
        var currentMax = 0;
        
        for (var i = 0; i < line.Length; i++)
        {
            for (var j = 1; j < line.Length; j++)
            {
                if (i >= j) continue;
                
                var pair = int.Parse($"{line[i]}{line[j]}");
                
                if (pair > currentMax) currentMax = pair;
            }
        }
        
        return currentMax;
    }

    private static long GetHighestNum(string line, int digits)
    {
        var numberToDelete = line.Length - digits;

        var digitsStack = new Stack<int>();
        
        foreach (var c in line)
        {
            var num = int.Parse($"{c}");
            
            if (digitsStack.Count == 0)
            {
                digitsStack.Push(num);
                continue;
            }

            while (digitsStack.Count > 0
                   && numberToDelete > 0
                   && digitsStack.Peek() < num)
            {
                digitsStack.Pop();
                numberToDelete--;
            }
            
            digitsStack.Push(num);
        }

        while (digitsStack.Count > digits)
        {
            digitsStack.Pop();
        }

        var numberString = string.Join("", digitsStack.Reverse().ToArray());
        
        return long.Parse(numberString);
    }
}
namespace AOC2025.Day6;

public class Day6
{
    public static long GetAnswerPart1()
    {
        var lines  = File.ReadAllLines("./input.txt");

        List<List<long>> numbersLists = [];
        List<char> operators = [];
        
        foreach (var line in lines)
        {
            var parts = line.Split(null).Where(s => !string.IsNullOrEmpty(s)).ToArray();

            if (parts.First() == "*" || parts.First() == "+")
            {
                foreach (var c in parts)
                {
                    operators.Add(c[0]);
                }
                break;
            }

            for (var i = 0; i < parts.Length; i++)
            {
                if (numbersLists.Count < i + 1)
                {
                    numbersLists.Add([long.Parse(parts[i])]);
                }
                else
                {
                    numbersLists[i].Add(long.Parse(parts[i]));
                }
            }
        }

        long totalCount = 0;

        for (var i = 0; i < numbersLists.Count; i++)
        {
            var numbers = numbersLists[i];
            var c = operators[i];

            var calculation = Calculate(numbers, c);

            totalCount += calculation;
        }
        
        return totalCount;
    }
    
    public static long GetAnswerPart2()
    {
        var lines  = File.ReadAllLines("./input.txt");

        List<List<long>> numbersLists = [];
        List<char> operators = [];

        var setNumber = -1;
        
        for (var i = 0; i < lines.First().Length; i++)
        {
            var chars = CollectCharsFromColumn(lines, i).ToArray();
            
            if (chars.Length == 0)
                continue;

            if (chars.Last() == '*' || chars.Last() == '+')
            {
                setNumber++;
                
                operators.Add(chars.Last());
                
                chars = chars.Take(chars.Length - 1).ToArray();
            }

            var number = long.Parse(chars);

            if (numbersLists.Count < setNumber + 1)
            {
                numbersLists.Add([number]);
            }
            else
            {
                numbersLists[setNumber].Add(number);
            }
        }

        long totalCount = 0;

        for (var i = 0; i < numbersLists.Count; i++)
        {
            var numbers = numbersLists[i];
            var c = operators[i];

            var calculation = Calculate(numbers, c);

            totalCount += calculation;
        }
        
        return totalCount;
    }

    private static long Calculate(List<long> numbers, char c)
    {
        var multiply = c == '*';
        long total = multiply ? 1 : 0;

        foreach (var number in numbers)
        {
            if (multiply)
            {
                total *= number;
            }
            else
            {
                total += number;
            }
        }
        
        return total;
    }

    private static IEnumerable<char> CollectCharsFromColumn(string[] lines, int column)
    {
        foreach (var line in lines)
        {
            var c = line[column];
            
            if (!string.IsNullOrEmpty(c.ToString()) && !string.IsNullOrWhiteSpace(c.ToString()))
                yield return c;
        }
    }
}
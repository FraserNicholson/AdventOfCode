namespace AOC2025.Day1;

public static class Day1
{
    public static int Part1()
    {
        var lines = File.ReadAllLines("./input.txt");

        var counter = 50;
        var numberOfZerosStopped = 0;
        var numberOfZerosPassed = 0;


        var startedOnZero = false;
        foreach (var line in lines)
        {
            startedOnZero = counter == 0;
            
            if (startedOnZero)
                numberOfZerosStopped++;
            
            var up = line.ToCharArray()[0] == 'R';
            var amount = int.Parse(line.ToCharArray().AsSpan()[1..]);

            while (amount > 100)
            {
                var amountOfZerosPassed = amount / 100;
                
                numberOfZerosPassed+= amountOfZerosPassed;
                amount %= 100;
            }

            if (up)
            {
                counter += amount;
            }
            else
            {
                counter -= amount;
            }

            if (counter < 0)
            {
                if (!startedOnZero) numberOfZerosPassed++;
                counter += 100;
            }

            if (counter > 99)
            {
                if (counter > 100) numberOfZerosPassed++;
                    
                counter -= 100;
            }
            
            Console.WriteLine("Line " + line);
            Console.WriteLine("Current counter " + counter);
            Console.WriteLine("Number of zeros stopped: " + numberOfZerosStopped);
            Console.WriteLine("Number of zeros passed: " + numberOfZerosPassed);
            Console.WriteLine("");
        }

        return numberOfZerosStopped + numberOfZerosPassed;
    }
}
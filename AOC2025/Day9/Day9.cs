namespace AOC2025.Day9;

public class Day9
{
    private record Coordinates(int X, int Y);
    
    public static int GetAnswerPart1()
    {
        var lines = File.ReadAllLines("./input.txt");

        List<Coordinates> coordinatesList = [];

        foreach (var line in lines)
        {
            var parts = line.Split(',');
            coordinatesList.Add(new Coordinates(int.Parse(parts[0]), int.Parse(parts[1])));
        }

        var biggestArea = 0;

        for (var i = 0; i < coordinatesList.Count; i++)
        {
            for (var j = 0; j < coordinatesList.Count; j++)
            {
                var coordinates1 = coordinatesList[i];
                var coordinates2 = coordinatesList[j];
                
                var area = (Math.Abs(coordinates1.X - coordinates2.X) + 1) * (Math.Abs(coordinates1.Y - coordinates2.Y) + 1) ;

                if (area > biggestArea)
                {
                    biggestArea = area;
                }
            }
        }

        return biggestArea;
    }
}
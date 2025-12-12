namespace AOC2025.Day8;

public static class Day8
{
    private record Coordinates(int X, int Y, int Z);
    private record CoordinatePair(int Coordinate1Index, Coordinates Coordinates1, int Coordinate2Index, Coordinates Coordinates2);
    
    public static long GetAnswerPart1()
    {
        var coordinateDistances = CalculateCoordinateDistances();

        const int allowedPairs = 1000;

        List<List<int>> circuits = [];
        
        for (var i = 0; i < allowedPairs; i++)
        {
            var nextLowestDistance = coordinateDistances.Skip(i).First();

            var circuitWithFirstCoord =
                circuits.SingleOrDefault(x => x.Contains(nextLowestDistance.Key.Coordinate1Index));

            var circuitWithSecondCoord =
                circuits.SingleOrDefault(x => x.Contains(nextLowestDistance.Key.Coordinate2Index));

            if (circuitWithFirstCoord != null && circuitWithSecondCoord != null)
            {
                if (circuitWithFirstCoord == circuitWithSecondCoord)
                    continue;
                
                // merge the 2 separate lists
                circuits.Remove(circuitWithFirstCoord);
                circuits.Remove(circuitWithSecondCoord);
                
                circuits.Add(circuitWithFirstCoord.Concat(circuitWithSecondCoord).ToList());
            }
            
            if (circuitWithSecondCoord != null)
            {
                circuitWithSecondCoord.Add(nextLowestDistance.Key.Coordinate1Index);
                continue;
            }
            
            if (circuitWithFirstCoord != null)
            {
                circuitWithFirstCoord.Add(nextLowestDistance.Key.Coordinate2Index);
                continue;
            }

            // Create new circuit
            circuits.Add([nextLowestDistance.Key.Coordinate1Index, nextLowestDistance.Key.Coordinate2Index]);
        }

        var top3Circuits = circuits.OrderByDescending(x => x.Count).Take(3).ToArray();
        
        var product = top3Circuits[0].Count * top3Circuits[1].Count * top3Circuits[2].Count;
        
        return product;
    }
    
    public static long GetAnswerPart2()
    {
        var coordinateDistances = CalculateCoordinateDistances();

        List<List<int>> circuits = [];

        var product = 0;
        const int numberOfCoords = 1000;
        
        while (!CircuitContainsAllCoords(circuits, numberOfCoords))
        {
            var nextLowestDistance = coordinateDistances.First();
            coordinateDistances.Remove(nextLowestDistance.Key);
            
            var circuitWithFirstCoord =
                circuits.SingleOrDefault(x => x.Contains(nextLowestDistance.Key.Coordinate1Index));

            var circuitWithSecondCoord =
                circuits.SingleOrDefault(x => x.Contains(nextLowestDistance.Key.Coordinate2Index));

            if (circuitWithFirstCoord != null && circuitWithSecondCoord != null)
            {
                if (circuitWithFirstCoord == circuitWithSecondCoord)
                {
                    continue;
                }
                
                // merge the 2 separate lists
                circuits.Remove(circuitWithFirstCoord);
                circuits.Remove(circuitWithSecondCoord);
                
                circuits.Add(circuitWithFirstCoord.Concat(circuitWithSecondCoord).ToList());

                if (CircuitContainsAllCoords(circuits, numberOfCoords))
                {
                    product = nextLowestDistance.Key.Coordinates1.X * nextLowestDistance.Key.Coordinates2.X;
                }
                
                continue;
            }
            
            if (circuitWithSecondCoord != null)
            {
                circuitWithSecondCoord.Add(nextLowestDistance.Key.Coordinate1Index);
                
                if (CircuitContainsAllCoords(circuits, numberOfCoords))
                {
                    product = nextLowestDistance.Key.Coordinates1.X * nextLowestDistance.Key.Coordinates2.X;
                }
                
                continue;
            }
            
            if (circuitWithFirstCoord != null)
            {
                circuitWithFirstCoord.Add(nextLowestDistance.Key.Coordinate2Index);
                
                if (CircuitContainsAllCoords(circuits, numberOfCoords))
                {
                    product = nextLowestDistance.Key.Coordinates1.X * nextLowestDistance.Key.Coordinates2.X;
                }
                
                continue;
            }

            // Create new circuit
            circuits.Add([nextLowestDistance.Key.Coordinate1Index, nextLowestDistance.Key.Coordinate2Index]);
        }
        
        return product;
    }

    private static Dictionary<CoordinatePair, double> CalculateCoordinateDistances()
    {
        var lines = File.ReadAllLines("./input.txt");

        List<Coordinates> coordinatesList = [];

        foreach (var line in lines)
        {
            var parts = line.Split(',');
            
            coordinatesList.Add(new Coordinates(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2])));
        }

        Dictionary<CoordinatePair, double> coordinateDistances = [];

        for (var i = 0; i < coordinatesList.Count; i++)
        {
            var coordinates = coordinatesList[i];

            foreach (var pair in GetCoordinatePairs(coordinates, i, coordinatesList))
            {
                coordinateDistances.TryAdd(pair.Item1, pair.Item2);
            }
        }
        
        coordinateDistances = coordinateDistances.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        
        return coordinateDistances;
    }

    private static IEnumerable<(CoordinatePair, double)> GetCoordinatePairs(Coordinates coordinates,
        int coordinatesIndex, List<Coordinates> otherCoordinates)
    {
        // We start from i+1 as we should have calculated the other distances previously
        for (var i = coordinatesIndex + 1; i < otherCoordinates.Count; i++)
        {
            var otherCoordinate = otherCoordinates[i];
            var distance = CalculateEuclideanDistance(coordinates, otherCoordinate);

            yield return (new CoordinatePair(coordinatesIndex, coordinates, i, otherCoordinate), distance);
        }
    }

    private static double CalculateEuclideanDistance(Coordinates coordinates1, Coordinates coordinates2)
    {
        var sumOfSquaredDiffs = Math.Pow(coordinates1.X - coordinates2.X, 2) +
                                Math.Pow(coordinates1.Y - coordinates2.Y, 2) +
                                Math.Pow(coordinates1.Z - coordinates2.Z, 2);
        
        return Math.Sqrt(sumOfSquaredDiffs);
    }

    private static bool CircuitContainsAllCoords(List<List<int>> circuits, int numberOfCoordinates)
    {
        return circuits.Count == 1 && circuits.First().Count == numberOfCoordinates;
    }
}
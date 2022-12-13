using AoC_Day_12;
using System.Collections;
using System.Drawing;
using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var start = new Point(0, 0);

    for (var i = 0; i < data.Length; i++)
    {
        for (var j = 0; j < data[i].Length; j++)
        {
            if (data[i][j] == 'S')
            {
                start = new Point(j, i);
            }
        }
    }

    Console.WriteLine($"Réponse 1 : " + GetShortestPath(data, start));
}


void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var shortestPath = Int32.MaxValue;

    for (var i = 0; i < data.Length; i++)
    {
        for (var j = 0; j < data[i].Length; j++)
        {
            if (data[i][j] == 'S' || data[i][j] == 'a')
            {
                var pathWeight = GetShortestPath(data, new Point(j, i));
                if (pathWeight < shortestPath)
                    shortestPath = pathWeight;
            }
        }
    }

    Console.WriteLine($"Réponse 2 : " + shortestPath);
}

int GetShortestPath(string[] data, Point start)
{
    var shortestPath = Int32.MaxValue;
    var toVisit = new Queue<ForestPath>();

    var neighboors = GetNeighboors(data, start);
    var root = new ForestPath(start, string.Empty);

    foreach (var neighboor in neighboors)
    {
        toVisit.Enqueue(new ForestPath(neighboor, root.CurrentPath) { Parent = root });
    }

    var dict = new Dictionary<Point, int>();
    dict.Add(root.Position, root.Weight);

    while (toVisit.Any())
    {
        var next = toVisit.Dequeue();

        if (!dict.ContainsKey(next.Position))
            dict.Add(next.Position, Int32.MaxValue);

        if (dict[next.Position] > next.Weight)
        {
            dict[next.Position] = next.Weight;

            neighboors = GetNeighboors(data, next.Position);

            foreach (var neighboor in neighboors)
            {
                if (data[neighboor.Y][neighboor.X] == 'E')
                {
                    shortestPath = next.Weight;
                    toVisit.Clear();
                    break;
                }
                else
                {
                    if (!next.CurrentPath.Contains(String.Format("({0},{1})", neighboor.X, neighboor.Y)))
                        toVisit.Enqueue(new ForestPath(neighboor, next.CurrentPath) { Parent = next });
                }
            }
        }
    }

    return shortestPath;
}

List<Point> GetNeighboors(string[] data, Point start)
{
    var neighboors = new List<Point>();

    var position = data[start.Y][start.X];
    var elevation = position != 'S' ? (int)position : (int)'a';

    if (start.X > 0)
    {
        var destination = data[start.Y][start.X - 1];
        var destinationElevation = destination != 'E' ? (int)destination : (int)'z';

        var left = elevation - destinationElevation;
        if (left >= -1)
        {
            neighboors.Add(new Point(start.X - 1, start.Y));
        }
    }

    if (start.X < data[0].Length - 1)
    {
        var destination = data[start.Y][start.X + 1];
        var destinationElevation = destination != 'E' ? (int)destination : (int)'z';

        var right = elevation - destinationElevation;
        if (right >= -1)
        {
            neighboors.Add(new Point(start.X + 1, start.Y));
        }
    }

    if (start.Y > 0)
    {
        var destination = data[start.Y - 1][start.X];
        var destinationElevation = destination != 'E' ? (int)destination : (int)'z';

        var top = elevation - destinationElevation;
        if (top >= -1)
        {
            neighboors.Add(new Point(start.X, start.Y - 1));
        }
    }

    if (start.Y < data.Length - 1)
    {
        var destination = data[start.Y + 1][start.X];
        var destinationElevation = destination != 'E' ? (int)destination : (int)'z';

        var bottom = elevation - destinationElevation;
        if (bottom >= -1)
        {
            neighboors.Add(new Point(start.X, start.Y + 1));
        }
    }

    return neighboors;
}
using System.Drawing;
using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var cave = GetCave(data, 500);

    Console.WriteLine($"Réponse 1 : " + GetSandCount(cave));
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var cave = GetCave(data, 500, true);

    Console.WriteLine($"Réponse 2 : " + GetSandCount(cave));
}

int GetSandCount(string[,] cave)
{
    var total = 0;

    var drip = -1;
    for (var i = 0; i < cave.GetLength(1); i++)
        if (cave[0, i] == "+")
            drip = i;

    var bust = false;
    while (!bust)
    {
        var sand = new Point(drip, 0);

        var sandStoppped = false;
        while (!sandStoppped)
        {
            if (sand.Y == cave.GetLength(0) - 1)
            {
                bust = true;
                sandStoppped = true;
            }
            else if (cave[sand.Y + 1, sand.X] != "#" && cave[sand.Y + 1, sand.X] != "o")
            {
                sand.Y++;
            }
            else if (sand.X == 0 || sand.X == cave.GetLength(1))
            {
                bust = true;
                sandStoppped = true;
            }
            else if (cave[sand.Y + 1, sand.X - 1] != "#" && cave[sand.Y + 1, sand.X - 1] != "o")
            {
                sand.Y++;
                sand.X--;
            }
            else if (cave[sand.Y + 1, sand.X + 1] != "#" && cave[sand.Y + 1, sand.X + 1] != "o")
            {
                sand.Y++;
                sand.X++;
            }
            else
            {
                total++;
                sandStoppped = true;
                cave[sand.Y, sand.X] = "o";

                // For no.2
                if (sand.Y == 0 && sand.X == 500)
                {
                    sandStoppped = true;
                    bust = true;
                }
            }
        }
    }

    return total;
}

string[,] GetCave(string[] data, int startingWith, bool IsNo2 = false)
{
    var segmentsList = new List<List<Point>>();

    for (var i = 0; i < data.Length; i++)
    {
        var segments = data[i].Split(" -> ").Select(x => new Point(Int32.Parse(x.Split(',')[0]), Int32.Parse(x.Split(',')[1]))).ToList();
        segmentsList.Add(segments);
    }

    var height = 0;
    var minValue = Int32.MaxValue;
    var maxValue = Int32.MinValue;

    if (IsNo2)
    {
        segmentsList.Add(new List<Point> { new Point(0, 0), new Point(1000, 0), });
    }

    foreach (var segments in segmentsList)
    {
        foreach(var segment in segments)
        {
            if (segment.X < minValue) 
                minValue = segment.X;

            if (segment.X > maxValue)
                maxValue = segment.X;

            if (segment.Y > height)
                height = segment.Y;
        }
    }

    if (IsNo2)
        height += 2;

    for (var i = 0; i < segmentsList.Count; i++)
        for (var j = 0; j < segmentsList[i].Count; j++)
            segmentsList[i][j] = new Point(segmentsList[i][j].X - minValue, segmentsList[i][j].Y);

    var width = maxValue - minValue + 1;

    var cave = new string[height + 1, width];

    for (var i = 0; i <= height; i++)
        for (var j = 0; j < width; j++)
            cave[i, j] = ".";

    for (var i = 0; i < segmentsList.Count; i++)
    {
        Point preceding = Point.Empty;
        for (var j = 0; j < segmentsList[i].Count; j++)
        {
            if (preceding != Point.Empty)
            {
                var line = segmentsList[i][j].Y;
                var space = segmentsList[i][j].X;

                var higherLine = Math.Max(line, preceding.Y);
                var lowerLine = Math.Min(line, preceding.Y);

                for(var k = lowerLine; k <= higherLine; k++)
                {
                    cave[k, segmentsList[i][j].X] = "#";
                }

                var higherSpace = Math.Max(space, preceding.X);
                var lowerSpace = Math.Min(space, preceding.X);

                for (var k = lowerSpace; k <= higherSpace; k++)
                {
                    cave[segmentsList[i][j].Y, k] = "#";
                }
            }
                
            preceding = segmentsList[i][j];
        }
    }

    if (IsNo2)
        for (var i = 0; i < cave.GetLength(1); i++)
            cave[cave.GetLength(0) - 1, i] = "#";

    cave[0, startingWith - minValue] = "+";

    return cave;
}
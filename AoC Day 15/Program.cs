using System.Drawing;
using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var test = true;
    var data = DataLoader.GetStringDataFromFile(test);

    var sensors = GetSensors(data);

    var line = 2000000;
    if (test)
        line = 10;

    HashSet<int> keys = new HashSet<int>();

    foreach (var sensor in sensors)
    {
        var xGap = Math.Abs(sensor.Item1.X - sensor.Item2.X);
        var yGap = Math.Abs(sensor.Item1.Y - sensor.Item2.Y);
        var gap = xGap + yGap;

        var lineGap = gap - Math.Abs(line - sensor.Item1.Y);

        for(var i = sensor.Item1.X - lineGap; i <= sensor.Item1.X + lineGap; i++)
            keys.Add(i);
    }

    foreach (var sensor in sensors)
    {
        if (sensor.Item2.Y == line)
            keys.Remove(sensor.Item2.X);

        if (sensor.Item1.Y == line)
            keys.Remove(sensor.Item1.X);
    }

    Console.WriteLine($"Réponse 1 : " + keys.Count);
}

List<Tuple<Point, Point>> GetSensors(string[] data)
{
    var sensors = new List<Tuple<Point, Point>>();

    for (var i = 0; i < data.Length; i++)
    {
        var segments = data[i].Split("at x=");

        var sensorPosition = new Point(0, 0);
        sensorPosition.X = int.Parse(segments[1].Split(',')[0]);
        sensorPosition.Y = int.Parse(segments[1].Split('=')[1].Split(':')[0]);

        var beaconPosition = new Point(0, 0);
        beaconPosition.X = int.Parse(segments[2].Split(',')[0]);
        beaconPosition.Y = int.Parse(segments[2].Split('=')[1]);

        sensors.Add(new Tuple<Point, Point>(sensorPosition, beaconPosition));
    }

    return sensors;
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile(true);

    for (var i = 0; i < data.Length; i++)
    {

    }

    Console.WriteLine($"Réponse 2 : ");
}
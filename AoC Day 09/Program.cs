using System.Drawing;
using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var headPosition = new[] { 0, 0 };
    var tailPosition = new[] { 0, 0 };

    var moves = new List<string>() { "0,0" };

    for (var i = 0; i < data.Length; i++)
    {
        var move = data[i].Split();

        for (var j = 0; j < Int32.Parse(move[1]); j++)
        {
            MoveHead(headPosition, move[0]);

            moves.Add(MoveTail(headPosition, tailPosition));
        }
    }

    Console.WriteLine($"Réponse 1 : " + moves.Distinct().Count());
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var moves = new List<string>() { "0,0" };

    var headPosition = new[] { 0, 0 };

    var knots = new Dictionary<int, int[]>();
    for (var i = 0; i < 9; i++)
        knots.Add(i + 1, new[] { 0, 0 });

    for (var i = 0; i < data.Length; i++)
    {
        var move = data[i].Split();

        for (var j = 0; j < Int32.Parse(move[1]); j++)
        {
            MoveHead(headPosition, move[0]);
            MoveTail(headPosition, knots[1]);

            for(var k = 1; k < 8; k++)
                MoveTail(knots[k], knots[k + 1]);

            moves.Add(MoveTail(knots[8], knots[9]));
        }
    }

    Console.WriteLine($"Réponse 2 : " + moves.Distinct().Count());
}

string MoveTail(int[] headPosition, int[] tailPosition)
{
    var moves = new Dictionary<string, int>()
    {
        { "right", headPosition[0] - tailPosition[0] },
        { "left", headPosition[0] - tailPosition[0] },
        { "up", headPosition[1] - tailPosition[1] },
        { "down", headPosition[1] - tailPosition[1] }
    };

    if (moves.Where(x => x.Value >= 2 || x.Value <= -2).Any())
    {
        if (moves["right"] >= 1)
            tailPosition[0]++;

        if (moves["left"] <= -1)
            tailPosition[0]--;

        if (moves["up"] >= 1)
            tailPosition[1]++;

        if (moves["down"] <= -1)
            tailPosition[1]--;
    }

    return tailPosition[0] + "," + tailPosition[1];
}

void MoveHead(int[] headPosition, string move)
{
    switch (move)
    {
        case "R":
            headPosition[0]++;
            break;
        case "L":
            headPosition[0]--;
            break;
        case "U":
            headPosition[1]++;
            break;
        case "D":
            headPosition[1]--;
            break;
    }
}
using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var X = 1;
    var cycle = 1;

    var signalStrength = 0;

    var queue = new Queue<int>();
    queue.Enqueue(20);
    queue.Enqueue(60);
    queue.Enqueue(100);
    queue.Enqueue(140);
    queue.Enqueue(180);
    queue.Enqueue(220);

    for (var i = 0; i < data.Length; i++)
    {
        if (data[i].Split()[0] == "addx")
        {
            cycle += 2;

            var nextCycle = 0;
            if (queue.TryPeek(out nextCycle) && cycle > nextCycle)
            {
                signalStrength += nextCycle * X;

                queue.Dequeue();
            }

            X += Int32.Parse(data[i].Split()[1]);
        }
        else
            cycle++;
    }

    Console.WriteLine($"Réponse 1 : " + signalStrength);
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var X = 1;
    var cycle = 1;
    var row = "";

    var spritePosition = new bool[40];
    spritePosition[0] = true;
    spritePosition[1] = true;
    spritePosition[2] = true;

    for (var i = 0; i < data.Length; i++)
    {
        if (data[i].Split()[0] == "addx")
        {
            for (var j = 0; j < 2; j++)
            {
                if (spritePosition[(cycle - 1) % 40])
                    row += "#";
                else
                    row += ".";

                cycle++;
            }

            X += Int32.Parse(data[i].Split()[1]);

            for (var j = 0; j < spritePosition.Length; j++)
                spritePosition[j] = false;

            if (X >= 0 && X <= 39)
                spritePosition[X] = true;
            if (X < 39)
                spritePosition[X + 1] = true;
            if (X > 0)
                spritePosition[X - 1] = true;
            
        }
        else
        {
            if (spritePosition[(cycle - 1) % 40])
                row += "#";
            else
                row += ".";

            cycle++;
        }
    }

    Console.WriteLine(row.Substring(0, 40));
    Console.WriteLine(row.Substring(40, 40));
    Console.WriteLine(row.Substring(80, 40));
    Console.WriteLine(row.Substring(120, 40));
    Console.WriteLine(row.Substring(160, 40));
    Console.WriteLine(row.Substring(200, 40));
}
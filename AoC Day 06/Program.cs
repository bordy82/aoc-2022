using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile().First();

    var index = 0;
    var signal = data.Substring(0, 4);

    for(var i = 4; i < data.Length; i++)
    {
        if (ValidateSignal(signal))
        {
            index = i;
            break;
        }

        signal = (signal + data[i]).Substring(1, 4);
    }

    Console.WriteLine($"Réponse 1 : " + index);
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile().First();

    var index = 0;
    var signal = data.Substring(0, 14);

    for (var i = 14; i < data.Length; i++)
    {
        if (ValidateSignal(signal))
        {
            index = i;
            break;
        }

        signal = (signal + data[i]).Substring(1, 14);
    }

    Console.WriteLine($"Réponse 2 : " + index);
}

bool ValidateSignal(string signal)
{
    var different = true;
    foreach (var c in signal)
    {
        if (signal.Count(x => x == c) > 1)
        {
            different = false;
        }
    }

    return different;
}
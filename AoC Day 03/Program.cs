using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var total = 0;

    for (var i = 0; i < data.Length; i++)
    {
        var rucksackLeft = data[i].Substring(0, data[i].Length / 2);
        var rucksackRight = data[i].Substring(data[i].Length / 2);

        foreach(char itemType in rucksackLeft)
        {
            if (rucksackRight.Contains(itemType))
            {
                total += GetPriority(itemType);
                break;
            }
        }
    }

    Console.WriteLine($"Réponse 1 : " + total);
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var total = 0;
    var rucksacks = new List<string>();

    for (var i = 0; i < data.Length; i++)
    {
        rucksacks.Add(data[i]);

        if (rucksacks.Count() == 3)
        {
            foreach (char itemType in rucksacks[0])
            {
                if (rucksacks[1].Contains(itemType) && rucksacks[2].Contains(itemType))
                {
                    total += GetPriority(itemType);
                    break;
                }
            }

            rucksacks.Clear();
        }
    }

    Console.WriteLine($"Réponse 2 : " + total);
}

int GetPriority(char itemType)
{
    var intValue = (int)itemType;

    if (intValue > 90)
        intValue = intValue - 96;
    else
        intValue = intValue - 38;

    return intValue;
}
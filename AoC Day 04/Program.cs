using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var total = 0;

    for (var i = 0; i < data.Length; i++)
    {
        var ranges = data[i].Split(',');

        var pair1 = ranges[0].Split('-');
        var minPair1 = Int32.Parse(pair1[0]);
        var maxPair1 = Int32.Parse(pair1[1]);

        var pair2 = ranges[1].Split('-');
        var minPair2 = Int32.Parse(pair2[0]);
        var maxPair2 = Int32.Parse(pair2[1]);

        if (minPair1 <= minPair2 && maxPair1 >= maxPair2)
            total++;
        else if (minPair2 <= minPair1 && maxPair2 >= maxPair1)
            total++;
    }

    Console.WriteLine($"Réponse 1 : " + total);
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var total = 0;

    for (var i = 0; i < data.Length; i++)
    {
        var dictionnary = new Dictionary<int, int>();

        var ranges = data[i].Split(',');

        AddPairToDictionnary(ranges[0], dictionnary);
        AddPairToDictionnary(ranges[1], dictionnary);

        foreach (var values in dictionnary)
        {
            if (values.Value > 1)
            {
                total++;
                break;
            }
        }
    }

    Console.WriteLine($"Réponse 2 : " + total);
}

void AddPairToDictionnary(string pair, Dictionary<int, int> dictionnary)
{
    var splittedPair = pair.Split('-');

    var min = Int32.Parse(splittedPair[0]);
    var max = Int32.Parse(splittedPair[1]);

    for (var i = min; i <= max; i++)
    {
        if (dictionnary.ContainsKey(i))
            dictionnary[i] = dictionnary[i] + 1;
        else
            dictionnary.Add(i, 1);
    }
}
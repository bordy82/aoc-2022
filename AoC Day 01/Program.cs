using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var currentCalories = 0;
    var elvesList = new List<int>();

    for (var i = 0; i < data.Length; i++)
    {
        if (data[i] == String.Empty)
        {
            elvesList.Add(currentCalories);
            currentCalories = 0;
        }
        else
        {
            currentCalories += Int32.Parse(data[i]);
        }
    }

    elvesList.Add(currentCalories);

    Console.WriteLine($"Réponse 1 : " + elvesList.OrderByDescending(x => x).First());
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var currentCalories = 0;
    var elvesList = new List<int>();

    for (var i = 0; i < data.Length; i++)
    {
        if (data[i] == String.Empty)
        {
            elvesList.Add(currentCalories);
            currentCalories = 0;
        }
        else
        {
            currentCalories += Int32.Parse(data[i]);
        }
    }

    elvesList.Add(currentCalories);

    var sortedElves = elvesList.OrderByDescending(x => x).ToList();

    Console.WriteLine($"Réponse 2 : " + (sortedElves[0] + sortedElves[1] + sortedElves[2]));
}
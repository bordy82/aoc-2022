using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var total = 0;

    for (var i = 0; i < data.Length; i++)
    {
        var elfChoice = GetValue(data[i][0]);
        var myChoice = GetValue(data[i][2]);

        total += GetPuzzleOneScore(elfChoice, myChoice);
    }

    Console.WriteLine($"Réponse 1 : " + total);
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var total = 0;

    for (var i = 0; i < data.Length; i++)
    {
        var elfChoice = GetValue(data[i][0]);
        var result = GetResult(data[i][2]);

        total += GetPuzzleTwoScore(elfChoice, result);
    }

    Console.WriteLine($"Réponse 2 : " + total);
}

int GetPuzzleOneScore(Values elfChoice, Values myChoice)
{
    var total = (int)myChoice;

    total += elfChoice == myChoice ? 3 : 0;

    total += elfChoice == Values.Rock && myChoice == Values.Paper ? 6 : 0;
    total += elfChoice == Values.Paper && myChoice == Values.Cisor ? 6 : 0;
    total += elfChoice == Values.Cisor && myChoice == Values.Rock ? 6 : 0;

    return total;
}

int GetPuzzleTwoScore(Values elfChoice, Results result)
{
    var total = (int)result;

    switch (result)
    {
        case Results.Lose:
            total += (int)GetLosingChoice(elfChoice);
            break;
        case Results.Win:
            total += (int)GetWinningChoice(elfChoice);
            break;
        default:
            total += (int)elfChoice;
            break;
    }

    return total;
}

Values GetWinningChoice(Values value)
{
    switch (value)
    {
        case Values.Rock: return Values.Paper;
        case Values.Paper: return Values.Cisor;
        case Values.Cisor: return Values.Rock;
    }

    return Values.None;
}

Values GetLosingChoice(Values value)
{
    switch (value)
    {
        case Values.Rock: return Values.Cisor;
        case Values.Paper: return Values.Rock;
        case Values.Cisor: return Values.Paper;
    }

    return Values.None;
}

Values GetValue(char value)
{
    switch (value)
    {
        case 'A': case 'X': return Values.Rock;
        case 'B': case 'Y': return Values.Paper;
        case 'C': case 'Z': return Values.Cisor;
    }

    return Values.None;
}

Results GetResult(char result)
{
    switch (result)
    {
        case 'X': return Results.Lose;
        case 'Y': return Results.Draw;
        case 'Z': return Results.Win;
    }

    return Results.Lose;
}

enum Values
{
    None = 0,
    Rock = 1,
    Paper = 2,
    Cisor = 3,
}

enum Results
{
    Lose = 0,
    Draw = 3,
    Win = 6,
}
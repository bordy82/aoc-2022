using AoC_Day_11;
using System.Numerics;
using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var monkeys = GetMonkeys(data);

    for (var i = 0; i < 20; i++)
    {
        foreach (var monkey in monkeys)
        {
            foreach (var item in monkey.StartingItems)
            {
                var newWorryLevel = monkey.GetNewWorryLevel(item);
                var boredWorryLevel = newWorryLevel / 3l;
                var monkeyToThrowTo = monkey.Test(boredWorryLevel);

                monkeys.First(x => x.Id == monkeyToThrowTo).StartingItems.Add(boredWorryLevel);
            }

            monkey.StartingItems.Clear();
        }
    }

    var orderedMonkeys = monkeys.OrderByDescending(x => x.InspectCount).ToList();

    Console.WriteLine($"Réponse 1 : " + (orderedMonkeys[0].InspectCount * orderedMonkeys[1].InspectCount));
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var monkeys = GetMonkeys(data);
        
    var commonDenominator = 1;
    foreach (var monkey in monkeys)
        commonDenominator *= monkey.TestDivisible;

    for (var i = 0; i < 10000; i++)
    {
        foreach (var monkey in monkeys)
        {
            foreach (var item in monkey.StartingItems)
            {
                var newWorryLevel = monkey.GetNewWorryLevel(item);
                var boredWorryLevel = newWorryLevel % commonDenominator;
                var monkeyToThrowTo = monkey.Test(boredWorryLevel);

                monkeys.First(x => x.Id == monkeyToThrowTo).StartingItems.Add(boredWorryLevel);
            }

            monkey.StartingItems.Clear();
        }
    }

    var orderedMonkeys = monkeys.OrderByDescending(x => x.InspectCount).ToList();

    Console.WriteLine($"Réponse 1 : " + (orderedMonkeys[0].InspectCount * orderedMonkeys[1].InspectCount));
}

List<Monkey> GetMonkeys(string[] data)
{
    var monkeys = new List<Monkey>();

    Monkey currentMonkey = null;
    for (var i = 0; i < data.Length; i++)
    {
        if (data[i].StartsWith("Monkey"))
        {
            currentMonkey = new Monkey(Int32.Parse(data[i].Substring(7, 1)));
        }
        else if (data[i].Contains("Starting items"))
        {
            currentMonkey.StartingItems.AddRange(data[i].Split("items: ")[1].Split(", ").Select(x => BigInteger.Parse(x)));
        }
        else if (data[i].Contains("Operation:"))
        {
            if (data[i].Contains("*"))
            {
                currentMonkey.Operation = Operation.Multiply;

                var opsValue = data[i].Split("* ")[1];
                currentMonkey.OperationValue = opsValue == "old" ? -1 : Int32.Parse(opsValue);
            }
            else
            {
                currentMonkey.Operation = Operation.Add;

                var opsValue = data[i].Split("+ ")[1];
                currentMonkey.OperationValue = opsValue == "old" ? -1 : Int32.Parse(opsValue);
            }
        }
        else if (data[i].Contains("Test:"))
        {
            currentMonkey.TestDivisible = Int32.Parse(data[i].Split("by ")[1]);
        }
        else if (data[i].Contains("If true:"))
        {
            currentMonkey.TestTrue = Int32.Parse(data[i].Split("monkey ")[1]);
        }
        else if (data[i].Contains("If false:"))
        {
            currentMonkey.TestFalse = Int32.Parse(data[i].Split("monkey ")[1]);
        }
        else
        {
            monkeys.Add(currentMonkey);
        }
    }

    monkeys.Add(currentMonkey);

    return monkeys;
}
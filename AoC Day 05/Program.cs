using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var operationLines = new List<string>();

    var stacks = GetStacksAndOperations(data, operationLines);

    foreach(var operation in operationLines)
    {
        var ops = operation.Split(' '); 
        for (var i = 0; i < Int32.Parse(ops[1]); i++)
        {
            var box = stacks[ops[3]].Pop();
            stacks[ops[5]].Push(box);
        }
    }

    Console.WriteLine($"Réponse 1 : " + GetSolution(stacks));
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var operationLines = new List<string>();

    var stacks = GetStacksAndOperations(data, operationLines);

    foreach (var operation in operationLines)
    {
        var ops = operation.Split(' ');
        var tempStack = new Stack<string>();
        for (var i = 0; i < Int32.Parse(ops[1]); i++)
        {
            var box = stacks[ops[3]].Pop();
            tempStack.Push(box);
        }

        for (var i = 0; i < Int32.Parse(ops[1]); i++)
        {
            var box = tempStack.Pop();
            stacks[ops[5]].Push(box);
        }
    }

    Console.WriteLine($"Réponse 2 : " + GetSolution(stacks));
}

Dictionary<string, Stack<string>> GetStacksAndOperations(string[] data, List<string> operationLines)
{
    var stackLines = new List<string>();
    var stacks = new Dictionary<string, Stack<string>>();

    var isOperations = false;
    for (var i = 0; i < data.Length; i++)
    {
        if (data[i] == String.Empty)
        {
            isOperations = true;
        }
        else
        {
            if (!isOperations)
            {
                stackLines.Add(data[i]);
            }
            else
            {
                operationLines.Add(data[i]);
            }
        }
    }

    var stackIds = stackLines.Last().Split(' ').Where(x => x != String.Empty).ToList();

    foreach (var id in stackIds)
        stacks.Add(id, new Stack<string>());

    for (var i = stackLines.Count - 2; i >= 0; i--)
    {
        foreach (var stack in stackIds)
        {
            var box = stackLines[i].Substring(1 + (4 * (Int32.Parse(stack) - 1)), 1);

            if (box.Replace(" ", string.Empty) != String.Empty)
                stacks[stack].Push(box);
        }
    }

    return stacks;
}

string GetSolution(Dictionary<string, Stack<string>> stacks)
{
    var solution = String.Empty;
    foreach (var stack in stacks)
        solution += stack.Value.Peek();

    return solution;
}
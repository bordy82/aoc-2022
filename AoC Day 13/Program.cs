using AoC_Day_13;
using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

// 451 too low, 743 too low
void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile(true);

    string left = string.Empty;
    string right = string.Empty;

    var cpt = 1;
    var total = 0;

    for (var i = 0; i < data.Length + 1; i++)
    {
        if (left == string.Empty)
        {
            left = data[i];
        }
        else if (right == string.Empty)
        {
            right = data[i];
        }
        else
        {
            var leftDict = GetNodes(left);
            var rightDict = GetNodes(right);

            var test = Validate(leftDict["start"].Split(',').ToList(), rightDict["start"].Split(',').ToList(), leftDict, rightDict);

            left = string.Empty;
            right = string.Empty;

            Console.WriteLine("[" + cpt + "] : " + test);

            if (test.HasValue && test.Value)
                total += cpt;

            cpt++;
        }
    }

    Console.WriteLine($"Réponse 1 : " + total);
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var nodes = new List<Tuple<string, Dictionary<string, string>>>();

    for (var i = 0; i < data.Length; i++)
    {
        if (data[i] != String.Empty)
        {
            var node = GetNodes(data[i]);
            nodes.Add(new Tuple<string, Dictionary<string, string>>(data[i], node));
        }
    }

    nodes.Add(new Tuple<string, Dictionary<string, string>>("[[2]]", GetNodes("[[2]]")));
    nodes.Add(new Tuple<string, Dictionary<string, string>>("[[6]]", GetNodes("[[6]]")));

    var sortedNodes = new List<Tuple<string, Dictionary<string, string>>>();

    foreach (var node in nodes)
    {
        if (sortedNodes.Count == 0)
            sortedNodes.Add(node);
        else
        {
            var index = -1;
            for (var i = 0; i < sortedNodes.Count; i++)
            {
                var result = Validate(node.Item2["start"].Split(',').ToList(), sortedNodes[i].Item2["start"].Split(',').ToList(), node.Item2, sortedNodes[i].Item2);

                if (result.HasValue && result.Value)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
                sortedNodes.Add(node);
            else
                sortedNodes.Insert(index, node);
        }
    }

    Console.WriteLine($"Réponse 2 : " + (sortedNodes.FindIndex(x => x.Item1 == "[[2]]") + 1) * (sortedNodes.FindIndex(x => x.Item1 == "[[6]]") + 1));
}

Dictionary<string, string> GetNodes(string content)
{
    var cpt = 0;
    var dict = new Dictionary<string, string>();

    while(content.IndexOf(']') >= 0)
    {
        var indexEndOfInnerCollection = content.IndexOf(']');
        var indexStartOfInnerCollection = content.Substring(0, indexEndOfInnerCollection).LastIndexOf('[');
        var innerCollection = content.Substring(indexStartOfInnerCollection + 1, indexEndOfInnerCollection - indexStartOfInnerCollection - 1);
        content = content.Substring(0, indexStartOfInnerCollection) + "{" + cpt + "}" + content.Substring(indexEndOfInnerCollection + 1);

        dict.Add("{" + cpt + "}", innerCollection);
        cpt++;
    }

    dict.Add("start", content);

    return dict;
}

bool? Validate(List<string> left, List<string> right, Dictionary<string, string> leftDict, Dictionary<string, string> rightDict)
{
    for(var i = 0; i < left.Where(x => x != "").Count(); i++)
    {
        if ((i + 1) > right.Where(x => x != "").Count())
        {
            Console.WriteLine("Right side is missing elements");
            return false;
        }

        var isLeftNumber = left[i].StartsWith('{') ? false : true;
        var isRightNumber = right[i].StartsWith('{') ? false : true;

        if (isLeftNumber && isRightNumber)
        {
            if (Int32.Parse(left[i]) > Int32.Parse(right[i]))
            {
                Console.WriteLine(String.Format("Left side is higher {0} > {1}", Int32.Parse(right[i]), Int32.Parse(left[i])));
                return false;
            }
            else if (Int32.Parse(left[i]) < Int32.Parse(right[i]))
            {
                Console.WriteLine(String.Format("Right side is higher {0} > {1}", Int32.Parse(right[i]), Int32.Parse(left[i])));
                return true;
            }
        }
        else
        {
            var newLeft = isLeftNumber ? new List<string>() { left[i] } : leftDict[left[i]].Split(',').ToList();
            var newRight = isRightNumber ? new List<string>() { right[i] } : rightDict[right[i]].Split(',').ToList();

            var result = Validate(newLeft, newRight, leftDict, rightDict);

            if (result.HasValue)
                return result;
        }
    }

    if (left.Where(x => x != "").Count() < right.Where(x => x != "").Count())
    {
        Console.WriteLine("Left side is missing elements");
        return true;
    }

    return null;
}
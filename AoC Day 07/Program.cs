using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var root = GetFileSystem(data);

    var total = 0;
    foreach(var directory in root.GetDirectories())
    {
        if (directory.GetSize() <= 100000)
        {
            total += directory.GetSize();
        }
    }

    Console.WriteLine($"Réponse 1 : " + total);
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var root = GetFileSystem(data);

    var totalSystemSize = root.GetSize();
    var spaceLeft = 70000000 - totalSystemSize;
    var neededSpace = 30000000 - spaceLeft;

    var smallestDirectory = root;

    foreach(var directory in root.GetDirectories())
    {
        if (directory.GetSize() > neededSpace)
        {
            if (directory.GetSize() < smallestDirectory.GetSize())
            {
                smallestDirectory = directory;
            }
        }
    }

    Console.WriteLine($"Réponse 2 : " + smallestDirectory.GetSize());
}

Directory GetFileSystem(string[] data)
{
    var root = new Directory("/");
    var currentDirectory = root;

    for (var i = 1; i < data.Length; i++)
    {
        if (data[i][0] == '$')
        {
            if (data[i].Substring(2, 2) == "cd")
            {
                var path = data[i].Substring(5);

                if (path == "..")
                {
                    currentDirectory = currentDirectory.Parent;
                }
                else
                {
                    currentDirectory = currentDirectory.Directories.First(x => x.Name == path);
                }
            }
        }
        else
        {
            if (data[i].Substring(0, 3) == "dir")
            {
                var newDir = new Directory(data[i].Substring(4));
                newDir.Parent = currentDirectory;

                currentDirectory.Directories.Add(newDir);
            }
            else
            {
                var size = Int32.Parse(data[i].Split(' ')[0]);
                var name = data[i].Split(' ')[1];

                currentDirectory.Files.Add(new File(name, size));
            }
        }
    }

    return root;
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Directory
{
    public string Name { get; set; }

    public IList<Directory> Directories { get; private set; }

    public IList<File> Files { get; private set; }

    public Directory Parent { get; set; }

    public Directory(string name)
    {
        this.Name = name;
        this.Directories = new List<Directory>();
        this.Files = new List<File>();
    }

    public int GetSize()
    {
        return Files.Sum(x => x.Size) + Directories.Sum(x => x.GetSize());
    }

    public IList<Directory> GetDirectories()
    {
        var directories = new List<Directory>();

        foreach(var directory in this.Directories)
        {
            directories.Add(directory);
            directories.AddRange(directory.GetDirectories());
        }

        return directories;
    }
}


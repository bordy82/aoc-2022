using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class File
{
    public string Name { get; private set; }
    public int Size { get; private set; }

    public File(string name, int size)
    {
        this.Name = name;
        this.Size = size;
    }
}


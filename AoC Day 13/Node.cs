using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_13
{
    internal class Node
    {
        public List<int> Values { get; set; }

        public Node()
        {
            this.Values = new List<int>();
        }
        internal void AddValue(int value)
        {
            this.Values.Add(value);
        }
    }
}

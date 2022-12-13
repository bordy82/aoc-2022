using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_12
{
    public class ForestPath
    {
        private string _currentPath;

        public string CurrentPath { get { return this._currentPath + String.Format("({0},{1})", this.Position.X, this.Position.Y); } }

        public Point Position { get; set; }

        public int Weight { get { return this.CurrentPath.Count(x => x == ','); } }

        public ForestPath Parent { get; set; }

        public ForestPath(Point position, string path)
        {
            this._currentPath = path;
            this.Position = position;
        }
    }
}

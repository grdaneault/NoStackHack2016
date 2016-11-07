using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack.WorldMap
{
    class EmptyTile : Tile
    {
        public const char Code = ' ';
        public EmptyTile(int x, int y, char background) : base(x, y, Code, background) { }
        public override bool IsFilled()
        {
            return false;
        }
    }
}

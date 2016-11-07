using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack.WorldMap
{
    class SolidTile : Tile
    {
        public const char Code = '#';
        public SolidTile(int x, int y, char code) : base(x, y, code, code) { }
        public override bool IsFilled()
        {
            return true;
        }
    }
}

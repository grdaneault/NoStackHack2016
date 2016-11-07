using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack.WorldMap
{
    class WaterTile : Tile
    {
        public const char Code = 'W';
        public WaterTile(int x, int y, char background) : base(x, y, Code, background) { }
        public override bool IsFilled()
        {
            return true;
        }
    }
}

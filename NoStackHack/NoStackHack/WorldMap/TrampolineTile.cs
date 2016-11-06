using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack.WorldMap
{
    class TrampolineTile : Tile
    {
        public const char Code = 'T';
        public TrampolineTile(int x, int y, char background) : base(x, y, Code, background) { }
        public override bool IsFilled()
        {
            return true;
        }
    }
}

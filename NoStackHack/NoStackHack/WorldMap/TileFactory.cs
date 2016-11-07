using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack.WorldMap
{
    class TileFactory
    {

        public Tile Create(int x, int y, char foreground, char background)
        {
            switch (foreground)
            {
                case TrampolineTile.Code:
                    return new TrampolineTile(x, y, background);
                case WaterTile.Code:
                    return new WaterTile(x, y, background);
                case EmptyTile.Code:
                    return new EmptyTile(x, y, background);
                default:
                    return new SolidTile(x, y, foreground);
            }
        }
    }
}

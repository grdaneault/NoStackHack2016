using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NoStackHack.WorldMap
{
    class World
    {
        private Tile[][] _map;
        private GraphicsDevice _device;
        private SpriteBatch _batch;
        private Rectangle _screenSize; //todo pull out into base class of some sort?

        public int Rows
        {
            get { return _map.Length; }
        }

        public int Cols
        {
            get { return _map[0].Length; }
        }

        public Point TileSize { get; private set; }

        public World(Tile[][] map)
        {
            _map = map;
        }

        public void Init(GraphicsDevice device, Rectangle screenSize)
        {
            _device = device;
            _batch = new SpriteBatch(_device);
            _screenSize = screenSize;
            TileSize = new Point(screenSize.Width/30, screenSize.Height/16);
        }

        private void Draw(Func<int, int, Texture2D> tileFunc)
        {
            _batch.Begin();
            for (var y = 0; y < Rows; y++)
            {
                for (var x = 0; x < Cols; x++)
                {
                    var tile = tileFunc(x, y);
                    if (tile != null)
                    {
                        var location = new Point((x)*TileSize.X, y*TileSize.Y);
                        _batch.Draw(tileFunc(x, y), new Rectangle(location, TileSize), Color.White);
                    }
                }
            }
            _batch.End();
        }

        public bool FilledCell(int x, int y)
        {
            return _map[y][x].IsFilled;
        }

        public void DrawForeground()
        {
            Draw((x, y) => _map[y][x].Foreground);
        }

        public void DrawBackground()
        {
            Draw((x, y) => _map[y][x].Background);
        }

    }
}

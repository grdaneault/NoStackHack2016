using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NoStackHack.Rendering;
using NoStackHack.Utilities;

namespace NoStackHack.WorldMap
{
    class World
    {
        private Tile[][] _map;
        private GraphicsDevice _device;
        private SpriteBatchDecarator _batch;
        private Rectangle _screenSize; //todo pull out into base class of some sort?
        private ISet<Tile> _touchingPlayer = new HashSet<Tile>();

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

        public void Init(RenderHelper render, Rectangle screenSize)
        {
            _device = render.Device;
            _batch = render.Batch;
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
                        Color color = _touchingPlayer.Contains(_map[y][x]) ? Color.Red : Color.White;
                        _batch.Draw(tileFunc(x, y), new Rectangle(location, TileSize), color);
                    }
                }
            }
            _batch.End();
        }

        public bool FilledCell(int x, int y)
        {
            return _map[y][x].IsFilled();
        }

        public void DrawForeground()
        {
            Draw((x, y) => _map[y][x].Foreground);
        }

        public void DrawBackground()
        {
            Draw((x, y) => _map[y][x].Background);
        }

        public void ClearTouching()
        {
            _touchingPlayer.Clear();
        }

        public IList<ICommand> UpdatePlayerForWorld(Player player)
        {
            var padding = 3;
            var top = (int)Math.Floor((player.Box.Top - padding) / TileSize.Y);
            var left = (int)Math.Floor((player.Box.Left - padding) / TileSize.X);
            var right = (int)Math.Floor((player.Box.Right + padding) / TileSize.X);
            var bottom = (int)Math.Floor((player.Box.Bottom + padding) / TileSize.Y);

            var commandSet = new HashSet<ICommand>();

            for (var x = left; x <= right; x++)
            {
                for (var y = bottom; y >= top; y--)
                {
                    if (y >=0 && y < Rows && x >= 0 && x < Cols)
                    {
                        var tile = _map[y][x];
                        commandSet.Add(tile.InteractWithPlayer(player));
                        _touchingPlayer.Add(tile);
                    }
                }
            }

            commandSet.Remove(null);

            return commandSet.ToList();
        }

        internal Box FindTileBelowBox(Box b, int bottomPadding=12)
        {
            var padding = 3;
            bottomPadding *= TileSize.Y;
            var top = (int)Math.Floor((b.Top - padding) / TileSize.Y);
            var left = (int)Math.Floor((b.Left - padding) / TileSize.X);
            var right = (int)Math.Floor((b.Right + padding) / TileSize.X);
            var bottom = (int)Math.Floor((b.Bottom + bottomPadding) / TileSize.Y);


            var furthestUp = float.MaxValue;
            Box workingBox = null;


            for (var x = left; x <= right; x++)
            {
                for (var y = top; y <= bottom; y++)
                {
                    if (y >= 0 && y < Rows && x >= 0 && x < Cols)
                    {
                        var tile = _map[y][x];
                        if (tile.IsFilled())
                        {
                            _touchingPlayer.Add(tile);
                            if (y < furthestUp)
                            {
                                furthestUp = y;
                                workingBox = new Box(tile.Position * TileSize.ToVector2(), TileSize.ToVector2());
                            }
                        }
                    }
                }
            }
            return workingBox;
            
        }
    }
}

using System.Collections.Generic;
using System.IO;
using System.Xml.Schema;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NoStackHack.Utilities;

namespace NoStackHack.WorldMap
{
    class WorldLoader
    {
        public static World Load(string mapFile, ContentManager content)
        {
            using (var reader = new StreamReader(File.OpenRead(mapFile)))
            {
                var dims = reader.ReadLine().Split(',');
                var dimensions = new Point(int.Parse(dims[0]), int.Parse(dims[1]));
                var map = new Tile[dimensions.Y][];

                for (var y = 0; y < dimensions.Y; y++)
                {
                    var line = reader.ReadLine();
                    map[y] = new Tile[dimensions.X];
                    for (var x = 0; x < dimensions.X; x++)
                    {
                        var background = line[2 * x + 1];
                        var foreground = line[2 * x];
                        map[y][x] = new Tile(background, foreground);
                    }
                }

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line.Length > 2)
                    {
                        char id = line[0];
                        string path = line.Substring(1);
                        Tile.RegisterTexture(id, content.Load<Texture2D>(path));
                    }
                }

                return new World(map);
            }
        }

        public static List<Box> GenerateHitboxes(World world)
        {
            List<Box> horizontalBoxes = new List<Box>();
            List<Box> verticalBoxes = new List<Box>();

            for (var y = 0; y < world.Rows; y++)
            {
                var inBox = false;
                Vector2 startCoord = Vector2.Zero;
                int width = 0;
                for (var x = 0; x < world.Cols; x++)
                {
                    if (!inBox && world.FilledCell(x, y))
                    {
                        startCoord = new Vector2(x * world.TileSize.X, y * world.TileSize.Y);
                        width = world.TileSize.X;
                        inBox = true;
                    } else if (inBox && world.FilledCell(x, y))
                    {
                        width += world.TileSize.X;
                    }
                    else if (inBox)
                    {
                        horizontalBoxes.Add(new Box(startCoord.X, startCoord.Y, width, world.TileSize.Y));
                        inBox = false;
                        startCoord = Vector2.Zero;
                        width = 0;
                    }
                }
                if (inBox)
                {
                    horizontalBoxes.Add(new Box(startCoord.X, startCoord.Y, width, world.TileSize.Y));
                }
            }

            for (var x = 0; x < world.Cols; x++)
            {
                var inBox = false;
                Vector2 startCoord = Vector2.Zero;
                int height = 0;
                for (var y = 0; y < world.Rows; y++)
                {
                    if (!inBox && world.FilledCell(x, y))
                    {
                        startCoord = new Vector2(x * world.TileSize.X, y * world.TileSize.Y);
                        height = world.TileSize.Y;
                        inBox = true;
                    }
                    else if (inBox && world.FilledCell(x, y))
                    {
                        height += world.TileSize.Y;
                    }
                    else if (inBox)
                    {
                        verticalBoxes.Add(new Box(startCoord.X, startCoord.Y, world.TileSize.X, height));
                        inBox = false;
                        startCoord = Vector2.Zero;
                        height = 0;
                    }
                }
                if (inBox)
                {
                    verticalBoxes.Add(new Box(startCoord.X, startCoord.Y, world.TileSize.X, height));
                }
            }

            List<Box> boxes = new List<Box>();
            foreach (var horizBox in horizontalBoxes)
            {
                var useless = false;
                foreach (var testVert in verticalBoxes)
                {
                    if (horizBox.IsInside(testVert))
                    {
                        useless = true;
                        break;
                    }
                }
                if (!useless)
                {
                    boxes.Add(horizBox);
                }
            }

            foreach (var vertBox in verticalBoxes)
            {
                var useless = false;
                foreach (var testHoriz in horizontalBoxes)
                {
                    if (vertBox.IsInside(testHoriz) && !vertBox.Equals(testHoriz))
                    {
                        useless = true;
                        break;
                    }
                }
                if (!useless)
                {
                    boxes.Add(vertBox);
                }
            }

            return boxes;
        }
    }
}

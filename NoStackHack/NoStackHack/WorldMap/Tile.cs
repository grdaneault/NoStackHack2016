using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NoStackHack.Utilities;

namespace NoStackHack.WorldMap
{
    abstract class Tile
    {
        public Texture2D Foreground { get; private set; }
        public Texture2D Background { get; private set; }

        public Color Color { get; private set; }

        private readonly char _foreground;
        private readonly char _background;

        public Vector2 Position { get; }

        public abstract bool IsFilled();

        private static Dictionary<char, List<Texture2D>> _textures = new Dictionary<char, List<Texture2D>>() {{' ', new List<Texture2D>()}};
        private static Random _random = new Random(10);

        public static void RegisterTexture(char id, Texture2D texture)
        {
            if (!_textures.ContainsKey(id))
            {
                _textures[id] = new List<Texture2D>();
            }
            _textures[id].Add(texture);
        }

        protected Tile(int x, int y, char foreground, char background)
        {
            Position = new Vector2(x, y);
            _foreground = foreground;
            _background = background;
        }

        public void PictureUp()
        {
            Foreground = RandomTexture(_foreground);
            Background = RandomTexture(_background);
        }

        private static Texture2D RandomTexture(char type)
        {
            var choices = _textures[type];
            if (choices.Count == 0)
            {
                return null;
            }
            else
            {
                return choices[_random.Next(0, choices.Count - 1)];
            }
        }

        public virtual ICommand InteractWithPlayer(Player player)
        {
            return null;
        }
    }
}

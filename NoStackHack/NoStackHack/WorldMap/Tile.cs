using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace NoStackHack.WorldMap
{
    class Tile
    {
        public Texture2D Foreground { get { return _textures[_foreground]; } }
        public Texture2D Background { get { return _textures[_background]; } }

        private char _foreground;
        private char _background;

        public bool IsFilled { get { return _foreground != ' '; } }

        private static Dictionary<char, Texture2D> _textures = new Dictionary<char, Texture2D>() {{' ', null}};

        public static void RegisterTexture(char id, Texture2D texture)
        {
            _textures[id] = texture;
        }

        public Tile(char foreground, char background)
        {
            _foreground = foreground;
            _background = background;
        }
    }
}

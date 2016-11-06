using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using Microsoft.Win32.SafeHandles;
using Microsoft.Xna.Framework;
using MonoGame.Utilities.Png;

namespace NoStackHack.Rendering
{
    class Star
    {
        public Vector2 Location { get; }
        public float Scale { get;  }
        private float _rotation;
        private Color _tint;
        private bool _pulseUp
            ;

        public Texture2D Texture { get; }

        public Rectangle Rectangle { get { return new Rectangle(Location.ToPoint(), new Point((int)(64 * Scale)));} }

        public Star(Vector2 location, float scale, float rotation, Color tint, Texture2D texture)
        {
            Location = location;
            Scale = scale;
            _rotation = rotation;
            _tint = new Color(tint, scale);
            Texture = texture;
            _pulseUp = true;
        }

        public Color ColorStep()
        {
            if (_pulseUp)
            {
                _tint = new Color(_tint, _tint.A + 1);
                if (_tint.A >= Math.Min(255, 255*Scale + 100))
                {
                    _pulseUp = false;
                }
            }
            else
            {
                _tint = new Color(_tint, _tint.A - 1);
                if (_tint.A <= Math.Max(0, 255 * Scale - 100))
                {
                    _pulseUp = false;
                }
            }

            return _tint;
        }

        public float RotationStep()
        {
            _rotation += 0.001f;
            return _rotation;
        }

        
    }
    class BackgroundImage
    {
        private List<Texture2D> _starTextures;
        private List<Star> _stars;
        private Texture2D _clouds;
        private Rectangle _screenSize;
        private SpriteBatchDecarator _batch;
        private GraphicsDevice _device;
        private List<Streak> _streaks;
        private Vector2 _mapSize;

        public void Init(RenderHelper render, Rectangle screenSize, Vector2 mapSize)
        {
            _device = render.Device;
            _batch = render.Batch;
            _screenSize = screenSize;
            _stars = new List<Star>(1000);
            _mapSize = mapSize;
        }

        public void LoadContent(ContentManager content)
        {
            _starTextures = new List<Texture2D>();
            for (var i = 1; i < 5; i++)
            {
                _starTextures.Add(content.Load<Texture2D>($"stars/{i}.png"));
            }
            _clouds = content.Load<Texture2D>("gray_repeating.png");

            var rand = new Random(10);

            for (var i = 0; i < 1000; i++)
            {
                var scale = rand.Next(0, 20) / 100.0f;
                var rotation = (float)(rand.Next(0, 360) / (2f * Math.PI));
                var texture = _starTextures[rand.Next(0, _starTextures.Count - 1)];
                var location = new Vector2(rand.Next((int)_mapSize.X), rand.Next((int)_mapSize.Y));
                _stars.Add(new Star(location, scale, rotation, Color.LightYellow, texture));
            }
        }

        public void Draw()
        {
            _batch.Begin();
            var color = new Color(Color.ForestGreen, 0.5f);
            _batch.Draw(_clouds, _screenSize, null, color, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            

            foreach (var star in _stars)
            {
                _batch.Draw(star.Texture, star.Rectangle, null, star.ColorStep(), star.RotationStep(), Vector2.Zero,
                    SpriteEffects.None, 1f);
            }

            _batch.End();
        }
    }
}

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
    class BackgroundImage
    {
        private Texture2D _clouds;
        private Texture2D _streak;
        private Rectangle _screenSize;
        private SpriteBatch _batch;
        private GraphicsDevice _device;
        private List<Streak> _streaks;

        public void Init(GraphicsDevice device, Rectangle screenSize)
        {
            _device = device;
            _batch = new SpriteBatch(_device);
            _screenSize = screenSize;
            _streaks = new List<Streak>(250);
            var rand = new Random(10);
            for (int i = 0; i < 150; i++)
            {
                var start = new Point(rand.Next(-400, _screenSize.Width + 400), rand.Next(-screenSize.Height, screenSize.Height));
                _streaks.Add(new Streak(start, rand.Next(300, 400), rand.Next(2, 4)));
            }
            for (int i = 0; i < 50; i++)
            {
                var start = new Point(rand.Next(-400, _screenSize.Width + 400), rand.Next(-screenSize.Height, screenSize.Height));
                _streaks.Add(new Streak(start, rand.Next(100, 200), rand.Next(6, 10)));
            }

            for (int i = 0; i < 50; i++)
            {
                var start = new Point(rand.Next(-400, _screenSize.Width + 400), rand.Next(-screenSize.Height, screenSize.Height));
                _streaks.Add(new Streak(start, rand.Next(10, 50), rand.Next(15, 20)));
            }
        }

        public void LoadContent(ContentManager content)
        {
            _clouds = content.Load<Texture2D>("gray_repeating.png");
            _streak = content.Load<Texture2D>("streak.png");
        }

        public void Draw()
        {
            _batch.Begin();
            var color = new Color(Color.ForestGreen, 0.5f);
            _batch.Draw(_clouds, _screenSize, null, color, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            _batch.End();

            _batch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Matrix.CreateRotationZ(0.1f));
            
            foreach (var streak in _streaks)
            {
                
                _batch.Draw(_streak, streak.Step(), streak.Color);


                streak.Loop(_screenSize);
            }
            _batch.End();
        }
    }
}

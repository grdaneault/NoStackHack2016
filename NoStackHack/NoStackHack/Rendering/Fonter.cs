using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NoStackHack.Rendering
{
    class Fonter
    {
        private GraphicsDevice _device;
        private SpriteBatch _batch;
        private Rectangle _screenSize;
        private SpriteFont _font;
        public List<Message> Messages = new List<Message>();

        public void Init(GraphicsDevice device, Rectangle screenSize)
        {
            _device = device;
            _batch = new SpriteBatch(_device);
            _screenSize = screenSize;
        }

        public void Load(ContentManager content)
        {
            _font = content.Load<SpriteFont>("basic");
        }

        public void Draw()
        {
            _batch.Begin();
            foreach (var message in Messages)
            {
                _batch.DrawString(_font, message.Text, message.Location, Color.Red);
            }
            _batch.End();
        }
    }
}

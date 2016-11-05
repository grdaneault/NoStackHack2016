using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack.Rendering
{
    class RenderHelper
    {

        public GraphicsDevice Device { get; private set; }
        public SpriteBatch Batch { get; private set; }
        public Texture2D Pixel { get; private set; }

        public void Init(GraphicsDevice device)
        {
            Device = device;
            Pixel = new Texture2D(Device, 1, 1, false, SurfaceFormat.Color);
            Pixel.SetData<Color>(new Color[] { Color.White });

            Batch = new SpriteBatch(Device);
            
        }

        
        public void DrawBox(Vector2 position, Vector2 size, Color color = default(Color))
        {
            if (color == default(Color))
                color = Color.White;

            Batch.Draw(Pixel, position, null, color, 0f, Vector2.Zero, size, SpriteEffects.None, 1f);
        }

    }
}

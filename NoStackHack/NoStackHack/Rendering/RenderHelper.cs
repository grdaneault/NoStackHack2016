using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NoStackHack.Utilities;
using System;

namespace NoStackHack.Rendering
{
    public class RenderHelper
    {
        public GraphicsDevice Device { get; private set; }
        public SpriteBatchDecarator Batch { get; private set; }
        public Texture2D Pixel { get; private set; }
        public Camera ActiveCamera { get; set; }

        public void Init(GraphicsDevice device)
        {
            Device = device;

            Pixel = new Texture2D(Device, 1, 1, false, SurfaceFormat.Color);
            Pixel.SetData<Color>(new Color[] { Color.White });
            ActiveCamera = new Camera(new Vector2(1920, 1080));
            //Batch = new SpriteBatch(Device);

            Batch = new SpriteBatchDecarator(new SpriteBatch(Device), this);
        }
        
        public void DrawBox(Box box, Color color = default(Color))
        {
            DrawBox(box.Position, box.Size, color);
        }

        public void DrawBox(Vector2 position, Vector2 size, Color color = default(Color))
        {
            if (color == default(Color))
                color = Color.White;

            Batch.Draw(Pixel, position, null, color, 0f, Vector2.Zero, size, SpriteEffects.None, 1f);
        }

        public void DrawLine(Vector2 start, Vector2 end, Color color = default(Color), float thickness=1f)
        {
            if (color == default(Color))
                color = Color.White;

            var centerPosition = (start + end) / 2;
            var length = (start - end).Length();
            var angle = (float) Math.Atan2((start - end).Y, (start - end).X);
            Batch.Draw(Pixel, start, null, null, new Vector2(1, 0), angle, new Vector2(length, thickness), color, SpriteEffects.None, 1);
            //Batch.Draw(Pixel, centerPosition, null, color, angle, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, 1f);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack.Rendering
{
    public class SpriteBatchDecarator
    {
        public SpriteBatch Batch { get; private set; }
        public RenderHelper Helper { get; private set; }
        public SpriteBatchDecarator(SpriteBatch batch, RenderHelper helper)
        {
            Batch = batch;
            Helper = helper;
        }

        public void Begin(
            SpriteSortMode mode=SpriteSortMode.Deferred,
            BlendState blend=null,
            SamplerState sampler=null,
            DepthStencilState depth=null,
            RasterizerState raster=null,
            Effect effect=null,
            Matrix? transform=null)
        {
            if (transform == null)
            {
                transform = Helper.ActiveCamera.GetTransform();
            }
            mode = SpriteSortMode.Deferred ;
           
            Batch.Begin(mode, blend, sampler, depth, raster, effect, transform);
        }

        public void Draw(Texture2D image,
            Vector2 position,
            Rectangle? source,
            Rectangle? destination,
            Vector2 offset,
            float angle,
            Vector2 scale,
            Color color,
            SpriteEffects effects,
            float depth)
        {
            depth /= 3;
            Batch.Draw(image, position, source, destination, offset, angle, scale, color, effects, depth);
        }

        public void Draw(Texture2D image,
            Vector2 position,
            Rectangle? source,
            Color color,
            float angle,
            Vector2 offset,
            Vector2 scale,
            SpriteEffects effects,
            float depth)
        {
            depth /= 3;
            Batch.Draw(image, position, source, color, angle, offset, scale, effects, depth);
        }

        public void Draw(Texture2D image, 
            Rectangle destination,
            Color color)
        {
            Batch.Draw(image, destination, color);
        }

        public void DrawString(SpriteFont font, 
            string text, 
            Vector2 position, 
            Color color)
        {
            Batch.DrawString(font, text, position, color, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }

        public void End()
        {
            Batch.End();
        }
    }


}

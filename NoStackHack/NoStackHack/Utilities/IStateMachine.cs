using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack.Utilities
{
    public interface IState
    {
        void Init();
        IState Update(GameTime gameTime);
        void Exit();
    }
}

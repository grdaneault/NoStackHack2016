using Microsoft.Xna.Framework;
using NoStackHack.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack.Rendering.Character
{
    public class VisualComponent
    {

        public StateMachine<GameTime> StateMachine { get; set; }

        public VisualComponent()
        {
            StateMachine = new StateMachine<GameTime>(new IdleState());
        }

        public void Update(GameTime time)
        {
            StateMachine.Update(time);
        }

        public void Render(RenderHelper render)
        {

        }

    }
}

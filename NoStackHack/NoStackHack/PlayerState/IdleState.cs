using System;
using Microsoft.Xna.Framework;
using NoStackHack.Utilities;

namespace NoStackHack.PlayerState
{
    class IdleState : DefaultState
    {
        public IdleState(Player player) : base(player)
        {
        }

        public override void Jump(float power)
        {
            NextState = new JumpingState(Player, power);
        }
    }
}

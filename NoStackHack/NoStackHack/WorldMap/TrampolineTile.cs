using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using NoStackHack.ControlInput;
using NoStackHack.ControlInput.Commands;
using NoStackHack.Utilities;

namespace NoStackHack.WorldMap
{
    class TrampolineTile : Tile
    {
        public const char Code = 'T';
        public TrampolineTile(int x, int y, char background) : base(x, y, Code, background) { }
        public override bool IsFilled()
        {
            return true;
        }

        public override ICommand InteractWithPlayer(Player player)
        {
            return new JumpCommand(60);
        }
    }
}

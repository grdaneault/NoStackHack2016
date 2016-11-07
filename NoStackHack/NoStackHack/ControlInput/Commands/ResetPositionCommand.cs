using NoStackHack.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack.ControlInput.Commands
{
    class ResetPositionCommand : ICommand
    {
        public void Execute(IGameObject gameObject)
        {
            var resetable = gameObject as IResetable;
            if (resetable == null) { return; }
            resetable.ResetPosition();
        }
    }
}

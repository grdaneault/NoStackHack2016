using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NoStackHack.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack.ControlInput
{
    public class InputHandler
    {
        private ICommand _buttonX;
        private ICommand _buttonY;
        private ICommand _buttonA;
        private ICommand _buttonB;

        public InputHandler()
        {
            Initialize();
        }

        private void Initialize()
        {
            //_buttonX = 
        }

        public void HandleInput()
        {
            if(GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
            {
                _buttonX.Execute();
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
            {
                _buttonY.Execute();
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
            {
                _buttonA.Execute();
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
            {
                _buttonB.Execute();
            }
        }
    }
}

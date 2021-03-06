﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NoStackHack.ControlInput.Commands;
using NoStackHack.Utilities;
using System;
using System.Collections.Generic;

namespace NoStackHack.ControlInput
{
    public class InputHandler
    {
        private ICommand _buttonX;
        private ICommand _buttonY;
        private ICommand _buttonA;
        private ICommand _buttonB;
        private IStickCommand _stickLeft;
        private IStickCommand _stickRight;

        public InputHandler()
        {
            Initialize();
        }

        private void Initialize()
        {
            _buttonX = new ResetPositionCommand();
            _buttonY = new NoCommand();
            _buttonA = new JumpCommand();
            _buttonB = new NoCommand();
            _stickLeft = new MoveCommand();
            _stickRight = new RotateCommand();
        }

        public List<ICommand> HandleInput(PlayerIndex player)
        {
            var commandList = new List<ICommand>();

            if(GamePad.GetState(player).Buttons.X == ButtonState.Pressed)
            {
                commandList.Add(_buttonX);
            }
            if (GamePad.GetState(player).Buttons.Y == ButtonState.Pressed)
            {
                commandList.Add(_buttonY);
            }
            if (GamePad.GetState(player).Buttons.A == ButtonState.Pressed)
            {
                commandList.Add(_buttonA);
            }
            if (GamePad.GetState(player).Buttons.B == ButtonState.Pressed)
            {
                commandList.Add(_buttonB);
            }

            _stickLeft.Direction = GamePad.GetState(player).ThumbSticks.Left;
            commandList.Add(_stickLeft);

            _stickRight.Direction = GamePad.GetState(player).ThumbSticks.Right;
            commandList.Add(_stickRight);

            return commandList;
        }
    }
}

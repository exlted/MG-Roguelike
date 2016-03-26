#region File Description

//-----------------------------------------------------------------------------
// InputState.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Roguelike
{
    /// <summary>
    ///    Helper for reading input from keyboard, gamepad, and touch input. This class
    ///    tracks both the current and previous state of the input devices, and implements
    ///    query methods for high level input actions such as "move up through the menu"
    ///    or "pause the game".
    /// </summary>
    public class InputState
    {
        public const int MaxInputs = 4;

        public readonly GamePadState[] CurrentGamePadStates;
        public readonly KeyboardState[] CurrentKeyboardStates;
        public readonly bool[] GamePadWasConnected;

        public readonly GamePadState[] LastGamePadStates;
        public readonly KeyboardState[] LastKeyboardStates;

        public InputState()
        {
            CurrentKeyboardStates = new KeyboardState[MaxInputs];
            CurrentGamePadStates = new GamePadState[MaxInputs];

            LastKeyboardStates = new KeyboardState[MaxInputs];
            LastGamePadStates = new GamePadState[MaxInputs];

            CurrentMouseState = new MouseState();
            LastMouseState = new MouseState();

            GamePadWasConnected = new bool[MaxInputs];
        }

        public MouseState CurrentMouseState
        {
            get;
            private set;
        }

        public MouseState LastMouseState
        {
            get;
            private set;
        }

        /// <summary>
        ///    Reads the latest state of the keyboard and gamepad.
        /// </summary>
        public void Update()
        {
            for (var _i = 0; _i < MaxInputs; _i++)
            {
                LastKeyboardStates[_i] = CurrentKeyboardStates[_i];
                LastGamePadStates[_i] = CurrentGamePadStates[_i];

                CurrentKeyboardStates[_i] = Keyboard.GetState();
                CurrentGamePadStates[_i] = GamePad.GetState((PlayerIndex)_i);

                // Keep track of whether a gamepad has ever been
                // connected, so we can detect if it is unplugged.
                if (CurrentGamePadStates[_i].IsConnected)
                {
                    GamePadWasConnected[_i] = true;
                }
            }

            LastMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }

        public bool IsNewLeftMouseClick(out MouseState mouseState)
        {
            mouseState = CurrentMouseState;
            return CurrentMouseState.LeftButton == ButtonState.Released && LastMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool IsNewRightMouseClick(out MouseState mouseState)
        {
            mouseState = CurrentMouseState;
            return CurrentMouseState.RightButton == ButtonState.Released && LastMouseState.RightButton == ButtonState.Pressed;
        }

        public bool IsNewThirdMouseClick(out MouseState mouseState)
        {
            mouseState = CurrentMouseState;
            return CurrentMouseState.MiddleButton == ButtonState.Pressed && LastMouseState.MiddleButton == ButtonState.Released;
        }

        public bool IsNewMouseScrollUp(out MouseState mouseState)
        {
            mouseState = CurrentMouseState;
            return CurrentMouseState.ScrollWheelValue > LastMouseState.ScrollWheelValue;
        }

        public bool IsNewMouseScrollDown(out MouseState mouseState)
        {
            mouseState = CurrentMouseState;
            return CurrentMouseState.ScrollWheelValue < LastMouseState.ScrollWheelValue;
        }

        public bool IsNewKeyPress(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                var _i = (int)playerIndex;

                return CurrentKeyboardStates[_i].IsKeyDown(key) && LastKeyboardStates[_i].IsKeyUp(key);
            }
            else
            {
                // Accept input from any player.
                return IsNewKeyPress(key, PlayerIndex.One, out playerIndex) || IsNewKeyPress(key, PlayerIndex.Two, out playerIndex)
                         || IsNewKeyPress(key, PlayerIndex.Three, out playerIndex) || IsNewKeyPress(key, PlayerIndex.Four, out playerIndex);
            }
        }

        public bool IsNewButtonPress(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                var _i = (int)playerIndex;

                return CurrentGamePadStates[_i].IsButtonDown(button) && LastGamePadStates[_i].IsButtonUp(button);
            }
            else
            {
                // Accept input from any player.
                return IsNewButtonPress(button, PlayerIndex.One, out playerIndex) || IsNewButtonPress(button, PlayerIndex.Two, out playerIndex)
                         || IsNewButtonPress(button, PlayerIndex.Three, out playerIndex) || IsNewButtonPress(button, PlayerIndex.Four, out playerIndex);
            }
        }

        public bool IsKeyPressed(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                var _i = (int)playerIndex;

                return CurrentKeyboardStates[_i].IsKeyDown(key);
            }
            else
            {
                // Accept input from any player.
                return IsKeyPressed(key, PlayerIndex.One, out playerIndex) || IsKeyPressed(key, PlayerIndex.Two, out playerIndex)
                         || IsKeyPressed(key, PlayerIndex.Three, out playerIndex) || IsKeyPressed(key, PlayerIndex.Four, out playerIndex);
            }
        }

        public bool IsButtonPressed(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                var _i = (int)playerIndex;

                return CurrentGamePadStates[_i].IsButtonDown(button);
            }
            else
            {
                // Accept input from any player.
                return IsButtonPressed(button, PlayerIndex.One, out playerIndex) || IsButtonPressed(button, PlayerIndex.Two, out playerIndex)
                         || IsButtonPressed(button, PlayerIndex.Three, out playerIndex) || IsButtonPressed(button, PlayerIndex.Four, out playerIndex);
            }
        }

        public bool IsExitGame(PlayerIndex? controllingPlayer)
        {
            PlayerIndex _playerIndex;

            return IsNewKeyPress(Keys.Escape, controllingPlayer, out _playerIndex) || IsNewButtonPress(Buttons.Back, controllingPlayer, out _playerIndex);
        }

        public bool IsLeft(PlayerIndex? controllingPlayer)
        {
            PlayerIndex _playerIndex;

            return IsNewButtonPress(Buttons.DPadLeft, controllingPlayer, out _playerIndex) || IsNewButtonPress(Buttons.LeftThumbstickLeft, controllingPlayer, out _playerIndex)
                || IsNewKeyPress(Keys.A, controllingPlayer, out _playerIndex);
        }

        public bool IsRight(PlayerIndex? controllingPlayer)
        {
            PlayerIndex _playerIndex;

            return IsNewButtonPress(Buttons.DPadRight, controllingPlayer, out _playerIndex)
                   || IsNewButtonPress(Buttons.LeftThumbstickRight, controllingPlayer, out _playerIndex) || IsNewKeyPress(Keys.D, controllingPlayer, out _playerIndex);
        }

        public bool IsUp(PlayerIndex? controllingPlayer)
        {
            PlayerIndex _playerIndex;

            return IsNewButtonPress(Buttons.DPadUp, controllingPlayer, out _playerIndex)
                   || IsNewButtonPress(Buttons.LeftThumbstickUp, controllingPlayer, out _playerIndex) || IsNewKeyPress(Keys.W, controllingPlayer, out _playerIndex);
        }

        public bool IsDown(PlayerIndex? controllingPlayer)
        {
            PlayerIndex _playerIndex;

            return IsNewButtonPress(Buttons.DPadDown, controllingPlayer, out _playerIndex)
                   || IsNewButtonPress(Buttons.LeftThumbstickDown, controllingPlayer, out _playerIndex) || IsNewKeyPress(Keys.S, controllingPlayer, out _playerIndex);
        }

        public bool IsSpace(PlayerIndex? controllingPlayer)
        {
            PlayerIndex _playerIndex;
            return IsNewKeyPress(Keys.Space, controllingPlayer, out _playerIndex);
        }

        public bool IsScrollLeft(PlayerIndex? controllingPlayer)
        {
            PlayerIndex _playerIndex;
            return IsKeyPressed(Keys.Left, controllingPlayer, out _playerIndex);
        }

        public bool IsScrollRight(PlayerIndex? controllingPlayer)
        {
            PlayerIndex _playerIndex;
            return IsKeyPressed(Keys.Right, controllingPlayer, out _playerIndex);
        }

        public bool IsScrollUp(PlayerIndex? controllingPlayer)
        {
            PlayerIndex _playerIndex;
            return IsKeyPressed(Keys.Up, controllingPlayer, out _playerIndex);
        }

        public bool IsScrollDown(PlayerIndex? controllingPlayer)
        {
            PlayerIndex _playerIndex;
            return IsKeyPressed(Keys.Down, controllingPlayer, out _playerIndex);
        }

        public bool IsZoomOut(PlayerIndex? controllingPlayer)
        {
            PlayerIndex _playerIndex;
            return (CurrentMouseState.ScrollWheelValue < LastMouseState.ScrollWheelValue) && IsKeyPressed(Keys.LeftControl, controllingPlayer, out _playerIndex);
        }

        public bool IsZoomIn(PlayerIndex? controllingPlayer)
        {
            PlayerIndex _playerIndex;
            return (CurrentMouseState.ScrollWheelValue > LastMouseState.ScrollWheelValue) && IsKeyPressed(Keys.LeftControl, controllingPlayer, out _playerIndex);
        }
    }
}
#region File Description
//-----------------------------------------------------------------------------
// InputState.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PL2D
{
    /// <summary>
    ///    Helper for reading input from keyboard, gamepad, and touch input. This class
    ///    tracks both the current and previous state of the input devices, and implements
    ///    query methods for high level input actions such as "move up through the menu"
    ///    or "pause the game".
    /// </summary>
    public class InputState
    {
        delegate bool InputType(Keys selectedKey, PlayerIndex? controllingPlayer);

        #region Fields&Update
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
            for (int i = 0; i < MaxInputs; i++)
            {
                LastKeyboardStates[i] = CurrentKeyboardStates[i];
                LastGamePadStates[i] = CurrentGamePadStates[i];

                CurrentKeyboardStates[i] = Keyboard.GetState();
                CurrentGamePadStates[i] = GamePad.GetState((PlayerIndex)i);

                // Keep track of whether a gamepad has ever been
                // connected, so we can detect if it is unplugged.
                if (CurrentGamePadStates[i].IsConnected)
                {
                    GamePadWasConnected[i] = true;
                }
            }

            LastMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }
        #endregion

        #region CheckButtonInput

        public bool IsNewLeftMouseClick(out MouseState mouseState)
        {
            mouseState = CurrentMouseState;
            return (CurrentMouseState.LeftButton == ButtonState.Released && LastMouseState.LeftButton == ButtonState.Pressed);
        }

        public bool IsNewRightMouseClick(out MouseState mouseState)
        {
            mouseState = CurrentMouseState;
            return (CurrentMouseState.RightButton == ButtonState.Released && LastMouseState.RightButton == ButtonState.Pressed);
        }

        public bool IsNewThirdMouseClick(out MouseState mouseState)
        {
            mouseState = CurrentMouseState;
            return (CurrentMouseState.MiddleButton == ButtonState.Pressed && LastMouseState.MiddleButton == ButtonState.Released);
        }

        public bool IsNewMouseScrollUp(out MouseState mouseState)
        {
            mouseState = CurrentMouseState;
            return (CurrentMouseState.ScrollWheelValue > LastMouseState.ScrollWheelValue);
        }

        public bool IsNewMouseScrollDown(out MouseState mouseState)
        {
            mouseState = CurrentMouseState;
            return (CurrentMouseState.ScrollWheelValue < LastMouseState.ScrollWheelValue);
        }

        public bool IsNewKeyPress(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                var i = (int)playerIndex;

                return (CurrentKeyboardStates[i].IsKeyDown(key) && LastKeyboardStates[i].IsKeyUp(key));
            }
            else
            {
                // Accept input from any player.
                return (IsNewKeyPress(key, PlayerIndex.One, out playerIndex) || IsNewKeyPress(key, PlayerIndex.Two, out playerIndex)
                         || IsNewKeyPress(key, PlayerIndex.Three, out playerIndex) || IsNewKeyPress(key, PlayerIndex.Four, out playerIndex));
            }
        }

        public bool IsNewButtonPress(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                var i = (int)playerIndex;

                return (CurrentGamePadStates[i].IsButtonDown(button) && LastGamePadStates[i].IsButtonUp(button));
            }
            else
            {
                // Accept input from any player.
                return (IsNewButtonPress(button, PlayerIndex.One, out playerIndex) || IsNewButtonPress(button, PlayerIndex.Two, out playerIndex)
                         || IsNewButtonPress(button, PlayerIndex.Three, out playerIndex) || IsNewButtonPress(button, PlayerIndex.Four, out playerIndex));
            }
        }

        public bool IsKeyPressed(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                var i = (int)playerIndex;

                return (CurrentKeyboardStates[i].IsKeyDown(key));
            }
            else
            {
                // Accept input from any player.
                return (IsKeyPressed(key, PlayerIndex.One, out playerIndex) || IsKeyPressed(key, PlayerIndex.Two, out playerIndex)
                         || IsKeyPressed(key, PlayerIndex.Three, out playerIndex) || IsKeyPressed(key, PlayerIndex.Four, out playerIndex));
            }
        }

        public bool IsButtonPressed(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                var i = (int)playerIndex;

                return (CurrentGamePadStates[i].IsButtonDown(button));
            }
            else
            {
                // Accept input from any player.
                return (IsButtonPressed(button, PlayerIndex.One, out playerIndex) || IsButtonPressed(button, PlayerIndex.Two, out playerIndex)
                         || IsButtonPressed(button, PlayerIndex.Three, out playerIndex) || IsButtonPressed(button, PlayerIndex.Four, out playerIndex));
            }
        }
        #endregion

        #region AllInputChecks

        public bool isKeyPressed(Keys selectedKey, PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsKeyPressed(selectedKey, controllingPlayer, out playerIndex);
        }

        public bool isNewKeyPressed(Keys selectedKey, PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsNewKeyPress(selectedKey, controllingPlayer, out playerIndex);
        }

        public bool isButtonPressed(Buttons selectedButton, PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsButtonPressed(selectedButton, controllingPlayer, out playerIndex);
        }

        public bool isNewButtonPressed(Buttons selectedButton, PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsNewButtonPress(selectedButton, controllingPlayer, out playerIndex);
        }

        #endregion
    }
}

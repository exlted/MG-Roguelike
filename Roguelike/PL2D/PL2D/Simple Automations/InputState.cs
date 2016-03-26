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

namespace PL2D
{
    /// <summary>
    ///    Helper for reading input from keyboard, gamepad, and touch input. This class
    ///    tracks both the current and previous State of the input devices, and implements
    ///    query methods for high level input actions such as "move up through the menu"
    ///    or "pause the game".
    /// </summary>
    public class InputState
    {
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
        ///    Reads the latest State of the keyboard and gamepad.
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

        #endregion Fields&Update

        #region CheckButtonInput

        /// <summary>
        /// Determines whether the specified key has been pressed since the last update
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="controllingPlayer">The controlling player.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Determines whether the specified button has been pressed since the last update
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="controllingPlayer">The controlling player.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Determines whether the specified key is pressed.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="controllingPlayer">The controlling player.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Determines whether the specified button is pressed
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="controllingPlayer">The controlling player.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns></returns>
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

        #endregion CheckButtonInput

        #region AllInputChecks

        /// <summary>
        /// Determines whether the specified key is pressed.
        /// </summary>
        /// <param name="selectedKey">The selected key.</param>
        /// <param name="controllingPlayer">The controlling player.</param>
        /// <returns></returns>
        public bool IsKeyPressed(Keys selectedKey, PlayerIndex? controllingPlayer)
        {
            PlayerIndex _playerIndex;

            return IsKeyPressed(selectedKey, controllingPlayer, out _playerIndex);
        }

        /// <summary>
        /// Determines whether the specified key has been pressed since the last update.
        /// </summary>
        /// <param name="selectedKey">The selected key.</param>
        /// <param name="controllingPlayer">The controlling player.</param>
        /// <returns></returns>
        public bool IsNewKeyPressed(Keys selectedKey, PlayerIndex? controllingPlayer)
        {
            PlayerIndex _playerIndex;

            return IsNewKeyPress(selectedKey, controllingPlayer, out _playerIndex);
        }

        /// <summary>
        /// Determines whether the specified selected button is pressed.
        /// </summary>
        /// <param name="selectedButton">The selected button.</param>
        /// <param name="controllingPlayer">The controlling player.</param>
        /// <returns></returns>
        public bool IsButtonPressed(Buttons selectedButton, PlayerIndex? controllingPlayer)
        {
            PlayerIndex _playerIndex;

            return IsButtonPressed(selectedButton, controllingPlayer, out _playerIndex);
        }

        /// <summary>
        /// Determines whether the specified selected button has been pressed since the last update.
        /// </summary>
        /// <param name="selectedButton">The selected button.</param>
        /// <param name="controllingPlayer">The controlling player.</param>
        /// <returns></returns>
        public bool IsNewButtonPressed(Buttons selectedButton, PlayerIndex? controllingPlayer)
        {
            PlayerIndex _playerIndex;

            return IsNewButtonPress(selectedButton, controllingPlayer, out _playerIndex);
        }

        /// <summary>
        /// Determines whether there has been a Left click since the last update.
        /// </summary>
        /// <param name="mouseState">State of the mouse.</param>
        /// <returns></returns>
        public bool IsNewLeftMouseClick(out MouseState mouseState)
        {
            mouseState = CurrentMouseState;
            return CurrentMouseState.LeftButton == ButtonState.Released && LastMouseState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Determines whether there has been a Right click since the last update.
        /// </summary>
        /// <param name="mouseState">State of the mouse.</param>
        /// <returns></returns>
        public bool IsNewRightMouseClick(out MouseState mouseState)
        {
            mouseState = CurrentMouseState;
            return CurrentMouseState.RightButton == ButtonState.Released && LastMouseState.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Determines whether the "third" button of the mouse has been Clicked since the last update
        /// </summary>
        /// <param name="mouseState">State of the mouse.</param>
        /// <returns></returns>
        public bool IsNewThirdMouseClick(out MouseState mouseState)
        {
            mouseState = CurrentMouseState;
            return CurrentMouseState.MiddleButton == ButtonState.Pressed && LastMouseState.MiddleButton == ButtonState.Released;
        }

        /// <summary>
        /// Determines whether the mouse has scrolled up since the last update.
        /// </summary>
        /// <param name="mouseState">State of the mouse.</param>
        /// <returns></returns>
        public bool IsNewMouseScrollUp(out MouseState mouseState)
        {
            mouseState = CurrentMouseState;
            return CurrentMouseState.ScrollWheelValue > LastMouseState.ScrollWheelValue;
        }

        /// <summary>
        /// Determines whether the mouse has scrolled down since the last update.
        /// </summary>
        /// <param name="mouseState">State of the mouse.</param>
        /// <returns></returns>
        public bool IsNewMouseScrollDown(out MouseState mouseState)
        {
            mouseState = CurrentMouseState;
            return CurrentMouseState.ScrollWheelValue < LastMouseState.ScrollWheelValue;
        }

        #endregion AllInputChecks
    }
}
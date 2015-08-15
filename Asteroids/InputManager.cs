using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Asteroids
{
    class InputManager
    {
        private static InputManager instance;
        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new InputManager();

                return instance;
            }
            set
            {
                instance = value;
            }
        }

        private List<PlayerIndex> players = new List<PlayerIndex>
            { PlayerIndex.One, PlayerIndex.Two, PlayerIndex.Three, PlayerIndex.Four };

        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;
        private Dictionary<PlayerIndex, GamePadState> currentGamePadState = new Dictionary<PlayerIndex, GamePadState>();
        private Dictionary<PlayerIndex, GamePadState> previousGamePadState = new Dictionary<PlayerIndex, GamePadState>();


        private InputManager()
        {
            currentKeyboardState = Keyboard.GetState();
            players.ForEach(p => currentGamePadState[p] = GamePad.GetState(p));
        }

        public void Update()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            players.ForEach(p => previousGamePadState[p] = currentGamePadState[p]);
            players.ForEach(p => currentGamePadState[p] = GamePad.GetState(p));
        }

        public bool KeyPressed(Keys key)
        {
            if (currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key))
                return true;

            return false;
        }

        public bool KeyDown(Keys key)
        {
            if (currentKeyboardState.IsKeyDown(key))
                return true;

            return false;
        }

        public bool KeyReleased(Keys key)
        {
            if (currentKeyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key))
                return false;

            return true;
        }

        public bool KeyUp(Keys key)
        {
            if (currentKeyboardState.IsKeyUp(key))
                return true;

            return false;
        }

        public bool GamePadButtonPressed(PlayerIndex playerIndex, Buttons button)
        {
            if (currentGamePadState[playerIndex].IsButtonDown(button) &&
                previousGamePadState[playerIndex].IsButtonUp(button))
                return true;

            return false;
        }

        public bool GamePadButtonDown(PlayerIndex playerIndex, Buttons button)
        {
            if (currentGamePadState[playerIndex].IsButtonDown(button))
                return true;

            return false;
        }

        public bool GamePadButtonReleased(PlayerIndex playerIndex, Buttons button)
        {
            if (currentGamePadState[playerIndex].IsButtonUp(button) &&
                previousGamePadState[playerIndex].IsButtonDown(button))
                return true;

            return false;
        }

        public bool GamePadButtonUp(PlayerIndex playerIndex, Buttons button)
        {
            if (currentGamePadState[playerIndex].IsButtonUp(button))
                return true;

            return false;
        }

        public Vector2 GamePadLeftThumbStick(PlayerIndex playerIndex)
        {
            return currentGamePadState[playerIndex].ThumbSticks.Left;
        }

        public Vector2 GamePadRightThumbStick(PlayerIndex playerIndex)
        {
            return currentGamePadState[playerIndex].ThumbSticks.Right;
        }

    }
}

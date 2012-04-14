using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HorrorMill.HorrorMill.Helpers.Xna.Inputs
{
    /// <summary>
    /// Class that contains functionality for handling user input
    /// </summary>
    public class InputHandler : GameComponent
    {
        // the keyboard is useful for debugging purposes
        public static KeyboardState KeyboardState { get; private set; }
        public static KeyboardState PreviousKeyboardState { get; private set; }

        public InputHandler(Game game) : base(game)
        {
            KeyboardState = Keyboard.GetState();
        }

        public override void Update(GameTime gameTime)
        {
            PreviousKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();
            base.Update(gameTime);
        }

        /// <summary>
        /// Flush the input buffer
        /// </summary>
        public static void Flush()
        {
            PreviousKeyboardState = KeyboardState;
        }

        public static bool KeyReleased(Keys key)
        {
            return KeyboardState.IsKeyUp(key) && PreviousKeyboardState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return KeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyUp(key);
        }

        public static bool KeyDown(Keys key)
        {
            return KeyboardState.IsKeyDown(key);
        }

    }
}
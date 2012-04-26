using System;
using HorrorMill.HorrorMill.Helpers.Xna.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMill.Helpers.Xna.UI
{
    public class AttackControl : DrawableGameComponent
    {
        private GameInput gameInput;

        private Texture2D controlTexture;
        private SpriteBatch spriteBatch;
        private Rectangle BasicAttackControlRectangle = new Rectangle(730, 350, 60, 60);
        private int millisecondsBetweenAttacks;
        private int millisecondsSinceLastAttack;

        public bool Attacking { get; set; }

        public bool buttonIsPressed;
        public bool buttonWasPressed;

        public AttackControl(Game game, GameInput gameInput) : base(game)
        {
            this.gameInput = gameInput;
            AddInputControls();
            millisecondsBetweenAttacks = 500;
            millisecondsSinceLastAttack = 500;
        }

        private void AddInputControls()
        {
            gameInput.AddTouchTapInput("BasicAttack", BasicAttackControlRectangle, false);
        }
        
        protected override void LoadContent()
        {
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            controlTexture = new Texture2D(Game.GraphicsDevice, 1, 1);
            controlTexture.SetData(new Color[] { new Color(1f, 1f, 1f, 0.5f) });
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Attacking = false;
            // Since they are touch taps, they will be in a given specific position,
            // so they will only affect one rectangle! (Refactor)
            buttonWasPressed = buttonIsPressed;
            buttonIsPressed = gameInput.IsPressed("BasicAttack");
            PerformPeriodicAttack(gameTime);

            base.Update(gameTime);
        }

        private void PerformPeriodicAttack(GameTime gameTime)
        {
            millisecondsSinceLastAttack += gameTime.ElapsedGameTime.Milliseconds;
            if (millisecondsSinceLastAttack >= millisecondsBetweenAttacks)
                if (buttonIsPressed)
                {
                    millisecondsSinceLastAttack = 0;
                    Attacking = true;
                }
        }

        private void PerformInmediateAttackWhenClickAndPeriodicAttackWhenHold(GameTime gameTime)
        {
            if (buttonWasPressed && !buttonIsPressed)
            {
                Attacking = true; // this is a single tap (like a single click)
                millisecondsSinceLastAttack = millisecondsBetweenAttacks;
            }
            else if (buttonWasPressed && buttonIsPressed)
            {
                // user is holding the finger on the control
                millisecondsSinceLastAttack += gameTime.ElapsedGameTime.Milliseconds;
                if (millisecondsSinceLastAttack >= millisecondsBetweenAttacks)
                {
                    Attacking = true;
                    millisecondsSinceLastAttack = 0;
                }
            }
            else
                millisecondsSinceLastAttack = millisecondsBetweenAttacks;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Draw(controlTexture, BasicAttackControlRectangle, Color.White);
        }

    }
}
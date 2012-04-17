﻿using HorrorMill.HorrorMill.Helpers.Xna.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMill.Helpers.Xna.UI
{
    public class CrossControl : DrawableGameComponent
    {
        private GameInput gameInput;

        private Texture2D controlTexture;
        private Rectangle LeftControlRectangle = new Rectangle(10, 350, 60, 60);
        private Rectangle DownControlRectangle = new Rectangle(70, 410, 60, 60);
        private Rectangle UpControlRectangle = new Rectangle(70, 290, 60, 60);
        private Rectangle RightControlRectangle = new Rectangle(130, 350, 60, 60);
        private SpriteBatch spriteBatch;

        private Vector2 motion;
        public Vector2 Motion { get { return motion; } set { motion = value; } }

        public CrossControl(Game game) : base(game)
        {
            gameInput = new GameInput();
            AddInputControls();
        }

        private void AddInputControls()
        {
            gameInput.AddTouchTapInput("MoveLeft", LeftControlRectangle, false);
            gameInput.AddTouchTapInput("MoveDown", DownControlRectangle, false);
            gameInput.AddTouchTapInput("MoveUp", UpControlRectangle, false);
            gameInput.AddTouchTapInput("MoveRight", RightControlRectangle, false);
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
            motion = Vector2.Zero;

            gameInput.BeginUpdate();
            if (gameInput.IsPressed("MoveLeft"))
                motion.X--;
            else if (gameInput.IsPressed("MoveRight"))
                motion.X++;

            if (gameInput.IsPressed("MoveUp"))
                motion.Y--;
            else if (gameInput.IsPressed("MoveDown"))
                motion.Y++;
            gameInput.EndUpdate();
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Draw(controlTexture, RightControlRectangle, Color.White);
            spriteBatch.Draw(controlTexture, LeftControlRectangle, Color.White);
            spriteBatch.Draw(controlTexture, DownControlRectangle, Color.White);
            spriteBatch.Draw(controlTexture, UpControlRectangle, Color.White);
        }




    }
}
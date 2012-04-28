using System;
using HorrorMill.Helpers.Xna.Entities;
using HorrorMill.HorrorMill.Helpers.Xna.Inputs;
using Microsoft.Xna.Framework;

namespace HorrorMill.Helpers.Xna.UI
{
    public class MenuItem : Control
    {
        private string text;
        private Font font;       

        private Rectangle clickableArea;
        protected override Rectangle ClickableArea { get { return clickableArea; }}


        private string ActionName { get { return "Action" + text; } }
        public override GameInput GameInput
        {
            get
            {
                return base.GameInput;
            }
            set
            {
                base.GameInput = value;
                GameInput.AddTouchTapInput(ActionName, ClickableArea, false);
            }
        }
        
        public MenuItem(Game game, String text, Vector2 position, Color color) : base(game)
        {
            this.text = text;
            font = new Font(game, text, position, color);
        }

        public override void Initialize()
        {
            font.Initialize();
            clickableArea = font.GetRectangle();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (GameInput.IsPressed(ActionName))
                RaiseClicked();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            font.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}

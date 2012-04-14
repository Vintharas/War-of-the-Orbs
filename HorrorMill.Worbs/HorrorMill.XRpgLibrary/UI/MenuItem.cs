using System;
using HorrorMill.Helpers.Xna.Entities;
using Microsoft.Xna.Framework;

namespace HorrorMill.Helpers.Xna.UI
{
    public class MenuItem : Control
    {
        private Font font;       
        private Rectangle clickableArea;
        protected override Rectangle ClickableArea
        {
            get { return clickableArea; }
        }

        public event Action Clicked;

        public MenuItem(Game game, String text, Vector2 position, Color color) : base(game)
        {
            font = new Font(game, text, position, color);
        }

        public override void Initialize()
        {
            font.Initialize();
            clickableArea = font.GetRectangle();
            base.Initialize();
        }

        public void CheckClick(Vector2 click)
        {
            Rectangle rectangleClick = new Rectangle((int)click.X, (int)click.Y, 1, 1);

            if (clickableArea.Intersects(rectangleClick) && this.Clicked != null)
            {
                this.Clicked();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            font.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}

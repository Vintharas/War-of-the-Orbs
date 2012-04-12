using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMill.Worbs.Entities
{
    class MenuItem : DrawableGameComponent
    {
        Font font;       
        Rectangle menuItemRectangle;
        public event Action Clicked;

        public MenuItem(Game game, String text, Vector2 position, Color color) : base(game)
        {
            font = new Font(game, text, position, color);
        }

        public override void Initialize()
        {
            font.Initialize();
            base.Initialize();
        }

        public void CheckClick(Vector2 click)
        {
            menuItemRectangle = font.GetRectangle();
            Rectangle rectangleClick = new Rectangle((int)click.X, (int)click.Y, 1, 1);

            if (menuItemRectangle.Intersects(rectangleClick) && this.Clicked != null)
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

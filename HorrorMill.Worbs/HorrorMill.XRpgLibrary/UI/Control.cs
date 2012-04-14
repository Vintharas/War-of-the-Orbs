using System;
using Microsoft.Xna.Framework;

namespace HorrorMill.Helpers.Xna.UI
{
    public abstract class Control : DrawableGameComponent
    {
        protected abstract Rectangle ClickableArea { get; }
        public event Action Clicked;

        protected Control(Game game) : base(game)
        {
        }

        public virtual void CheckClick(Vector2 click)
        {
            Rectangle rectangleClick = new Rectangle((int)click.X, (int)click.Y, 1, 1);
            if (ClickableArea.Intersects(rectangleClick) && this.Clicked != null)
            {
                this.Clicked();
            }
        }
    }
}
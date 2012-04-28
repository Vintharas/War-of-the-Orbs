using System;
using HorrorMill.HorrorMill.Helpers.Xna.Inputs;
using Microsoft.Xna.Framework;

namespace HorrorMill.Helpers.Xna.UI
{
    public abstract class Control : DrawableGameComponent
    {
        protected abstract Rectangle ClickableArea { get; }
        public event Action Clicked;

        public virtual GameInput GameInput { get; set; }


        protected Control(Game game) : base(game)
        {
        }

        protected void RaiseClicked()
        {
            if (Clicked != null) Clicked();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using HorrorMill.HorrorMill.Helpers.Xna.Inputs;
using Microsoft.Xna.Framework;

namespace HorrorMill.Helpers.Xna.UI
{
    public class GameControls : DrawableGameComponent
    {
        private readonly GameInput gameInput;
        public CrossControl CrossControl { get; private set; }
        public AttackControl AttackControl { get; private set; }
        private List<DrawableGameComponent> controls;

        public GameControls(Game game) : base(game)
        {
            gameInput = new GameInput();
            CrossControl = new CrossControl(game, gameInput);
            AttackControl = new AttackControl(game, gameInput);
            controls = new List<DrawableGameComponent> {CrossControl, AttackControl};
        }

        public void AddControl(Control control)
        {
            controls.Add(control);
        }

        public override void Initialize()
        {
            foreach (var c in controls)
                c.Initialize();
            foreach (var c in controls.OfType<Control>())
                c.GameInput = gameInput;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            gameInput.BeginUpdate();
            foreach (var c in controls)
                c.Update(gameTime);
            gameInput.EndUpdate();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var c in controls)
                c.Draw(gameTime);
            base.Draw(gameTime);
        }


    }
}
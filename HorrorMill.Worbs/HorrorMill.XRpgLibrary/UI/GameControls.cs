using System.Collections.Generic;
using HorrorMill.HorrorMill.Helpers.Xna.Inputs;
using Microsoft.Xna.Framework;

namespace HorrorMill.Helpers.Xna.UI
{
    public class GameControls : DrawableGameComponent
    {
        private readonly GameInput gameInput;
        public CrossControl CrossControl { get; private set; }
        public AttackControl AttackControl { get; private set; }
        public List<DrawableGameComponent> controls;

        public GameControls(Game game) : base(game)
        {
            gameInput = new GameInput();
            CrossControl = new CrossControl(game, gameInput);
            AttackControl = new AttackControl(game, gameInput);
            controls = new List<DrawableGameComponent> {CrossControl, AttackControl};
        }

        public override void Initialize()
        {
            foreach (var c in controls)
                c.Initialize();
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
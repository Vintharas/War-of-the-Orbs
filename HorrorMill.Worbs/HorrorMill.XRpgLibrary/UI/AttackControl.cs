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

        public bool Attacking = false; //TODO make property

        public AttackControl(Game game)
            : base(game)
        {
            gameInput = new GameInput();
            AddInputControls();
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
            gameInput.BeginUpdate();
            // Since they are touch taps, they will be in a given specific position,
            // so they will only affect one rectangle! (Refactor)
            Attacking = gameInput.IsPressed("BasicAttack");
            
            gameInput.EndUpdate();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Draw(controlTexture, BasicAttackControlRectangle, Color.White);
        }

    }
}
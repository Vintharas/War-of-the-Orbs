using HorrorMill.HorrorMill.Helpers.Xna.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMill.Helpers.Xna.UI
{
    public class GraphicButton : Control
    {

        private string textureName;
        private Texture2D texture;

        private SpriteBatch spriteBatch;

        private Vector2 position;

        private string ClickActionName { get { return "ClickOn" + textureName; } }
        protected override Rectangle ClickableArea { get { return clickableArea; } }
        private Rectangle clickableArea;

        public override GameInput GameInput
        {
            get { return base.GameInput; }
            set 
            { 
                base.GameInput = value;
                // Add user input
                GameInput.AddTouchTapInput(ClickActionName, ClickableArea, false);
            }
        }

        public GraphicButton(Game game, string textureName, Vector2 position) : base(game)
        {
            this.textureName = textureName;
            this.position = position;
        }

        protected override void LoadContent()
        {
            spriteBatch = (SpriteBatch) Game.Services.GetService(typeof (SpriteBatch));
            texture = Game.Content.Load<Texture2D>(textureName);
            clickableArea = new Rectangle((int) position.X, (int) position.Y, texture.Width, texture.Height);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (GameInput.IsPressed(ClickActionName))
                RaiseClicked();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(texture, position, Color.White);
            base.Draw(gameTime);
        }


    }
}
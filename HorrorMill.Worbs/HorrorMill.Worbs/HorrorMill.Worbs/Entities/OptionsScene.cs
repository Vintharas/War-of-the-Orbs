using HorrorMill.Helpers.Xna.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMill.Worbs.Entities
{
    public class OptionsScene : Scene
    {
        private SpriteBatch spriteBatch;

        public OptionsScene(Game game): base(game)
        {
            Font gameTitle = new Font(game, "This is the Options!", new Vector2(0, 0), Color.Red);
            SceneComponents.Add(gameTitle);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
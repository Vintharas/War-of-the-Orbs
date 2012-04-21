using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMill.Helpers.Xna.Sprites
{
    public class NonAnimatedSprite : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;

        private string assetName;
        private Vector2 position;
        private DrawablePosition fixedPosition;
        private Texture2D texture;

        private int WindowWidth { get { return Game.GraphicsDevice.Viewport.Width; } }
        private int WindowHeight { get { return Game.GraphicsDevice.Viewport.Height; } }

        public NonAnimatedSprite(Game game, string assetName, Vector2 position, SpriteBatch spriteBatch) : base(game)
        {
            this.assetName = assetName;
            this.position = position;
            this.spriteBatch = spriteBatch;
        }

        public NonAnimatedSprite(Game game, string assetName, DrawablePosition position, SpriteBatch spriteBatch) : base(game)
        {
            this.assetName = assetName;
            this.fixedPosition = position;
            this.spriteBatch = spriteBatch;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>(assetName);
            if (fixedPosition != DrawablePosition.None)
                position = GetFixedPosition();
            base.LoadContent();
        }

        /// <summary>
        /// Get position from SpritePosition enum based on the size of the sprite
        /// </summary>
        /// <returns></returns>
        private Vector2 GetFixedPosition()
        {
            switch (fixedPosition)
            {
                case DrawablePosition.Centered:
                    return new Vector2(WindowWidth/2 - texture.Width/2,
                        WindowHeight/2 - texture.Height/2);
                default:
                    return Vector2.Zero;
                case DrawablePosition.TopCentered:
                    return new Vector2(WindowWidth/2 - texture.Width/2, 0);
            }
        }

        /// <summary>
        /// Draw non animated s
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
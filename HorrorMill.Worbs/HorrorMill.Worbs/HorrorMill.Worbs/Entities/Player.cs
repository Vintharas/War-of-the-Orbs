using HorrorMill.Engines.TileEngine.Entities;
using Microsoft.Xna.Framework;

namespace HorrorMill.Worbs.Entities
{
    public class Player : DrawableGameComponent
    {
        public Camera Camera { get; set; }

        public Player(Game game) : base(game)
        {
            Camera = new Camera(game, game.GraphicsDevice.Viewport.Bounds);
        }

        public Player(Game game, Camera camera) : base(game)
        {
            Camera = camera;
        }

        public override void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
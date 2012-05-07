using HorrorMill.Engines.TileEngine.Entities;
using Microsoft.Xna.Framework;

namespace HorrorMill.Engines.Rpg
{
    public class Level : DrawableGameComponent
    {
        private readonly TileMap map;

        public Level(Game game, TileMap map) : base(game)
        {
            this.map = map;
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
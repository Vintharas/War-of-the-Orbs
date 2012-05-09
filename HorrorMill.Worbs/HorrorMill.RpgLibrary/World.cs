using System.Collections.Generic;
using HorrorMill.Engines.TileEngine;
using Microsoft.Xna.Framework;

namespace HorrorMill.Engines.Rpg
{
    public class World : DrawableGameComponent
    {
        public Dictionary<string, Level> Levels { get; private set; }
        public Level CurrentLevel { get; protected set; }

        private TheTileEngine engine = new TheTileEngine(32, 32);

        public World(Game game) : base(game)
        {
            Levels = new Dictionary<string, Level>();
        }

        public override void Initialize()
        {
            foreach (var level in Levels.Values)
                level.Initialize();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            CurrentLevel.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            CurrentLevel.Draw(gameTime);
            base.Draw(gameTime);
        }

        protected void AddLevel(Level level)
        {
            Levels[level.Name] = level;
            if (CurrentLevel == null)
                CurrentLevel = level;
        }




        
    }
}
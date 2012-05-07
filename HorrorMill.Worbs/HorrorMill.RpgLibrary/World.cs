using System.Collections.Generic;
using HorrorMill.Engines.Rpg.Entities;
using HorrorMill.Engines.TileEngine;
using HorrorMill.Engines.TileEngine.Entities;
using HorrorMill.Helpers.Xna.UI;
using Microsoft.Xna.Framework;

namespace HorrorMill.Engines.Rpg
{
    public class World : DrawableGameComponent
    {
        public Dictionary<string, Level> Levels { get; private set; }


        public World(Game game) : base(game)
        {
            Levels = new Dictionary<string, Level>();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        
    }
}
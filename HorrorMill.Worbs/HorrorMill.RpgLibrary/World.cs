﻿using System.Collections.Generic;
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


        // testing custom tile TheTileEngine
        private TheTileEngine engine = new TheTileEngine(32, 32);
        private TileMap map;
        private Player player;
        private Camera camera;

        private GameControls controls;
        private CrossControl CrossControl { get { return controls.CrossControl; } }
        private AttackControl AttackControl { get { return controls.AttackControl; } }
        private HealthBar playerHealthBar;

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
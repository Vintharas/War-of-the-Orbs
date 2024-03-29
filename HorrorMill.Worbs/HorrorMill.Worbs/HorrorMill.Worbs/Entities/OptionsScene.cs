﻿using System;
using System.Collections.Generic;
using HorrorMill.Engines.TileEngine;
using HorrorMill.Engines.TileEngine.Entities;
using HorrorMill.Helpers.Xna.Entities;
using Microsoft.Xna.Framework;

namespace HorrorMill.Worbs.Entities
{
    public class OptionsScene : Scene
    {

        public OptionsScene(Game game): base(game)
        {
            Font optionsText = new Font(game, "This is the Options!", new Vector2(0, 0), Color.Red);
            // Player
            SceneComponents.Add(optionsText);
        }

    }
}
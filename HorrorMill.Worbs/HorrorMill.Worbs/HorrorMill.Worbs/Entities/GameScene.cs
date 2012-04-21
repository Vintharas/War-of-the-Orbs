using System;
using System.Collections.Generic;
using HorrorMill.Engines.TileEngine;
using HorrorMill.Engines.TileEngine.Entities;
using HorrorMill.Helpers.Xna.Entities;
using HorrorMill.Helpers.Xna.UI;
using HorrorMill.HorrorMill.Helpers.Xna.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using HorrorMill.Engines.Rpg;

namespace HorrorMill.Worbs.Entities
{
    public class GameScene : Scene
    {
        // testing custom tile engine
        private Engine engine = new Engine(32, 32);
        private TileMap map;
        private Player player;
        private Camera camera;
        private CrossControl crossControl;

        public GameScene(Game game): base(game)
        {
            // Initialize map settings
            camera = new Camera(game, new Rectangle(0, 0, 800, 480)); // TODO: Fix this so it is not hardcoded -> GraphicsDevice is not initialized at this point, need to wrap it somehow (perhaps add it as a service) so the camera will access it later when it's initialized 

            MapInformation mapInformation = new MapInformation(Game);
            mapInformation.LoadMapFromXML("test map");
            map = MapGenerator.Generate(mapInformation, camera);

            Font gameTitle = new Font(game, "Generated map", new Vector2(0, 0), Color.Black);

            crossControl = new CrossControl(game);

            // Player
            player = new Player(game, camera);
            SceneComponents.Add(map);
            SceneComponents.Add(gameTitle);
            SceneComponents.Add(player);
            SceneComponents.Add(crossControl);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            camera.MoveCamera(crossControl.Motion);
        }


    }
}
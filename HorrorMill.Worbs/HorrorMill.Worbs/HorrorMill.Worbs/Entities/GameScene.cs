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

            // tilesets
            TileSet tileSet1 = new TileSet(Game, @"TileSheets\tileset1", 8, 8, 32, 32);
            TileSet tileSet2 = new TileSet(Game, @"TileSheets\tileset2", 8, 8, 32, 32);
            List<TileSet> tileSets = new List<TileSet> {tileSet1, tileSet2};

            // map layers
            MapLayer grass = new MapLayer(40, 40);
            for (int y = 0; y < grass.Height; y++)
                for (int x = 0; x < grass.Width; x++)
                    grass[x, y] = new Tile(0, 0);
            MapLayer splatter = new MapLayer(40, 40);
            Random random = new Random();
            for (int i = 0; i < 80; i++)
            {
                int x = random.Next(0, 40);
                int y = random.Next(0, 40);
                int tileIndex = random.Next(2, 14);
                Tile tile = new Tile(tileIndex, 0); // tileSet: 0
                splatter[x, y] = tile;
            }
            splatter[1, 0] = new Tile(0, 1);
            splatter[2, 0] = new Tile(2, 1);
            splatter[3, 0] = new Tile(0, 1);
            List<MapLayer> mapLayers = new List<MapLayer>{grass, splatter};

            map = new TileMap(Game, tileSets, mapLayers, camera);

            Font gameTitle = new Font(game, "This is the Game Scene!", new Vector2(0, 0), Color.Red);

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
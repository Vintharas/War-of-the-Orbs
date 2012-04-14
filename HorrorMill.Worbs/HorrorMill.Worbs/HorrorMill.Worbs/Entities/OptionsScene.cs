using HorrorMill.Engines.TileEngine;
using HorrorMill.Engines.TileEngine.Entities;
using HorrorMill.Helpers.Xna.Entities;
using Microsoft.Xna.Framework;

namespace HorrorMill.Worbs.Entities
{
    public class OptionsScene : Scene
    {
        // testing custom tile engine
        private Engine engine = new Engine(32, 32);
        private TileSet tileSet;
        private TileMap map;

        public OptionsScene(Game game): base(game)
        {
            // Initialize map settings
            tileSet = new TileSet(Game, @"TileSheets\tileset1", 8, 8, 32, 32);
            MapLayer layer = new MapLayer(40, 40);
            for (int y = 0; y < layer.Height; y++)
                for (int x = 0; x < layer.Width; x++)
                    layer[x, y] = new Tile(0, 0);
            map = new TileMap(Game, tileSet, layer);
            Font gameTitle = new Font(game, "This is the Options!", new Vector2(0, 0), Color.Red);
            SceneComponents.Add(map);
            SceneComponents.Add(gameTitle);
        }

    }
}
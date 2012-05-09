using HorrorMill.Engines.Rpg;
using HorrorMill.Engines.Rpg.Entities;
using HorrorMill.Engines.Rpg.Maps;
using HorrorMill.Engines.TileEngine.Entities;
using Microsoft.Xna.Framework;

namespace HorrorMill.Worbs.Entities
{
    public class WorbsWorld : World
    {
        private Player player;
        private Camera camera;

        public WorbsWorld(Game game, Player player) : base(game)
        {
            this.player = player;
            camera = player.Camera;
            AddFirstLevel();
        }

        private void AddFirstLevel()
        {
            // Load all level information from XML: map and enemies.
            MapInformation mapInformation = new MapInformation(Game);
            mapInformation.LoadMapFromXML("test map");
            MapGenerator mapGen = new MapGenerator(Game);
            TileMap map = mapGen.Generate(mapInformation, camera);
            // Create a level from that xml level information
            Level firstLevel = new Level(Game, "First Island", map, mapGen.Enemies, player);
            AddLevel(firstLevel);
        }
    }
}
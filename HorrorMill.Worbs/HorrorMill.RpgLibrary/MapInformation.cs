using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using HorrorMill.Engines.TileEngine.Entities;

namespace HorrorMill.Engines.Rpg
{
    public class MapInformation
    {
        public List<TileSet> TileSets;
        //public List<EnemyInfo> Enemies;
        public int NumberOfDecorativeObjects;
        public int TileWidth;
        public int TileHeight;
        private Game baseGame;
        public enum MapLayerEnum { Ground, Decorations, Enemies, Sky };

        public MapInformation(Game game)
        {
            baseGame = game; 

            TileSets = new List<TileSet>();
            //Enemies = new List<Enemy>();

            NumberOfDecorativeObjects = 80;
            TileWidth = 40;
            TileHeight = 40;
        }

        public void LoadMapFromXML(string mapName)
        {
            this.TileSets.Add(new TileSet(baseGame, @"TileSheets\tileset1", 8, 8, 32, 32));
            this.TileSets.Add(new TileSet(baseGame, @"TileSheets\tileset2", 8, 8, 32, 32));

            //AddEnemies(30, hp, dmg, sprite)
            //AddEnemies(1, hp, dmg, sprite)

            //Add bosses here
            //Add number of enemies here
        }
    }
}

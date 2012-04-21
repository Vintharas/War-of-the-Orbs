using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using HorrorMill.Engines.TileEngine.Entities;

namespace HorrorMill.Engines.Rpg
{
    public class MapInformation
    {
        public List<TileSet> TileSets;
        public List<Enemy> Enemies;
        public int NumberOfDecorativeObjects;
        public int Width;
        public int Height;
        private Game baseGame;
        public enum MapLayerEnum { Ground, Decorations, Enemies, Sky };

        public MapInformation(Game game)
        {
            baseGame = game; 

            TileSets = new List<TileSet>();
            Enemies = new List<Enemy>();

            NumberOfDecorativeObjects = 80;
            Width = 40;
            Height = 40;
        }

        public void LoadMapFromXML(string mapName)
        {
            this.TileSets.Add(new TileSet(baseGame, @"TileSheets\tileset1", 8, 8, 32, 32));
            this.TileSets.Add(new TileSet(baseGame, @"TileSheets\tileset2", 8, 8, 32, 32));
            this.Enemies.Add(new Enemy("Big Boss", 100, 25));
        }
    }
}

using System;
using System.Collections.Generic;
using HorrorMill.Engines.Rpg.Entities;
using Microsoft.Xna.Framework;
using HorrorMill.Engines.TileEngine.Entities;
using HorrorMill.Helpers.Xna.Sprites;

namespace HorrorMill.Engines.Rpg
{
    public class MapInformation
    {
        public List<TileSet> TileSets;
        public List<EnemyInformation> Enemies;
        public int NumberOfDecorativeObjects;
        public int TileWidth;
        public int TileHeight;
        private Game baseGame;
        public enum MapLayerEnum { Ground, Decorations, Enemies, Sky };

        public MapInformation(Game game)
        {
            baseGame = game; 

            TileSets = new List<TileSet>();
            Enemies = new List<EnemyInformation>();

            NumberOfDecorativeObjects = 80;
            TileWidth = 40;
            TileHeight = 40;
        }

        public void LoadMapFromXML(string mapName)
        {
            //Add map tiles
            this.TileSets.Add(new TileSet(baseGame, @"TileSheets\tileset1", 8, 8, 32, 32));
            this.TileSets.Add(new TileSet(baseGame, @"TileSheets\tileset2", 8, 8, 32, 32));

            //Add enemies
            for (int i = 0; i < 30; i++)
            {
                EnemyInformation e = new EnemyInformation("wizard", 100, 20);
                e.SpriteIdleDown = new SpriteSheet("SpriteSheets/Enemy/RedWizard/IdleDown", new Point(0, 0), new Point(50, 50), new Point(1, 1), SpriteDirection.Right);
                e.SpriteIdleUp = new SpriteSheet("SpriteSheets/Enemy/RedWizard/IdleUp", new Point(0, 0), new Point(50, 50), new Point(1, 1), SpriteDirection.Right);
                e.SpriteIdleRight = new SpriteSheet("SpriteSheets/Enemy/RedWizard/IdleRight", new Point(0, 0), new Point(50, 50), new Point(1, 1), SpriteDirection.Right);
                e.SpriteIdleLeft = new SpriteSheet("SpriteSheets/Enemy/RedWizard/IdleLeft", new Point(0, 0), new Point(50, 50), new Point(1, 1), SpriteDirection.Right);
                e.SpriteWalk = new SpriteSheet("SpriteSheets/Enemy/RedWizard/Walk", new Point(0, 0), new Point(50, 50), new Point(2, 1), SpriteDirection.Right);
                e.SpriteWalkUp = new SpriteSheet("SpriteSheets/Enemy/RedWizard/WalkUp", new Point(0, 0), new Point(50, 50), new Point(2, 1), SpriteDirection.Right);
                e.SpriteWalkDown = new SpriteSheet("SpriteSheets/Enemy/RedWizard/WalkDown", new Point(0, 0), new Point(50, 50), new Point(2, 1), SpriteDirection.Right);
                Enemies.Add(e);
            }

            //Add bosses here
            //Add number of enemies here
        }
    }
}

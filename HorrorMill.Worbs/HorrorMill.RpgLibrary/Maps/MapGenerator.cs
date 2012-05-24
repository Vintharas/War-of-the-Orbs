using System;
using System.Collections.Generic;
using HorrorMill.Engines.Rpg.Entities;
using HorrorMill.Engines.TileEngine.Entities;
using Microsoft.Xna.Framework;

namespace HorrorMill.Engines.Rpg.Maps
{
    public class MapGenerator
    {
        private Game game;
        private List<Enemy> enemies;

        public MapGenerator(Game game)
        {
            this.game = game;
            enemies = new List<Enemy>();
        }

        public TileMap Generate(MapInformation mapInformation, Camera camera)
        {
            Random random = new Random();

            //Create map
            List<MapLayer> mapLayers = new List<MapLayer>();

            //Ground
            MapLayer ground = new MapLayer(mapInformation.TileWidth, mapInformation.TileHeight);
            for (int y = 0; y < ground.Height; y++)
                for (int x = 0; x < ground.Width; x++)
                {
                    int grassTile = random.Next(0, 2);
                    ground[x, y] = new Tile(grassTile, 0);
                }

            //Decoration
            bool collision = false;
            MapLayer decoration = new MapLayer(40, 40);
            MapLayer decorationTop = new MapLayer(40, 40);
            for (int i = 0; i < mapInformation.NumberOfDecorativeObjects; i++)
            {
                int x = random.Next(0, 40);
                int y = random.Next(0, 40);
                //TODO ADD CHECK SO NO OVERWRITE
                int tileIndex = random.Next(3, 15);
                
                if(tileIndex == 14) //Add tree 
                {
                    Tile treeTile = new Tile(tileIndex, 0); // tileSet: 0
                    if (y > 0)
                        decorationTop[x, y - 1] = treeTile;
                    tileIndex = 15; //to get the foot below
                }

                if (tileIndex == 15)
                    collision = true;
                else
                    collision = false;

                Tile tile = new Tile(tileIndex, 0, collision); // tileSet: 0
                decoration[x, y] = tile;
            }

            mapLayers.Insert(0, ground);
            mapLayers.Insert(1, decoration);
            mapLayers.Insert(2, decorationTop);

            //Add player position
            //TODO make sure it is created along water at the sides
            int playerX = random.Next(1, (mapInformation.TileWidth * 32)-32); //TODO have better calc
            int playerY = random.Next(1, (mapInformation.TileHeight * 32)-32); //TODO have better calc
            camera.Position = new Vector2((float)150, (float)150);
            //camera.Position = new Vector2((float)playerX, (float)playerY);

            //Create enemies
            foreach (EnemyInformation ei in mapInformation.Enemies)
            {
                Enemy enemy = new Enemy(game);                          //TODO dont add enemies to close to the player
                int enemyX = random.Next(1, (mapInformation.TileWidth * 32) - 32); //TODO have better calc
                int enemyY = random.Next(1, (mapInformation.TileWidth * 32) - 32); //TODO have better calc
                enemy.Create(ei, new Vector2((float)enemyX, (float)enemyY));
                enemies.Add(enemy);
            }

            return new TileMap(camera.Game, mapInformation.TileSets, mapLayers, camera);
        }

        public List<Enemy> Enemies { get { return enemies; } }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using HorrorMill.Engines.TileEngine.Entities;

namespace HorrorMill.Engines.Rpg
{
    public class MapGenerator
    {
        public static TileMap Generate(MapInformation mapInformation, Camera camera)
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
                    //ground[x, y] = new Tile(grassTile, 0);
                    ground[x, y] = new Tile(0, 0);
                }

            //Decoration
            MapLayer decoration = new MapLayer(40, 40);
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
                        decoration[x, y-1] = treeTile;
                    tileIndex = 15; //to get the foot below
                }

                Tile tile = new Tile(tileIndex, 0); // tileSet: 0
                decoration[x, y] = tile;
            }

            mapLayers.Add(ground); //TODO this is layer 0
            mapLayers.Add(decoration); //TODO this is layer 1
                                          // TODO layer 2 that you cant walk over

            //Add player position
            //TODO make sure it is created along water at the sides
            int playerX = random.Next(1, (mapInformation.TileWidth * 32)-32); //TODO have better calc
            int playerY = random.Next(1, (mapInformation.TileHeight * 32)-32); //TODO have better calc
            camera.Position = new Vector2((float)50, (float)50);
            //camera.Position = new Vector2((float)playerX, (float)playerY);

            //Add bosses
            

            return new TileMap(camera.Game, mapInformation.TileSets, mapLayers, camera);
        }
    }
}

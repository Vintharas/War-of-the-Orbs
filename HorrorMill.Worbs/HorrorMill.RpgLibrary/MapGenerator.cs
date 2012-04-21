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
            MapLayer ground = new MapLayer(mapInformation.Width, mapInformation.Height);
            for (int y = 0; y < ground.Height; y++)
                for (int x = 0; x < ground.Width; x++)
                {
                    int grassTile = random.Next(0, 2);
                    ground[x, y] = new Tile(grassTile, 0);
                }

            //Decoration
            MapLayer decoration = new MapLayer(40, 40);
            for (int i = 0; i < mapInformation.NumberOfDecorativeObjects; i++)
            {
                int x = random.Next(0, 40);
                int y = random.Next(0, 40);
                //ADD CHECK SO NO OVERWRITE
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

            mapLayers.Add(ground);
            mapLayers.Add(decoration);

            //Add player

            //Add bosses


            return new TileMap(camera.Game, mapInformation.TileSets, mapLayers, camera);
        }
    }
}

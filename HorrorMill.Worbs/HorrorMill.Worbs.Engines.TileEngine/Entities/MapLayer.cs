using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace HorrorMill.Engines.TileEngine.Entities
{
    public class MapLayer
    {
        // matrix [rows, columns]
        // rows -> "height" of matrix/map therefore it's the y coordinate
        // columns -> "width" of matrix/map therefore it's the x coordinate
        // map [rows, columns] => [y, x]
        private Tile[,] map;
        public int Width { get { return map.GetLength(1); } }
        public int Height { get { return map.GetLength(0); } }

        public MapLayer(Tile[,] map)
        {
            this.map = (Tile[,]) map.Clone();
        }

        /// <summary>
        /// Instantiate a map layer.
        /// </summary>
        /// <param name="width">Width of the layer measured in tiles</param>
        /// <param name="height">Height of the layer measured in tiles</param>
        public MapLayer(int width, int height)
        {
            map = new Tile[height, width];
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    map[y, x] = null;//new Tile(0, 0);
        }

        public Tile this[int x, int y]
        {
            get { return map[y, x]; }
            set { map[y, x] = value; }
        }

        List<CollisionTile> tilesWithCollision;
        public List<CollisionTile> GetTilesWithCollision()
        {
            if (tilesWithCollision == null)
            {
                tilesWithCollision = new List<CollisionTile>();
                for (int y = 0; y < Height; y++)
                    for (int x = 0; x < Width; x++)
                        if (map[y, x] != null && map[y, x].Collision)
                        {
                            tilesWithCollision.Add(
                                new CollisionTile
                                {
                                    Tile = map[y, x],
                                    CollisionRectangle = new Rectangle(x*32, y*32, 32, 32) // need to put the right collision rectangle
                                });
                        }

            }
            return tilesWithCollision;
        }
    

    }

    public class CollisionTile
    {
        public Tile Tile { get; set; }
        public Rectangle CollisionRectangle { get; set; }
    }
}
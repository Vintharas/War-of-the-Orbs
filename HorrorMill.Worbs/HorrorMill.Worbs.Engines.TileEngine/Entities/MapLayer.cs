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
                    map[y, x] = new Tile(0, 0);
        }

        public Tile this[int x, int y]
        {
            get { return map[y, x]; }
            set { map[y, x] = value; }
        }
        

    }
}
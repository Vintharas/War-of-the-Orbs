using Microsoft.Xna.Framework;

namespace HorrorMill.Engines.TileEngine
{
    public class Engine
    {
        public static int TileWidth { get; private set; }
        public static int TileHeight { get; private set; }

        public Engine(int tileWidth, int tileHeight)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }

        /// <summary>
        /// Get the map tile which corresponds to a given screen position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static Point VectorToCell(Vector2 position)
        {
            return new Point((int) position.X/TileWidth, (int) position.Y/TileHeight);
        }

    }
}
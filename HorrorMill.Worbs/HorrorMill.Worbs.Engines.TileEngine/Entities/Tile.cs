namespace HorrorMill.Engines.TileEngine.Entities
{
    /// <summary>
    /// Class that represents a single Tile
    /// </summary>
    public class Tile
    {
        public int TileIndex { get; private set; }
        public int TileSet { get; private set; }

        public Tile(int tileIndex, int tileSet)
        {
            TileIndex = tileIndex;
            TileSet = tileSet;
        }

    }
}
namespace HorrorMill.Engines.TileEngine.Entities
{
    /// <summary>
    /// Class that represents a single Tile
    /// </summary>
    public class Tile
    {
        public int TileIndex { get; private set; }
        public int TileSet { get; private set; }
        public bool Collision { get; private set; }

        public Tile(int tileIndex, int tileSet) : this(tileIndex, tileSet, false)
        {
        }

        public Tile(int tileIndex, int tileSet, bool collision) 
        {
            TileIndex = tileIndex;
            TileSet = tileSet;
            Collision = collision;
        }
    }
}
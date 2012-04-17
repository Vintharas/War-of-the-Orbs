using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMill.Engines.TileEngine.Entities
{
    /// <summary>
    /// Class that represents a tileset or tilesheet
    /// </summary>
    public class TileSet : DrawableGameComponent
    {
        private string textureName;
        public Texture2D Texture { get; private set; }
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }
        public int TilesWide { get; private set; }
        public int TilesHigh { get; private set; }
        

        private Rectangle[] sourceRectangles;
        public Rectangle[] SourceRectangles
        {
            // return copy of the array so we avoid it being modified from the outside
            // POSSIBLE_BOTTLENECK: copying a large array can be expensive
            get { return (Rectangle[])sourceRectangles.Clone(); }
        }

        public TileSet(Game game, string textureName, int tilesWide, int tilesHigh, int tileWidth, int tileHeight)
            : base(game)
        {
            this.textureName = textureName;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            TilesWide = tilesWide;
            TilesHigh = tilesHigh;
            CalculateSourceRectangles();
        }

        protected override void LoadContent()
        {
            Texture = Game.Content.Load<Texture2D>(textureName);
            base.LoadContent();
        }

        /// <summary>
        /// Calculate the rectangles that represent the position of the different
        /// tiles within a tile set. Each tile is represented as an index of the array.
        /// </summary>
        private void CalculateSourceRectangles()
        {
            int tiles = TilesWide*TilesHigh;
            sourceRectangles = new Rectangle[tiles];
            int tile = 0;
            for (int y = 0; y < TilesHigh; y++)
                for (int x = 0; x < TilesWide; x++)
                {
                    sourceRectangles[tile] = new Rectangle(x*TileWidth, y*TileHeight, TileWidth, TileHeight);
                    tile++;
                }
        }
    }
}
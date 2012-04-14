using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMill.Engines.TileEngine.Entities
{
    /// <summary>
    /// Class that represents a layered map of tiles
    /// </summary>
    public class TileMap : DrawableGameComponent
    {
        private List<TileSet> tileSets;
        private List<MapLayer> mapLayers;
        private SpriteBatch spriteBatch;
        private Camera camera;

        public TileMap(Game game, List<TileSet> tileSets, List<MapLayer> mapLayers, Camera camera)
            : base(game)
        {
            this.tileSets = tileSets;
            this.mapLayers = mapLayers;
            this.camera = camera;
        }

        public TileMap(Game game, TileSet tileSet, MapLayer mapLayer, Camera camera) : base(game)
        {
            tileSets = new List<TileSet> {tileSet};
            mapLayers = new List<MapLayer> {mapLayer};
            this.camera = camera;
        }

        public override void Initialize()
        {
            // Initialize tilesets so the tile spritesheets will be loaded
            foreach (var tileSet in tileSets)
                tileSet.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = (SpriteBatch) Game.Services.GetService(typeof (SpriteBatch));
            base.LoadContent();
        }

        public override void  Draw(GameTime gameTime)
        {
            // these two objects are reused (more efficient)
            Rectangle destination = new Rectangle(0, 0, Engine.TileWidth, Engine.TileHeight);
            Tile tile;

            foreach (var layer in mapLayers)
                for (int y = 0; y < layer.Height; y++)
                {
                    destination.Y = y*Engine.TileHeight - (int)camera.Position.Y;
                    for (int x = 0; x < layer.Width; x++)
                    {
                        tile = layer[x, y];
                        destination.X = x*Engine.TileWidth - (int)camera.Position.X;
                        spriteBatch.Draw(
                            tileSets[tile.TileSet].Texture,  // Tileset spritesheet
                            destination,                     // Position in the screen where to put the tile
                            tileSets[tile.TileSet].SourceRectangles[tile.TileIndex],  // Tile sprite position in the Tileset spritesheet
                            Color.White);
                    }
                }
            base.Draw(gameTime);
        }
        


    }
}
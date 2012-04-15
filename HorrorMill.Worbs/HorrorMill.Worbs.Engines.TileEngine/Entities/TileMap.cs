using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMill.Engines.TileEngine.Entities
{
    /// <summary>
    /// Class that represents a layered map of tiles. 
    /// This class is the one in charge of drawing all map layers
    /// </summary>
    public class TileMap : DrawableGameComponent
    {
        private List<TileSet> tileSets;
        private List<MapLayer> mapLayers;
        private SpriteBatch spriteBatch;
        // the camera represents which part of the map to show
        private Camera camera;

        private static int mapWidth;
        public static int WidthInPixels { get { return mapWidth*Engine.TileWidth; } }
        private static int mapHeight;
        public static int HeightInPixels { get { return mapHeight*Engine.TileHeight; } }

        public TileMap(Game game, List<TileSet> tileSets, List<MapLayer> mapLayers, Camera camera)
            : base(game)
        {
            this.tileSets = tileSets;
            this.mapLayers = mapLayers;
            SetMapSize();
            ValidateSizeOfAllLayers();
            this.camera = camera;
        }

        private void SetMapSize()
        {
            mapWidth = mapLayers[0].Width;
            mapHeight = mapLayers[0].Height;
        }

        private void ValidateSizeOfAllLayers()
        {
            if (mapLayers.Any(LayerHasInvalidSize))
                throw new Exception("Map layer size exception. All layers must have the same size (width, height)");
        }

        private bool LayerHasInvalidSize(MapLayer layer)
        {
            return layer.Width != mapWidth || layer.Height != mapHeight;
        }

        public TileMap(Game game, TileSet tileSet, MapLayer mapLayer, Camera camera) : 
            this(game, new List<TileSet>{tileSet}, new List<MapLayer>{mapLayer}, camera) {}
        
        public void AddLayer(MapLayer layer)
        {
            if (LayerHasInvalidSize(layer))
                throw new Exception("Map layer size exception. All layers must have the same size (width, height)");
            mapLayers.Add(layer);
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
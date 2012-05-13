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
        private Camera camera;

        private static int mapWidth;
        public static int WidthInPixels { get { return mapWidth*TheTileEngine.TileWidth; } }
        private static int mapHeight;
        public static int HeightInPixels { get { return mapHeight*TheTileEngine.TileHeight; } }

        public TileMap(Game game, List<TileSet> tileSets, List<MapLayer> mapLayers, Camera camera)
            : base(game)
        {
            this.tileSets = tileSets;
            this.mapLayers = mapLayers;
            this.camera = camera;
            SetMapSize();
            ValidateSizeOfAllLayers();
        }

        public TileMap(Game game, TileSet tileSet, MapLayer mapLayer, Camera camera) : this(game, new List<TileSet> { tileSet }, new List<MapLayer> { mapLayer }, camera) { }

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

        public override void Draw(GameTime gameTime)
        {
            DrawBeforePlayer(gameTime);
            //DrawAfterPlayer(gameTime); TODO MapLayer always put tile everywhere so whe addind a new layer that should be on top it overwrites everything
            base.Draw(gameTime);
        }

        public void DrawBeforePlayer(GameTime gameTime)
        {
            DrawLayer(gameTime, new List<int>() { 0, 1 });
        }

        public void DrawAfterPlayer(GameTime gameTime)
        {
            DrawLayer(gameTime, new List<int>() { 2 });
        }

        public void DrawLayer(GameTime gameTime, List<int> layersToDraw)
        {
            Point cameraPoint = TheTileEngine.VectorToCell(camera.Position);
            Point viewPoint = TheTileEngine.VectorToCell(
                new Vector2(
                    camera.Position.X + camera.ViewPortRectangle.Width,
                    camera.Position.Y + camera.ViewPortRectangle.Height));

            Point min = new Point(Math.Max(0, cameraPoint.X - 1), Math.Max(0, cameraPoint.Y - 1));   // The minimum to draw is either 0(the beginnig of the map) or the camera position
            Point max = new Point(Math.Min(viewPoint.X + 1, mapWidth), Math.Min(viewPoint.Y + 1, mapHeight)); // the maximum to draw is either the end of the screen or the end of the map

            // these two objects are reused (more efficient)
            Rectangle destination = new Rectangle(0, 0, TheTileEngine.TileWidth, TheTileEngine.TileHeight);
            Tile tile;

            foreach (int layerIndex in layersToDraw)
            {
                MapLayer layer = mapLayers[layerIndex];
                //foreach (var layer in mapLayers)
                for (int y = min.Y; y < max.Y; y++)
                {
                    destination.Y = y * TheTileEngine.TileHeight;
                    for (int x = min.X; x < max.X; x++)
                    {
                        tile = layer[x, y];
                        destination.X = x * TheTileEngine.TileWidth;
                        spriteBatch.Draw(
                            tileSets[tile.TileSet].Texture,  // Tileset spritesheet
                            destination,                     // Position in the screen where to put the tile
                            tileSets[tile.TileSet].SourceRectangles[tile.TileIndex],  // Tile sprite position in the Tileset spritesheet
                            Color.White);
                    }
                }
            }
        }

        public bool CheckCollision(Rectangle rectangle)
        {
            MapLayer layer = mapLayers[1];
            List<CollisionTile> tiles = layer.GetTilesWithCollision();
            foreach (CollisionTile tile in tiles)
            {
                if(rectangle.Intersects(tile.CollisionRectangle)) 
                {
                    return true;
                }
            }

            //foreach (var tile in layer.GetTilesWithCollision())
            //{
                
            //}
            return false;
        }

    }
}
using HorrorMill.Helpers.Xna.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xTile;
using xTile.Dimensions;
using xTile.Display;

namespace HorrorMill.Worbs.Entities
{
    public class GameScene : Scene
    {
        Map map;
        IDisplayDevice mapDisplayDevice;
        xTile.Dimensions.Rectangle viewPort;
        private SpriteBatch spriteBatch;

        public GameScene(Game game) : base(game)
        {
            Font gameTitle = new Font(game, "This is The Game", new Vector2(0, 0), Color.Red);
            SceneComponents.Add(gameTitle);
        }

        public override void Initialize()
        {
            //Initialize xTile map display device
            mapDisplayDevice = new XnaDisplayDevice(Game.Content, Game.GraphicsDevice);
            viewPort = new xTile.Dimensions.Rectangle(new Size(800, 480));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            map = Game.Content.Load<Map>("Maps\\Map01");
            map.LoadTileSheets(mapDisplayDevice);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            map.Update(gameTime.ElapsedGameTime.Milliseconds);
            viewPort.X++;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            map.Draw(mapDisplayDevice, viewPort);

            base.Draw(gameTime);
        }


    }
}
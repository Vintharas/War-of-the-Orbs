using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMill.Helpers.Xna.UI
{
    public class HealthBar : DrawableGameComponent
    {

        private string borderTextureName;
        private string healthTextureName;
        private Texture2D borderTexture;
        private Texture2D healthTexture;
        private Texture2D backgroundTexture;
        private SpriteBatch spriteBatch;

        private int offset = 5;
        private Rectangle BorderRectangle { get { return new Rectangle((int)position.X, (int)position.Y, borderTexture.Width, borderTexture.Height);}}
        private Rectangle BackgroundRectangle {get { return new Rectangle((int)position.X + offset, (int)position.Y + offset, borderTexture.Width - 2*offset, borderTexture.Height - 2*offset);}}
        private Rectangle HealthRectangle { get; set; }

        private Vector2 position;

        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }

        public HealthBar(Game game, string borderTextureName, string healthTextureName, Vector2 position, int maxHealth) : base(game)
        {
            this.borderTextureName = borderTextureName;
            this.healthTextureName = healthTextureName;
            this.position = position;
            this.MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public override void Initialize()
        {
            base.Initialize();
            HealthRectangle = BackgroundRectangle;
        }

        protected override void LoadContent()
        {
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

            borderTexture = Game.Content.Load<Texture2D>(borderTextureName);
            healthTexture = Game.Content.Load<Texture2D>(healthTextureName);
            //healthTexture = new Texture2D(Game.GraphicsDevice, 1, 1);
            //healthTexture.SetData(new Color[] { new Color(0.7f, 0.1f, 0.1f, 1f) });       // shade of red
            backgroundTexture = new Texture2D(Game.GraphicsDevice, 1, 1);
            backgroundTexture.SetData(new Color[] { new Color(0.5f, 0.1f, 0.1f, 1f) }); // Darked shade of red

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            int rectangleWidth = ((CurrentHealth*BackgroundRectangle.Width)/MaxHealth);
            HealthRectangle = new Rectangle((int) position.X + offset, (int) position.Y + offset, rectangleWidth, BackgroundRectangle.Height);
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(backgroundTexture, BackgroundRectangle, Color.White);
            spriteBatch.Draw(healthTexture, HealthRectangle, Color.White);
            spriteBatch.Draw(borderTexture, BorderRectangle, Color.White);
            base.Draw(gameTime);
        }

    }
}
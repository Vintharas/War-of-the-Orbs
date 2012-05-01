using HorrorMill.Engines.TileEngine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMill.Engines.Rpg.Entities
{
    public class Projectile : DrawableGameComponent
    {
        private Texture2D texture;
        private readonly string textureName;
        private readonly Camera camera;
        private Vector2 position;
        private Viewport viewport;
        private Vector2 speed;

        private SpriteBatch spriteBatch;

        public bool Active { get; set; }
        public int Damage { get; set; }
        public int Width { get { return this.texture.Width; } }
        public int Height{ get { return this.texture.Height; } }


        public Projectile(Game game, string textureName, Vector2 position, int damage, Vector2 speed, Camera camera) : base(game)
        {
            this.textureName = textureName;
            this.position = position;
            this.viewport = game.GraphicsDevice.Viewport;
            Damage = damage;
            this.speed = speed;
            this.camera = camera;
            Active = true;
            speed.Normalize();
        }

        protected override void LoadContent()
        {
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            texture = Game.Content.Load<Texture2D>(textureName);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            position += speed;

            // Deactivate the bullet if it goes out of screen
            // TODO: This may not be true, we can allow it to continue and hit enemies that are not
            // seen by the player but that are there. (like in realms of the mad god)
            Active = IsWithinScreenBoundaries();
        }

        private bool IsWithinScreenBoundaries()
        {
            return position.X - camera.Position.X > 0 &&
                   position.X + texture.Width/2 - camera.Position.X < viewport.Width &&
                   position.Y - camera.Position.Y > 0 &&
                   position.Y + texture.Height/2 - camera.Position.Y < viewport.Height;
        }

        public override void Draw(GameTime gameTime)
        {
            if(Active) 
                spriteBatch.Draw(this.texture, 
                            this.position, // Position where to draw projectile
                            null, // null draws the full texture (we need to modify this if we want to have projectiles that animate)
                            Color.White, 0f, new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0f);

            base.Draw(gameTime);
        }

        public Rectangle CollisionRectangle { get { return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); } }
    }
}

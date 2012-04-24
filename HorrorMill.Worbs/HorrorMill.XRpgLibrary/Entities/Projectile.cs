using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMill.Helpers.Xna.Entities
{
    public class Projectile : DrawableGameComponent
    {
        private Texture2D texture;
        private readonly string textureName;
        private Vector2 position;
        private Viewport viewport;
        private float speed;

        private SpriteBatch spriteBatch;

        public bool Active { get; set; }
        public int Damage { get; set; }
        public int Width { get { return this.texture.Width; } }
        public int Height{ get { return this.texture.Height; } }


        public Projectile(Game game, string textureName, Vector2 position, int damage, float speed) : base(game)
        {
            this.textureName = textureName;
            this.position = position;
            this.viewport = game.GraphicsDevice.Viewport;
            Damage = damage;
            this.speed = speed;
            Active = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            texture = Game.Content.Load<Texture2D>(textureName);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // Projectiles always move to the right
            this.position.X += this.speed;

            // Deactivate the bullet if it goes out of screen
            // TODO: This may not be true, we can allow it to continue and hit enemies that are not
            // seen by the player but that are there. (like in realms of the mad god)
            if (this.position.X + this.texture.Width / 2 > viewport.Width)
                Active = false;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(this.texture, 
                            this.position, // Position where to draw projectile
                            null, // null draws the full texture (we need to modify this if we want to have projectiles that animate)
                            Color.White, 0f, new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0f);
            base.Draw(gameTime);
        }

    }
}

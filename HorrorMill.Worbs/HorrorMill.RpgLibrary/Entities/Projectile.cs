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

        private Vector2 startingPosition;
        private int maxDistanceX = 800;
        private int maxDistanceY = 480;

        private SpriteBatch spriteBatch;

        public bool PlayerProjectile { get; set; }
        public bool Active { get; set; }
        public int Damage { get; set; }
        public int Width { get { return this.texture.Width; } }
        public int Height{ get { return this.texture.Height; } }
        
        public Projectile(Game game, string textureName, Vector2 position, int damage, Vector2 speed, bool playerProjectile, Camera camera) : base(game)
        {
            this.textureName = textureName;
            this.position = position;
            this.startingPosition = this.position;
            this.viewport = game.GraphicsDevice.Viewport;
            Damage = damage;
            this.speed = speed;
            this.PlayerProjectile = playerProjectile;
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

            // TODO: Base distance for X and Y on weapon with screen % so weapon have 80% distance on screen X=800 = 640
            Active = IsWithinScreenBoundaries();
        }

        private bool IsWithinScreenBoundaries()
        {
            //Check projectile to Right
            if (speed.X > 0 && (startingPosition.X + maxDistanceX > position.X))
                return true;
            
            // ...Left
            if(speed.X < 0 && (startingPosition.X - maxDistanceX < position.X))
                return true;

            // ...Down
            if (speed.Y > 0 && (startingPosition.Y + maxDistanceY > position.Y))
                return true;

            // ...Up
            if (speed.Y < 0 && (startingPosition.Y - maxDistanceY < position.Y))
                return true;

            return false;
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

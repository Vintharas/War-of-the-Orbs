using Microsoft.Xna.Framework;

namespace HorrorMill.Engines.TileEngine.Entities
{
    public class Camera : GameComponent
    {
        public Vector2 Position { get; private set; }
        
        private float speed;
        public float Speed
        {
            get { return speed; }
            set { speed = (float) MathHelper.Clamp(value, 1f, 16f); }
        }

        public float Zoom { get; private set; }

        private Rectangle viewPortRectangle;

        public Camera(Game game, Rectangle viewPortRectangle): this(game, viewPortRectangle, Vector2.Zero)
        {
        }

        public Camera(Game game, Rectangle viewPortRectangle, Vector2 position) : base(game)
        {
            this.viewPortRectangle = viewPortRectangle;
            Position = position;
            speed = 4f;
            Zoom = 1f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
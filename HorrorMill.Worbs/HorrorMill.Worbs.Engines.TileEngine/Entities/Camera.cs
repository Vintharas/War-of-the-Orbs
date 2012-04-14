using HorrorMill.HorrorMill.Helpers.Xna.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HorrorMill.Engines.TileEngine.Entities
{
    public class Camera : GameComponent
    {
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        
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
            if (InputHandler.KeyDown(Keys.Left))
                position.X -= speed;
            else if (InputHandler.KeyDown(Keys.Right))
                position.X += speed;

            if (InputHandler.KeyDown(Keys.Up))
                position.Y -= speed;
            else if (InputHandler.KeyDown(Keys.Down))
                position.Y += speed;

            base.Update(gameTime);
        }

    }
}
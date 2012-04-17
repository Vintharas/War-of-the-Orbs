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

        public void MoveCamera(Vector2 motion)
        {
            if (motion != Vector2.Zero)
                motion.Normalize(); // to avoid that the camera moves faster diagonally than horizontally or vertically

            position += motion*speed;

            LockCamera();
        }

        private void LockCamera()
        {
            position.X = MathHelper.Clamp(position.X, 0, TileMap.WidthInPixels - viewPortRectangle.Width);
            position.Y = MathHelper.Clamp(position.Y, 0, TileMap.HeightInPixels - viewPortRectangle.Height);
        }

    }
}
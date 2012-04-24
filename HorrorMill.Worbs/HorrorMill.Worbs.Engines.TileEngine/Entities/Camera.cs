using HorrorMill.Helpers.Xna.Sprites;
using HorrorMill.HorrorMill.Helpers.Xna.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HorrorMill.Engines.TileEngine.Entities
{
    public enum CameraMode {Free, Follow}

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
        public CameraMode Mode { get; private set; }

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
            Mode = CameraMode.Follow;
        }

        public void Move(Vector2 motion)
        {
            if (Mode == CameraMode.Follow) return;
            
            if (motion != Vector2.Zero)
                motion.Normalize(); // to avoid that the camera moves faster diagonally than horizontally or vertically
            position += motion*speed;
            Lock();
        }

        private void Lock()
        {
            position.X = MathHelper.Clamp(position.X, 0, TileMap.WidthInPixels - viewPortRectangle.Width);
            position.Y = MathHelper.Clamp(position.Y, 0, TileMap.HeightInPixels - viewPortRectangle.Height);
        }

        public void LockToPosition(Vector2 position)
        {
            this.position = position;
        }

        public void LockToSpriteRectangle(Rectangle spriteRectangle)
        {
            position.X = spriteRectangle.X + spriteRectangle.Width / 2 - (viewPortRectangle.Width / 2);
            position.Y = spriteRectangle.Y + spriteRectangle.Height / 2 - (viewPortRectangle.Height / 2);
            Lock();
        }

        public void ToggleCameraMode()
        {
            Mode = Mode == CameraMode.Follow ? CameraMode.Free : CameraMode.Follow;
        }
    }
}
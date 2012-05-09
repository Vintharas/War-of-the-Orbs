using HorrorMill.Engines.TileEngine.Entities;
using HorrorMill.Helpers.Xna.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HorrorMill.Worbs.Entities
{
    public class Player : DrawableGameComponent
    {
        /// <summary>
        /// This enum reflects the different Player sprite sheet states -> TODO: Extract this to a PlayerSpriteSheet class, so we don't mix player state with sprite state
        /// </summary>
        public enum State
        {
            IdleDown,
            IdleUp,
            IdleRight,
            IdleLeft,
            Walk,
            WalkUp,
            WalkDown,
            Attack
        }

        private MultiSprite multiSprite;
        private SpriteBatch spriteBatch;

        public Camera Camera { get; set; }
        public Vector2 Position { get { return multiSprite.Position; } }
        public Rectangle Rectangle { get { return multiSprite.Rectangle; } }
        public Vector2 PositionMiddleCenter { get { return new Vector2(Position.X + Rectangle.Width/2, Position.Y + Rectangle.Height/2); }}
        public Vector2 Direction { get; set; }

        public int Damage { get { return 30; } }
        public int HitPoints { get; set; }   // Total hit points
        public int Health { get; set; }      // Current Health

        public Player(Game game) : this(game, new Camera(game, game.GraphicsDevice.Viewport.Bounds)){}

        public Player(Game game, Camera camera) : base(game)
        {
            Camera = camera;
            var startingPosition = Camera.Position;
            multiSprite = new MultiSprite(startingPosition, 70);
            multiSprite.States.Add(State.IdleDown.ToString(), new SpriteSheet("SpriteSheets/Player/Wizard/IdleDown", new Point(0, 0), new Point(50, 50), new Point(1, 1), SpriteDirection.Right));
            multiSprite.States.Add(State.IdleUp.ToString(), new SpriteSheet("SpriteSheets/Player/Wizard/IdleUp", new Point(0, 0), new Point(50, 50), new Point(1, 1), SpriteDirection.Right));
            multiSprite.States.Add(State.IdleRight.ToString(), new SpriteSheet("SpriteSheets/Player/Wizard/IdleRight", new Point(0, 0), new Point(50, 50), new Point(1, 1), SpriteDirection.Right));
            multiSprite.States.Add(State.IdleLeft.ToString(), new SpriteSheet("SpriteSheets/Player/Wizard/IdleLeft", new Point(0, 0), new Point(50, 50), new Point(1, 1), SpriteDirection.Right));
            multiSprite.States.Add(State.Walk.ToString(), new SpriteSheet("SpriteSheets/Player/Wizard/Walk", new Point(0, 0), new Point(50, 50), new Point(2, 1), SpriteDirection.Right));
            multiSprite.States.Add(State.WalkUp.ToString(), new SpriteSheet("SpriteSheets/Player/Wizard/WalkUp", new Point(0, 0), new Point(50, 50), new Point(2, 1), SpriteDirection.Right));
            multiSprite.States.Add(State.WalkDown.ToString(), new SpriteSheet("SpriteSheets/Player/Wizard/WalkDown", new Point(0, 0), new Point(50, 50), new Point(2, 1), SpriteDirection.Right));
            //multiSprite.States.Add(State.Attack.ToString(), new SpriteSheet("SpriteSheets/Player/Wizard/Attack", new Point(0, 0), new Point(140, 160), new Point(2, 1), SpriteDirection.Right));
            multiSprite.CurrentState = State.IdleDown.ToString();

            // Load player characteristics
            HitPoints = 100;
            Health = HitPoints;
        }

        protected override void LoadContent()
        {
            spriteBatch = (SpriteBatch) Game.Services.GetService(typeof (SpriteBatch));
            multiSprite.LoadContent(Game.Content);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // update sprites
            multiSprite.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            multiSprite.Draw(gameTime, spriteBatch, Camera.Position);
            base.Draw(gameTime);
        }

        public void Move(Vector2 motion)
        {
            // if there is no motion, the player sprite is set to an idle state
            if (motion == Vector2.Zero)
            {
                if (multiSprite.PreviousState == State.Walk.ToString())
                    multiSprite.CurrentState = State.IdleRight.ToString();
                if (multiSprite.PreviousState == State.WalkDown.ToString())
                    multiSprite.CurrentState = State.IdleDown.ToString();
                if (multiSprite.PreviousState == State.WalkUp.ToString())
                    multiSprite.CurrentState = State.IdleUp.ToString();
            }
            else // otherwise update the sprite to reflect the player action
            {
                Direction = motion;
                if (motion.Y > 0)
                    multiSprite.CurrentState = State.WalkDown.ToString();
                else
                    multiSprite.CurrentState = State.WalkUp.ToString();
                if (motion.X != 0)
                    multiSprite.CurrentState = State.Walk.ToString();

                motion.Normalize();
                motion = motion*Camera.Speed;
                motion = LockToMap(motion);
                multiSprite.Move((int)motion.X, (int)motion.Y);
            }
        }

        private Vector2 LockToMap(Vector2 motion)
        {
            // Refactor this! Probably should handle position either in the multisprite or in the player
            // this code is basically calculating the amount of motion possible that doesn't get the player sprite out of the window
            Vector2 previousPosition = multiSprite.Position;
            Vector2 newPosition = previousPosition + motion;
            newPosition.X = MathHelper.Clamp(newPosition.X, 0, TileMap.WidthInPixels - multiSprite.Rectangle.Width);
            newPosition.Y = MathHelper.Clamp(newPosition.Y, 0, TileMap.HeightInPixels - multiSprite.Rectangle.Height);
            return newPosition - previousPosition;
        }
    }
}
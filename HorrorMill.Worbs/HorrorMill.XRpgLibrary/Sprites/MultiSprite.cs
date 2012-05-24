using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMill.Helpers.Xna.Sprites
{
    /// <summary>
    /// Class that represents a multisprite, a sprite that can have different states and draw different animations
    /// depending on the state
    /// </summary>
    public class MultiSprite
    {
        public Dictionary<string, SpriteSheet> States { get; private set; }

        private string currentState;
        public string CurrentState 
        {
            get { return currentState; }
            set 
            { 
                PreviousState = CurrentState;
                currentState = value;
            } 
        }
        public string PreviousState { get; private set; }
        public SpriteSheet CurrentSheet { get { return States[CurrentState]; } }

        private Texture2D TextureImage { get { return CurrentSheet.textureImage; } }
        private Point CurrentFrame { get { return CurrentSheet.CurrentFrame; }}
        private Point FrameSize { get { return CurrentSheet.FrameSize; } }

        private const int DEFAULT_MILLISECONDS_PER_FRAME = 16;
        private readonly int millisecondsPerFrame;
        private int timeSinceLastFrame;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(
                    (int)(position.X),
                    (int)(position.Y),
                    (int)(FrameSize.X),
                    (int)(FrameSize.Y));
            }
        }
        // Gets the collision rect based on position, framesize and collision offset
        public Rectangle CollisionRect { get { return Rectangle; } }

        // Get current position of the sprite
        public Vector2 Position
        {
            get { return position; }
        }
        protected Vector2 position;
        private SpriteDirection movementDirection;

        /// <summary>
        /// Initialize members. No offset, default milliseconds per frame
        /// </summary>
        /// <param name="position"></param>
        public MultiSprite(Vector2 position) : this(position, DEFAULT_MILLISECONDS_PER_FRAME){}

        /// <summary>
        /// Initialize members
        /// </summary>
        /// <param name="position"></param>
        /// <param name="millisecondsPerFrame"></param>
        public MultiSprite(Vector2 position, int millisecondsPerFrame)
        {
            this.position = position;
            this.millisecondsPerFrame = millisecondsPerFrame;
            States = new Dictionary<string, SpriteSheet>();
            movementDirection = SpriteDirection.Right;
        }

        public void LoadContent(ContentManager content)
        {
            foreach (var spriteSheet in States.Values)
                spriteSheet.LoadContent(content);
        }

        /// <summary>
        /// Update sprite to show
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            // Update frame if time to do so based on framerate
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                // Increment to next frame
                timeSinceLastFrame = 0;
                CurrentSheet.NextFrame();
            }
        }

        /// <summary>
        /// Draw sprite
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="vector2"> </param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffect = SpriteEffects.None;
            if ((CurrentSheet.SpriteDirection == SpriteDirection.Left && movementDirection == SpriteDirection.Right)
                || (CurrentSheet.SpriteDirection == SpriteDirection.Right && movementDirection == SpriteDirection.Left))
                spriteEffect = SpriteEffects.FlipHorizontally;
     
            spriteBatch.Draw(TextureImage,
                             position,
                             new Rectangle(CurrentFrame.X*FrameSize.X,
                                           CurrentFrame.Y*FrameSize.Y,
                                           FrameSize.X, FrameSize.Y),
                             Color.White, 0, Vector2.Zero,
                             1f, spriteEffect, 0);
        }



        // Detect if this sprite is off the screen and irrelevant
        public bool IsOutOfBounds(Rectangle clientRect)
        {
            if (position.X < -FrameSize.X ||
                position.X > clientRect.Width ||
                position.Y < -FrameSize.Y ||
                position.Y > clientRect.Height)
            {
                return true;
            }

            return false;
        }

        public void Move(int x, int y)
        {
            position.X += x;
            position.Y += y;
            if (x > 0)
                movementDirection = SpriteDirection.Right;
            else if (x < 0)
                movementDirection = SpriteDirection.Left;
        }

        public void UndoMove(int x, int y)
        {
            position.X += x;
            position.Y += y;
        }
    }
}
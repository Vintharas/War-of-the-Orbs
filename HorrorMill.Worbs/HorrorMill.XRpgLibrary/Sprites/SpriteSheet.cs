using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMill.Helpers.Xna.Sprites
{
    /// <summary>
    /// Class that represents a sprite sheet
    /// </summary>
    public class SpriteSheet
    {
        public Texture2D textureImage { get; set; }
        public Point CurrentFrame
        {
            get { return currentFrame; }
            set { currentFrame = value; }
        }
        private Point currentFrame;
        public Point FrameSize { get; set; }
        public Point SheetSize { get; set; }
        public string AssetName { get; set; }
        public SpriteDirection SpriteDirection { get; set; }

        /// <summary>
        /// Initialize members
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="currentFrame"></param>
        /// <param name="frameSize"></param>
        /// <param name="sheetSizeInFrames"></param>
        /// <param name="spriteDirection"></param>
        public SpriteSheet(string assetName, Point currentFrame, Point frameSize, Point sheetSizeInFrames, SpriteDirection spriteDirection)
        {
            AssetName = assetName;
            CurrentFrame = currentFrame;
            FrameSize = frameSize;
            SheetSize = sheetSizeInFrames;
            SpriteDirection = spriteDirection;
        }

        /// <summary>
        /// Load spriteSheet texture
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            textureImage = content.Load<Texture2D>(AssetName);
        }

        /// <summary>
        /// Advance to the next frame in the spriteSheet
        /// </summary>
        public void NextFrame()
        {
            ++currentFrame.X;
            if (currentFrame.X >= SheetSize.X)
            {
                currentFrame.X = 0;
                ++currentFrame.Y;
                if (currentFrame.Y >= SheetSize.Y)
                    currentFrame.Y = 0;
            }
        }
    }
}
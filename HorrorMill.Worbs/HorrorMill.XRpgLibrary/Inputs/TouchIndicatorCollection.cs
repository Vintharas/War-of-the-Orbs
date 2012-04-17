using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;


namespace HorrorMill.HorrorMill.Helpers.Xna.Inputs
{
    public class TouchIndicatorCollection
    {
        List<TouchIndicator> touchPositions = new List<TouchIndicator>();

        public void Update(GameTime gameTime, ContentManager content)
        {
            TouchCollection currentTouchLocationState = TouchPanel.GetState();
            foreach (TouchLocation location in currentTouchLocationState)
            {
                bool isTouchIDAlreadyStored = false;
                foreach (TouchIndicator indicator in touchPositions)
                {
                    if (location.Id == indicator.TouchID)
                    {
                        isTouchIDAlreadyStored = true;
                        break;
                    }
                }

                if (!isTouchIDAlreadyStored)
                {
                    TouchIndicator indicator = new TouchIndicator(location.Id, content);
                    touchPositions.Add(indicator);
                }
            }

            foreach (TouchIndicator indicator in touchPositions)
            {
                indicator.Update(currentTouchLocationState);
            }
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (TouchIndicator indicator in touchPositions)
            {
                indicator.Draw(batch);
            }
        }

    }
}

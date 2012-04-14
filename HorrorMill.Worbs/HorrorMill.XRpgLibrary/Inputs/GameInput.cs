using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;


namespace HorrorMill.HorrorMill.Helpers.Xna.Inputs
{
    class GameInput
    {
        Dictionary<string, Input> Inputs = new Dictionary<string, Input>();

        public Input GetInput(string theAction)
        {
            //Add the Action if it doesn't already exist
            if (Inputs.ContainsKey(theAction) == false)
            {
                Inputs.Add(theAction, new Input());
            }

            return Inputs[theAction];
        }

        public void BeginUpdate()
        {
            Input.BeginUpdate();
        }

        public void EndUpdate()
        {
            Input.EndUpdate();
        }

        public bool IsConnected(PlayerIndex thePlayer)
        {
            // If there never WAS a gamepad connected, just say the gamepad is STILL connected
            if (Input.GamepadConnectionState[thePlayer] == false)
                return true;

            return Input.IsConnected(thePlayer);
        }

        public bool IsPressed(string theAction)
        {
            if (!Inputs.ContainsKey(theAction))
            {
                return false;
            }

            return Inputs[theAction].IsPressed(PlayerIndex.One);
        }

        public bool IsPressed(string theAction, PlayerIndex thePlayer)
        {
            if (Inputs.ContainsKey(theAction) == false)
            {
                return false;
            }

            return Inputs[theAction].IsPressed(thePlayer);
        }

        public bool IsPressed(string theAction, PlayerIndex? thePlayer)
        {
            if (thePlayer == null)
            {
                PlayerIndex theReturnedControllingPlayer;
                return IsPressed(theAction, thePlayer, out theReturnedControllingPlayer);
            }

            return IsPressed(theAction, (PlayerIndex)thePlayer);
        }

        public bool IsPressed(string theAction, PlayerIndex? thePlayer, out PlayerIndex theControllingPlayer)
        {
            if (!Inputs.ContainsKey(theAction))
            {
                theControllingPlayer = PlayerIndex.One;
                return false;
            }

            if (thePlayer == null)
            {
                if (IsPressed(theAction, PlayerIndex.One))
                {
                    theControllingPlayer = PlayerIndex.One;
                    return true;
                }

                if (IsPressed(theAction, PlayerIndex.Two))
                {
                    theControllingPlayer = PlayerIndex.Two;
                    return true;
                }

                if (IsPressed(theAction, PlayerIndex.Three))
                {
                    theControllingPlayer = PlayerIndex.Three;
                    return true;
                }

                if (IsPressed(theAction, PlayerIndex.Four))
                {
                    theControllingPlayer = PlayerIndex.Four;
                    return true;
                }

                theControllingPlayer = PlayerIndex.One;
                return false;
            }

            theControllingPlayer = (PlayerIndex)thePlayer;
            return IsPressed(theAction, (PlayerIndex)thePlayer);
        }

        public void AddGamePadInput(string theAction, Buttons theButton,
                            bool isReleasedPreviously)
        {
            GetInput(theAction).AddGamepadInput(theButton, isReleasedPreviously);
        }

        public void AddTouchTapInput(string theAction, Rectangle theTouchArea,
                                     bool isReleasedPreviously)
        {
            GetInput(theAction).AddTouchTapInput(theTouchArea, isReleasedPreviously);
        }

        public void AddTouchSlideInput(string theAction, Input.Direction theDirection,
                                       float slideDistance)
        {
            GetInput(theAction).AddTouchSlideInput(theDirection, slideDistance);
        }

        public void AddKeyboardInput(string theAction, Keys theKey,
                                     bool isReleasedPreviously)
        {
            GetInput(theAction).AddKeyboardInput(theKey, isReleasedPreviously);
        }

        public void AddTouchGestureInput(string theAction, GestureType theGesture,
                                         Rectangle theRectangle)
        {
            GetInput(theAction).AddTouchGesture(theGesture, theRectangle);
        }

        public void AddAccelerometerInput(string theAction, Input.Direction theDirection,
                                          float tiltThreshold)
        {
            GetInput(theAction).AddAccelerometerInput(theDirection, tiltThreshold);
        }

        public Vector2 CurrentGesturePosition(string theAction)
        {
            return GetInput(theAction).CurrentGesturePosition();
        }

        public Vector2 CurrentGestureDelta(string theAction)
        {
            return GetInput(theAction).CurrentGestureDelta();
        }

        public Vector2 CurrentGesturePosition2(string theAction)
        {
            return GetInput(theAction).CurrentGesturePosition2();
        }

        public Vector2 CurrentGestureDelta2(string theAction)
        {
            return GetInput(theAction).CurrentGestureDelta2();
        }

        public Point CurrentTouchPoint(string theAction)
        {
            Vector2? currentPosition = GetInput(theAction).CurrentTouchPosition();
            if (currentPosition == null)
            {
                return new Point(-1, -1);
            }

            return new Point((int)currentPosition.Value.X, (int)currentPosition.Value.Y);
        }

        public Vector2 CurrentTouchPosition(string theAction)
        {
            Vector2? currentTouchPosition = GetInput(theAction).CurrentTouchPosition();
            if (currentTouchPosition == null)
            {
                return new Vector2(-1, -1);
            }

            return (Vector2)currentTouchPosition;
        }

        public float CurrentGestureScaleChange(string theAction)
        {
            // Scaling is dependent on the Pinch gesture. If no input has been setup for 
            // Pinch then just return 0 indicating no scale change has occurred.
            if (!GetInput(theAction).PinchGestureAvailable) return 0;

            // Get the current and previous locations of the two fingers
            Vector2 currentPositionFingerOne = CurrentGesturePosition(theAction);
            Vector2 previousPositionFingerOne
                = CurrentGesturePosition(theAction) - CurrentGestureDelta(theAction);
            Vector2 currentPositionFingerTwo = CurrentGesturePosition2(theAction);
            Vector2 previousPositionFingerTwo
                = CurrentGesturePosition2(theAction) - CurrentGestureDelta2(theAction);

            // Figure out the distance between the current and previous locations
            float currentDistance = Vector2.Distance(currentPositionFingerOne, currentPositionFingerTwo);
            float previousDistance
                = Vector2.Distance(previousPositionFingerOne, previousPositionFingerTwo);

            // Calculate the difference between the two and use that to alter the scale
            float scaleChange = (currentDistance - previousDistance) * .01f;
            return scaleChange;
        }

        public Vector3 CurrentAccelerometerReading(string theAction)
        {
            return GetInput(theAction).CurrentAccelerometerReading;
        }

    }
}

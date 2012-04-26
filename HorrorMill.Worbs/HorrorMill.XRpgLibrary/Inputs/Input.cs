using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Devices.Sensors;


namespace HorrorMill.HorrorMill.Helpers.Xna.Inputs
{
    public class Input
    {
        Dictionary<Keys, bool> keyboardInputs = new Dictionary<Keys, bool>();
        Dictionary<Buttons, bool> gamepadInputs = new Dictionary<Buttons, bool>();
        Dictionary<Rectangle, bool> touchTapInputs = new Dictionary<Rectangle, bool>();
        Dictionary<Direction, float> touchSlideInputs = new Dictionary<Direction, float>();
        Dictionary<int, GestureDefinition> gestureInputs = new Dictionary<int, GestureDefinition>();
        Dictionary<Direction, float> accelerometerInputs = new Dictionary<Direction, float>();

        static public Dictionary<PlayerIndex, GamePadState> CurrentGamePadState = new Dictionary<PlayerIndex, GamePadState>();
        static public Dictionary<PlayerIndex, GamePadState> PreviousGamePadState = new Dictionary<PlayerIndex, GamePadState>();

        static public TouchCollection CurrentTouchLocationState;
        static public TouchCollection PreviousTouchLocationState;
        static public KeyboardState CurrentKeyboardState;
        static public KeyboardState PreviousKeyboardState;

        public static Dictionary<PlayerIndex, bool> GamepadConnectionState = new Dictionary<PlayerIndex, bool>();
        static private List<GestureDefinition> detectedGestures = new List<GestureDefinition>();


        static private Accelerometer accelerometerSensor;
        static private Vector3 currentAccelerometerReading;

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public Input()
        {
            if (CurrentGamePadState.Count == 0)
            {
                CurrentGamePadState.Add(PlayerIndex.One, GamePad.GetState(PlayerIndex.One));
                CurrentGamePadState.Add(PlayerIndex.Two, GamePad.GetState(PlayerIndex.Two));
                CurrentGamePadState.Add(PlayerIndex.Three, GamePad.GetState(PlayerIndex.Three));
                CurrentGamePadState.Add(PlayerIndex.Four, GamePad.GetState(PlayerIndex.Four));

                PreviousGamePadState.Add(PlayerIndex.One, GamePad.GetState(PlayerIndex.One));
                PreviousGamePadState.Add(PlayerIndex.Two, GamePad.GetState(PlayerIndex.Two));
                PreviousGamePadState.Add(PlayerIndex.Three, GamePad.GetState(PlayerIndex.Three));
                PreviousGamePadState.Add(PlayerIndex.Four, GamePad.GetState(PlayerIndex.Four));

                GamepadConnectionState.Add(PlayerIndex.One,
                    CurrentGamePadState[PlayerIndex.One].IsConnected);
                GamepadConnectionState.Add(PlayerIndex.Two,
                    CurrentGamePadState[PlayerIndex.Two].IsConnected);
                GamepadConnectionState.Add(PlayerIndex.Three,
                    CurrentGamePadState[PlayerIndex.Three].IsConnected);
                GamepadConnectionState.Add(PlayerIndex.Four,
                    CurrentGamePadState[PlayerIndex.Four].IsConnected);
            }

            if (accelerometerSensor == null)
            {
                accelerometerSensor = new Accelerometer();
                accelerometerSensor.ReadingChanged
                    += new EventHandler<AccelerometerReadingEventArgs>(AccelerometerReadingChanged);
            }
        }

        static public void BeginUpdate()
        {
            CurrentGamePadState[PlayerIndex.One] = GamePad.GetState(PlayerIndex.One);
            CurrentGamePadState[PlayerIndex.Two] = GamePad.GetState(PlayerIndex.Two);
            CurrentGamePadState[PlayerIndex.Three] = GamePad.GetState(PlayerIndex.Three);
            CurrentGamePadState[PlayerIndex.Four] = GamePad.GetState(PlayerIndex.Four);

            CurrentTouchLocationState = TouchPanel.GetState();
            CurrentKeyboardState = Keyboard.GetState(PlayerIndex.One);

            detectedGestures.Clear();
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();
                detectedGestures.Add(new GestureDefinition(gesture));
            }
        }

        static public void EndUpdate()
        {
            PreviousGamePadState[PlayerIndex.One] = CurrentGamePadState[PlayerIndex.One];
            PreviousGamePadState[PlayerIndex.Two] = CurrentGamePadState[PlayerIndex.Two];
            PreviousGamePadState[PlayerIndex.Three] = CurrentGamePadState[PlayerIndex.Three];
            PreviousGamePadState[PlayerIndex.Four] = CurrentGamePadState[PlayerIndex.Four];

            PreviousTouchLocationState = CurrentTouchLocationState;
            PreviousKeyboardState = CurrentKeyboardState;
        }

        private void AccelerometerReadingChanged(object sender, AccelerometerReadingEventArgs e)
        {
            currentAccelerometerReading.X = (float)e.X;
            currentAccelerometerReading.Y = (float)e.Y;
            currentAccelerometerReading.Z = (float)e.Z;
        }

        public void AddKeyboardInput(Keys theKey, bool isReleasedPreviously)
        {
            if (keyboardInputs.ContainsKey(theKey))
            {
                keyboardInputs[theKey] = isReleasedPreviously;
                return;
            }
            keyboardInputs.Add(theKey, isReleasedPreviously);
        }

        public void AddGamepadInput(Buttons theButton, bool isReleasedPreviously)
        {
            if (gamepadInputs.ContainsKey(theButton))
            {
                gamepadInputs[theButton] = isReleasedPreviously;
                return;
            }
            gamepadInputs.Add(theButton, isReleasedPreviously);
        }

        public void AddTouchTapInput(Rectangle theTouchArea, bool isReleasedPreviously)
        {
            if (touchTapInputs.ContainsKey(theTouchArea))
            {
                touchTapInputs[theTouchArea] = isReleasedPreviously;
                return;
            }
            touchTapInputs.Add(theTouchArea, isReleasedPreviously);
        }

        public void AddTouchSlideInput(Direction theDirection, float slideDistance)
        {
            if (touchSlideInputs.ContainsKey(theDirection))
            {
                touchSlideInputs[theDirection] = slideDistance;
                return;
            }
            touchSlideInputs.Add(theDirection, slideDistance);
        }

        public bool PinchGestureAvailable = false;
        public void AddTouchGesture(GestureType theGesture, Rectangle theTouchArea)
        {
            TouchPanel.EnabledGestures = theGesture | TouchPanel.EnabledGestures;
            gestureInputs.Add(gestureInputs.Count, new GestureDefinition(theGesture, theTouchArea));
            if (theGesture == GestureType.Pinch)
            {
                PinchGestureAvailable = true;
            }
        }

        static private bool isAccelerometerStarted = false;
        public void AddAccelerometerInput(Direction direction, float tiltThreshold)
        {
            if (!isAccelerometerStarted)
            {
                try
                {
                    accelerometerSensor.Start();
                    isAccelerometerStarted = true;
                }
                catch (AccelerometerFailedException e)
                {
                    isAccelerometerStarted = false;
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }

            accelerometerInputs.Add(direction, tiltThreshold);
        }

        public void RemoveAccelerometerInputs()
        {
            if (isAccelerometerStarted)
            {
                try
                {
                    accelerometerSensor.Stop();
                    isAccelerometerStarted = false;
                }
                catch (AccelerometerFailedException e)
                {
                    // The sensor couldn't be stopped.
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }

            accelerometerInputs.Clear();
        }


        static public bool IsConnected(PlayerIndex thePlayerIndex)
        {
            return CurrentGamePadState[thePlayerIndex].IsConnected;
        }


        public bool IsPressed(PlayerIndex thePlayerIndex)
        {
            return IsPressed(thePlayerIndex, null);
        }

        public bool IsPressed(PlayerIndex thePlayerIndex, Rectangle? theCurrentObjectLocation)
        {
            if (IsKeyboardInputPressed())
            {
                return true;
            }

            if (IsGamepadInputPressed(thePlayerIndex))
            {
                return true;
            }

            if (IsTouchTapInputPressed())
            {
                return true;
            }

            if (IsTouchSlideInputPressed())
            {
                return true;
            }

            if (IsGestureInputPressed(theCurrentObjectLocation))
            {
                return true;
            }

            return false;
        }

        private bool IsKeyboardInputPressed()
        {
            foreach (Keys aKey in keyboardInputs.Keys)
            {
                if (keyboardInputs[aKey]
                && CurrentKeyboardState.IsKeyDown(aKey)
                && !PreviousKeyboardState.IsKeyDown(aKey))
                {
                    return true;
                }
                else if (!keyboardInputs[aKey]
                && CurrentKeyboardState.IsKeyDown(aKey))
                {
                    return true;
                }
            }

            return false;
        }


        private bool IsGamepadInputPressed(PlayerIndex thePlayerIndex)
        {
            foreach (Buttons aButton in gamepadInputs.Keys)
            {
                if (gamepadInputs[aButton]
                    && CurrentGamePadState[thePlayerIndex].IsButtonDown(aButton)
                    && !PreviousGamePadState[thePlayerIndex].IsButtonDown(aButton))
                {
                    return true;
                }
                else if (!gamepadInputs[aButton]
                        && CurrentGamePadState[thePlayerIndex].IsButtonDown(aButton))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsTouchTapInputPressed()
        {
            // TODO: Refactor this, before it didn't support multitouch, refactor whole class
            // cause if this was not supported, I wonder what more crap there is XD
            foreach (Rectangle touchArea in touchTapInputs.Keys)
            {
                foreach (Rectangle currentlyTouchedRectangle in CurrentlyTouchedRectangles)
                {
                    if (touchTapInputs[touchArea]
                        && touchArea.Intersects(currentlyTouchedRectangle)
                        && PreviousTouchPosition() == null)
                    {
                        return true;
                    }
                    else if (!touchTapInputs[touchArea]
                            && touchArea.Intersects(currentlyTouchedRectangle))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsTouchSlideInputPressed()
        {
            foreach (Direction slideDirection in touchSlideInputs.Keys)
            {
                if (CurrentTouchPosition() != null && PreviousTouchPosition() != null)
                {
                    switch (slideDirection)
                    {
                        case Direction.Up:
                            {
                                if (CurrentTouchPosition().Value.Y + touchSlideInputs[slideDirection]
                                    < PreviousTouchPosition().Value.Y)
                                {
                                    return true;
                                }
                                break;
                            }

                        case Direction.Down:
                            {
                                if (CurrentTouchPosition().Value.Y - touchSlideInputs[slideDirection]
                                    > PreviousTouchPosition().Value.Y)
                                {
                                    return true;
                                }
                                break;
                            }

                        case Direction.Left:
                            {
                                if (CurrentTouchPosition().Value.X + touchSlideInputs[slideDirection]
                                    < PreviousTouchPosition().Value.X)
                                {
                                    return true;
                                }
                                break;
                            }

                        case Direction.Right:
                            {
                                if (CurrentTouchPosition().Value.X - touchSlideInputs[slideDirection]
                                    > PreviousTouchPosition().Value.X)
                                {
                                    return true;
                                }
                                break;
                            }
                    }
                }
            }

            return false;
        }

        private bool IsGestureInputPressed(Rectangle? theNewDetectionLocation)
        {
            currentGestureDefinition = null;

            if (detectedGestures.Count == 0) return false;

            // Check to see if any of the Gestures defined in the gestureInputs 
            // dictionary have been performed and detected.
            foreach (GestureDefinition userDefinedGesture in gestureInputs.Values)
            {
                foreach (GestureDefinition detectedGesture in detectedGestures)
                {
                    if (detectedGesture.Type == userDefinedGesture.Type)
                    {
                        // If a Rectangle area to check against has been passed in, then
                        // use that one, otherwise use the one originally defined
                        Rectangle areaToCheck = userDefinedGesture.CollisionArea;
                        if (theNewDetectionLocation != null)
                            areaToCheck = (Rectangle)theNewDetectionLocation;

                        // If the gesture detected was made in the area where users were
                        // interested in Input (they intersect), then a gesture input is
                        // considered detected.
                        if (detectedGesture.CollisionArea.Intersects(areaToCheck))
                        {
                            if (currentGestureDefinition == null)
                            {
                                currentGestureDefinition
                                    = new GestureDefinition(detectedGesture.Gesture);
                            }
                            else
                            {
                                // Some gestures like FreeDrag and Flick are registered many, 
                                // many times in a single Update frame. Since there is only 
                                // one variable to store the gesture info, you must add on
                                // any additional gesture values so there is a combination 
                                // of all the gesture information in currentGesture
                                currentGestureDefinition.Delta += detectedGesture.Delta;
                                currentGestureDefinition.Delta2 += detectedGesture.Delta2;
                                currentGestureDefinition.Position += detectedGesture.Position;
                                currentGestureDefinition.Position2 += detectedGesture.Position2;
                            }
                        }
                    }
                }
            }

            if (currentGestureDefinition != null) return true;

            return false;
        }

        private bool IsAccelerometerInputPressed()
        {
            foreach (KeyValuePair<Direction, float> input in accelerometerInputs)
            {
                switch (input.Key)
                {
                    case Direction.Up:
                        {
                            if (Math.Abs(currentAccelerometerReading.Y) > input.Value
                            && currentAccelerometerReading.Y < 0)
                            {
                                return true;
                            }
                            break;
                        }

                    case Direction.Down:
                        {
                            if (Math.Abs(currentAccelerometerReading.Y) > input.Value
                            && currentAccelerometerReading.Y > 0)
                            {
                                return true;
                            }
                            break;
                        }

                    case Direction.Left:
                        {
                            if (Math.Abs(currentAccelerometerReading.X) > input.Value
                            && currentAccelerometerReading.X < 0)
                            {
                                return true;
                            }
                            break;
                        }

                    case Direction.Right:
                        {
                            if (Math.Abs(currentAccelerometerReading.X) > input.Value
                            && currentAccelerometerReading.X > 0)
                            {
                                return true;
                            }
                            break;
                        }
                }
            }

            return false;
        }

        GestureDefinition currentGestureDefinition;
        public Vector2 CurrentGesturePosition()
        {
            if (currentGestureDefinition == null)
                return Vector2.Zero;

            return currentGestureDefinition.Position;
        }

        public Vector2 CurrentGesturePosition2()
        {
            if (currentGestureDefinition == null)
                return Vector2.Zero;

            return currentGestureDefinition.Position2;
        }

        public Vector2 CurrentGestureDelta()
        {
            if (currentGestureDefinition == null)
                return Vector2.Zero;

            return currentGestureDefinition.Delta;
        }

        public Vector2 CurrentGestureDelta2()
        {
            if (currentGestureDefinition == null)
                return Vector2.Zero;

            return currentGestureDefinition.Delta2;
        }


        public Vector2? CurrentTouchPosition()
        {
            // This only handles one location! if there are several being touched!!
            foreach (TouchLocation location in CurrentTouchLocationState)
            {
                switch (location.State)
                {
                    case TouchLocationState.Pressed:
                        return location.Position;

                    case TouchLocationState.Moved:
                        return location.Position;
                }
            }

            return null;
        }

        private Vector2? PreviousTouchPosition()
        {
            foreach (TouchLocation location in PreviousTouchLocationState)
            {
                switch (location.State)
                {
                    case TouchLocationState.Pressed:
                        return location.Position;

                    case TouchLocationState.Moved:
                        return location.Position;
                }
            }

            return null;
        }


        private Rectangle CurrentTouchRectangle
        {
            get
            {
                Vector2? touchPosition = CurrentTouchPosition();
                if (touchPosition == null)
                    return Rectangle.Empty;

                return new Rectangle((int)touchPosition.Value.X - 5,
                                     (int)touchPosition.Value.Y - 5,
                                     10,
                                     10);
            }
        }

        private List<Rectangle> CurrentlyTouchedRectangles
        {
            get
            {
                // This only handles one location! if there are several being touched!!
                List<Vector2> touchPositions = new List<Vector2>();
                foreach (TouchLocation location in CurrentTouchLocationState)
                {
                    switch (location.State)
                    {
                        case TouchLocationState.Pressed:
                        case TouchLocationState.Moved:
                            touchPositions.Add(location.Position);
                            break;
                    }
                }
                return touchPositions.Select(p => new Rectangle((int) p.X, (int) p.Y, 10, 10)).ToList();
            }
        }


        public Vector3 CurrentAccelerometerReading
        {
            get
            {
                return currentAccelerometerReading;
            }
        }



    }
}

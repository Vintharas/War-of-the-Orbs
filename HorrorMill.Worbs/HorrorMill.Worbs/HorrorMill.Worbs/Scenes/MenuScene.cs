using System;
using System.Collections.Generic;
using HorrorMill.Helpers.Xna.Entities;
using HorrorMill.Helpers.Xna.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace HorrorMill.Worbs.Scenes
{
    class MenuScene : Scene
    {
        private List<MenuItem> menuItems;

        public MenuScene(Game game) : base(game)
        {
            int y = 10;
            int x = 10;
            int itemNo = 1;

            MenuItem menuItemStart = new MenuItem(game, "Start", new Vector2(x, y*itemNo++), Color.White);
            menuItemStart.Clicked += new Action(menuItemStart_Clicked);
            MenuItem menuItemOptions = new MenuItem(game, "Options", new Vector2(x, 50), Color.White);
            menuItemOptions.Clicked += new Action(menuItemOptions_Clicked);

            menuItems = new List<MenuItem>();
            menuItems.Add(menuItemStart);
            menuItems.Add(menuItemOptions);

            foreach (MenuItem menuItem in menuItems)
                SceneComponents.Add(menuItem);   
        }

        void menuItemStart_Clicked()
        {
            //System.Diagnostics.Debug.WriteLine("clicked Start!");
            RaiseSwitchScene(SceneType.Game);
        }

        void menuItemOptions_Clicked()
        {
            RaiseSwitchScene(SceneType.Options);
        }

        public override void Initialize()
        {
            // enable user gestures
            TouchPanel.EnabledGestures = GestureType.Tap;

            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {

            if (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();
                if (gesture.GestureType == GestureType.Tap)
                    foreach (MenuItem menuItem in menuItems)
                        menuItem.CheckClick(gesture.Position);
            }
            base.Update(gameTime);
        }
    }
}

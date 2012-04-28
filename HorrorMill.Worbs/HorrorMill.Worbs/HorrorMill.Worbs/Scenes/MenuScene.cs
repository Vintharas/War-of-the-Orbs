using System;
using System.Collections.Generic;
using HorrorMill.Helpers.Xna.Entities;
using HorrorMill.Helpers.Xna.UI;
using HorrorMill.HorrorMill.Helpers.Xna.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace HorrorMill.Worbs.Scenes
{
    class MenuScene : Scene
    {
        private List<MenuItem> menuItems;
        private GameInput gameInput;

        public MenuScene(Game game) : base(game)
        {
            Type = SceneType.Menu;

            int y = 10;
            int x = 10;
            int itemNo = 1;

            gameInput = new GameInput();
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
            base.Initialize();
            foreach (var menuItem in menuItems)
                menuItem.GameInput = gameInput;
        }

        public override void Update(GameTime gameTime)
        {
            gameInput.BeginUpdate();
            base.Update(gameTime);
            gameInput.EndUpdate();
        }
    }
}

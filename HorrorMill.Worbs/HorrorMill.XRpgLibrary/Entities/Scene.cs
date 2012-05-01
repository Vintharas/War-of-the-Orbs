using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMill.Helpers.Xna.Entities
{
    /// <summary>
    /// Class that represents a game scene
    /// </summary>
    public class Scene : DrawableGameComponent
    {

        public List<GameComponent> SceneComponents { get; set; }
        public List<DrawableGameComponent> StaticSceneComponents { get; set; } // game components that remain static in the screen

        public SceneType Type { get; set; }
        // Each scene defines it's own transitions via this event
        public event Action<SceneType> SwitchScene;
        protected SpriteBatch spriteBatch;


        public Scene(Game game) : base(game)
        {
            SceneComponents = new List<GameComponent>();
            StaticSceneComponents = new List<DrawableGameComponent>();
        }

        public override void Initialize()
        {
            foreach (var c in SceneComponents)
                c.Initialize();
            foreach (var c in StaticSceneComponents)
                c.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = (SpriteBatch) Game.Services.GetService(typeof (SpriteBatch));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var c in SceneComponents)
                if (c.Enabled)
                    c.Update(gameTime);
            foreach (var c in StaticSceneComponents)
                if (c.Enabled)
                    c.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// Method that provides a hook for initializing the sprite batch drawing in a different way
        /// than the default
        /// </summary>
        public virtual void BeginDraw()
        {
            spriteBatch.Begin();
        }

        public override void Draw(GameTime gameTime)
        {
            BeginDraw();
            foreach (var c in SceneComponents)
            {
                if (c is DrawableGameComponent)
                {
                    DrawableGameComponent d = c as DrawableGameComponent;
                    if (d.Visible)
                        d.Draw(gameTime);
                }
            }
            spriteBatch.End();
            spriteBatch.Begin();
            foreach (var d in StaticSceneComponents)
                d.Draw(gameTime);
            spriteBatch.End();
            base.Draw(gameTime);

        }

        public void RaiseSwitchScene(SceneType scene)
        {
            if (SwitchScene != null)
                SwitchScene(scene);
        }
    }
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HorrorMill.Worbs.Entities
{
    public class Scene : DrawableGameComponent
    {
        public List<DrawableGameComponent> SceneDrawableComponents { get; set; }
        public List<GameComponent> SceneComponents { get; set; }
        public event Action<SceneType> SwitchScene;

        public Scene(Game game) : base(game)
        {
            SceneComponents = new List<GameComponent>();
            SceneDrawableComponents = new List<DrawableGameComponent>();
        }

        public override void Initialize()
        {
            foreach (var c in SceneComponents)
            {
                c.Initialize();
            }

            foreach (var d in SceneDrawableComponents)
            {
                d.Initialize();
            }

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var c in SceneComponents)
            {
                c.Update(gameTime);
            }

            foreach (var d in SceneDrawableComponents)
            {
                d.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var d in SceneDrawableComponents)
            {
                d.Draw(gameTime);
            }

            base.Draw(gameTime);
        }

        protected void RaiseSwitchScene(SceneType scene)
        {
            if (SwitchScene != null)
                SwitchScene(scene);
        }
    }
}

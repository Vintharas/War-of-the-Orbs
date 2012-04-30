using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace HorrorMill.Helpers.Xna.Entities
{
    public class Scene : DrawableGameComponent
    {
        public List<GameComponent> SceneComponents { get; set; }
        // Each scene defines it's own transitions via this event
        public event Action<SceneType> SwitchScene;
        public SceneType Type { get; set; }

        public Scene(Game game) : base(game)
        {
            SceneComponents = new List<GameComponent>();
        }

        public override void Initialize()
        {
            foreach (var c in SceneComponents)
                c.Initialize();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var c in SceneComponents)
                if (c.Enabled)
                    c.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var c in SceneComponents)
            {
                if (c is DrawableGameComponent)
                {
                    DrawableGameComponent d = c as DrawableGameComponent;
                    if (d.Visible)
                        d.Draw(gameTime);
                }
            }

            base.Draw(gameTime);
        }

        public void RaiseSwitchScene(SceneType scene)
        {
            if (SwitchScene != null)
                SwitchScene(scene);
        }
    }
}

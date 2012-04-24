using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace HorrorMill.Helpers.Xna.Entities
{
    public abstract class SceneManager : DrawableGameComponent
    {
        private Dictionary<SceneType, Scene> scenes;
        private Scene activeScene;

        public Scene ActiveScene
        {
            get { return activeScene; }
            protected set
            {
                // disable previous scene
                if (activeScene != null)
                    activeScene.Enabled = false;
                activeScene = value;
                activeScene.Enabled = true;
            }
        }

        public SceneManager(Game game) : base(game)
        {
            scenes = new Dictionary<SceneType, Scene>();
        }

        public void AddScene(SceneType sceneType, Scene scene)
        {
            scene.Enabled = false;
            scene.SwitchScene += OnSwitchScene;
            scenes[sceneType] = scene;
        }

        public void SetActiveScene(SceneType sceneType)
        {
            ActiveScene = scenes[sceneType];
        }

        private void OnSwitchScene(SceneType sceneType)
        {
            foreach (var s in scenes.Values)
                if(s.Type == sceneType)
                    s.Initialize();

            SetActiveScene(sceneType);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            activeScene.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            activeScene.Draw(gameTime);
            base.Draw(gameTime);
        }

    }
}
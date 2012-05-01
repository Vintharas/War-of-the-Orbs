using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HorrorMill.Helpers.Xna.Entities
{
    public abstract class SceneManager : DrawableGameComponent
    {
        private Dictionary<SceneType, Scene> scenes;  // dictionary that holds a reference to all scenes so they only need to be instantiated once
        private Stack<Scene> previousScenes;  // stack that stores previous scenes to enable the "Back button functionality"
        private Scene activeScene;

        public Scene ActiveScene
        {
            get { return activeScene; }
            protected set
            {
                // disable previous scene
                if (activeScene != null)
                {
                    activeScene.Enabled = false;
                    activeScene.Visible = false;
                }
                activeScene = value;
                activeScene.Enabled = true;
                activeScene.Visible = true;
            }
        }

        public SceneManager(Game game) : base(game)
        {
            scenes = new Dictionary<SceneType, Scene>();
            previousScenes = new Stack<Scene>();
        }

        public void AddScene(SceneType sceneType, Scene scene)
        {
            scene.Enabled = false;
            scene.SwitchScene += OnSwitchScene;
            scenes[sceneType] = scene;
        }

        public void SetActiveScene(SceneType sceneType)
        {
            previousScenes.Push(ActiveScene);
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
            // Back button functionality
            if (PlayerPressedBackButton())
            {
                if (ThereArePreviousScenes())
                    ActiveScene = previousScenes.Pop();
                else
                    Game.Exit();
            }
            // updates the activeScene only
            activeScene.Update(gameTime);
            base.Update(gameTime);
        }

        private static bool PlayerPressedBackButton()
        {
            return GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed;
        }

        private bool ThereArePreviousScenes()
        {
            return previousScenes.Count == 0;
        }

        public override void Draw(GameTime gameTime)
        {
            activeScene.Draw(gameTime);
            base.Draw(gameTime);
        }

    }
}
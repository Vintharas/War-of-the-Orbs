using HorrorMill.Helpers.Xna.Entities;
using Microsoft.Xna.Framework;

namespace HorrorMill.Worbs.Entities
{
    public class WorbsSceneManager : SceneManager
    {
        public WorbsSceneManager(Game game) : base(game)
        {
            AddScene(SceneType.Menu, new MenuScene(game));
            AddScene(SceneType.Game, new GameScene(game));
            AddScene(SceneType.Options, new OptionsScene(game));
            SetActiveScene(SceneType.Menu);
        }
    }
}
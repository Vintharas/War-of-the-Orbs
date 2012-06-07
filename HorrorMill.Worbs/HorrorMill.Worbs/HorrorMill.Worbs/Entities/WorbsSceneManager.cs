using HorrorMill.Helpers.Xna.Entities;
using HorrorMill.Worbs.Scenes;
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
            AddScene(SceneType.Inventory, new InventoryScene(game, GameScene.Player));
            SetActiveScene(SceneType.Menu);
            ActiveScene.Initialize(); //First scene must be initialized
        }
    }
}
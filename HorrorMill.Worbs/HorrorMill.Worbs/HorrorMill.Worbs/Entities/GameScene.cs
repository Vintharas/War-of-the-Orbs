using Microsoft.Xna.Framework;

namespace HorrorMill.Worbs.Entities
{
    public class GameScene : Scene
    {
        public GameScene(Game game) : base(game)
        {
            Font gameTitle = new Font(game, "This is The Game", new Vector2(0, 0), Color.Red);
            SceneDrawableComponents.Add(gameTitle);
        }

    }
}
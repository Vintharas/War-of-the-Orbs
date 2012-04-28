using HorrorMill.Helpers.Xna.Entities;
using Microsoft.Xna.Framework;

namespace HorrorMill.Worbs.Scenes
{
    public class OptionsScene : Scene
    {

        public OptionsScene(Game game): base(game)
        {
            Type = SceneType.Options;

            Font optionsText = new Font(game, "This is the Options!", new Vector2(0, 0), Color.Red);
            // Player
            SceneComponents.Add(optionsText);
        }

    }
}
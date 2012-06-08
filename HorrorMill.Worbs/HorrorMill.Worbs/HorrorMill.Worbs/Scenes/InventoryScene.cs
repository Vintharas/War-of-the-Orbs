using HorrorMill.Engines.Rpg.Entities;
using HorrorMill.Helpers.Xna.Entities;
using Microsoft.Xna.Framework;

namespace HorrorMill.Worbs.Scenes
{
    public class InventoryScene : Scene
    {
        private readonly Player player;
        private Inventory playerInventory { get { return player.Entity.Inventory; } }

        public InventoryScene(Game game, Player player) : base(game)
        {
            Type = SceneType.Inventory;
            this.player = player;
        }


    }
}
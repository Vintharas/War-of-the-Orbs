using HorrorMill.Engines.Rpg.Entities;
using HorrorMill.Engines.TileEngine.Entities;
using HorrorMill.Helpers.Xna.Entities;
using HorrorMill.Helpers.Xna.UI;
using HorrorMill.Worbs.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMill.Worbs.Scenes
{
    public class GameScene : Scene
    {
        public static Player Player { get; private set; } // TODO: sacrilege!

        // Separate UI from actual game -> UI in GameScene, game within World/Level structure
        private WorbsWorld theWorldOfWorbs;
        private Camera camera;

        private GameControls controls;
        private CrossControl CrossControl { get { return controls.CrossControl; } }
        private AttackControl AttackControl { get { return controls.AttackControl; } }
        private HealthBar playerHealthBar;

        public GameScene(Game game): base(game)
        {
            Type = SceneType.Game;
            // Initialize camera and player
            camera = new Camera(game, new Rectangle(0, 0, 800, 480)); // TODO: Fix this so it is not hardcoded -> GraphicsDevice is not initialized at this point, need to wrap it somehow (perhaps add it as a service) so the camera will access it later when it's initialized 
            // Player
            Player = new Player(game, camera);
            // World
            theWorldOfWorbs = new WorbsWorld(game, Player);
            SceneComponents.Add(theWorldOfWorbs);
            //User interface
            controls = new GameControls(game);
            GraphicButton inventoryButton = new GraphicButton(game, "ShowInventory", "Sprites/menu-button-30", new Vector2(760, 10));
            controls.AddControl(inventoryButton);
            inventoryButton.Clicked += ShowInventory;
            StaticSceneComponents.Add(controls);
            playerHealthBar = new HealthBar(game, "Sprites/player-health-bar", "Sprites/blood-stream", new Vector2(10, 10), Player.Health);
            StaticSceneComponents.Add(playerHealthBar);
        }

        private void ShowInventory()
        {
            RaiseSwitchScene(SceneType.Inventory);
        }

        public override void Update(GameTime gameTime)
        {
            // Update the status of the game based on user input
            // Move player
            Player.Move(CrossControl.Motion);
            camera.LockToSpriteRectangle(Player.Rectangle);
            // Player Attack
            if (AttackControl.Attacking)
                theWorldOfWorbs.CurrentLevel.AddProjectile(Player.PositionMiddleCenter, Player.Direction, Player.Damage, true);

            // Update map, enemies, projectiles, etc
            base.Update(gameTime);
            // Update health bar
            playerHealthBar.CurrentHealth = Player.Health;
        }

        public override void BeginDraw()
        {
            // this overriden draw method offsets everything the spriteBatch
            // draws based on the camera position
            spriteBatch.Begin(SpriteSortMode.Deferred, 
                              BlendState.AlphaBlend, 
                              SamplerState.PointClamp, 
                              null, null, null,
                              Player.Camera.Transformation);
        }


    }
}
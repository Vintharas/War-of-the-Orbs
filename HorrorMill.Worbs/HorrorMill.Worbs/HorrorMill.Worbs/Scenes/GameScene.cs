using HorrorMill.Engines.Rpg;
using HorrorMill.Engines.Rpg.Entities;
using HorrorMill.Engines.Rpg.Maps;
using HorrorMill.Engines.TileEngine;
using HorrorMill.Engines.TileEngine.Entities;
using HorrorMill.Helpers.Xna.Entities;
using HorrorMill.Helpers.Xna.UI;
using HorrorMill.Worbs.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace HorrorMill.Worbs.Scenes
{
    public class GameScene : Scene
    {
        // Separate UI from actual game -> UI in GameScene, game within World/Level structure
        private World theWorldOfWorbs;

        // testing custom tile TheTileEngine
        private TheTileEngine engine = new TheTileEngine(32, 32);
        private TileMap map;
        private Player player;
        private Camera camera;

        private GameControls controls;
        private CrossControl CrossControl { get { return controls.CrossControl; } }
        private AttackControl AttackControl { get { return controls.AttackControl; } }
        private HealthBar playerHealthBar;

        public GameScene(Game game): base(game)
        {
            Type = SceneType.Game;
            //base.Initialized = true;
            
            // Initialize map settings
            camera = new Camera(game, new Rectangle(0, 0, 800, 480)); // TODO: Fix this so it is not hardcoded -> GraphicsDevice is not initialized at this point, need to wrap it somehow (perhaps add it as a service) so the camera will access it later when it's initialized 

            MapInformation mapInformation = new MapInformation(game);
            mapInformation.LoadMapFromXML("test map");
            MapGenerator mapGen = new MapGenerator(game);
            map = mapGen.Generate(mapInformation, camera);
            SceneComponents.Add(map);

            // Player
            player = new Player(game, camera);
            SceneComponents.Add(player);

            //Enemies
            foreach (Enemy e in mapGen.Enemies)
                SceneComponents.Add(e);

            //User interface
            controls = new GameControls(game);
            GraphicButton menuButton = new GraphicButton(game, "Sprites/menu-button-30", new Vector2(760, 10));
            controls.AddControl(menuButton);
            StaticSceneComponents.Add(controls);
            playerHealthBar = new HealthBar(game, "Sprites/player-health-bar", "Sprites/blood-stream", new Vector2(10, 10), player.Health);
            StaticSceneComponents.Add(playerHealthBar);

        }

        public override void Update(GameTime gameTime)
        {
            player.Move(CrossControl.Motion);
            camera.LockToSpriteRectangle(player.Rectangle);
            
            if (AttackControl.Attacking)
                AddProjectile(player.PositionMiddleCenter);
            
            //Check collision for Projectile on Enemies
            foreach (Projectile p in SceneComponents.OfType<Projectile>().ToList())
            {
                foreach (Enemy e in SceneComponents.OfType<Enemy>().ToList())
                {
                    if (e.CollisionRectangle.Intersects(p.CollisionRectangle) && e.Visible)
                    {
                        e.TakeDamage(player.Damage);
                        p.Active = false;
                    }
                }
            }
            //Check collision for Enemy
            foreach (Enemy e in SceneComponents.OfType<Enemy>().ToList())
            {
                if (player.Rectangle.Intersects(e.CollisionRectangle))
                {
                    // both player and enemy take damage
                    player.Health -= e.Damage;
                    e.TakeDamage(player.Damage);
                }
            }

            // Update health bar
            playerHealthBar.CurrentHealth = player.Health;

            //Check Player for Map


            //Do some cleaning
            CleanDiedEnemies();
            CleanProjectilesOutOfView();

            base.Update(gameTime);
        }

        public override void BeginDraw()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, 
                              BlendState.AlphaBlend, 
                              SamplerState.PointClamp, 
                              null, null, null,
                              player.Camera.Transformation);
        }

        private void CleanDiedEnemies()
        {
            foreach (Enemy e in SceneComponents.OfType<Enemy>().ToList())
            {
                if (e.Dead)
                    SceneComponents.Remove(e);
            }
        }

        private void AddProjectile(Vector2 position)
        {
            var projectileSpeed = player.Direction*10;
            Projectile p = new Projectile(this.Game, "SpriteSheets/Projectiles/fire", position, 20, projectileSpeed, camera);
            p.Initialize();
            this.SceneComponents.Add(p);
        }

        private void CleanProjectilesOutOfView()
        {
            foreach (Projectile p in SceneComponents.OfType<Projectile>().ToList())
            {
                if (!p.Active)
                    SceneComponents.Remove(p);
            }

            //for (int i = SceneComponents.Count - 1; i >= 0; i--)
            //{
            //    if (SceneComponents[i] is Projectile)
            //    {
            //        Projectile p = SceneComponents[i] as Projectile;
            //        if (!p.Active) SceneComponents.RemoveAt(i);
            //    }
            //}
        }
    }
}
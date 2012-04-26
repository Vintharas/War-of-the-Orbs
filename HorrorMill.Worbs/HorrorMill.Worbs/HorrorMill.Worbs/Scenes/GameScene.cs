using HorrorMill.Engines.Rpg;
using HorrorMill.Engines.Rpg.Entities;
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
        // testing custom tile engine
        private Engine engine = new Engine(32, 32);
        private TileMap map;
        private Player player;
        private Camera camera;

        private GameControls controls;
        private CrossControl CrossControl { get { return controls.CrossControl; } }
        private AttackControl AttackControl { get { return controls.AttackControl; } }

        public GameScene(Game game): base(game)
        {
            base.Type = SceneType.Game;
            //base.Initialized = true;
            
            // Initialize map settings
            camera = new Camera(game, new Rectangle(0, 0, 800, 480)); // TODO: Fix this so it is not hardcoded -> GraphicsDevice is not initialized at this point, need to wrap it somehow (perhaps add it as a service) so the camera will access it later when it's initialized 

            MapInformation mapInformation = new MapInformation(Game);
            mapInformation.LoadMapFromXML("test map");
            MapGenerator mapGen = new MapGenerator(game);
            map = mapGen.Generate(mapInformation, camera);
            SceneComponents.Add(map);

            //Font gameTitle = new Font(game, "Generated map", new Vector2(0, 0), Color.Black);
            //SceneComponents.Add(gameTitle);

            // Player
            player = new Player(game, camera);
            SceneComponents.Add(player);

            //Enemies
            foreach (Enemy e in mapGen.Enemies)
                SceneComponents.Add(e);

            //Move Control
            controls = new GameControls(game);
            SceneComponents.Add(controls);
        }

        public override void Update(GameTime gameTime)
        {
            player.Move(CrossControl.Motion);
            camera.LockToSpriteRectangle(player.Rectangle);
            
            if (AttackControl.Attacking)
                AddProjectile(player.PositionMiddleCenter);
            CleanProjectilesOutOfView();

            //Check collision for Projectile on Enemies
            foreach (Projectile p in SceneComponents.OfType<Projectile>().ToList())
            {
                foreach (Enemy e in SceneComponents.OfType<Enemy>().ToList())
                {
                    if (e.CollisionRectangle.Intersects(p.CollisionRectangle))
                    {
                        e.Visible = false;
                    }
                }
            }

            //Check collision for Enemy
            foreach (Enemy e in SceneComponents.OfType<Enemy>().ToList())
            {
                if (player.Rectangle.Intersects(e.CollisionRectangle))
                {
                    e.Visible = false;
                }
            }

            base.Update(gameTime);
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
            for (int i = SceneComponents.Count - 1; i >= 0; i--)
            {
                if (SceneComponents[i] is Projectile)
                {
                    Projectile p = SceneComponents[i] as Projectile;
                    if (!p.Active) SceneComponents.RemoveAt(i);
                }
            }
        }
    }
}
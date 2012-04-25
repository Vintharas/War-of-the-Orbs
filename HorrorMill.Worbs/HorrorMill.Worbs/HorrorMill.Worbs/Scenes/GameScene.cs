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

namespace HorrorMill.Worbs.Scenes
{
    public class GameScene : Scene
    {
        // testing custom tile engine
        private Engine engine = new Engine(32, 32);
        private TileMap map;
        private Player player;
        private Camera camera;
        private CrossControl crossControl;
        private AttackControl attackControl;

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
            crossControl = new CrossControl(game);
            SceneComponents.Add(crossControl);

            //Attack Control
            attackControl = new AttackControl(game);
            SceneComponents.Add(attackControl);
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                RaiseSwitchScene(SceneType.Menu);

            player.Move(crossControl.Motion);
            camera.LockToSpriteRectangle(player.Rectangle);
            
            if (attackControl.Attacking)
                AddProjectile(player.PositionMiddleCenter);
            CleanProjectilesOutOfView();

            base.Update(gameTime);
        }

        private void AddProjectile(Vector2 position)
        {
            var projectileSpeed = player.Direction*5;
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
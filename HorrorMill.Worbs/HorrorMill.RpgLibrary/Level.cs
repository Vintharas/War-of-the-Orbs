using System.Collections.Generic;
using System.Linq;
using HorrorMill.Engines.Rpg.Entities;
using HorrorMill.Engines.TileEngine.Entities;
using Microsoft.Xna.Framework;

namespace HorrorMill.Engines.Rpg
{
    public class Level : DrawableGameComponent
    {
        public string Name { get; private set; }
        private readonly TileMap map;
        private List<Enemy> enemies;
        private Player player;
        private List<GameComponent> levelComponents; 
         

        public Level(Game game, string name, TileMap map, List<Enemy> enemies, Player player) : base(game)
        {
            levelComponents = new List<GameComponent>();
            Name = name;
            this.map = map;
            this.enemies = enemies;
            this.player = player;
            
            levelComponents.Add(map);
            levelComponents.Add(player);
            foreach (var enemy in enemies)
                levelComponents.Add(enemy);
        }

        public override void Initialize()
        {
            foreach (var levelComponent in levelComponents)
                levelComponent.Initialize();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // Collision detection
            bool mapCollision = map.CheckCollision(player.Rectangle);
            if (mapCollision)
            {
                player.TakeDamage(10);
            }

            //Check collision for Projectile on Enemies
            foreach (Projectile p in levelComponents.OfType<Projectile>().ToList())
            {
                foreach (Enemy e in levelComponents.OfType<Enemy>().ToList())
                {
                    if (e.CollisionRectangle.Intersects(p.CollisionRectangle) && e.Visible)
                    {
                        e.TakeDamage(player.Damage);
                        p.Active = false;
                    }
                }
            }
            //Check collision for Enemy
            foreach (Enemy e in levelComponents.OfType<Enemy>().ToList())
            {
                if (player.Rectangle.Intersects(e.CollisionRectangle))
                {
                    // both player and enemy take damage
                    player.TakeDamage(e.Damage);
                    e.TakeDamage(player.Damage);
                }
            }

            //Do some cleaning
            CleanDiedEnemies();
            CleanProjectilesOutOfView();
            // Update level game components
            foreach (var levelComponent in levelComponents)
                levelComponent.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var levelComponent in levelComponents)
            {
                if (levelComponent is DrawableGameComponent)
                {
                    (levelComponent as DrawableGameComponent).Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }


        private void CleanDiedEnemies()
        {
            foreach (Enemy e in levelComponents.OfType<Enemy>().ToList())
            {
                if (e.Dead)
                    levelComponents.Remove(e);
            }
        }

        public void AddProjectile(Vector2 position)
        {
            var projectileSpeed = player.Direction * 10;
            Projectile p = new Projectile(this.Game, "SpriteSheets/Projectiles/fire", position, 20, projectileSpeed, player.Camera);
            p.Initialize();
            this.levelComponents.Add(p);
        }

        private void CleanProjectilesOutOfView()
        {
            foreach (Projectile p in levelComponents.OfType<Projectile>().ToList())
            {
                if (!p.Active)
                    levelComponents.Remove(p);
            }
        }

    }
}
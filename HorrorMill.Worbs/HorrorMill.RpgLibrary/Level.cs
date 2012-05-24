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
                player.UndoMove();
            }

            //Check collision for Projectile on...
            foreach (Projectile p in levelComponents.OfType<Projectile>().ToList())
            {
                //... Enemies
                foreach (Enemy e in levelComponents.OfType<Enemy>().ToList())
                {
                    if (p.PlayerProjectile && e.CollisionRectangle.Intersects(p.CollisionRectangle) && e.Visible)
                    {
                        e.TakeDamage(player.Damage);
                        p.Active = false;
                    }
                }

                //... Player
                if(!p.PlayerProjectile && p.CollisionRectangle.Intersects(player.Rectangle)) 
                {
                    player.TakeDamage(p.Damage);
                    p.Active = false;
                }

                //... Map
                bool mapProjectileCollision = map.CheckCollision(p.CollisionRectangle);
                if (mapProjectileCollision)
                {
                    p.Active = false;
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

            //Try to make Enemy kill you
            foreach (Enemy e in levelComponents.OfType<Enemy>().ToList())
            {
                if (EnemyAI.IsPlayerInRange(e.DetectionRange, player.Position, e.Position))
                {
                    //Attack
                    Vector2 direction = EnemyAI.GetAttackDirection(player.PositionMiddleCenter, e.PositionMiddleCenter);
                    if (EnemyAI.IsPlayerInRange(e.AttackRange, player.Position, e.Position) && direction != Vector2.Zero)
                    {
                        if (e.CanAttack(gameTime))
                        {
                            AddProjectile(e.PositionMiddleCenter, direction, e.Damage, false);
                            e.Move(Vector2.Zero); //Improve this so we get the right sprite
                        }
                    }
                    //Move to player
                    else
                    {
                        Vector2 moveDirection = EnemyAI.GetMovementDirection(player.PositionMiddleCenter, e.PositionMiddleCenter);
                        e.Move(moveDirection);
                    }
                }
                //else add else for random movement?
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
            map.DrawBeforePlayer(gameTime);
            foreach (var levelComponent in levelComponents)
            {
                if (levelComponent is DrawableGameComponent)
                {
                    (levelComponent as DrawableGameComponent).Draw(gameTime);
                }
            }
            map.DrawAfterPlayer(gameTime);
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

        public void AddProjectile(Vector2 position, Vector2 direction, int damage, bool playerProjectile)
        {
            //var projectileSpeed = player.Direction * 10;
            var projectileSpeed = direction*10;
            // TODO: need to tie the projectile speed to the player's weapon somehow
            Projectile p = new Projectile(this.Game, "SpriteSheets/Projectiles/fire", position, damage, projectileSpeed, playerProjectile, player.Camera);
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
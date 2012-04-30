using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using HorrorMill.Helpers.Xna.Sprites;
using Microsoft.Xna.Framework.Graphics;
using HorrorMill.Engines.TileEngine.Entities;

namespace HorrorMill.Engines.Rpg
{
    public class Enemy : DrawableGameComponent
    {
        public enum State
        {
            IdleDown,
            IdleUp,
            IdleRight,
            IdleLeft,
            Walk,
            WalkUp,
            WalkDown,
            Attack
        }

        private int enemyHealth;
        public int Health { get { return enemyHealth; } }
        private int enemyDamage;
        public int Damage { get { return enemyDamage; } }
        private Vector2 enemyPosition;
        public Vector2 Position { get { return enemyPosition; } }
        private MultiSprite enemySprite;
        private SpriteBatch spriteBatch;
        private Camera Camera;
        private bool dead = false;
        public bool Dead { get { return dead; } }

        public Enemy(Game game, Camera camera) : base(game)
        {
            Camera = camera;
        }

        public void Create(EnemyInformation enemyInfo, Vector2 position)
        {
            enemyHealth = enemyInfo.Health;
            enemyDamage = enemyInfo.Damage;
            enemyPosition = position;

            enemySprite = new MultiSprite(position, 70);
            enemySprite.States.Add(State.IdleDown.ToString(), enemyInfo.SpriteIdleDown);
            enemySprite.States.Add(State.IdleUp.ToString(), enemyInfo.SpriteIdleUp);
            enemySprite.States.Add(State.IdleRight.ToString(), enemyInfo.SpriteIdleRight);
            enemySprite.States.Add(State.IdleLeft.ToString(), enemyInfo.SpriteIdleLeft);
            enemySprite.States.Add(State.Walk.ToString(), enemyInfo.SpriteWalk);
            enemySprite.States.Add(State.WalkUp.ToString(), enemyInfo.SpriteWalkUp);
            enemySprite.States.Add(State.WalkDown.ToString(), enemyInfo.SpriteWalkDown);
            //multiSprite.States.Add(State.Attack.ToString(), );
            enemySprite.CurrentState = State.IdleDown.ToString();
        }

        public void TakeDamage(int dmg)
        {
            enemyHealth -= dmg;
            if (enemyHealth < 1)
            {
                dead = true;
            }
        }

        protected override void LoadContent()
        {
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            enemySprite.LoadContent(Game.Content);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // update sprites
            enemySprite.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            enemySprite.Draw(gameTime, spriteBatch, Camera.Position);
            base.Draw(gameTime);
        }

        public Rectangle CollisionRectangle { get { return enemySprite.CollisionRect; } }
    }
}

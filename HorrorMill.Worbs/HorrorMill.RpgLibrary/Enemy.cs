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

        public Enemy(Game game, Camera camera) : base(game)
        {
            Camera = camera;
        }

        public void Create(int health, int damage, Vector2 position)
        {
            enemyHealth = health;
            enemyDamage = damage;
            enemyPosition = position;

            enemySprite = new MultiSprite(position, 70);
            enemySprite.States.Add(State.IdleDown.ToString(), new SpriteSheet("SpriteSheets/Player/Wizard/IdleDown", new Point(0, 0), new Point(50, 50), new Point(1, 1), SpriteDirection.Right));
            enemySprite.States.Add(State.IdleUp.ToString(), new SpriteSheet("SpriteSheets/Player/Wizard/IdleUp", new Point(0, 0), new Point(50, 50), new Point(1, 1), SpriteDirection.Right));
            enemySprite.States.Add(State.IdleRight.ToString(), new SpriteSheet("SpriteSheets/Player/Wizard/IdleRight", new Point(0, 0), new Point(50, 50), new Point(1, 1), SpriteDirection.Right));
            enemySprite.States.Add(State.IdleLeft.ToString(), new SpriteSheet("SpriteSheets/Player/Wizard/IdleLeft", new Point(0, 0), new Point(50, 50), new Point(1, 1), SpriteDirection.Right));
            enemySprite.States.Add(State.Walk.ToString(), new SpriteSheet("SpriteSheets/Player/Wizard/Walk", new Point(0, 0), new Point(50, 50), new Point(2, 1), SpriteDirection.Right));
            enemySprite.States.Add(State.WalkUp.ToString(), new SpriteSheet("SpriteSheets/Player/Wizard/WalkUp", new Point(0, 0), new Point(50, 50), new Point(2, 1), SpriteDirection.Right));
            enemySprite.States.Add(State.WalkDown.ToString(), new SpriteSheet("SpriteSheets/Player/Wizard/WalkDown", new Point(0, 0), new Point(50, 50), new Point(2, 1), SpriteDirection.Right));
            //multiSprite.States.Add(State.Attack.ToString(), new SpriteSheet("SpriteSheets/Player/Wizard/Attack", new Point(0, 0), new Point(140, 160), new Point(2, 1), SpriteDirection.Right));
            enemySprite.CurrentState = State.IdleDown.ToString();
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

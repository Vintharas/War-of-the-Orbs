using HorrorMill.Helpers.Xna.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorrorMill.Engines.Rpg.Entities
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

        private int health;
        public int Health { get { return health; } }
        private int damage;
        public int Damage { get { return damage; } }
        private Vector2 position;
        public Vector2 Position { get { return position; } }
        public Vector2 PositionMiddleCenter { get { return new Vector2(Position.X + enemySprite.Rectangle.Width / 2, Position.Y + enemySprite.Rectangle.Height / 2); } }
        private MultiSprite enemySprite;
        private SpriteBatch spriteBatch;
        private int millisecondsSinceLastAttack = 500;
        public int DetectionRange { get { return 400; } }
        public int AttackRange { get { return 300; } }

        private bool dead = false;
        public bool Dead { get { return dead; } }


        public Enemy(Game game) : base(game)
        {
        }

        public void Create(EnemyInformation enemyInfo, Vector2 enemyPosition)
        {
            health = enemyInfo.Health;
            damage = enemyInfo.Damage;
            position = enemyPosition;

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
            health -= dmg;
            if (health < 1)
            {
                dead = true;
            }
        }

        public bool CanAttack(GameTime gameTime)
        {   
            millisecondsSinceLastAttack += gameTime.ElapsedGameTime.Milliseconds;
            if (millisecondsSinceLastAttack >= 500)
            {
                millisecondsSinceLastAttack = 0;
                return true;
            }

            return false;
        }

        public void Move(Vector2 motion)
        {
            // if there is no motion, the player sprite is set to an idle state
            if (motion == Vector2.Zero)
            {
                if (enemySprite.PreviousState == State.Walk.ToString())
                    enemySprite.CurrentState = State.IdleRight.ToString();
                if (enemySprite.PreviousState == State.WalkDown.ToString())
                    enemySprite.CurrentState = State.IdleDown.ToString();
                if (enemySprite.PreviousState == State.WalkUp.ToString())
                    enemySprite.CurrentState = State.IdleUp.ToString();
            }
            else // otherwise update the sprite to reflect the player action
            {
                if (motion.Y > 0)
                    enemySprite.CurrentState = State.WalkDown.ToString();
                else
                    enemySprite.CurrentState = State.WalkUp.ToString();

                if (motion.X != 0)
                    enemySprite.CurrentState = State.Walk.ToString();

                motion.Normalize();
                enemySprite.Move((int)motion.X, (int)motion.Y);
                this.position = enemySprite.Position;
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
            enemySprite.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }

        public Rectangle CollisionRectangle { get { return enemySprite.CollisionRect; } }
    }
}

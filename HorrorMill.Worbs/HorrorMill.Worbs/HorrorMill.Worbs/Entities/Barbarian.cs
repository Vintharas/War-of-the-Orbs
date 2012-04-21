using HorrorMill.Helpers.Xna.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HorrorMill.Worbs.Entities
{
    public class Barbarian : DrawableGameComponent
    {
        public enum State
        {
            Idle,
            Walk,
            WalkUp,
            Attack
        }

        private MultiSprite multiSprite;
        private readonly SpriteBatch spriteBatch;

        public Barbarian(Game game, Vector2 startingPosition, SpriteBatch spriteBatch) : base(game)
        {
            multiSprite = new MultiSprite(startingPosition, 70);
            multiSprite.States.Add(State.Idle.ToString(), new SpriteSheet("Images/Player/WarriorStill", new Point(0,0), new Point(120, 160), new Point(1,1), SpriteDirection.Right));
            multiSprite.States.Add(State.Walk.ToString(), new SpriteSheet("Images/Player/WarriorWalking", new Point(0,0), new Point(80, 140), new Point(4, 1), SpriteDirection.Right));
            multiSprite.States.Add(State.WalkUp.ToString(), new SpriteSheet("Images/Player/WarriorWalkingUp", new Point(0,0), new Point(80, 140), new Point(4, 1), SpriteDirection.Right));
            multiSprite.States.Add(State.Attack.ToString(), new SpriteSheet("Images/Player/WarriorAttack", new Point(0,0), new Point(140, 160), new Point(2,1), SpriteDirection.Right));
            multiSprite.CurrentState = State.Idle.ToString();
            this.spriteBatch = spriteBatch;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            multiSprite.LoadContent(Game.Content);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // get input
            KeyboardState keyboardState = Keyboard.GetState();
            foreach (var key in keyboardState.GetPressedKeys())
            {

                if (key == Keys.Right)
                {
                    multiSprite.CurrentState = State.Walk.ToString();
                    multiSprite.Move(10, 0);
                }
                if (key == Keys.Left)
                {
                    multiSprite.CurrentState = State.Walk.ToString();
                    multiSprite.Move(-10, 0);
                }
                if (key == Keys.Down)
                {
                    multiSprite.CurrentState = State.Walk.ToString();
                    multiSprite.Move(0, 10);
                }
                if (key == Keys.Up)
                {
                    multiSprite.CurrentState = State.WalkUp.ToString();
                    multiSprite.Move(0, -10);
                }
                if (key == Keys.Space)
                {
                    multiSprite.CurrentState = State.Attack.ToString();
                }
               


            }
            if (keyboardState.GetPressedKeys().Length == 0)
                multiSprite.CurrentState = State.Idle.ToString();
            // update sprites
            multiSprite.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            multiSprite.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
    }
}
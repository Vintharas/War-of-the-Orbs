using HorrorMill.Helpers.Xna.Sprites;

namespace HorrorMill.Engines.Rpg.Entities
{
    public class EnemyInformation
    {
        private readonly Entity entity;
        private string name;

        public EnemyInformation(string name, Entity entity)
        {
            this.name = name;
            this.entity = entity;
        }

        public string Name { get { return name; } }
        public Entity Entity { get { return entity; } }

        private SpriteSheet spriteIdleDown;
        public SpriteSheet SpriteIdleDown { get { return spriteIdleDown; } set { spriteIdleDown = value; } }
        private SpriteSheet spriteIdleUp;
        public SpriteSheet SpriteIdleUp { get { return spriteIdleUp; } set { spriteIdleUp = value; } }
        private SpriteSheet spriteIdleRight;
        public SpriteSheet SpriteIdleRight { get { return spriteIdleRight; } set { spriteIdleRight = value; } }
        private SpriteSheet spriteIdleLeft;
        public SpriteSheet SpriteIdleLeft { get { return spriteIdleLeft; } set { spriteIdleLeft = value; } }
        private SpriteSheet spriteWalk;
        public SpriteSheet SpriteWalk { get { return spriteWalk; } set { spriteWalk = value; } }
        private SpriteSheet spriteWalkUp;
        public SpriteSheet SpriteWalkUp { get { return spriteWalkUp; } set { spriteWalkUp = value; } }
        private SpriteSheet spriteWalkDown;
        public SpriteSheet SpriteWalkDown { get { return spriteWalkDown; } set { spriteWalkDown = value; } }
        private SpriteSheet spriteAttack;
        public SpriteSheet SpriteAttack { get { return spriteAttack; } set { spriteAttack = value; } }


    }
}

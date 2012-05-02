using HorrorMill.Helpers.Xna.Sprites;

namespace HorrorMill.Engines.Rpg.Entities
{
    public class EnemyInformation
    {
        private string name;
        private int health;
        private int damage;
        private int armor;

        public EnemyInformation(string enemyName, int enemyHealth, int enemyDamage)
        {
            name = enemyName;
            health = enemyHealth;
            damage = enemyDamage;
        }

        public string Name { get { return name; } }

        public int Health { get { return health; } }

        public int Damage { get { return damage; } }

        public int Armor { get { return armor; } }

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

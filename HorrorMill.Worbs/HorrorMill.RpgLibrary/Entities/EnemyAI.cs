using Microsoft.Xna.Framework;

namespace HorrorMill.Engines.Rpg.Entities
{
    public class EnemyAI
    {
        public static bool IsPlayerInRange(int rangeCheck, Vector2 playerPosition, Vector2 enemyPosition) 
        {
            int range;

            if (playerPosition.X == enemyPosition.X)
            {
                if(playerPosition.Y > enemyPosition.Y)
                    range = (int)(playerPosition.Y - enemyPosition.Y);
                else
                    range = (int)(enemyPosition.Y - playerPosition.Y);
            }
            else if (playerPosition.Y == enemyPosition.Y)
            {
                if (playerPosition.X > enemyPosition.X)
                    range = (int)(playerPosition.X - enemyPosition.X);
                else
                    range = (int)(enemyPosition.X - playerPosition.X);
            }
            else
            {
                int x, y;
                if (playerPosition.Y > enemyPosition.Y)
                    y = (int)(playerPosition.Y - enemyPosition.Y);
                else
                    y = (int)(enemyPosition.Y - playerPosition.Y);

                if (playerPosition.X > enemyPosition.X)
                    x = (int)(playerPosition.X - enemyPosition.X);
                else
                    x = (int)(enemyPosition.X - playerPosition.X);

                range = (int)System.Math.Sqrt(x*x + y*y);
            }

            if (range <= rangeCheck)
                return true;
            else
                return false;
        }

        public static Vector2 GetAttackDirection(Vector2 playerPosition, Vector2 enemyPosition)
        {
            int attackSizeMax = 20; //ToDo get this from sprite /2?
            int attackSizeMin = -20;
            int y = (int)(playerPosition.X - enemyPosition.X); //TODO why flipped?!
            int x = (int)(playerPosition.Y - enemyPosition.Y); //TODO why flipped?!

            if (x < attackSizeMax && x > attackSizeMin)
            {
                if (playerPosition.X > enemyPosition.X)
                    return new Vector2(1, 0);
                else
                    return new Vector2(-1, 0);
            }
            else if (y < attackSizeMax && y > attackSizeMin)
            {
                if (playerPosition.Y > enemyPosition.Y)
                    return new Vector2(0, 1);
                else
                    return new Vector2(0, -1);
            }
            else
                return Vector2.Zero;
        }

        public static Vector2 GetMovementDirection(Vector2 playerPosition, Vector2 enemyPosition)
        {
            int y = (int)(playerPosition.X - enemyPosition.X); //TODO why flipped?!
            int x = (int)(playerPosition.Y - enemyPosition.Y); //TODO why flipped?!

            if (y > x)
            {
                if (playerPosition.X > enemyPosition.X)
                    return new Vector2(1, 0);
                else
                    return new Vector2(-1, 0);
            }
            else
            {
                if (playerPosition.Y > enemyPosition.Y)
                    return new Vector2(0, 1);
                else
                    return new Vector2(0, -1);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace HorrorMill.Engines.Rpg
{
    public class Enemy
    {
        public string Name;
        public int Health;
        public int Damage;
        //public Sprite sprite;

        public Enemy(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
        }
    }
}

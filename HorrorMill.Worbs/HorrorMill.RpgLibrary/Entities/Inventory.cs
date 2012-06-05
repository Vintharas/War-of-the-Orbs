using System.Collections.Generic;
using System.Linq;
using HorrorMill.Engines.Rpg.Items;

namespace HorrorMill.Engines.Rpg.Entities
{
    public class Inventory
    {
        public List<Item> Items { get; private set; }

        public Weapon Weapon { get { return Items.OfType<Weapon>().FirstOrDefault(i => i.Equipped); } }
        public Orb Orb { get { return Items.OfType<Orb>().FirstOrDefault(i => i.Equipped); } }
        public Shield Shield { get { return Items.OfType<Shield>().FirstOrDefault(i => i.Equipped); } }

        public Armor Head { get { return Items.OfType<Armor>().FirstOrDefault(i => i.Location == ArmorLocation.Head && i.Equipped); } }
        public Armor Hands { get { return Items.OfType<Armor>().FirstOrDefault(i => i.Location == ArmorLocation.Hands && i.Equipped); } }
        public Armor Body { get { return Items.OfType<Armor>().FirstOrDefault(i => i.Location == ArmorLocation.Body && i.Equipped); } }
        public Armor Feet { get { return Items.OfType<Armor>().FirstOrDefault(i => i.Location == ArmorLocation.Feet && i.Equipped); } }

        public float CompositeAttack { get { return Weapon == null ?  0 : Weapon.Attack; } }
        public float CompositeDamage { get { return Weapon == null ? 0 : Weapon.Damage + Weapon.DamageModifier; } }
        public float CompositeDefense { get { return HeadArmorDefense + BodyArmorDefense + HandsArmorDefense + FeetDefense; } }

        public float HeadArmorDefense { get { return Head == null ? 0 : Head.Defense + Head.DefenseModifier; } }
        public float BodyArmorDefense { get { return Body == null ? 0 : Body.Defense + Body.DefenseModifier; } }
        public float HandsArmorDefense { get { return Hands == null ? 0 : Hands.Defense + Hands.DefenseModifier; } }
        public float FeetDefense { get { return Feet == null ? 0 : Feet.Defense + Feet.DefenseModifier; } }
        

        public Inventory()
        {
            Items = new List<Item>();
        }

        public void Add(Item item)
        {
            Items.Add(item);
        }
    }
}
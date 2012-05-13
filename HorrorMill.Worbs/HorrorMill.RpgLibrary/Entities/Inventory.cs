using System.Collections.Generic;
using System.Linq;
using HorrorMill.Engines.Rpg.Items;

namespace HorrorMill.Engines.Rpg.Entities
{
    public class Inventory
    {
        public List<Item> Items { get; private set; }

        public Weapon Weapon { get { return Items.OfType<Weapon>().FirstOrDefault(i => i.Equipped); } }
        public Shield Shield { get { return Items.OfType<Shield>().FirstOrDefault(i => i.Equipped); } }
        public Armor Helmet {get { return null; }} // TODO: complete
        public Armor Gloves {get { return null; }}
        public Armor BodyArmor { get { return Items.OfType<Armor>().FirstOrDefault(i => i.Equipped && i.Location == ArmorLocation.Body); } }
        public Armor Feet { get { return null; } }

        public float CompositeAttack { get { return Weapon == null ?  0 : Weapon.Attack; } }
        public float CompositeDamage { get { return Weapon == null ? 0 : Weapon.Damage + Weapon.DamageModifier; } }
        public float CompositeDefense { get { return BodyArmorDefense; } } // TODO: complete

        public float BodyArmorDefense { get { return BodyArmor == null ? 0 : BodyArmor.Defense + BodyArmor.DefenseModifier; } }

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
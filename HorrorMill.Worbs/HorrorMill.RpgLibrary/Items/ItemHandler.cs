using System.Collections.Generic;

namespace HorrorMill.Engines.Rpg.Items
{
    /// <summary>
    /// Class that contains functionalty for managing items.
    /// It should load all possible types of items and them provide them to the game as requested.
    /// Other stuff could be:
    ///     - Loot generation
    ///     - Randomly generate items based on level?
    ///     - Assign strings to regular items that are associated with Attribute modifiers or other kind of modifiers
    ///     - ...
    /// </summary>
    public class ItemHandler
    {
        private Dictionary<string, Armor> armors;
        private Dictionary<string, Weapon> weapons;
        private Dictionary<string, Shield> shields; 
        private Dictionary<string, Orb> orbs;
        // can add other items, potions, herbs, etc, etc

        public ItemHandler()
        {
            armors = new Dictionary<string, Armor>();
            weapons = new Dictionary<string, Weapon>();
            shields = new Dictionary<string, Shield>();
            orbs = new Dictionary<string, Orb>();
        }

        public Armor GetArmor(string name)
        {
            return armors[name].Clone() as Armor;
        }

        public Weapon GetWeapon(string name)
        {
            return weapons[name].Clone() as Weapon;
        }

        public Shield GetShield(string name)
        {
            return shields[name].Clone() as Shield;
        }

        public Orb GetOrb(string name)
        {
            return orbs[name].Clone() as Orb;
        }

        /// <summary>
        /// Load items programmatically. This is temporary, items should be 
        /// loaded from an xml file
        /// </summary>
        public void LoadItems()
        {
            weapons.Add("Apprentice's Wand", new Weapon {Name = "Apprentice Wand", Attack = 10, Damage = 10, Type = "Wand"});
            weapons.Add("Master's Wand", new Weapon {Name = "Master's Wand", Attack = 20, Damage = 20, Type = "Wand"});
            armors.Add("Apprentice's Robe", new Armor {Name = "Apprentice's Robe", Defense = 10, Location = ArmorLocation.Body, Type = "Robe"});
        }

    }
}
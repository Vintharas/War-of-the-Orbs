namespace HorrorMill.Engines.Rpg.Entities
{
    /// <summary>
    /// Abstraction of any entity within the game (player, npcs, monsters...)
    /// </summary>
    public class Entity
    {
        public string Name { get; set; }
        public EntityType Type { get; private set; }
        public Gender Gender { get; private set; }

        public Attributes Attributes { get; private set; }
        public Attributes AttributeModifiers { get; private set; }

        public int Strength { get { return Attributes.Strength + AttributeModifiers.Strength; } }
        public int Dexterity { get { return Attributes.Dexterity + AttributeModifiers.Dexterity; } }
        public int Intelligence { get { return Attributes.Intelligence + AttributeModifiers.Intelligence; } }
        public int Constitution { get { return Attributes.Constitution + AttributeModifiers.Constitution; } }

        public VariableAttribute Health { get; set; }
        public VariableAttribute Mana { get; set; }     // for magic
        public VariableAttribute Stamina { get; set; }  // for physical skills

        public int Attack { get; private set; }
        public int Damage { get; private set; }
        public int Defense { get; private set; }

        public int Level { get; private set; }
        public long Experience { get; private set; }

        /// <summary>
        /// Return an instance of an entity. The base attributes of an entity
        /// are based on the entity's class (warrior, barbarian, rogue, mage...)
        /// The attribute modifiers are based on the race.
        /// </summary>
        /// <param name="name"> </param>
        /// <param name="class"></param>
        /// <param name="race"></param>
        /// <param name="gender"></param>
        /// <param name="type"> </param>
        public Entity(string name, Gender gender, EntityType type, EntityClass @class, EntityRace race)
        {
            this.Name = name;
            Attributes = @class.Attributes;
            AttributeModifiers = race.AttributeModifiers;
            Gender = gender;
            this.Type = type;

            Health = new VariableAttribute(0);
            Mana = new VariableAttribute(0);
            Stamina = new VariableAttribute(0);
        }

    }
}
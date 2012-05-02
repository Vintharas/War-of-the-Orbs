namespace HorrorMill.Engines.Rpg.Entities
{
    /// <summary>
    /// Abstraction of any entity within the game (player, npcs, monsters...)
    /// </summary>
    public class Entity
    {
        public string Type { get; protected set; }
        public Gender Gender { get; private set; }

        public Attributes Attributes { get; protected set; }
        public Attributes AttributeModifiers { get; protected set; }

        public int Strength { get { return Attributes.Strength + AttributeModifiers.Strength; } }
        public int Dexterity { get { return Attributes.Dexterity + AttributeModifiers.Dexterity; } }
        public int Intelligence { get { return Attributes.Intelligence + AttributeModifiers.Intelligence; } }
        public int Constitution { get { return Attributes.Constitution + AttributeModifiers.Constitution; } }

        public VariableAttribute Health { get; set; }
        public VariableAttribute Mana { get; set; }     // for magic
        public VariableAttribute Stamina { get; set; }  // for physical skills

        public int Attack { get; protected set; }
        public int Damage { get; protected set; }
        public int Defense { get; protected set; }

        public int Level { get; protected set; }
        public long Experience { get; protected set; }

        /// <summary>
        /// Return an instance of an entity. The base attributes of an entity
        /// are based on the entity's class (warrior, barbarian, rogue, mage...)
        /// The attribute modifiers are based on the race.
        /// </summary>
        /// <param name="class"></param>
        /// <param name="race"></param>
        /// <param name="gender"></param>
        public Entity(EntityClass @class, EntityRace race, Gender gender)
        {
            Attributes = @class.Attributes;
            AttributeModifiers = race.AttributeModifiers;
            Gender = gender;

            Health = new VariableAttribute(0);
            Mana = new VariableAttribute(0);
            Stamina = new VariableAttribute(0);
        }

    }
}
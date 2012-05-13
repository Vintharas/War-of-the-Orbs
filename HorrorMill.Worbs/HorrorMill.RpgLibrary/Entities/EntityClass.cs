using System.Collections.Generic;

namespace HorrorMill.Engines.Rpg.Entities
{
    /// <summary>
    /// Class that represents a class of an entity: Warrior, Mage, Thief, etc
    /// It will be used to provide a basic se of attributes for an entity
    /// based on its class
    /// </summary>
    public class EntityClass
    {
        public string Name { get; set; }
        public Attributes Attributes { get; set; }

        public string HealthFormula { get; set; }
        public string StaminaFormula { get; set; }
        public string ManaFormula { get; set; }

        public string SpeedFormula { get; set; }

        public string AttackFormula { get; set; }
        public string DamageFormula { get; set; }
        public string DefenseFormula { get; set; }
    }
}
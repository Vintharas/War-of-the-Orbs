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
        public string SpeedFormula { get; set; }
        public string ManaFormula { get; set; }

        public static Dictionary<string, EntityClass> GetClasses()
        {
            // TODO: This should be loaded from an xml file in the future
            Dictionary<string, EntityClass> classes = new Dictionary<string, EntityClass>();
            classes["Warrior"] = new EntityClass
                                     {
                                         Name = "Warrior",
                                         Attributes = new Attributes
                                                          {
                                                              Strength = 14,
                                                              Dexterity = 12,
                                                              Cunning = 10,
                                                              WillPower = 12,
                                                              Intelligence = 10,
                                                              Constitution = 12
                                                          },
                                        HealthFormula = "20|CON|12",
                                        StaminaFormula = "12|WIL|12",
                                        SpeedFormula = "12|DEX|12",
                                        ManaFormula = "0|0|0"
                                     };
            classes["Wizard"] = new EntityClass
                                    {
                                        Name = "Wizard",
                                        Attributes = new Attributes
                                                         {
                                                             Strength = 10,
                                                             Dexterity = 10,
                                                             Cunning = 12,
                                                             WillPower = 14,
                                                             Intelligence = 14,
                                                             Constitution = 10
                                                         },
                                        HealthFormula = "10|CON|10",
                                        StaminaFormula = "0|0|0",
                                        ManaFormula = "20|INT|12",
                                        SpeedFormula = "12|DEX|12"
                                    };
            return classes;
        }
    }
}
using System.Collections.Generic;

namespace HorrorMill.Engines.Rpg.Entities
{
    public class EntityClassManager
    {
        private readonly Dictionary<string, EntityClass> classes;

        public EntityClassManager()
        {
            classes = new Dictionary<string, EntityClass>();
        }

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
                                         SpeedFormula = "0|DEX|0",
                                         ManaFormula = "0|0|0",
                                         AttackFormula = "12|DEX|12",
                                         DefenseFormula = "12|DEX|12",
                                         DamageFormula = "20|STR|12"
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
                                        SpeedFormula = "0|DEX|0",
                                        AttackFormula = "10|DEX|10",
                                        DefenseFormula = "10|DEX|10",
                                        DamageFormula = "10|STR|10"
                                    };
            return classes;
        }
    }
}
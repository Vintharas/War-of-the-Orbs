using HorrorMill.Engines.Rpg.Spells;

namespace HorrorMill.Engines.Rpg.Items
{
    public class Orb : Item
    {
        public Spell Spell { get; set; }

        public override Item Clone()
        {
            return new Orb
                {
                    Name = Name,
                    Spell = Spell,
                    Type = Type,
                    Weight = Weight,
                    AttributeModifiers = AttributeModifiers
                };
        }
    }
}
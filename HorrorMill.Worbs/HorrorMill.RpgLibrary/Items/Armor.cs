namespace HorrorMill.Engines.Rpg.Items
{
    public class Armor : Item
    {
        public float Defense { get; set; }
        public float DefenseModifier { get; set; }
        public ArmorLocation Location { get; set; }

        public override Item Clone()
        {
            return new Armor
            {
                Defense = Defense,
                DefenseModifier = DefenseModifier,
                Location = Location,
                Name = Name,
                Type = Type,
                Weight = Weight,
                AttributeModifiers = AttributeModifiers
            };
        }
    }
}
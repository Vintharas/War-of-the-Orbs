namespace HorrorMill.Engines.Rpg.Items
{
    public class Shield : Item
    {
        public int Defense { get; set; }
        public int DefenseModifier { get; set; }

        public override Item Clone()
        {
            return new Shield
            {
                Defense = Defense,
                DefenseModifier = DefenseModifier,
                Name = Name,
                Type = Type,
                Weight = Weight,
                AttributeModifiers = AttributeModifiers.Clone()
            };
        }
    }
}
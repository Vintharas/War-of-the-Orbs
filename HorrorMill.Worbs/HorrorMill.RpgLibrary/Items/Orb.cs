namespace HorrorMill.Engines.Rpg.Items
{
    public class Orb : Item
    {
        public override Item Clone()
        {
            return new Orb
                {
                    Name = Name,
                    Type = Type,
                    Weight = Weight,
                    AttributeModifiers = AttributeModifiers
                };
        }
    }
}
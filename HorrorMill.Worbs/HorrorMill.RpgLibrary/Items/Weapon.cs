namespace HorrorMill.Engines.Rpg.Items
{
    public class Weapon : Item
    {
        public float Attack { get; set; }
        public float AttackModifier { get; set; }
        public float Damage { get; set; }
        public float DamageModifier { get; set; }

        public override Item Clone()
        {
            return new Weapon
                       {
                           Attack = Attack,
                           AttackModifier = AttackModifier,
                           Damage = Damage,
                           DamageModifier = DamageModifier,
                           Name = Name,
                           Type = Type,
                           Weight = Weight,
                           AttributeModifiers = AttributeModifiers.Clone()
                       };
        }
    }

}
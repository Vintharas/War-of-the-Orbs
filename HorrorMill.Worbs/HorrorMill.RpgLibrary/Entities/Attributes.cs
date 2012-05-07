namespace HorrorMill.Engines.Rpg.Entities
{
    /// <summary>
    /// Structure that represents the attributes of a given entity
    /// </summary>
    public struct Attributes
    {
        public int Strength { get; set; }       // damage
        public int Dexterity { get; set; }      // attack, defense
        public int Constitution { get; set; }   // hitpoints, resistance, stamina
        public int Intelligence { get; set; }   // magic

        //public int WillPower { get; set; }      
        //public int Cunning { get; set; }        
        public Attributes Clone()
        {
            return new Attributes
                       {
                           Strength = Strength,
                           Dexterity = Dexterity,
                           Constitution = Constitution,
                           Intelligence = Intelligence
                       };
        }
    }
}
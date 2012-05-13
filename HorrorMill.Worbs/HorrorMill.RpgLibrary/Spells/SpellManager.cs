using System.Collections.Generic;

namespace HorrorMill.Engines.Rpg.Spells
{
    public class SpellManager
    {
        private readonly Dictionary<string, Spell> spells;
        public Dictionary<string, Spell> Spells { get { return spells; } } 

        public SpellManager(Dictionary<string, Spell> spells)
        {
            this.spells = spells;
        }
    }
}
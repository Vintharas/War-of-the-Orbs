using System.Collections.Generic;

namespace HorrorMill.Engines.Rpg.Entities
{
    public class EntityRaceManager
    {
        public Dictionary<string, EntityRace> Races { get; private set; }

        public EntityRaceManager(Dictionary<string, EntityRace> races)
        {
            Races = races;
        }

        public static Dictionary<string, EntityRace> GetRaces()
        {
            Dictionary<string, EntityRace> entityRaces = new Dictionary<string, EntityRace>();
            entityRaces["Human"] =
                new EntityRace
                    {
                        AttributeModifiers = new Attributes(),
                        // no modifiers,
                        Name = "Human"
                    };
            return entityRaces;
        }
    }
}
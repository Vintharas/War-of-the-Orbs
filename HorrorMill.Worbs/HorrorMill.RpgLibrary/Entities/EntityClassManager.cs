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

         
    }
}
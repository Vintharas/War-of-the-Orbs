using HorrorMill.Engines.Rpg.Entities;

namespace HorrorMill.Engines.Rpg.Items
{
    public abstract class Item
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public float Weight { get; set; } // this will affect how fast monsters are
        public Attributes AttributeModifiers { get; set; }
        public bool Equipped { get; set; }

        public abstract Item Clone();

    }
}
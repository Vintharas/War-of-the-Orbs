namespace HorrorMill.Engines.Rpg.Items
{
    public abstract class Item
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public float Weight { get; set; } // this will affect how fast monsters are

    }
}
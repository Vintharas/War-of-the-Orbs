using System;
using HorrorMill.Engines.Rpg.Entities;

namespace HorrorMill.Engines.Rpg.GameMechanics
{
    public struct Modifier
    {
        public Attributes AttributeModifiers;
        public int Duration;
        public TimeSpan TimeLeft;

        public Modifier(Attributes attributeModifiers)
        {
            AttributeModifiers = attributeModifiers;
            Duration = -1;
            TimeLeft = TimeSpan.Zero;
        }

        public Modifier(Attributes attributeModifiers, int duration)
        {
            AttributeModifiers = attributeModifiers;
            Duration = duration;
            TimeLeft = TimeSpan.FromSeconds(duration);
        }

        public void Update(TimeSpan elapsedTime)
        {
            if (Duration == -1)
                return;

            TimeLeft -= elapsedTime;
            if (TimeLeft.TotalMilliseconds < 0)
            {
                TimeLeft = TimeSpan.Zero;
                AttributeModifiers = new Attributes();
            }

        }
    }
}
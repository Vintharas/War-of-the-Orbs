using Microsoft.Xna.Framework;

namespace HorrorMill.Engines.Rpg.Entities
{
    public class VariableAttribute
    {
        public int MaxValue { get; set; }

        private int currentValue;
        public int CurrentValue {
            get { return currentValue; }
            set { currentValue = (int)MathHelper.Clamp(value, 0, MaxValue); }
        }

        public VariableAttribute(int maxValue)
        {
            MaxValue = maxValue;
            CurrentValue = maxValue;
        }

    }
}
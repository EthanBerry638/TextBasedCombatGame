using System.Runtime;

namespace TextBasedCombat.Entities
{
    public enum PotionType
    {
        Heal,
        Damage,
        AttackPowerDebuff,
        AttackPowerBuff
    }
    public class Potion
    {
        public string Name { get; set; }
        public PotionType Type { get; private set; }
        public int Value { get; set; }
        public string Description { get; set; }

        public Potion(string name, PotionType type, int value, string description)
        {
            Name = name;
            Type = type;
            Value = value;
            Description = description;
        }

        public void ApplyEffect()
        {
        }

        public string ToString()
        {
            string potionNameAndDescription = $"";
            return potionNameAndDescription;
        }
    }
}
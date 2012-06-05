using System;
using System.Collections.Generic;
using HorrorMill.Engines.Rpg.GameMechanics;
using Microsoft.Xna.Framework;

namespace HorrorMill.Engines.Rpg.Entities
{
    /// <summary>
    /// Abstraction of any entity within the game (player, npcs, monsters...)
    /// </summary>
    public class Entity : DrawableGameComponent
    {
        public string Name { get; set; }
        public EntityType Type { get; private set; }
        public Gender Gender { get; private set; }

        public Attributes Attributes { get; private set; }
        public Attributes AttributeModifiers { get; private set; }

        public List<Modifier> SpellModifiers { get; private set; } // Attribute modifiers based on spells TODO: Develop and include in game mechanics (frozen, petrified, poisoned, burnt, slowed, etc)

        public int Strength { get { return Attributes.Strength + AttributeModifiers.Strength; } }
        public int Dexterity { get { return Attributes.Dexterity + AttributeModifiers.Dexterity; } }
        public int Intelligence { get { return Attributes.Intelligence + AttributeModifiers.Intelligence; } }
        public int Constitution { get { return Attributes.Constitution + AttributeModifiers.Constitution; } }
        public int Willpower { get { return Attributes.WillPower + AttributeModifiers.WillPower; }}
        public int Cunning { get { return Attributes.Cunning + AttributeModifiers.Cunning; } }

        public VariableAttribute Health { get; set; }
        public VariableAttribute Mana { get; set; }     // for magic
        public VariableAttribute Stamina { get; set; }  // for physical skills

        public float Speed { get; private set; } // TODO: modify speed based on armor weight

        private float BaseAttack { get; set; }
        private float BaseDamage { get; set; }
        private float BaseDefense { get; set; }

        public float Attack { get { return BaseAttack + Inventory.CompositeAttack; } }
        public float Damage { get { return BaseDamage + Inventory.CompositeDamage; } }
        public float Defense { get { return BaseDefense + Inventory.CompositeDefense; } }
        

        public int Level { get; private set; }
        public long Experience { get; private set; }

        public Inventory Inventory { get; private set; }

        /// <summary>
        /// Return an instance of an entity. The base attributes of an entity
        /// are based on the entity's class (warrior, barbarian, rogue, mage...)
        /// The attribute modifiers are based on the race.
        /// </summary>
        /// <param name="name"> </param>
        /// <param name="class"></param>
        /// <param name="race"></param>
        /// <param name="gender"></param>
        /// <param name="type"> </param>
        public Entity(Game game, string name, Gender gender, EntityType type, EntityClass @class, EntityRace race) : base (game)
        {
            this.Name = name;
            Attributes = @class.Attributes;
            AttributeModifiers = race.AttributeModifiers;
            Gender = gender;
            this.Type = type;
            Inventory = new Inventory();

            Health = CalculateVariableAttributeFromFormula(@class.HealthFormula);
            Mana = CalculateVariableAttributeFromFormula(@class.ManaFormula);
            Stamina = CalculateVariableAttributeFromFormula(@class.StaminaFormula);
            Speed = MathHelper.Clamp(CalculateFromFormula(@class.SpeedFormula), 1f, 6f);
            BaseAttack = CalculateFromFormula(@class.AttackFormula);
            BaseDamage = CalculateFromFormula(@class.DamageFormula);
            BaseDefense = CalculateFromFormula(@class.DefenseFormula);
            
            SpellModifiers = new List<Modifier>();
        }

        private float CalculateFromFormula(string formula)
        {
            if (formula == "0|0|0")
                return 0;
            else
            {
                string[] formulaElements = formula.Split('|');
                int baseDie = int.Parse(formulaElements[0]);
                int baseAttribute = GetAttribute(formulaElements[1]);
                int perLevelDie = int.Parse(formulaElements[2]);
                return (Mechanics.RollDie((DieType)baseDie) + baseAttribute + Level * perLevelDie);
            }
        }

        /// <summary>
        /// Calculate a variable attribute from a formula
        /// Formulas are expressed as (BaseDie|BaseAttribute|ExtraDiePerLevel)
        /// </summary>
        /// <param name="formula"></param>
        /// <returns></returns>
        private VariableAttribute CalculateVariableAttributeFromFormula(string formula)
        {
            if (formula == "0|0|0")
                return new VariableAttribute(0);
            else
            {
                string[] formulaElements = formula.Split('|');
                int baseDie = int.Parse(formulaElements[0]);
                int baseAttribute = GetAttribute(formulaElements[1]);
                int perLevelDie = int.Parse(formulaElements[2]);
                VariableAttribute attribute =
                    new VariableAttribute((Mechanics.RollDie((DieType)baseDie) + baseAttribute + Level * perLevelDie)*100);
                return attribute;
            }
        }

        private int GetAttribute(string attribute)
        {
            switch (attribute)
            {
                case "CON":
                    return Constitution;
                case "INT":
                    return Intelligence;
                case "WIL":
                    return Willpower;
                case "STR":
                    return Strength;
                case "CUN":
                    return Cunning;
                case "DEX":
                    return Dexterity;
                default:
                    throw new Exception("No attribute exists with code: " + attribute);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var modifier in SpellModifiers)
                modifier.Update(gameTime.ElapsedGameTime);
            base.Update(gameTime);
        }

        public void TakeDamage(int damage)
        {
            Health.CurrentValue -= damage;
        }
        
    }
}
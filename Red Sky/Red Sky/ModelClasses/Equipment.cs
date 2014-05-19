using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Red_Sky.ModelClasses
{
    class Equipment : Item
    {
        public BodyPartType WornOn;
        public BodyPartType HeldBy;

        public int Vitality { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public int Charisma { get; set; }
        public int Wisdom { get; set; }
        public int Willpower { get; set; }
        public int Perception { get; set; }
        public int Luck { get; set; }

        public int MaxHP { get; set; }
        public int MaxMP { get; set; }
        public float PhysicalStatusDefense { get; set; }
        public int Endurance { get; set; }
        public int Weight { get; set; }
        public int AttackSpeed { get; set; }
        public int MagicalAbility { get; set; }
        public int SkillPerLevel { get; set; }
        public float EXPRate { get; set; }
        public int ExpToNextLevel { get; set; }
        public float PriceRatio { get; set; }
        public float MagicalStatusDefense { get; set; }
        public float RangedAccuracy { get; set; }
        public float MeleeAccuracy { get; set; }
        public float CriticalHitRatio { get; set; }

        public int PhysicalAttackStrength { get; set; }
        public int MagicalAttackStrength { get; set; }
        public int RangedAttackStrength { get; set; }

        public int AttackDamage { get; set; }
        

        public Equipment(RedSkyGame game)
            : base(game)
        {
            Usage = ItemUse.Wearable;
            Targets = ItemTargets.None;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}

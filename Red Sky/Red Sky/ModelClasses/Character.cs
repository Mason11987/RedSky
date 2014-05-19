using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Red_Sky.ModelClasses
{
    public enum SpriteDirection { Front, Left, Right, Back };

    class Character : DrawableGameComponent
    {
        public List<Status> Statuses;
        public List<Skill> Skills;
        public List<BodyPart> BodyParts;
        public Vector2 SpriteLocation;
        public int AnimateFrame;
        public int AnimateFrameCount;

        public string Name;

        /// <summary>Level of the unit</summary>
        public int  Level { get; set; }
        /// <summary>Total experience earned</summary>
        public int  Experience { get; set; }
        /// <summary>Current Health of the Unit</summary>
        public int  HP { get; set; }
        /// <summary>Current Mana of the Unit</summary>
        public int  MP { get; set; }


        

        /// <summary>Endurance, Stamina, Resiliency of the Unit.  Impacts Max HP, Status Defense, and Endurance</summary>
        private int vitality;
        public int Vitality { get { return vitality + (int)EquipmentStatSum("Vitality"); }}
        /// <summary>Strength of the Unit.  Impacts, Weight, Max HP, Weapon and Armor Requirements, Speed</summary>
        private int strength;
        public int Strength { get { return strength + (int)EquipmentStatSum("Strength"); } }
        /// <summary>Agility of the Unit.  Impacts Attack and Movement Speed, and Accuracy</summary>
        private int dexterity;
        public int Dexterity { get { return dexterity + (int)EquipmentStatSum("Dexterity"); } }
        /// <summary>Problem Solving Ability of the Unit.  Impacts, Speaking, Magic Ability, Skill Points per level, EXP rate, and EXP needed per level</summary>
        private int intelligence;
        public int Intelligence { get { return intelligence + (int)EquipmentStatSum("Intelligence"); } }
        /// <summary>Social Skills of the Unit.  Impacts Prices, and NPC reactions</summary>
        private int charisma;
        public int Charisma { get { return charisma + (int)EquipmentStatSum("Charisma"); } }
        /// <summary>Common sense of the Unit.  Impacts Magic Ability, Magicaly entity communication, and ability to discern motives/feelings</summary>
        private int wisdom;
        public int Wisdom { get { return wisdom + (int)EquipmentStatSum("Wisdom"); } }
        /// <summary>Mental resistance of the Unit.  Impacts Status Resistance</summary>
        private int willpower;
        public int Willpower { get { return willpower + (int)EquipmentStatSum("Willpower"); } }
        /// <summary>Perception of the Unit. Impacts Ranged Accuracy, and Detecting clues, traps, and hidden enemie.</summary>
        private int perception;
        public int Perception { get { return perception + (int)EquipmentStatSum("Perception"); } }
        /// <summary>Luck of the Unit. Impacts Critical Hits, and Encounters</summary>
        private int luck;
        public int Luck { get { return luck + (int)EquipmentStatSum("Luck"); } }

        /// <summary>Max HP, Based on Strength and Vitality.</summary>
        public int MaxHP { get { return (Strength + Vitality) * 10 + (int)EquipmentStatSum("MaxHP"); } }
        /// <summary>Max MP, Based on Intelligence and Wisdom.</summary>
        public int MaxMP { get { return (Intelligence + Wisdom) * 2 + (int)EquipmentStatSum("MaxMP"); } }
        /// <summary>Ability to resist physical attacks.  Based on Vitality</summary>
        public float PhysicalStatusDefense { get { return Vitality / 100.0f + (float)EquipmentStatSum("PhysicalStatusDefense"); } }
        /// <summary>Ability to Persist.  Based on Vitality</summary>
        public int Endurance { get { return Vitality + (int)EquipmentStatSum("Endurance"); } }
        /// <summary>Weight. Based on Strength, and Equipment Weight</summary>
        public int Weight { get { return Strength + (int)EquipmentStatSum("Weight"); } }
        /// <summary>Speed of Attacks. Based on Dexterity.</summary>
        public int AttackSpeed { get { return Dexterity + (int)EquipmentStatSum("AttackSpeed"); } }    
        /// <summary>Strength of Magical actions.  Based on Intelligence and Wisdom</summary>
        public int MagicalAbility { get { return Intelligence + Wisdom + (int)EquipmentStatSum("MagicalAbility"); } }  
        /// <summary>SKill points available per level.  Based on Intelligence</summary>
        public int SkillPerLevel { get { return 5 + (int)(Intelligence / 5.0f) + (int)EquipmentStatSum("SkillPerLevel"); } }  
        /// <summary>Rate of EXP gain.  Based on Intelligence</summary>
        public float EXPRate { get { return 1 + (Intelligence / 100.0f) + (float)EquipmentStatSum("EXPRate"); } }  
        /// <summary>Exp required to gain a level</summary>
        public int ExpToNextLevel { get { return 100 + (int)EquipmentStatSum("ExpToNextLevel"); } }  
        /// <summary>Cost of products.  Based on Charisma</summary>
        public float PriceRatio { get { return 1 - (Charisma / 100.0f) + (float)EquipmentStatSum("PriceRatio"); } }  
        /// <summary>Magical resistance.  Based on Willpower</summary>
        public float MagicalStatusDefense { get { return Willpower / 100.0f + (float)EquipmentStatSum("MagicalStatusDefense"); } }  
        /// <summary>Accuracy of Ranged attacks.  Based on Perception</summary>
        public float RangedAccuracy { get { return Perception + (float)EquipmentStatSum("RangedAccuracy"); } }  
        /// <summary>Accuracy of Melee attacks.  Based on Dexterity</summary>
        public float MeleeAccuracy { get { return Dexterity + (float)EquipmentStatSum("MeleeAccuracy"); } }  
        /// <summary>Rate of critical attacks. Based on Luck</summary>
        public float CriticalHitRatio { get { return Luck / 100.0f + (float)EquipmentStatSum("CriticalHitRatio"); } }

        public bool CanChangeEquipment { get { return true; } }

        public Character(RedSkyGame game)
            : base(game)
        {
            Name = "Test";
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                int secs = (int)(gameTime.TotalGameTime.TotalSeconds * 4);

                // Sets animate frame based on elapsed time, if last frame, set to middle frame (standing still)
                AnimateFrame = secs % AnimateFrameCount == 3 ? 1 : secs % AnimateFrameCount;
            }
            base.Update(gameTime);
        }

        internal static Character Initalize<T>(RedSkyGame game) where T : Character
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { game});
        }

        public virtual void InitializeStats()
        {
            Level = 1;
            Experience = 0;
            IncrementPersonalStat("Vitality", 50);
            IncrementPersonalStat("Strength", 5);
            IncrementPersonalStat("Dexterity", 5);
            IncrementPersonalStat("Intelligence", 123);
            IncrementPersonalStat("Charisma", 5);
            IncrementPersonalStat("Perception", 5);
            IncrementPersonalStat("Wisdom", 5);
            IncrementPersonalStat("Willpower", 5);
            IncrementPersonalStat("Luck", 5);
            HP = MaxHP;
            MP = MaxMP;

        }


        internal Rectangle GetSpriteFrame(SpriteDirection direction)
        {
            Rectangle SpriteSet = GetSpriteSet();

            switch (direction)
            {
                case SpriteDirection.Front:
                    SpriteSet.Offset(0, 0);
                    break;
                case SpriteDirection.Left:
                    SpriteSet.Offset(0, 32);
                    break;
                case SpriteDirection.Right:
                    SpriteSet.Offset(0, 64);
                    break;
                case SpriteDirection.Back:
                    SpriteSet.Offset(0, 96);
                    break;
                default:
                    break;
            }

            return SpriteSet;
        }

        internal Rectangle GetAnimatedSpriteFrame(SpriteDirection direction)
        {
            Rectangle SpriteSet = GetSpriteSet();

            switch (direction)
            {
                case SpriteDirection.Front:
                    SpriteSet.Offset(AnimateFrame * 32, 0);
                    break;
                case SpriteDirection.Left:
                    SpriteSet.Offset(AnimateFrame * 32, 32);
                    break;
                case SpriteDirection.Right:
                    SpriteSet.Offset(AnimateFrame * 32, 64);
                    break;
                case SpriteDirection.Back:
                    SpriteSet.Offset(AnimateFrame * 32, 96);
                    break;
                default:
                    break;
            }

            return SpriteSet;

            
        }

        internal Rectangle GetSpriteSet()
        {
            return new Rectangle((int)SpriteLocation.X * 96, (int)SpriteLocation.Y * 96, 32, 32);
        }

        public override string ToString()
        {
            return Name;
        }

        internal virtual Dictionary<string, float> GetStatsList()
        {
            List<string> keys = new List<string>() { "Vitality", "Strength", "Dexterity", "Intelligence", "Charisma", "Wisdom", "Willpower", "Perception", "Luck" };
            List<float> values = new List<float>() {this.Vitality, this.Strength, this.Dexterity, this.Intelligence, this.Charisma, this.Wisdom,
                this.Willpower, this.Perception, this.Luck};

            return keys.Zip(values, (k, v) => new { k, v })
              .ToDictionary(x => x.k, x => x.v);
        }


        internal Dictionary<string, float> GetAlteredStatsListWearing(BodyPart bodyPart, Equipment equipment)
        {
            Equipment preserveWearing = bodyPart.Wearing;

            ChangeEquipWorn(bodyPart, equipment);

            Dictionary<string, float> statsList = GetStatsList();

            ChangeEquipWorn(bodyPart, preserveWearing);

            return statsList;
        }

        internal Dictionary<string, float> GetAlteredStatsListHolding(BodyPart bodyPart, Equipment equipment)
        {
            Equipment preserveHolding = bodyPart.Holding;

            ChangeEquipHeld(bodyPart, equipment);

            Dictionary<string, float> statsList = GetStatsList();

            ChangeEquipHeld(bodyPart, preserveHolding);

            return statsList;
        }

        internal virtual Dictionary<string, float> GetSecondaryStatsList()
        {
            List<string> keys = new List<string>() { "Phyical Status Defense", "Magical Status Defense", "Endurance", "Weight", "Attack Speed", "Magical Ability", "Ranged Accuracy", "Physical Accuracy", "Critical Hit Ratio", "Skill Per Level", "Experience Rate", "Price Ratio" };
            List<float> values = new List<float>() {this.PhysicalStatusDefense, this.MagicalStatusDefense, this.Endurance, this.Weight, this.AttackSpeed, this.MagicalAbility, 
                this.RangedAccuracy, this.MeleeAccuracy, this.CriticalHitRatio, this.SkillPerLevel, this.EXPRate, this.PriceRatio};

            return keys.Zip(values, (k, v) => new { k, v })
              .ToDictionary(x => x.k, x => x.v);
        }

        internal Dictionary<string, float> GetAlteredSecondaryStatsListWearing(BodyPart bodyPart, Equipment equipment)
        {
            Equipment preserveWearing = bodyPart.Wearing;

            ChangeEquipWorn(bodyPart, equipment);

            Dictionary<string, float> statsList = GetSecondaryStatsList();

            ChangeEquipWorn(bodyPart, preserveWearing);

            return statsList;
        }

        internal Dictionary<string, float> GetAlteredSecondaryStatsListHolding(BodyPart bodyPart, Equipment equipment)
        {
            Equipment preserveHolding = bodyPart.Holding;

            ChangeEquipHeld(bodyPart, equipment);

            Dictionary<string, float> statsList = GetSecondaryStatsList();

            ChangeEquipHeld(bodyPart, preserveHolding);

            return statsList;
        }

        private float EquipmentStatSum(string StatName)
        {
            float sum = 0.0f;
            System.Reflection.PropertyInfo prop = typeof(Equipment).GetProperty(StatName);

            foreach (Equipment equip in getEquipmetList())
            {
                var thisStat = prop.GetValue(equip, null);
                if (thisStat is int)
                    sum += (int)thisStat;
                else if (thisStat is float)
                    sum += (float)thisStat;
            }

            return sum;
        }

        private List<Equipment> getEquipmetList()
        {
            List<Equipment> equipmentList = new List<Equipment>();
            if (BodyParts != null)
            {
                foreach (BodyPart part in BodyParts)
                {
                    if (part.Holding != null)
                        equipmentList.Add(part.Holding);
                    if (part.Wearing != null && part.isPrimary)
                        equipmentList.Add(part.Wearing);
                    if (part.BodyParts != null)
                    {
                        foreach (BodyPart innerpart in part.BodyParts)
                        {
                            if (innerpart.Holding != null)
                                equipmentList.Add(innerpart.Holding);
                            if (innerpart.Wearing != null && innerpart.isPrimary)
                                equipmentList.Add(innerpart.Wearing);
                            if (innerpart.BodyParts != null)
                            {
                                foreach (BodyPart inmostpart in innerpart.BodyParts)
                                {
                                    if (inmostpart.Holding != null)
                                        equipmentList.Add(inmostpart.Holding);
                                    if (inmostpart.Wearing != null && inmostpart.isPrimary)
                                        equipmentList.Add(inmostpart.Wearing);
                                }
                            }
                        }
                    }
                }
            }
            return equipmentList;
        }


        public void IncrementPersonalStat(string StatName, int change)
        {
            try
            {
                FieldInfo fieldInfo = typeof(Character).GetField(StatName.ToLower(), BindingFlags.NonPublic | BindingFlags.Instance);

                var curStat = fieldInfo.GetValue(this);

                fieldInfo.SetValue(this, (int)curStat + change);
            }
            catch (Exception)
            {
                
                throw;
            }

        }




        internal void ChangeEquipHeld(BodyPart thisPart, Equipment thisEquipment)
        {
            thisPart.Holding = thisEquipment;
        }

        internal void ChangeEquipWorn(BodyPart thisPart, Equipment thisEquipment)
        {
            thisPart.Wearing = thisEquipment;
            if (thisPart.WearShare != null)
            {
                foreach (BodyPart part in thisPart.WearShare)
                {
                    part.Wearing = thisEquipment;
                }
            }
        }
    }
}

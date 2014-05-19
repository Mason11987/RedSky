using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Red_Sky.ModelClasses
{
    class Party : DrawableGameComponent 
    {
        public List<Character> Characters;
        public Dictionary<Item, int> Items;
        public bool inBattle;

        public Site Site { get; set; }

        public Party(RedSkyGame game)
            : base(game)
        {

        }

        internal void Join(Character addChar)
        {
            if (Characters == null)
                Characters = new List<Character>();
            Characters.Add(addChar);
        }

        internal void UseItem(Item item)
        {
            if (Items[item] > 0 && (item.Usage == ItemUse.Usable || (item.Usage == ItemUse.BattleUsable && inBattle)) &&  item.Targets == ItemTargets.None )
            {
                if (item.Use())
                {
                    Items[item]--;
                }
            }
        }

        internal void UseItem(Item item, Character character)
        {
            if (Items[item] > 0 && (item.Usage == ItemUse.Usable || (item.Usage == ItemUse.BattleUsable && inBattle)) && item.Targets == ItemTargets.Character)
            {
                if (item.Use(character))
                {
                    Items[item]--;
                }
            }
        }

        internal void UseItem(Item item, Party party)
        {
            if (Items[item] > 0 && (item.Usage == ItemUse.Usable || (item.Usage == ItemUse.BattleUsable && inBattle)) && item.Targets == ItemTargets.Party)
            {
                if (item.Use(party))
                {
                    Items[item]--;
                    if (Items[item] == 0)
                        Items.Remove(item);
                }
            }
        }

        internal bool ChangeEquipmentHeld(Character thisCharacter, BodyPart thisPart, Equipment thisEquipment)
        {
            if (!thisCharacter.CanChangeEquipment)
                return false;

            if (thisPart.Holding != null)
            {
                Equipment previousEquipment = thisPart.Holding;
                Items[previousEquipment]++;
            }

            thisCharacter.ChangeEquipHeld(thisPart, thisEquipment);

            if (thisEquipment != null)
            {
                Items[thisEquipment]--;
            }

            return true;

        }

        internal bool ChangeEquipmentWorn(Character thisCharacter, BodyPart thisPart, Equipment thisEquipment)
        {
            if (!thisCharacter.CanChangeEquipment)
                return false;

            if (thisPart.Wearing != null)
            {
                Equipment previousEquipment = thisPart.Wearing;
                Items[previousEquipment]++;
            }

            thisCharacter.ChangeEquipWorn(thisPart, thisEquipment);

            if (thisEquipment != null)
            {
                Items[thisEquipment]--;
            }

            return true;
        }
    }
}

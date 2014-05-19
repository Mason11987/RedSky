using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Red_Sky.ModelClasses
{
    public enum ItemUse
    {
        Wearable,
        Usable,
        BattleUsable
    }

    public enum ItemTargets
    {
        None,
        Character,
        Party
    }

    public class Item : DrawableGameComponent
    {
        public string Name;

        public ItemUse Usage;
        public ItemTargets Targets;

        public string Description;

        public Item(RedSkyGame game)
            : base(game)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override string ToString()
        {
            return Name;
        }

        internal bool Use()
        {
            Console.WriteLine("Using " + Name + " on no one");
            return true;
        }

        internal bool Use(Character character)
        {
            Console.WriteLine("Using " + Name + " on " + character.Name);
            return true;
        }

        internal bool Use(Party party)
        {
            Console.WriteLine("Using " + Name + " on party.");
            return true;
        }

        //This returns the string of text which will be displayed as informative text before using this item on a specific character
        internal string UseStatStringReference(Character thisCharacter)
        {
            return "HP:" + thisCharacter.HP + " / " + thisCharacter.MaxHP + " for " + Name;
        }
    }
}

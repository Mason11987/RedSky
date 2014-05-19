using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Red_Sky.ModelClasses
{
    public enum BodyPartType
    {
        None, 
        Body,
        Head,
        Arm,
        Hand,
        Finger,
        Leg,
        Foot,
        Toe,
    }

    class BodyPart : DrawableGameComponent
    {
        public List<BodyPart> BodyParts;
        public Equipment Holding;
        public Equipment Wearing;
        public List<BodyPart> WearShare;
        public bool isPrimary = true;
        public BodyPart attachedTo;
        public BodyPartType Type;

        public string Name { get; set; }
        public bool canHold { get; set; }
        public bool canWear { get; set; }

        public BodyPart(RedSkyGame game, string name, BodyPartType bodyPartType, bool canhold, bool canwear) 
            : base (game)
        {
            Name = name ;
            canHold = canhold;
            Holding = null;
            canWear = canwear;
            Wearing = null;
            Type = bodyPartType;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        internal void EquipWith(BodyPart linkedPart)
        {
            if (WearShare == null)
                WearShare = new List<BodyPart>();
            this.WearShare.Add(linkedPart);
            

            if (linkedPart.WearShare == null)
                linkedPart.WearShare = new List<BodyPart>();
            linkedPart.WearShare.Add(this);

            linkedPart.isPrimary = false;
            
        }

        internal void Attach(BodyPart attachPart)
        {
            if (BodyParts == null)
                BodyParts = new List<BodyPart>();
            BodyParts.Add(attachPart);
            attachedTo = this;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

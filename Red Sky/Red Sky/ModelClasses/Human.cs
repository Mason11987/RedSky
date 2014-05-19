using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Red_Sky.ModelClasses
{
    class Human : Character
    {
        public Human(RedSkyGame game)
            : base(game)
        {

            InitializeStats();

            Statuses = new List<Status>();
            Skills = new List<Skill>();

            
            BodyParts = GetBaseHumanoidBodyParts(game);

            AnimateFrame = 0;
            AnimateFrameCount = 4;
        }

        public override void InitializeStats()
        {
            base.InitializeStats();
        }


        static public List<BodyPart> GetBaseHumanoidBodyParts(RedSkyGame game)
        {
            List<BodyPart> BodyParts = new List<BodyPart>();

            BodyParts.Add(new BodyPart(game, "Head", BodyPartType.Head, false, true));

            BodyParts.Add(new BodyPart(game, "Body", BodyPartType.Body, false, true));

            //Arms
            BodyPart LeftArm = new BodyPart(game, "Left Arm", BodyPartType.Arm, false, true);
            BodyParts.Add(LeftArm);
            BodyPart LeftHand = new BodyPart(game, "Left Hand", BodyPartType.Hand, true, true);
            LeftArm.Attach(LeftHand);
            for (int i = 0; i < 5; i++)
            {
                LeftHand.Attach(new BodyPart(game, "Left Finger " + i.ToString(), BodyPartType.Finger, false, true));
            }
            BodyPart RightArm = new BodyPart(game, "Right Arm", BodyPartType.Arm, false, true);
            RightArm.EquipWith(LeftArm);
            BodyParts.Add(RightArm);
            BodyPart RightHand = new BodyPart(game, "Right Hand", BodyPartType.Hand, true, true);
            RightHand.EquipWith(LeftHand);
            RightArm.Attach(RightHand);
            for (int i = 0; i < 5; i++)
            {
                RightHand.Attach(new BodyPart(game, "Right Finger " + i.ToString(), BodyPartType.Finger, false, true));
            }

            //Legs
            BodyPart LeftLeg = new BodyPart(game, "Left Leg", BodyPartType.Leg, false, true);
            BodyParts.Add(LeftLeg);
            BodyPart LeftFoot = new BodyPart(game, "Left Foot", BodyPartType.Foot, false, true);
            LeftLeg.Attach(LeftFoot);
            for (int i = 0; i < 5; i++)
            {
                LeftFoot.Attach(new BodyPart(game, "Left Toe " + i.ToString(), BodyPartType.Toe, false, false));
            }
            BodyPart RightLeg = new BodyPart(game, "Right Leg", BodyPartType.Leg, false, true);
            RightLeg.EquipWith(LeftLeg);
            BodyParts.Add(RightLeg);
            BodyPart RightFoot = new BodyPart(game, "Right Foot", BodyPartType.Foot, false, true);
            RightFoot.EquipWith(LeftFoot);
            RightLeg.Attach(RightFoot);
            for (int i = 0; i < 5; i++)
            {
                RightFoot.Attach(new BodyPart(game, "Right Toe " + i.ToString(), BodyPartType.Toe, false, false));
            }


            return BodyParts;
        }
    }
}

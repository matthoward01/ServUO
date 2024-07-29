using System;

namespace Server.Mobiles
{
    [CorpseName("a balron corpse")]
    public class SummonedBalron : BaseCreature
    {
        [Constructable]
        public SummonedBalron()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("balron");
            Body = 40;
            BaseSoundID = 357;

            SetStr(1185);
            SetDex(255);
            SetInt(250);

            SetHits(711);

            SetDamage(29);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Fire, 25);
            SetDamageType(ResistanceType.Energy, 25);

            SetResistance(ResistanceType.Physical, 80);
            SetResistance(ResistanceType.Fire, 80);
            SetResistance(ResistanceType.Cold, 60);
            SetResistance(ResistanceType.Poison, 100);
            SetResistance(ResistanceType.Energy, 50);

            SetSkill(SkillName.Anatomy, 50.0);
            SetSkill(SkillName.EvalInt, 120.0);
            SetSkill(SkillName.Magery,120.0);
            SetSkill(SkillName.Meditation, 50.0);
            SetSkill(SkillName.MagicResist, 150.0);
            SetSkill(SkillName.Tactics, 120.0);
            SetSkill(SkillName.Wrestling, 120.0);            

            this.VirtualArmor = 90;
            this.ControlSlots = Core.SE ? 4 : 5;
        }

        public SummonedBalron(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty
        {
            get
            {
                return 125.0;
            }
        }
        public override double DispelFocus
        {
            get
            {
                return 0;
            }
        }
        public override Poison PoisonImmune
        {
            get
            {
                return Poison.DarkGlow;
            }
        }
        public override bool CanFly
        {
            get
            {
                return true;
            }
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}

using System;

namespace Server.Mobiles
{
    [CorpseName("a greater air elemental corpse")]
    public class SummonedGreaterAirElemental : BaseCreature
    {
        [Constructable]
        public SummonedGreaterAirElemental()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = "a greater air elemental";
            this.Body = 13;
            this.Hue = 0x4001;
            this.BaseSoundID = 655;

            this.SetStr(315);
            this.SetDex(185);
            this.SetInt(125);

            this.SetHits(900);
            this.SetDamage(17);            

            this.SetDamageType(ResistanceType.Physical, 20);
            this.SetDamageType(ResistanceType.Cold, 40);
            this.SetDamageType(ResistanceType.Energy, 40);

            this.SetResistance(ResistanceType.Physical, 85);
            this.SetResistance(ResistanceType.Fire, 65);
            this.SetResistance(ResistanceType.Cold, 65);
            this.SetResistance(ResistanceType.Poison, 65);
            this.SetResistance(ResistanceType.Energy, 55);

            this.SetSkill(SkillName.MagicResist, 120);
            this.SetSkill(SkillName.Tactics, 120);
            this.SetSkill(SkillName.Wrestling, 120);
            this.SetSkill(SkillName.Magery, 120);
            this.SetSkill(SkillName.EvalInt, 120);

            this.VirtualArmor = 40;
            this.ControlSlots = 2;
        }

        public SummonedGreaterAirElemental(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty
        {
            get
            {
                return 150.0;
            }
        }
        public override double DispelFocus
        {
            get
            {
                return 45.0;
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

            if (this.BaseSoundID == 263)
                this.BaseSoundID = 655;
        }
    }
}

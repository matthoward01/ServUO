using Server.Items;
using System;

namespace Server.Mobiles
{
    [CorpseName("a greater water elemental corpse")]
    public class SummonedGreaterWaterElemental : BaseCreature
    {
        [Constructable]
        public SummonedGreaterWaterElemental()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = "a greater water elemental";
            this.Body = 16;
            this.BaseSoundID = 278;

            this.SetStr(500);
            this.SetDex(160);
            this.SetInt(140);

            this.SetHits(600);
            this.SetMana(700);

            this.SetDamage(16);

            this.SetDamageType(ResistanceType.Physical, 50);
            this.SetDamageType(ResistanceType.Cold, 50);

            this.SetResistance(ResistanceType.Physical, 70);
            this.SetResistance(ResistanceType.Fire, 50);
            this.SetResistance(ResistanceType.Cold, 60);
            this.SetResistance(ResistanceType.Poison, 80);
            this.SetResistance(ResistanceType.Energy, 60);

            this.SetSkill(SkillName.MagicResist, 110);
            this.SetSkill(SkillName.Tactics, 110);
            this.SetSkill(SkillName.Wrestling, 110);
            this.SetSkill(SkillName.Magery, 110);
            this.SetSkill(SkillName.EvalInt, 100);

            this.VirtualArmor = 40;
            this.ControlSlots = 2;
            this.CanSwim = true;
        }

        public SummonedGreaterWaterElemental(Serial serial)
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
        }
    }
}

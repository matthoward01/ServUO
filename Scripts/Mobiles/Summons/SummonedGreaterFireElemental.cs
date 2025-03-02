using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a greater fire elemental corpse")]
    public class SummonedGreaterFireElemental : BaseCreature
    {
        [Constructable]
        public SummonedGreaterFireElemental()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = "a greater fire elemental";
            this.Body = 15;
            this.BaseSoundID = 838;

            this.SetStr(200);
            this.SetDex(200);
            this.SetInt(100);

            this.SetDamage(14);

            this.SetDamageType(ResistanceType.Physical, 0);
            this.SetDamageType(ResistanceType.Fire, 100);

            this.SetResistance(ResistanceType.Physical, 60);
            this.SetResistance(ResistanceType.Fire, 100);
            this.SetResistance(ResistanceType.Cold, 10);
            this.SetResistance(ResistanceType.Poison, 60);
            this.SetResistance(ResistanceType.Energy, 60);

            this.SetSkill(SkillName.EvalInt, 100.0);
            this.SetSkill(SkillName.Magery, 100.0);
            this.SetSkill(SkillName.MagicResist, 100.0);
            this.SetSkill(SkillName.Tactics, 100.0);
            this.SetSkill(SkillName.Wrestling, 100.0);

            this.VirtualArmor = 40;
            this.ControlSlots = 2;

            this.AddItem(new LightSource());
        }

        public SummonedGreaterFireElemental(Serial serial)
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

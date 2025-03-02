using System;

namespace Server.Mobiles
{
    [CorpseName("a greater earth elemental corpse")]
    public class SummonedGreaterEarthElemental : BaseCreature
    {
        [Constructable]
        public SummonedGreaterEarthElemental()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = "a greater earth elemental";
            this.Body = 14;
            this.BaseSoundID = 268;

            this.SetStr(155);
            this.SetDex(85);
            this.SetInt(92);

            this.SetHits(600);
            
            this.SetDamage(16);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 80);
            this.SetResistance(ResistanceType.Fire, 60);
            this.SetResistance(ResistanceType.Cold, 60);
            this.SetResistance(ResistanceType.Poison, 70);
            this.SetResistance(ResistanceType.Energy, 50);

            this.SetSkill(SkillName.MagicResist, 100);
            this.SetSkill(SkillName.Tactics, 100);
            this.SetSkill(SkillName.Wrestling, 100);

            this.SetSpecialAbility(SpecialAbility.ColossalRage);

            this.VirtualArmor = 34;
            this.ControlSlots = 2;
        }

        public SummonedGreaterEarthElemental(Serial serial)
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

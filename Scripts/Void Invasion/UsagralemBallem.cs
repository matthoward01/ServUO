using System;
using Server;
using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "an Usagralem Ballem's corpse" )]
	public class UsagralemBallem : BaseVoidCreature
	{
		public override int Stage { get { return 3; } }
		
		[Constructable]
		public UsagralemBallem()
			: base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
		{
			this.Name = "an Usagrallem Ballem";
            this.Hue = 2071;
            this.Body = 318;
            this.BaseSoundID = 0x165;

            this.SetStr(900, 1000);
            this.SetDex(1028);
            this.SetInt(1000, 1100);

            this.SetHits(2000, 2200);
            this.SetMana(5000);

            this.SetDamage(17, 21);

            this.SetDamageType(ResistanceType.Physical, 20);
            this.SetDamageType(ResistanceType.Fire, 20);
            this.SetDamageType(ResistanceType.Cold, 20);
            this.SetDamageType(ResistanceType.Poison, 20);
            this.SetDamageType(ResistanceType.Energy, 20);

            this.SetResistance(ResistanceType.Physical, 30, 40);
            this.SetResistance(ResistanceType.Fire, 40, 60);
            this.SetResistance(ResistanceType.Cold, 40, 60);
            this.SetResistance(ResistanceType.Poison, 40, 60);
            this.SetResistance(ResistanceType.Energy, 40, 60);

            this.SetSkill(SkillName.MagicResist, 80.0, 90.0);
            this.SetSkill(SkillName.Tactics, 80.0, 90.0);
            this.SetSkill(SkillName.Wrestling, 80.0, 90.0);

            this.Fame = 18000;
            this.Karma = -18000;

            this.VirtualArmor = 64;

            this.PackItem(new DaemonBone(30));
			
			m_ActiveVoidCreatures++;
		}

		public UsagralemBallem( Serial serial )
			: base( serial )
		{
		}
		
		public override bool IgnoreYoungProtection
        {
            get
            {
                return Core.ML;
            }
        }
		
        public override bool BardImmune
        {
            get
            {
                return !Core.SE;
            }
        }
		
        public override bool Unprovokable
        {
            get
            {
                return Core.SE;
            }
        }
		
        public override bool AreaPeaceImmune
        {
            get
            {
                return Core.SE;
            }
        }
		
        public override Poison PoisonImmune
        {
            get
            {
                return Poison.Lethal;
            }
        }
		
        public override WeaponAbility GetWeaponAbility()
        {
            switch ( Utility.Random(3) )
            {
                default:
                case 0:
                    return WeaponAbility.DoubleStrike;
                case 1:
                    return WeaponAbility.WhirlwindAttack;
                case 2:
                    return WeaponAbility.CrushingBlow;
            }
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 1);
            AddLoot(LootPack.FilthyRich, 2);
        }

		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			m_ActiveVoidCreatures--;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			/*int version = */
			reader.ReadInt();

			m_ActiveVoidCreatures++;
		}
	}
}

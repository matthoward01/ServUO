using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an Ortanord's corpse" )]
	public class Ortanord : BaseCreature
	{
		public static readonly int MaxAmount = 5;

		private static int m_Amount;

		public static void Spawn( Point3D loc, Map map )
		{
			if ( MaxAmount > m_Amount )
			{
				Mobile m = new Ortanord();
				m.MoveToWorld( loc, map );
			}
		}

		public override bool AlwaysMurderer { get { return true; } }

		[Constructable]
		public Ortanord()
			: base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Ortanord";
            Body = 58;
            Hue = 2071;
            BaseSoundID = 466;

            SetStr(50, 50);
            SetDex(50, 50);
            SetInt(51, 51);

            SetHits(100, 100);
            SetMana(1001, 1001);
            SetStam(50, 50);

            SetDamage(5, 8);

            SetDamageType(ResistanceType.Energy, 100);

            SetResistance(ResistanceType.Physical, 80, 90);
            SetResistance(ResistanceType.Fire, 80, 90);
            SetResistance(ResistanceType.Cold, 80, 90);
            SetResistance(ResistanceType.Poison, 80, 90);
            SetResistance(ResistanceType.Energy, 80, 90);

            SetSkill(SkillName.MagicResist, 104.4, 108.0);
            SetSkill(SkillName.Tactics, 19.0, 19.8);
            SetSkill(SkillName.Anatomy, 15.6, 16.8);
            SetSkill(SkillName.Wrestling, 15.4, 16.6);
            SetSkill(SkillName.Magery, 104.7, 107.3);
            SetSkill(SkillName.Meditation, 20.0, 20.0);

            Fame = 8000;
            Karma = -8000;

            VirtualArmor = 40;

            //QLPoints = 3;

            if (0.25 > Utility.RandomDouble())
                PackItem(new DaemonBone(10));

			Team = 1;

			m_Amount++;
		}

		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			m_Amount--;
		}

		public Ortanord( Serial serial )
			: base( serial )
		{
		}
		
		public override bool BardImmune
        {
            get
            {
                return !Core.AOS;
            }
        }
		
        public override Poison PoisonImmune
        {
            get
            {
                return Poison.Lethal;
            }
        }
		
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average, 2);
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

			m_Amount++;
		}
	}
}

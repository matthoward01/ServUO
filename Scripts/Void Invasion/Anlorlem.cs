using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an Anlorlem's corpse" )]
	public class Anlorlem : BaseVoidCreature, IGatherer
	{
		protected override void CreateEvolutionHandlers()
		{
			AddEvolutionHandler( new GroupingPathHandler( this, typeof( Anlorvaglem ), 2000 ) );
		}
		
		public override int Stage { get { return 2; } }

		[Constructable]
		public Anlorlem()
			: base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
		{
			this.Name = "an Anlorlem";
            this.Body = 72;
            this.Hue = 2071;
            this.BaseSoundID = 644;

            this.SetStr(900, 1000);
            this.SetDex(1000, 1200);
            this.SetInt(900, 950);

            this.SetHits(500, 650);

            this.SetDamage(18, 22);

            this.SetDamageType(ResistanceType.Physical, 50);
            this.SetDamageType(ResistanceType.Poison, 50);

            this.SetResistance(ResistanceType.Physical, 45, 55);
            this.SetResistance(ResistanceType.Fire, 30, 40);
            this.SetResistance(ResistanceType.Cold, 35, 45);
            this.SetResistance(ResistanceType.Poison, 90, 100);
            this.SetResistance(ResistanceType.Energy, 35, 45);

            this.SetSkill(SkillName.MagicResist, 40.1, 70.0);
            this.SetSkill(SkillName.Tactics, 90.1, 100.0);
            this.SetSkill(SkillName.Wrestling, 90.1, 100.0);

            this.Fame = 16000;
            this.Karma = -16000;

            this.VirtualArmor = 50;

            this.PackItem(new DaemonBone(15));
			
			m_ActiveVoidCreatures++;
		}

		public Anlorlem( Serial serial )
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
        public override bool Unprovokable
        {
            get
            {
                return true;
            }
        }
        public override bool ReacquireOnMovement
        {
            get
            {
                return true;
            }
        }
        public override Poison PoisonImmune
        {
            get
            {
                return Poison.Greater;
            }
        }
        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.FilthyRich);
            this.AddLoot(LootPack.Average, 2);
            this.AddLoot(LootPack.MedScrolls, 2);
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

		private Mobile m_GatherTarget;
		private DateTime m_NextGatherAttempt;

		public Mobile GatherTarget
		{
			get { return m_GatherTarget; }
			set { m_GatherTarget = value; }
		}

		public DateTime NextGatherAttempt
		{
			get { return m_NextGatherAttempt; }
			set { m_NextGatherAttempt = value; }
		}

		public bool DoesGather { get { return true; } }
	}
}

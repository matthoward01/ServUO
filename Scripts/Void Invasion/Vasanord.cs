using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Engines.Plants;
using Server.Engines.VoidCreatureInvasion;

namespace Server.Mobiles
{
	[CorpseName( "a Vasanord's corpse" )]
	public class Vasanord : BaseVoidCreature
	{
		public override int Stage { get { return 3; } }
		
		[Constructable]
		public Vasanord()
			: base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.6, 1.2 )
		{
			this.Name = "vasanord";
			this.Body = 780;

			this.SetStr( 805, 869 );
			this.SetDex( 51, 64 );
			this.SetInt( 38, 48 );

			this.SetHits( 5000, 5200 );
			this.SetMana( 40, 70 );
			this.SetStam( 50, 80 );

			this.SetDamage( 10, 23 );

			this.SetDamageType( ResistanceType.Physical, 20 );
			this.SetDamageType( ResistanceType.Fire, 20 );
			this.SetDamageType( ResistanceType.Cold, 20 );
			this.SetDamageType( ResistanceType.Poison, 20 );
			this.SetDamageType( ResistanceType.Energy, 20 );

			this.SetResistance( ResistanceType.Physical, 30, 50 );
			this.SetResistance( ResistanceType.Fire, 20, 50 );
			this.SetResistance( ResistanceType.Cold, 20, 40 );
			this.SetResistance( ResistanceType.Poison, 100 );
			this.SetResistance( ResistanceType.Energy, 20, 50 );

			this.SetSkill( SkillName.MagicResist, 72.8, 77.7 );
			this.SetSkill( SkillName.Tactics, 50.7, 110.0 );
			this.SetSkill( SkillName.EvalInt, 99.5, 120.0 );
			this.SetSkill( SkillName.Magery, 95.5, 106.9 );
			this.SetSkill( SkillName.Wrestling, 53.6, 98.6 );

			this.Fame = 15000;
			this.Karma = -15000;

			this.VirtualArmor = 28;

		    this.PackItem( new DaemonBone( 30 ) );
			
			
		}

		public Vasanord( Serial serial )
			: base( serial )
		{
		}
		
		public override bool BardImmune
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
                return Poison.Lethal;
            }
        }
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 2 );
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

		public void SpawnKorpre( Mobile m )
		{
				Map map = this.Map;

				if ( map == null )
					return;

				EKorpre spawned = new EKorpre();

				spawned.Team = this.Team;

				bool validLocation = false;
				Point3D loc = this.Location;

				for ( int j = 0; !validLocation && j < 10; ++j )
				{
					int x = X + Utility.Random( 3 ) - 1;
					int y = Y + Utility.Random( 3 ) - 1;
					int z = map.GetAverageZ( x, y );

					if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
						loc = new Point3D( x, y, Z );
					else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
						loc = new Point3D( x, y, z );
				}

				spawned.MoveToWorld( loc, map );
				spawned.Combatant = m;
		}

		public void EatKorpres()
		{
			List<Mobile> toEat = new List<Mobile>();

			foreach ( Mobile m in this.GetMobilesInRange( 2 ) )
			{
				if ( m is EKorpre )
				{
					toEat.Add( m );
				}
				else if ( m is Korpre )
				{
					toEat.Add( m );
				}
			}

			if ( toEat.Count > 0 )
			{
				PlaySound( Utility.Random( 0x3B, 2 ) ); // Eat sound

				foreach ( Mobile m in toEat )
				{
					Hits += ( m.Hits / 2 );
					m.Delete();
				}
			}
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( this.Hits > ( this.HitsMax / 4 ) )
			{
				if ( 0.25 >= Utility.RandomDouble() )
					SpawnKorpre( attacker );
			}
			else if ( 0.25 >= Utility.RandomDouble() )
			{
				EatKorpres();
			}
		}
	}
}

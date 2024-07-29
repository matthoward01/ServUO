using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;
using Server.Targeting;
using Server.Regions;
using Server.Commands;

namespace Server.Engines.VoidCreatureInvasion
{
	public class VoidCreatureInvasionSystemController : Item
	{
		private int m_NumToSpawn;
		private bool m_Active;
		private ArrayList m_SpawnPoints;
		private static Random rnd = new Random();
		public static readonly Map VoidInvasionMap = Map.TerMur;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Active 
		{ 
			get { return m_Active; } 
			set { m_Active = value;}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool RemoveAllVoids
		{
			get{ return false; }
			set{ if( value ) RemoveVoids(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int NumToSpawn
		{
			get{ return m_NumToSpawn; }
			set{ m_NumToSpawn = value; }
		}

		[Constructable]
		public VoidCreatureInvasionSystemController() : base( 3036 )
		{
			this.m_SpawnPoints = new ArrayList();
			this.Name = "Void Creature Invasion System Controller";
			this.Movable = false;
			this.Visible = false;
			this.m_NumToSpawn = 25;
			this.m_Active = false;
			this.m_SpawnPoints = new ArrayList();

			new InternalTimer( this ).Start();
		}
		
        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel >= AccessLevel.Administrator)
            {
                from.SendGump( new PropertiesGump( from, this));
            }
            else
            {
                from.SendMessage("You don't have permission access this.");
            }
            base.OnDoubleClick(from);
        }

		public void GenerateSpawnPoints()
		{
		Point3D m_SpawnPointA=new Point3D(1043,4022,-40);m_SpawnPoints.Add(m_SpawnPointA);Point3D m_SpawnPointB=new Point3D(1099,3965,-39);m_SpawnPoints.Add(m_SpawnPointB);Point3D m_SpawnPointC=new Point3D(1118,3903,-41);m_SpawnPoints.Add(m_SpawnPointC);Point3D m_SpawnPointD=new Point3D(1109,3838,-41);m_SpawnPoints.Add(m_SpawnPointD);Point3D m_SpawnPointE=new Point3D(888,3943,-40);m_SpawnPoints.Add(m_SpawnPointE);Point3D m_SpawnPointF=new Point3D(863,3881,-40);m_SpawnPoints.Add(m_SpawnPointF);Point3D m_SpawnPointG=new Point3D(905,3849,-37);m_SpawnPoints.Add(m_SpawnPointG);Point3D m_SpawnPointH=new Point3D(891,3822,-37);m_SpawnPoints.Add(m_SpawnPointH);Point3D m_SpawnPointI=new Point3D(916,3798,-45);m_SpawnPoints.Add(m_SpawnPointI);Point3D m_SpawnPointJ=new Point3D(894,3716,-45);m_SpawnPoints.Add(m_SpawnPointJ);Point3D m_SpawnPointK=new Point3D(829,3715,-47);m_SpawnPoints.Add(m_SpawnPointK);Point3D m_SpawnPointL=new Point3D(797,3765,-45);m_SpawnPoints.Add(m_SpawnPointL);Point3D m_SpawnPointM=new Point3D(734,3794,-43);m_SpawnPoints.Add(m_SpawnPointM);Point3D m_SpawnPointN=new Point3D(788,3836,-23);m_SpawnPoints.Add(m_SpawnPointN);Point3D m_SpawnPointO=new Point3D(910,3855,1);m_SpawnPoints.Add(m_SpawnPointO);Point3D m_SpawnPointP=new Point3D(779,3898,100);m_SpawnPoints.Add(m_SpawnPointP);Point3D m_SpawnPointQ=new Point3D(802,3959,-43);m_SpawnPoints.Add(m_SpawnPointQ);Point3D m_SpawnPointR=new Point3D(785,3983,-43);m_SpawnPoints.Add(m_SpawnPointR);Point3D m_SpawnPointS=new Point3D(755,4035,-43);m_SpawnPoints.Add(m_SpawnPointS);Point3D m_SpawnPointT=new Point3D(716,3932,-42);m_SpawnPoints.Add(m_SpawnPointT);Point3D m_SpawnPointU=new Point3D(682,3909,-42);m_SpawnPoints.Add(m_SpawnPointU);Point3D m_SpawnPointV=new Point3D(634,3909,-43);m_SpawnPoints.Add(m_SpawnPointV);Point3D m_SpawnPointW=new Point3D(621,3945,-43);m_SpawnPoints.Add(m_SpawnPointW);Point3D m_SpawnPointX=new Point3D(668,3981,-43);m_SpawnPoints.Add(m_SpawnPointX);Point3D m_SpawnPointY=new Point3D(677,4016,-43);m_SpawnPoints.Add(m_SpawnPointY);Point3D m_SpawnPointZ=new Point3D(585,3980,-44);m_SpawnPoints.Add(m_SpawnPointZ);Point3D m_SpawnPointAA=new Point3D(574,3941,-40);m_SpawnPoints.Add(m_SpawnPointAA);Point3D m_SpawnPointBB=new Point3D(582,3981,-37);m_SpawnPoints.Add(m_SpawnPointBB);Point3D m_SpawnPointCC=new Point3D(504,3936,-43);m_SpawnPoints.Add(m_SpawnPointCC);Point3D m_SpawnPointDD=new Point3D(469,3861,-42);m_SpawnPoints.Add(m_SpawnPointDD);Point3D m_SpawnPointEE=new Point3D(445,3852,-43);m_SpawnPoints.Add(m_SpawnPointEE);Point3D m_SpawnPointFF=new Point3D(498,3815,-34);m_SpawnPoints.Add(m_SpawnPointFF);Point3D m_SpawnPointGG=new Point3D(506,3751,-32);m_SpawnPoints.Add(m_SpawnPointGG);Point3D m_SpawnPointHH=new Point3D(533,3747,-43);m_SpawnPoints.Add(m_SpawnPointHH);Point3D m_SpawnPointII=new Point3D(503,3710,-42);m_SpawnPoints.Add(m_SpawnPointII);Point3D m_SpawnPointJJ=new Point3D(534,3672,-42);m_SpawnPoints.Add(m_SpawnPointJJ);Point3D m_SpawnPointKK=new Point3D(592,3636,38);m_SpawnPoints.Add(m_SpawnPointKK);Point3D m_SpawnPointLL=new Point3D(538,3577,38);m_SpawnPoints.Add(m_SpawnPointLL);Point3D m_SpawnPointMM=new Point3D(478,3529,38);m_SpawnPoints.Add(m_SpawnPointMM);Point3D m_SpawnPointNN=new Point3D(424,3532,38);m_SpawnPoints.Add(m_SpawnPointNN);Point3D m_SpawnPointOO=new Point3D(397,3474,38);m_SpawnPoints.Add(m_SpawnPointOO);Point3D m_SpawnPointPP=new Point3D(369,3541,39);m_SpawnPoints.Add(m_SpawnPointPP);Point3D m_SpawnPointQQ=new Point3D(314,3585,38);m_SpawnPoints.Add(m_SpawnPointQQ);Point3D m_SpawnPointRR=new Point3D(387,3640,38);m_SpawnPoints.Add(m_SpawnPointRR);Point3D m_SpawnPointSS=new Point3D(544,3440,37);m_SpawnPoints.Add(m_SpawnPointSS);Point3D m_SpawnPointTT=new Point3D(556,3372,37);m_SpawnPoints.Add(m_SpawnPointTT);Point3D m_SpawnPointUU=new Point3D(482,3404,38);m_SpawnPoints.Add(m_SpawnPointUU);Point3D m_SpawnPointVV=new Point3D(534,3249,37);m_SpawnPoints.Add(m_SpawnPointVV);Point3D m_SpawnPointWW=new Point3D(482,3184,38);m_SpawnPoints.Add(m_SpawnPointWW);Point3D m_SpawnPointXX=new Point3D(405,3176,38);m_SpawnPoints.Add(m_SpawnPointXX);Point3D m_SpawnPointYY=new Point3D(371,3163,35);m_SpawnPoints.Add(m_SpawnPointYY);Point3D m_SpawnPointZZ=new Point3D(397,3128,37);m_SpawnPoints.Add(m_SpawnPointZZ);Point3D m_SpawnPointAB=new Point3D(460,3135,38);m_SpawnPoints.Add(m_SpawnPointAB);Point3D m_SpawnPointAC=new Point3D(486,3152,38);m_SpawnPoints.Add(m_SpawnPointAC);Point3D m_SpawnPointAD=new Point3D(538,3125,49);m_SpawnPoints.Add(m_SpawnPointAD);Point3D m_SpawnPointAE=new Point3D(584,3109,47);m_SpawnPoints.Add(m_SpawnPointAE);Point3D m_SpawnPointAF=new Point3D(514,3016,36);m_SpawnPoints.Add(m_SpawnPointAF);Point3D m_SpawnPointAG=new Point3D(538,2956,9);m_SpawnPoints.Add(m_SpawnPointAG);Point3D m_SpawnPointAH=new Point3D(588,2957,38);m_SpawnPoints.Add(m_SpawnPointAH);Point3D m_SpawnPointAI=new Point3D(538,2916,40);m_SpawnPoints.Add(m_SpawnPointAI);Point3D m_SpawnPointAJ=new Point3D(609,3190,38);m_SpawnPoints.Add(m_SpawnPointAJ);Point3D m_SpawnPointAK=new Point3D(659,3174,38);m_SpawnPoints.Add(m_SpawnPointAK);Point3D m_SpawnPointAL=new Point3D(686,3154,38);m_SpawnPoints.Add(m_SpawnPointAL);Point3D m_SpawnPointAM=new Point3D(713,3188,38);m_SpawnPoints.Add(m_SpawnPointAM);Point3D m_SpawnPointAN=new Point3D(730,3165,85);m_SpawnPoints.Add(m_SpawnPointAN);Point3D m_SpawnPointAO=new Point3D(736,3130,38);m_SpawnPoints.Add(m_SpawnPointAO);Point3D m_SpawnPointAP=new Point3D(699,3114,38);m_SpawnPoints.Add(m_SpawnPointAP);Point3D m_SpawnPointAQ=new Point3D(701,3092,59);m_SpawnPoints.Add(m_SpawnPointAQ);Point3D m_SpawnPointAR=new Point3D(731,3090,62);m_SpawnPoints.Add(m_SpawnPointAR);Point3D m_SpawnPointAS=new Point3D(776,3081,50);m_SpawnPoints.Add(m_SpawnPointAS);Point3D m_SpawnPointAT=new Point3D(827,3080,38);m_SpawnPoints.Add(m_SpawnPointAT);Point3D m_SpawnPointAU=new Point3D(839,3122,38);m_SpawnPoints.Add(m_SpawnPointAU);Point3D m_SpawnPointAV=new Point3D(857,3164,83);m_SpawnPoints.Add(m_SpawnPointAV);Point3D m_SpawnPointAW=new Point3D(901,3127,39);m_SpawnPoints.Add(m_SpawnPointAW);Point3D m_SpawnPointAX=new Point3D(951,3109,40);m_SpawnPoints.Add(m_SpawnPointAX);Point3D m_SpawnPointAY=new Point3D(935,3093,62);m_SpawnPoints.Add(m_SpawnPointAY);Point3D m_SpawnPointAZ=new Point3D(880,3077,38);m_SpawnPoints.Add(m_SpawnPointAZ);Point3D m_SpawnPointBA=new Point3D(870,3038,61);m_SpawnPoints.Add(m_SpawnPointBA);Point3D m_SpawnPointBC=new Point3D(831,3036,50);m_SpawnPoints.Add(m_SpawnPointBC);Point3D m_SpawnPointBD=new Point3D(793,3006,49);m_SpawnPoints.Add(m_SpawnPointBD);Point3D m_SpawnPointBE=new Point3D(784,2966,38);m_SpawnPoints.Add(m_SpawnPointBE);Point3D m_SpawnPointBF=new Point3D(782,2931,38);m_SpawnPoints.Add(m_SpawnPointBF);Point3D m_SpawnPointBG=new Point3D(823,2900,22);m_SpawnPoints.Add(m_SpawnPointBG);Point3D m_SpawnPointBH=new Point3D(852,2929,38);m_SpawnPoints.Add(m_SpawnPointBH);Point3D m_SpawnPointBI=new Point3D(867,2960,38);m_SpawnPoints.Add(m_SpawnPointBI);Point3D m_SpawnPointBJ=new Point3D(880,2981,38);m_SpawnPoints.Add(m_SpawnPointBJ);Point3D m_SpawnPointBK=new Point3D(911,3008,37);m_SpawnPoints.Add(m_SpawnPointBK);Point3D m_SpawnPointBL=new Point3D(910,2961,38);m_SpawnPoints.Add(m_SpawnPointBL);Point3D m_SpawnPointBM=new Point3D(887,2891,37);m_SpawnPoints.Add(m_SpawnPointBM);Point3D m_SpawnPointBN=new Point3D(977,3037,50);m_SpawnPoints.Add(m_SpawnPointBN);Point3D m_SpawnPointBO=new Point3D(944,3042,38);m_SpawnPoints.Add(m_SpawnPointBO);Point3D m_SpawnPointBP=new Point3D(976,3071,54);m_SpawnPoints.Add(m_SpawnPointBP);Point3D m_SpawnPointBQ=new Point3D(1034,3073,38);m_SpawnPoints.Add(m_SpawnPointBQ);Point3D m_SpawnPointBR=new Point3D(1025,3031,30);m_SpawnPoints.Add(m_SpawnPointBR);Point3D m_SpawnPointBS=new Point3D(1005,2992,37);m_SpawnPoints.Add(m_SpawnPointBS);Point3D m_SpawnPointBT=new Point3D(1024,2956,50);m_SpawnPoints.Add(m_SpawnPointBT);Point3D m_SpawnPointBU=new Point3D(1060,2971,50);m_SpawnPoints.Add(m_SpawnPointBU);Point3D m_SpawnPointBV=new Point3D(1077,2998,62);m_SpawnPoints.Add(m_SpawnPointBV);Point3D m_SpawnPointBW=new Point3D(1110,2974,38);m_SpawnPoints.Add(m_SpawnPointBW);Point3D m_SpawnPointBX=new Point3D(1061,2962,37);m_SpawnPoints.Add(m_SpawnPointBX);Point3D m_SpawnPointBY=new Point3D(1126,3028,38);m_SpawnPoints.Add(m_SpawnPointBY);Point3D m_SpawnPointBZ=new Point3D(1137,3010,38);m_SpawnPoints.Add(m_SpawnPointBZ);Point3D m_SpawnPointCA=new Point3D(1084,3072,38);m_SpawnPoints.Add(m_SpawnPointCA);Point3D m_SpawnPointCD=new Point3D(1019,3111,38);m_SpawnPoints.Add(m_SpawnPointCD);Point3D m_SpawnPointCE=new Point3D(1002,3163,38);m_SpawnPoints.Add(m_SpawnPointCE);Point3D m_SpawnPointCF=new Point3D(1013,3198,38);m_SpawnPoints.Add(m_SpawnPointCF);Point3D m_SpawnPointCG=new Point3D(1041,3211,38);m_SpawnPoints.Add(m_SpawnPointCG);Point3D m_SpawnPointCH=new Point3D(1059,3165,38);m_SpawnPoints.Add(m_SpawnPointCH);Point3D m_SpawnPointCI=new Point3D(1110,3131,-43);m_SpawnPoints.Add(m_SpawnPointCI);Point3D m_SpawnPointCJ=new Point3D(1131,3164,-42);m_SpawnPoints.Add(m_SpawnPointCJ);Point3D m_SpawnPointCK=new Point3D(1127,3203,-43);m_SpawnPoints.Add(m_SpawnPointCK);Point3D m_SpawnPointCL=new Point3D(1121,3242,-42);m_SpawnPoints.Add(m_SpawnPointCL);Point3D m_SpawnPointCM=new Point3D(1147,3259,-42);m_SpawnPoints.Add(m_SpawnPointCM);Point3D m_SpawnPointCN=new Point3D(1175,3284,-42);m_SpawnPoints.Add(m_SpawnPointCN);Point3D m_SpawnPointCO=new Point3D(1204,3302,-43);m_SpawnPoints.Add(m_SpawnPointCO);Point3D m_SpawnPointCP=new Point3D(1181,3336,-42);m_SpawnPoints.Add(m_SpawnPointCP);Point3D m_SpawnPointCQ=new Point3D(1185,3383,-42);m_SpawnPoints.Add(m_SpawnPointCQ);Point3D m_SpawnPointCR=new Point3D(1128,3382,-42);m_SpawnPoints.Add(m_SpawnPointCR);Point3D m_SpawnPointCS=new Point3D(1109,3448,-42);m_SpawnPoints.Add(m_SpawnPointCS);Point3D m_SpawnPointCT=new Point3D(1066,3480,-43);m_SpawnPoints.Add(m_SpawnPointCT);Point3D m_SpawnPointCU=new Point3D(1124,3497,-42);m_SpawnPoints.Add(m_SpawnPointCU);Point3D m_SpawnPointCV=new Point3D(1172,3469,-42);m_SpawnPoints.Add(m_SpawnPointCV);Point3D m_SpawnPointCW=new Point3D(1194,3518,-42);m_SpawnPoints.Add(m_SpawnPointCW);Point3D m_SpawnPointCX=new Point3D(1187,3567,-42);m_SpawnPoints.Add(m_SpawnPointCX);Point3D m_SpawnPointCY=new Point3D(1128,3568,-42);m_SpawnPoints.Add(m_SpawnPointCY);Point3D m_SpawnPointCZ=new Point3D(1192,3616,-42);m_SpawnPoints.Add(m_SpawnPointCZ);Point3D m_SpawnPointDA=new Point3D(1186,3650,-42);m_SpawnPoints.Add(m_SpawnPointDA);Point3D m_SpawnPointDB=new Point3D(1132,3710,-43);m_SpawnPoints.Add(m_SpawnPointDB);Point3D m_SpawnPointDC=new Point3D(1132,3736,-42);m_SpawnPoints.Add(m_SpawnPointDC);Point3D m_SpawnPointDE=new Point3D(1119,3766,-43);m_SpawnPoints.Add(m_SpawnPointDE);Point3D m_SpawnPointDF=new Point3D(1091,3752,-43);m_SpawnPoints.Add(m_SpawnPointDF);Point3D m_SpawnPointDG=new Point3D(1082,3725,-43);m_SpawnPoints.Add(m_SpawnPointDG);CheckSpawn();
		}

		public void CheckSpawn()
		{
			if ( this.m_Active == false )
			{
				this.RemoveVoids();
			}
			this.SpawnVoids();
		}
		
		private void SpawnVoids()
		{
			if ( this.m_Active == true && BaseVoidCreature.m_ActiveVoidCreatures < m_NumToSpawn )
			{
				int newKorpre = Utility.RandomMinMax( 4, 6 );
				int r = rnd.Next(m_SpawnPoints.Count);

					Point3D loc = ((Point3D)m_SpawnPoints[r]);

				for ( int i = 0; i < newKorpre; ++i )
				{
					Korpre Korpre = new Korpre();

					Effects.SendLocationParticles( EffectItem.Create( loc, VoidInvasionMap, TimeSpan.FromSeconds( 10.0 ) ), 0x37CC, 1, 50, 0x49A, 7, 9909, 0 );
					Korpre.MoveToWorld( loc, VoidInvasionMap );
				}
			}
		}

		private void RemoveVoids()
		{
			List<Mobile> toDelete = new List<Mobile>();
			foreach ( Mobile mvc in World.Mobiles.Values )
			{
				if ( mvc is BaseVoidCreature )
				{
					toDelete.Add(mvc);
				}
			}
			
			for (int i = 0; i < toDelete.Count; ++i)
                toDelete[i].Delete();
		}

		public override void Delete()
		{
			this.RemoveVoids();
			base.Delete();
		}

		private class InternalTimer : Timer
		{
			private VoidCreatureInvasionSystemController sys;
			public InternalTimer( VoidCreatureInvasionSystemController s ) : base( TimeSpan.FromMinutes( 1.0 )) //Minutes Seconds
			{
				sys = s;
			}
			protected override void OnTick()
			{
				if ( sys != null && !sys.Deleted )
				{
					sys.m_SpawnPoints = new ArrayList();
					sys.GenerateSpawnPoints();
					new InternalTimer( sys ).Start();
				}
			}
		}
		
		public VoidCreatureInvasionSystemController( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			writer.Write( (bool) m_Active );
			writer.Write( (int) m_NumToSpawn );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_Active = reader.ReadBool();
			m_NumToSpawn = reader.ReadInt();
			
			new InternalTimer( this ).Start();
		}
		
	}
}
#region References
using Server.Engines.Quests.Ninja;
using Server.Items;
using Server.Multis;
using Server.Spells;
using Server.Spells.Chivalry;
using Server.Spells.First;
using Server.Spells.Fourth;
using Server.Spells.Mysticism;
using Server.Spells.Necromancy;
using Server.Spells.Third;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
#endregion

namespace Server.Mobiles
{
	public class PaladinAI : MageAI
	{
		public override SkillName CastSkill { get { return SkillName.Chivalry; } }
		public override bool UsesMagery { get { return false; } }
        public override double HealChance { get { return .1; } }

        public PaladinAI(BaseCreature m)
			: base(m)
		{ }

        //TODO: Steven - Added Think method override
        public override bool Think()
        {
            if (m_Mobile.Deleted)
                return false;

            if (EnemyOfOneSpell.UnderEffect(m_Mobile))
            {
                bool castSpell = false;

                if (EnemyOfOneSpell.GetContext(m_Mobile).TargetType != null && (!m_Mobile.Aggressors.All(ai => EnemyOfOneSpell.GetContext(m_Mobile).TargetType.Equals(ai.Attacker.GetType()))
                    || !m_Mobile.Aggressed.All(ai => EnemyOfOneSpell.GetContext(m_Mobile).TargetType.Equals(ai.Defender.GetType()))))
                {
                    castSpell = true;
                }

                if(castSpell)
                {
                    EnemyOfOneSpell.RemoveEffect(m_Mobile);
                }
                
            }

            return base.Think();
        }

        //TODO: Steven - Override the ChooseSpell method
        public override Spell ChooseSpell(IDamageable c)
        {
            if (c == null || !c.Alive)
                return null;

            Spell spell = null;

            spell = CheckCastHealingSpell();

            if (spell != null)
                return spell;

            if (!(c is Mobile))
            {
                m_Mobile.DebugSay("Just doing damage...");
                spell = GetRandomDamageSpell();
            }else
            {
                spell = GetRandomBuffSpell();
            }
            
            if(spell == null)
            {
                spell = GetRandomDamageSpell();

                if(spell != null && !(spell.AcquireIndirectTargets(m_Mobile.Location,3).Count() > 2))
                {
                    spell = null;
                }
            }

            if (m_Mobile.Poisoned) // Top cast priority is cure
            {
                m_Mobile.DebugSay("I am going to cure myself");

                spell = GetCureSpell();
            }

            if (spell != null)
            {
                return spell;
            }
            else
            {
                return null;
            }
        }
        
        public override Spell GetRandomDamageSpell()
		{
			if (m_Mobile.Mana > 10 && 0.1 > Utility.RandomDouble()  )
				return new HolyLightSpell(m_Mobile, null);

			return null;
		}

		public override Spell GetRandomCurseSpell()
		{
			if (m_Mobile.Mana > 10)
				return new DispelEvilSpell(m_Mobile, null);

			return null;
		}

		public override Spell GetRandomBuffSpell()
		{
			var mana = m_Mobile.Mana;
			var select = 1;

            //TODO: Steven - Making Paladin's smarter about using buffs
            /*
			if (mana >= 15)
				select = 3;

            if (mana >= 20 && !EnemyOfOneSpell.UnderEffect(m_Mobile))
            {
                return new EnemyOfOneSpell(m_Mobile, null);
            }
            */

            if (m_Mobile.Combatant != null && mana >= 20 && !EnemyOfOneSpell.UnderEffect(m_Mobile) &&
                 m_Mobile.Aggressors.All(ai => ai.Attacker.GetType().Equals(m_Mobile.Combatant.GetType())))
            {
                return new EnemyOfOneSpell(m_Mobile, null);
            }

            if(mana >= 10 && !ConsecrateWeaponSpell.IsUnderEffects(m_Mobile))
            {
                return new ConsecrateWeaponSpell(m_Mobile, null);
            }

            if(mana >= 15 && !DivineFurySpell.UnderEffect(m_Mobile))
            {
                return new DivineFurySpell(m_Mobile, null);
            }

            if (mana >= 20 && isCursed())
            {
                return new RemoveCurseSpell(m_Mobile, null);
            }
            /*
            switch (Utility.Random(select))
			{
				case 0:
					return new RemoveCurseSpell(m_Mobile, null);
				case 1:
					return new DivineFurySpell(m_Mobile, null);
				case 2:
					return new ConsecrateWeaponSpell(m_Mobile, null);
				case 3:
                    return new EnemyOfOneSpell(m_Mobile, null);
            }
            */
            return null;
		}

		public override Spell GetHealSpell()
		{
			if (m_Mobile.Mana > 10)
				return new CloseWoundsSpell(m_Mobile, null);

			return null;
		}

		public override Spell GetCureSpell()
		{
			if (m_Mobile.Mana > 10)
				return new CleanseByFireSpell(m_Mobile, null);

			return null;
		}

		protected override bool ProcessTarget()
		{
			if (m_Mobile.Target == null)
				return false;

			m_Mobile.Target.Invoke(m_Mobile, m_Mobile);
			return true;
		}

        //TODO: Steven - Method to check for curses
        private bool isCursed()
        {
            if (CorpseSkinSpell.IsUnderEffects(m_Mobile))
            {
                return true;
            }

            if (BloodOathSpell.UnderEffects(m_Mobile))
            {
                return true;
            }

            if (SpellPlagueSpell.HasSpellPlague(m_Mobile))
            {
                return true;
            }

            return false;
        }
	}
}

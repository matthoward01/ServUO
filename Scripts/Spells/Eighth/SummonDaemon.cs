using System;
using Server.Mobiles;

namespace Server.Spells.Eighth
{
    //TODO: MATT - CREATING SUMMON BALRON SPELL
    //public class SummonDaemonSpell : MagerySpell
    public class SummonBalronSpell : MagerySpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Summon Balron", "Kal Vas Corp",
            //"Summon Daemon", "Kal Vas Xen Corp",
            269,
            9050,
            false,
            Reagent.Bloodmoss,
            Reagent.MandrakeRoot,
            Reagent.SpidersSilk,
            Reagent.SulfurousAsh);
        //public SummonDaemonSpell(Mobile caster, Item scroll)
        public SummonBalronSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.Eighth;
            }
        }
        public override bool CheckCast()
        {
            if (!base.CheckCast())
                return false;

            if ((this.Caster.Followers + (Core.SE ? 4 : 5)) > this.Caster.FollowersMax)
            {
                this.Caster.SendLocalizedMessage(1049645); // You have too many followers to summon that creature.
                return false;
            }

            return true;
        }

        public override void OnCast()
        {
            if (this.CheckSequence())
            { 
                TimeSpan duration = TimeSpan.FromSeconds((2 * this.Caster.Skills.Magery.Fixed) / 5);

                if (Core.AOS)
                {
                    if (this.Caster.Skills.Magery.Fixed >= 115)
                    {
                        BaseCreature m_Balron = new SummonedBalron();
                        SpellHelper.Summon(m_Balron, this.Caster, 0x216, duration, false, false);
                        m_Balron.FixedParticles(0x3728, 8, 20, 5042, EffectLayer.Head);
                    }
                    else
                    {
                        BaseCreature m_Daemon = new SummonedDaemon();
                        SpellHelper.Summon(m_Daemon, this.Caster, 0x216, duration, false, false);
                        m_Daemon.FixedParticles(0x3728, 8, 20, 5042, EffectLayer.Head);
                    }
                }
                else
                    SpellHelper.Summon(new Balron(), this.Caster, 0x216, duration, false, false);
            }

            this.FinishSequence();
        }
    }
}

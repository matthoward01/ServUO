using System;

namespace Server.Items
{
    public class GreaterAgilityPotion : BaseAgilityPotion
    {
        [Constructable]
        public GreaterAgilityPotion()
            : base(PotionEffect.AgilityGreater)
        {
        }

        public GreaterAgilityPotion(Serial serial)
            : base(serial)
        {
        }

        public override int DexOffset
        {
            get
            {
                return 20;
            }
        }
        public override TimeSpan Duration
        {
            get
            {
                return TimeSpan.FromMinutes(10.0);
            }
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}

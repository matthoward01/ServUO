using System;

namespace Server.Items
{
    public class LissithsSilk : PeerlessKey
    { 
        [Constructable]
        public LissithsSilk()
            : base(0x2001)
        {
            Weight = 1;
            Hue = 0x4FB;
            LootType = LootType.Blessed;
        }

        public LissithsSilk(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber
        {
            get
            {
                return 1074333;
            }
        }// lissith's silk
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

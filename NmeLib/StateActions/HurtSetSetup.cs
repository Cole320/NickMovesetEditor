using System.Collections.Generic;

namespace NmeLib.StateActions
{
    public class HurtSetSetup : ISerializable
    {
        public List<HurtBone> HurtBones { get; private set; } = new List<HurtBone>();

        public HurtSetSetup()
        {
        }

        internal HurtSetSetup(BulkSerializeReader reader)
        {
            _ = reader.ReadInt();
            HurtBones = reader.ReadList(r => new HurtBone(r));
        }

        public void Write(BulkSerializeWriter writer)
        {
            writer.Write(0);
            writer.Write(HurtBones);
        }
    }
}
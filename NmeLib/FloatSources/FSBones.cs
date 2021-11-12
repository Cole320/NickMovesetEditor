namespace NmeLib.FloatSources
{
    public class FSBones : FloatSource
    {
        public Attributes Attribute { get; set; }

        public FSBones()
        { }

        internal FSBones(BulkSerializeReader reader) : base(reader)
        {
            Attribute = (Attributes)reader.ReadInt();
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(Attribute);
        }

        public enum Attributes
        {
            RotationAngle,
            LookAngle,
            TiltAngle,
            MirrorByDirection,
            OffsetX,
            OffsetY
        }
    }
}
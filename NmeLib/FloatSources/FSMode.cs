namespace NmeLib.FloatSources
{
    public class FSMode : FloatSource
    {
        public Attributes Attribute { get; set; }

        public FSMode()
        {
        }

        internal FSMode(BulkSerializeReader reader) : base(reader)
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
            InputAllowed
        }
    }
}
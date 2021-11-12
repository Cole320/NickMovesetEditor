using NmeLib.FloatSources;

namespace NmeLib.ObjectSources
{
    public class OSFloat : ObjectSource
    {
        public FloatSource Source { get; set; }

        public OSFloat()
        {
        }

        internal OSFloat(BulkSerializeReader reader) : base(reader)
        {
            Source = FloatSource.Read(reader);
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(Source);
        }
    }
}
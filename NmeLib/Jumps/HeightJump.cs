using NmeLib.FloatSources;

namespace NmeLib.Jumps
{
    public class HeightJump : Jump
    {
        public FloatSource Height { get; set; }

        public HeightJump()
        {
        }

        internal HeightJump(BulkSerializeReader reader) : base(reader)
        {
            Height = FloatSource.Read(reader);
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(Height);
        }
    }
}
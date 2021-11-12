using NmeLib.FloatSources;

namespace NmeLib.StateActions
{
    public class SAColorTint : StateAction
    {
        public string Id { get; set; }
        public bool Activate { get; set; }
        public bool Permanent { get; set; }
        public FloatSource RunForTime { get; set; }

        public SAColorTint()
        {
        }

        internal SAColorTint(BulkSerializeReader reader) : base(reader)
        {
            Id = reader.ReadString();
            Activate = reader.ReadBool();
            Permanent = reader.ReadBool();
            RunForTime = FloatSource.Read(reader);
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(Id);
            writer.Write(Activate);
            writer.Write(Permanent);
            writer.Write(RunForTime);
        }
    }
}
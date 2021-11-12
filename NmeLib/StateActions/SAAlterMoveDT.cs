using NmeLib.FloatSources;

namespace NmeLib.StateActions
{
    public class SAAlterMoveDT : StateAction
    {
        public bool ClearAMDT { get; set; }
        public FloatSource After { get; set; }
        public FloatSource Falloff { get; set; }

        public SAAlterMoveDT()
        {
        }

        internal SAAlterMoveDT(BulkSerializeReader reader) : base(reader)
        {
            ClearAMDT = reader.ReadBool();
            After = FloatSource.Read(reader);
            Falloff = FloatSource.Read(reader);
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(ClearAMDT);
            writer.Write(After);
            writer.Write(Falloff);
        }
    }
}
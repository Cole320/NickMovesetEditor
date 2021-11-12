namespace NmeLib.StateActions
{
    public class SASnapAnimWeights : StateAction
    {
        public bool ForceSample { get; set; }

        public SASnapAnimWeights()
        {
        }

        internal SASnapAnimWeights(BulkSerializeReader reader) : base(reader)
        {
            ForceSample = reader.ReadBool();
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(ForceSample);
        }
    }
}
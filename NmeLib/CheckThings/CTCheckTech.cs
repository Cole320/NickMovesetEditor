namespace NmeLib.CheckThings
{
    public class CTCheckTech : CheckThing
    {
        public string TechTimerId { get; set; }

        public CTCheckTech()
        {
        }

        internal CTCheckTech(BulkSerializeReader reader) : base(reader)
        {
            TechTimerId = reader.ReadString();
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(TechTimerId);
        }
    }
}
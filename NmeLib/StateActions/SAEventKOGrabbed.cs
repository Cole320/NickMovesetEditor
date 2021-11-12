namespace NmeLib.StateActions
{
    public class SAEventKOGrabbed : StateAction
    {
        public SAEventKO.KOType KO { get; set; }

        public SAEventKOGrabbed()
        {
        }

        internal SAEventKOGrabbed(BulkSerializeReader reader) : base(reader)
        {
            KO = (SAEventKO.KOType)reader.ReadInt();
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(KO);
        }
    }
}
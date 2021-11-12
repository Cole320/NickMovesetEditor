namespace NmeLib.StateActions
{
    public class SASetHitboxSFX : StateAction
    {
        public int Hitbox { get; set; }
        public string SfxId { get; set; }

        public SASetHitboxSFX()
        {
        }

        internal SASetHitboxSFX(BulkSerializeReader reader) : base(reader)
        {
            Hitbox = reader.ReadInt();
            SfxId = reader.ReadString();
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(Hitbox);
            writer.Write(SfxId);
        }
    }
}
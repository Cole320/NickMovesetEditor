using NmeLib.FloatSources;

namespace NmeLib.StateActions
{
    public class SAPlaySFXTimeline : StateAction
    {
        public ManipType Manip { get; set; }
        public bool Loop { get; set; }
        public FloatSource Source { get; set; }
        public string Timeline { get; set; }

        public SAPlaySFXTimeline()
        {
        }

        internal SAPlaySFXTimeline(BulkSerializeReader reader) : base(reader)
        {
            Manip = (ManipType)reader.ReadInt();
            Loop = reader.ReadBool();
            Source = FloatSource.Read(reader);
            Timeline = reader.ReadString();
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(Manip);
            writer.Write(Loop);
            writer.Write(Source);
            writer.Write(Timeline);
        }

        public enum ManipType
        {
            Play,
            Rate,
            Position
        }
    }
}
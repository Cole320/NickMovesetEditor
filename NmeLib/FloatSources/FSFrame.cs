namespace NmeLib.FloatSources
{
    public class FSFrame : FSValue
    {
        public FSFrame()
        {
        }

        public FSFrame(float x) : base(x)
        {
        }

        internal FSFrame(BulkSerializeReader reader) : base(reader)
        {
        }
    }
}
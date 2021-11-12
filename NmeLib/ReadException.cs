using System;

namespace NmeLib
{
    public class ReadException : Exception
    {
        private readonly BulkSerializeReader reader;

        public ReadException(BulkSerializeReader r) : base()
        {
            reader = r;
        }

        public ReadException(BulkSerializeReader r, string m) : base(m)
        {
            reader = r;
        }

        public ReadException(BulkSerializeReader r, string m, Exception inner) : base(m, inner)
        {
            reader = r;
        }

        public override string Message => base.Message + $" at reader int position: {reader.NextInt}, float position: {reader.NextFloat}, string position: {reader.NextString}";
    }
}
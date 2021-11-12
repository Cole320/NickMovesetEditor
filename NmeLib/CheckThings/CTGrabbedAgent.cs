﻿using System.Collections.Generic;

namespace NmeLib.CheckThings
{
    public class CTGrabbedAgent : CheckThing
    {
        public List<string> MatchTags { get; private set;  } = new List<string>();

        public CTGrabbedAgent()
        {
        }

        internal CTGrabbedAgent(BulkSerializeReader reader) : base(reader)
        {
            MatchTags = reader.ReadList(r => r.ReadString());
        }

        public override void Write(BulkSerializeWriter writer)
        {
            base.Write(writer);
            writer.Write(MatchTags);
        }
    }
}
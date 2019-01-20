using System.Runtime.Serialization;
using Neo4jMapper;

namespace UsageGuide.Entities
{
    public class MovieEntity
    {
        [NodeId]
        [IgnoreDataMember]
        public long Id { get; set; }

        public string title { get; set; }
        public string tagline { get; set; }
        public int released { get; set; }
    }
}

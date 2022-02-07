using System.Runtime.Serialization;
using Neo4j.Mapper;

namespace UsageGuide.Entities
{
    public class MovieEntity
    {
        [NodeId]
        [IgnoreDataMember]
        public long id { get; set; }

        public string title { get; set; }
        public string tagline { get; set; }
        public int released { get; set; }

        public string imdb { get; set; }
    }
}

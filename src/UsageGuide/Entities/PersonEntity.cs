using System.Collections.Generic;
using System.Runtime.Serialization;
using Neo4jMapper;

namespace UsageGuide.Entities
{
    public class PersonEntity
    {
        [NodeId]
        [IgnoreDataMember]
        public long Id { get; set; }

        public string name { get; set; }
        public int born { get; set; }

        public IEnumerable<MovieEntity> MoviesActedIn { get; set; }
    }
}

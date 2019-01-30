using System.Runtime.Serialization;
using Neo4jMapper;

namespace Examples.Entities
{
    public class Film
    {
        [NodeId]
        [IgnoreDataMember]
        public long Id { get; set; }

        public int Year { get; set; }
        public int Box { get; set; }
        public string Name { get; set; }
    }
}

using System.Collections.Generic;
using System.Runtime.Serialization;
using Neo4j.Mapper;

namespace Examples.Entities
{
    public class People
    {
        [NodeId]
        [IgnoreDataMember]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Role { get; set; }

        [IgnoreDataMember]
        public IEnumerable<Film> Films { get; set; }
    }
}

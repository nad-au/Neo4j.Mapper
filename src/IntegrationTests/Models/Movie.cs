using Neo4jMapper;

namespace IntegrationTests.Models
{
    public class Movie
    {
        [NodeId]
        public long Id { get; set; }

        public string title { get; set; }
        public string tagline { get; set; }
        public int released { get; set; }
    }
}

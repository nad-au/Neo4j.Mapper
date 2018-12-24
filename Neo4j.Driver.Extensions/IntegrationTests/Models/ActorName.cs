using System.Collections.Generic;

namespace IntegrationTests.Models
{
    public class ActorName
    {
        public string name { get; set; }
        public IEnumerable<Movie> MovesActedIn { get; set; }
    }
}

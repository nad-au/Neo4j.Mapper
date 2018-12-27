using System.Collections.Generic;

namespace IntegrationTests.Models
{
    public class Person
    {
        public string name { get; set; }
        public int born { get; set; }

        public IEnumerable<Movie> MovesActedIn { get; set; }
    }
}

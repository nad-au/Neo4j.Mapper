using System.Collections.Generic;

namespace UnitTests.Models
{
    public class Person
    {
        public string Name { get; set; }
        public int Born { get; set; }

        public IEnumerable<Movie> MovesActedIn { get; set; }
    }
}

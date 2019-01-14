using System.Collections.Generic;

namespace Benchmarks.Models
{
    public class Person
    {
        public string name { get; set; }
        public int born { get; set; }

        public IEnumerable<Movie> MovesActedIn { get; set; }
    }
}

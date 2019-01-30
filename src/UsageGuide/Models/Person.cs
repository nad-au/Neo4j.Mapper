using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UsageGuide.Models
{
    public class Person
    {
        public string name { get; set; }
        public int born { get; set; }

        [IgnoreDataMember]
        public List<Movie> MoviesActedIn { get; set; }
    }
}

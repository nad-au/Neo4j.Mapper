using System.Collections.Generic;

namespace IntegrationTests.Models
{
    public class PersonWithIdentities
    {
        public string Name { get; set; }
        public List<OtherName> OtherNames { get;set; }
    }
}

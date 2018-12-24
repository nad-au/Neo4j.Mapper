using System;
using Neo4j.Driver.V1;

namespace IntegrationTests.Models
{
    public class TimeStamp
    {
        public string Name { get; set; }
        public ZonedDateTime When { get; set; }
        public DateTimeOffset WhenDateTime => When.ToDateTimeOffset();
    }
}

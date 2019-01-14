using System;
using Neo4j.Driver.V1;

namespace UnitTests.Models
{
    public class EntityWithDriverTypes
    {
        public LocalDate DateValue { get; set; }
        public LocalTime TimeValue { get; set; }
        public ZonedDateTime DateTimeOffsetValue { get; set; }
        public LocalDateTime LocalDateTime { get; set; }
        public Person Person { get; set; }
    }

    public class EntityWithClrTypes
    {
        public DateTime DateValue { get; set; }
        public TimeSpan TimeValue { get; set; }
        public DateTimeOffset DateTimeOffsetValue { get; set; }
        public DateTime LocalDateTime { get; set; }
    }
}

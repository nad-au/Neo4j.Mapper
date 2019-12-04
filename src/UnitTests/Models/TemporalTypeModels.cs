using System;
using Neo4j.Driver.V1;

namespace UnitTests.Models
{
    public class EntityWithDriverTypes
    {
        public LocalDate DateValue { get; set; }
        public LocalDate NullableDateValue { get; set; }
        public LocalTime TimeValue { get; set; }
        public LocalTime NullableTimeValue { get; set; }
        public ZonedDateTime DateTimeOffsetValue { get; set; }
        public ZonedDateTime NullableDateTimeOffsetValue { get; set; }
        public LocalDateTime DateTimeValue { get; set; }
        public LocalDateTime NullableDateTimeValue { get; set; }
    }

    public class EntityWithClrTypes
    {
        public DateTime DateValue { get; set; }
        public DateTime? NullableDateValue { get; set; }
        public TimeSpan TimeValue { get; set; }
        public TimeSpan? NullableTimeValue { get; set; }
        public DateTimeOffset DateTimeOffsetValue { get; set; }
        public DateTimeOffset? NullableDateTimeOffsetValue { get; set; }
        public DateTime DateTimeValue { get; set; }
        public DateTime? NullableDateTimeValue { get; set; }
    }
}

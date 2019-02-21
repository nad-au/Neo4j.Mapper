using System;
using Neo4j.Driver.V1;
using ServiceStack;

namespace Neo4jMapper
{
    // ReSharper disable once InconsistentNaming
    public static class Neo4jMapperConfig
    {
        public static void RegisterTypeConverters()
        {
            RegisterLocalDateToDateTimeConverter();
            RegisterLocalTimeToTimeSpanConverter();
            RegisterZonedDateTimeToDateTimeOffsetConverter();
            RegisterLocalDateTimeToDateTimeConverter();
        }

        public static void RegisterLocalDateToDateTimeConverter()
        {
            AutoMapping.RegisterConverter((LocalDate localDate) => localDate.ToDateTime());
            AutoMapping.RegisterConverter((DateTime dateTime) => new LocalDate(dateTime));
        }

        public static void RegisterLocalTimeToTimeSpanConverter()
        {
            AutoMapping.RegisterConverter((LocalTime localTime) => localTime.ToTimeSpan());
            AutoMapping.RegisterConverter((TimeSpan timeSpan) => new LocalTime(timeSpan));
        }

        public static void RegisterZonedDateTimeToDateTimeOffsetConverter()
        {
            AutoMapping.RegisterConverter((ZonedDateTime zonedDateTime) => zonedDateTime.ToDateTimeOffset());
            AutoMapping.RegisterConverter((DateTimeOffset dateTimeOffset) => new ZonedDateTime(dateTimeOffset));
        }

        public static void RegisterLocalDateTimeToDateTimeConverter()
        {
            AutoMapping.RegisterConverter((LocalDateTime localDateTime) => localDateTime.ToDateTime());
            AutoMapping.RegisterConverter((DateTime dateTime) => new LocalDateTime(dateTime));
        }
    }
}

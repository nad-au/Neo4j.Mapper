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
            AutoMappingUtils.RegisterConverter((LocalDate localDate) => localDate.ToDateTime());
            AutoMappingUtils.RegisterConverter((DateTime dateTime) => new LocalDate(dateTime));
        }

        public static void RegisterLocalTimeToTimeSpanConverter()
        {
            AutoMappingUtils.RegisterConverter((LocalTime localTime) => localTime.ToTimeSpan());
            AutoMappingUtils.RegisterConverter((TimeSpan timeSpan) => new LocalTime(timeSpan));
        }

        public static void RegisterZonedDateTimeToDateTimeOffsetConverter()
        {
            AutoMappingUtils.RegisterConverter((ZonedDateTime zonedDateTime) => zonedDateTime.ToDateTimeOffset());
            AutoMappingUtils.RegisterConverter((DateTimeOffset dateTimeOffset) => new ZonedDateTime(dateTimeOffset));
        }

        public static void RegisterLocalDateTimeToDateTimeConverter()
        {
            AutoMappingUtils.RegisterConverter((LocalDateTime localDateTime) => localDateTime.ToDateTime());
            AutoMappingUtils.RegisterConverter((DateTime dateTime) => new LocalDateTime(dateTime));
        }
    }
}

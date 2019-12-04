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
            AutoMapping.RegisterConverter<LocalDate, DateTime>(localDate => localDate?.ToDateTime() ?? default);
            AutoMapping.RegisterConverter<LocalDate, DateTime?>(localDateTime => localDateTime?.ToDateTime());
            AutoMapping.RegisterConverter<DateTime, LocalDate>(dateTime => new LocalDate(dateTime));
            AutoMapping.RegisterConverter<DateTime?, LocalDate>(dateTime => dateTime.HasValue ? new LocalDate(dateTime.Value) : null);
        }

        public static void RegisterLocalTimeToTimeSpanConverter()
        {
            AutoMapping.RegisterConverter<LocalTime, TimeSpan>(localTime => localTime?.ToTimeSpan() ?? default);
            AutoMapping.RegisterConverter<LocalTime, TimeSpan?>(localTime => localTime?.ToTimeSpan());
            AutoMapping.RegisterConverter<TimeSpan, LocalTime>(timeSpan => new LocalTime(timeSpan));
            AutoMapping.RegisterConverter<TimeSpan?, LocalTime>(timeSpan => timeSpan.HasValue ? new LocalTime(timeSpan.Value) : null);
        }

        public static void RegisterZonedDateTimeToDateTimeOffsetConverter()
        {
            AutoMapping.RegisterConverter<ZonedDateTime, DateTimeOffset>(zonedDateTime => zonedDateTime?.ToDateTimeOffset() ?? default);
            AutoMapping.RegisterConverter<ZonedDateTime, DateTimeOffset?>(zonedDateTime => zonedDateTime?.ToDateTimeOffset());
            AutoMapping.RegisterConverter<DateTimeOffset, ZonedDateTime>(dateTimeOffset => new ZonedDateTime(dateTimeOffset));
            AutoMapping.RegisterConverter<DateTimeOffset?, ZonedDateTime>(dateTimeOffset => dateTimeOffset.HasValue ? new ZonedDateTime(dateTimeOffset.Value) : null);
        }

        public static void RegisterLocalDateTimeToDateTimeConverter()
        {
            AutoMapping.RegisterConverter<LocalDateTime, DateTime>(localDateTime => localDateTime?.ToDateTime() ?? default);
            AutoMapping.RegisterConverter<LocalDateTime, DateTime?>(localDateTime => localDateTime?.ToDateTime());
            AutoMapping.RegisterConverter<DateTime, LocalDateTime>(dateTime => new LocalDateTime(dateTime));
            AutoMapping.RegisterConverter<DateTime?, LocalDateTime>(dateTime => dateTime.HasValue ? new LocalDateTime(dateTime.Value) : null);
        }
    }
}

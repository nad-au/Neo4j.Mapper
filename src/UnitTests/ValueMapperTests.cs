using System;
using System.Collections.Generic;
using FluentAssertions;
using Neo4j.Driver;
using Neo4j.Mapper;
using NUnit.Framework;
using ServiceStack;
using UnitTests.Models;

namespace UnitTests
{
    [TestFixture]
    public class ValueMapperTests
    {
        [Test]
        public void Should_Throw_Exception_When_Return_Type_Is_Enumerable_But_Cypher_Value_Is_Not_Enumerable()
        {
            var result = Assert.Throws<InvalidOperationException>(
                () => ValueMapper.MapValue<IEnumerable<string>>(1));

            result.Message.Should().Be(
                "The cypher value is not a list and cannot be mapped to target type: System.Collections.Generic.IEnumerable`1[System.String]");
        }

        [Test]
        public void Should_Throw_Exception_When_Cypher_Value_Is_Enumerable_But_Return_Type_Is_Not_Enumerable()
        {
            var result = Assert.Throws<InvalidOperationException>(
                () => ValueMapper.MapValue<int>(new[] { 1 }));

            result.Message.Should().Be(
                "The cypher value is a list and cannot be mapped to target type: System.Int32");
        }

        [Test]
        public void Should_Convert_LocalDate_To_DateTime()
        {
            MapperConfig.RegisterLocalDateToDateTimeConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.DateValue), new LocalDate(2019, 1, 12)}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            entity.DateValue.Year.Should().Be(2019);
            entity.DateValue.Month.Should().Be(1);
            entity.DateValue.Day.Should().Be(12);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Null_LocalDate_To_DateTime_Default()
        {
            MapperConfig.RegisterLocalDateToDateTimeConverter();
            LocalDate localDate = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.DateValue), localDate}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            entity.DateValue.Should().Be(default);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_LocalDate_To_Nullable_DateTime()
        {
            MapperConfig.RegisterLocalDateToDateTimeConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateValue), new LocalDate(2019, 1, 12)}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            entity.NullableDateValue.Should().NotBeNull();
            entity.NullableDateValue.Value.Year.Should().Be(2019);
            entity.NullableDateValue.Value.Month.Should().Be(1);
            entity.NullableDateValue.Value.Day.Should().Be(12);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_LocalDate_To_Nullable_DateTime_As_Null()
        {
            MapperConfig.RegisterLocalDateToDateTimeConverter();
            LocalDate localDate = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateValue), localDate}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            entity.NullableDateValue.Should().BeNull();

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_DateTime_To_LocalDate()
        {
            MapperConfig.RegisterLocalDateToDateTimeConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.DateValue), new DateTime(2019, 1, 12, 8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            entity.DateValue.Year.Should().Be(2019);
            entity.DateValue.Month.Should().Be(1);
            entity.DateValue.Day.Should().Be(12);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Nullable_DateTime_To_LocalDate()
        {
            MapperConfig.RegisterLocalDateToDateTimeConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateValue), new DateTime(2019, 1, 12, 8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            entity.NullableDateValue.Year.Should().Be(2019);
            entity.NullableDateValue.Month.Should().Be(1);
            entity.NullableDateValue.Day.Should().Be(12);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Nullable_DateTime_To_LocalDate_As_Null()
        {
            MapperConfig.RegisterLocalDateToDateTimeConverter();
            DateTime? dateTime = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateValue), dateTime}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            entity.NullableDateValue.Should().BeNull();

            AutoMappingUtils.Reset();
        }
        
        [Test]
        public void Should_Convert_LocalTime_To_TimeSpan()
        {
            MapperConfig.RegisterLocalTimeToTimeSpanConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.TimeValue), new LocalTime(8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            entity.TimeValue.Hours.Should().Be(8);
            entity.TimeValue.Minutes.Should().Be(45);
            entity.TimeValue.Seconds.Should().Be(34);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Null_LocalTime_To_TimeSpan_Default()
        {
            MapperConfig.RegisterLocalTimeToTimeSpanConverter();
            LocalTime localTime = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.TimeValue), localTime}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            entity.TimeValue.Should().Be(default);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_LocalTime_To_Nullable_TimeSpan()
        {
            MapperConfig.RegisterLocalTimeToTimeSpanConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableTimeValue), new LocalTime(8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            entity.NullableTimeValue.Should().NotBeNull();
            entity.NullableTimeValue.Value.Hours.Should().Be(8);
            entity.NullableTimeValue.Value.Minutes.Should().Be(45);
            entity.NullableTimeValue.Value.Seconds.Should().Be(34);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_LocalTime_To_Nullable_TimeSpan_As_Null()
        {
            MapperConfig.RegisterLocalTimeToTimeSpanConverter();
            LocalTime localTime = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableTimeValue), localTime}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            entity.NullableTimeValue.Should().BeNull();

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_TimeSpan_To_LocalTime()
        {
            MapperConfig.RegisterLocalTimeToTimeSpanConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.TimeValue), new TimeSpan(8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            entity.TimeValue.Hour.Should().Be(8);
            entity.TimeValue.Minute.Should().Be(45);
            entity.TimeValue.Second.Should().Be(34);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Nullable_TimeSpan_To_LocalTime()
        {
            MapperConfig.RegisterLocalTimeToTimeSpanConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableTimeValue), new TimeSpan(8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            entity.NullableTimeValue.Hour.Should().Be(8);
            entity.NullableTimeValue.Minute.Should().Be(45);
            entity.NullableTimeValue.Second.Should().Be(34);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Nullable_TimeSpan_To_LocalTime_As_Null()
        {
            MapperConfig.RegisterLocalTimeToTimeSpanConverter();
            TimeSpan? timeSpan = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableTimeValue), timeSpan}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            entity.NullableTimeValue.Should().BeNull();

            AutoMappingUtils.Reset();
        }
        
        [Test]
        public void Should_Convert_ZonedDateTime_To_DateTimeOffset()
        {
            MapperConfig.RegisterZonedDateTimeToDateTimeOffsetConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.DateTimeOffsetValue), new ZonedDateTime(2019, 1, 12, 8, 45, 34, Zone.Of(0))}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            entity.DateTimeOffsetValue.Year.Should().Be(2019);
            entity.DateTimeOffsetValue.Month.Should().Be(1);
            entity.DateTimeOffsetValue.Day.Should().Be(12);
            entity.DateTimeOffsetValue.Hour.Should().Be(8);
            entity.DateTimeOffsetValue.Minute.Should().Be(45);
            entity.DateTimeOffsetValue.Second.Should().Be(34);
            entity.DateTimeOffsetValue.Offset.Seconds.Should().Be(0);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Null_ZonedDateTime_To_DateTimeOffset_Default()
        {
            MapperConfig.RegisterZonedDateTimeToDateTimeOffsetConverter();
            ZonedDateTime zonedDateTime = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.DateTimeOffsetValue), zonedDateTime}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            entity.DateTimeOffsetValue.Should().Be(default);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_ZonedDateTime_To_Nullable_DateTimeOffset()
        {
            MapperConfig.RegisterZonedDateTimeToDateTimeOffsetConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateTimeOffsetValue), new ZonedDateTime(2019, 1, 12, 8, 45, 34, Zone.Of(0))}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            entity.NullableDateTimeOffsetValue.Should().NotBeNull();
            entity.NullableDateTimeOffsetValue.Value.Year.Should().Be(2019);
            entity.NullableDateTimeOffsetValue.Value.Month.Should().Be(1);
            entity.NullableDateTimeOffsetValue.Value.Day.Should().Be(12);
            entity.NullableDateTimeOffsetValue.Value.Hour.Should().Be(8);
            entity.NullableDateTimeOffsetValue.Value.Minute.Should().Be(45);
            entity.NullableDateTimeOffsetValue.Value.Second.Should().Be(34);
            entity.NullableDateTimeOffsetValue.Value.Offset.Seconds.Should().Be(0);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_ZonedDateTime_To_Nullable_DateTimeOffset_As_Null()
        {
            MapperConfig.RegisterZonedDateTimeToDateTimeOffsetConverter();
            ZonedDateTime zonedDateTime = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateTimeOffsetValue), zonedDateTime}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            entity.NullableDateTimeOffsetValue.Should().BeNull();

            AutoMappingUtils.Reset();
        }
        
        [Test]
        public void Should_Convert_DateTimeOffset_To_ZonedDateTime()
        {
            MapperConfig.RegisterZonedDateTimeToDateTimeOffsetConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.DateTimeOffsetValue), new DateTimeOffset(2019, 1, 12, 8, 45, 34, TimeSpan.Zero)}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            entity.DateTimeOffsetValue.Year.Should().Be(2019);
            entity.DateTimeOffsetValue.Month.Should().Be(1);
            entity.DateTimeOffsetValue.Day.Should().Be(12);
            entity.DateTimeOffsetValue.Hour.Should().Be(8);
            entity.DateTimeOffsetValue.Minute.Should().Be(45);
            entity.DateTimeOffsetValue.Second.Should().Be(34);
            entity.DateTimeOffsetValue.Zone.Should().Be(Zone.Of(0));

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Nullable_DateTimeOffset_To_ZonedDateTime()
        {
            MapperConfig.RegisterZonedDateTimeToDateTimeOffsetConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateTimeOffsetValue), new DateTimeOffset(2019, 1, 12, 8, 45, 34, TimeSpan.Zero)}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            entity.NullableDateTimeOffsetValue.Year.Should().Be(2019);
            entity.NullableDateTimeOffsetValue.Month.Should().Be(1);
            entity.NullableDateTimeOffsetValue.Day.Should().Be(12);
            entity.NullableDateTimeOffsetValue.Hour.Should().Be(8);
            entity.NullableDateTimeOffsetValue.Minute.Should().Be(45);
            entity.NullableDateTimeOffsetValue.Second.Should().Be(34);
            entity.NullableDateTimeOffsetValue.Zone.Should().Be(Zone.Of(0));

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Nullable_DateTimeOffset_To_ZonedDateTime_As_Null()
        {
            MapperConfig.RegisterZonedDateTimeToDateTimeOffsetConverter();
            DateTimeOffset? dateTimeOffset = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateTimeOffsetValue), dateTimeOffset}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            entity.NullableDateTimeOffsetValue.Should().BeNull();

            AutoMappingUtils.Reset();
        }
        
        [Test]
        public void Should_Convert_LocalDateTime_To_DateTime()
        {
            MapperConfig.RegisterLocalDateTimeToDateTimeConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.DateTimeValue), new LocalDateTime(2019, 1, 12, 8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            entity.DateTimeValue.Year.Should().Be(2019);
            entity.DateTimeValue.Month.Should().Be(1);
            entity.DateTimeValue.Day.Should().Be(12);
            entity.DateTimeValue.Hour.Should().Be(8);
            entity.DateTimeValue.Minute.Should().Be(45);
            entity.DateTimeValue.Second.Should().Be(34);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_LocalDateTime_To_DateTime_Default()
        {
            MapperConfig.RegisterLocalDateTimeToDateTimeConverter();
            LocalDateTime localDateTime = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.DateTimeValue), localDateTime}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            entity.DateTimeValue.Should().Be(default);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_LocalDateTime_To_Nullable_DateTime()
        {
            MapperConfig.RegisterLocalDateTimeToDateTimeConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateTimeValue), new LocalDateTime(2019, 1, 12, 8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            entity.NullableDateTimeValue.Should().NotBeNull();
            entity.NullableDateTimeValue.Value.Year.Should().Be(2019);
            entity.NullableDateTimeValue.Value.Month.Should().Be(1);
            entity.NullableDateTimeValue.Value.Day.Should().Be(12);
            entity.NullableDateTimeValue.Value.Hour.Should().Be(8);
            entity.NullableDateTimeValue.Value.Minute.Should().Be(45);
            entity.NullableDateTimeValue.Value.Second.Should().Be(34);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_LocalDateTime_To_Nullable_DateTime_As_Null()
        {
            MapperConfig.RegisterLocalDateTimeToDateTimeConverter();
            LocalDateTime localDateTime = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateTimeValue), localDateTime}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            entity.NullableDateTimeValue.Should().BeNull();

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_DateTime_To_LocalDateTime()
        {
            MapperConfig.RegisterLocalDateTimeToDateTimeConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.DateTimeValue), new DateTime(2019, 1, 12, 8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            entity.DateTimeValue.Year.Should().Be(2019);
            entity.DateTimeValue.Month.Should().Be(1);
            entity.DateTimeValue.Day.Should().Be(12);
            entity.DateTimeValue.Hour.Should().Be(8);
            entity.DateTimeValue.Minute.Should().Be(45);
            entity.DateTimeValue.Second.Should().Be(34);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Nullable_DateTime_To_LocalDateTime()
        {
            MapperConfig.RegisterLocalDateTimeToDateTimeConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateTimeValue), new DateTime(2019, 1, 12, 8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            entity.NullableDateTimeValue.Year.Should().Be(2019);
            entity.NullableDateTimeValue.Month.Should().Be(1);
            entity.NullableDateTimeValue.Day.Should().Be(12);
            entity.NullableDateTimeValue.Hour.Should().Be(8);
            entity.NullableDateTimeValue.Minute.Should().Be(45);
            entity.NullableDateTimeValue.Second.Should().Be(34);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Nullable_DateTime_To_LocalDateTime_As_Null()
        {
            MapperConfig.RegisterLocalDateTimeToDateTimeConverter();
            DateTime? dateTime = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateTimeValue), dateTime}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            entity.NullableDateTimeValue.Should().BeNull();

            AutoMappingUtils.Reset();
        }
    }
}

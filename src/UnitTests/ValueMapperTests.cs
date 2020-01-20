using System;
using System.Collections.Generic;
using Neo4j.Driver;
using Neo4jMapper;
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

            Assert.AreEqual("The cypher value is not a list and cannot be mapped to target type: System.Collections.Generic.IEnumerable`1[System.String]",
                result.Message);
        }

        [Test]
        public void Should_Throw_Exception_When_Cypher_Value_Is_Enumerable_But_Return_Type_Is_Not_Enumerable()
        {
            var result = Assert.Throws<InvalidOperationException>(
                () => ValueMapper.MapValue<int>(new[] { 1 }));

            Assert.AreEqual("The cypher value is a list and cannot be mapped to target type: System.Int32",
                result.Message);
        }

        [Test]
        public void Should_Convert_LocalDate_To_DateTime()
        {
            Neo4jMapperConfig.RegisterLocalDateToDateTimeConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.DateValue), new LocalDate(2019, 1, 12)}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            Assert.AreEqual(2019, entity.DateValue.Year);
            Assert.AreEqual(1, entity.DateValue.Month);
            Assert.AreEqual(12, entity.DateValue.Day);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Null_LocalDate_To_DateTime_Default()
        {
            Neo4jMapperConfig.RegisterLocalDateToDateTimeConverter();
            LocalDate localDate = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.DateValue), localDate}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            Assert.AreEqual((DateTime)default, entity.DateValue);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_LocalDate_To_Nullable_DateTime()
        {
            Neo4jMapperConfig.RegisterLocalDateToDateTimeConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateValue), new LocalDate(2019, 1, 12)}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            Assert.IsNotNull(entity.NullableDateValue);
            Assert.AreEqual(2019, entity.NullableDateValue.Value.Year);
            Assert.AreEqual(1, entity.NullableDateValue.Value.Month);
            Assert.AreEqual(12, entity.NullableDateValue.Value.Day);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_LocalDate_To_Nullable_DateTime_As_Null()
        {
            Neo4jMapperConfig.RegisterLocalDateToDateTimeConverter();
            LocalDate localDate = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateValue), localDate}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            Assert.IsNull(entity.NullableDateValue);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_DateTime_To_LocalDate()
        {
            Neo4jMapperConfig.RegisterLocalDateToDateTimeConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.DateValue), new DateTime(2019, 1, 12, 8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            Assert.AreEqual(2019, entity.DateValue.Year);
            Assert.AreEqual(1, entity.DateValue.Month);
            Assert.AreEqual(12, entity.DateValue.Day);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Nullable_DateTime_To_LocalDate()
        {
            Neo4jMapperConfig.RegisterLocalDateToDateTimeConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateValue), new DateTime(2019, 1, 12, 8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            Assert.AreEqual(2019, entity.NullableDateValue.Year);
            Assert.AreEqual(2019, entity.NullableDateValue.Year);
            Assert.AreEqual(1, entity.NullableDateValue.Month);
            Assert.AreEqual(12, entity.NullableDateValue.Day);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Nullable_DateTime_To_LocalDate_As_Null()
        {
            Neo4jMapperConfig.RegisterLocalDateToDateTimeConverter();
            DateTime? dateTime = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateValue), dateTime}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            Assert.IsNull(entity.NullableDateValue);

            AutoMappingUtils.Reset();
        }
        
        [Test]
        public void Should_Convert_LocalTime_To_TimeSpan()
        {
            Neo4jMapperConfig.RegisterLocalTimeToTimeSpanConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.TimeValue), new LocalTime(8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            Assert.AreEqual(8, entity.TimeValue.Hours);
            Assert.AreEqual(45, entity.TimeValue.Minutes);
            Assert.AreEqual(34, entity.TimeValue.Seconds);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Null_LocalTime_To_TimeSpan_Default()
        {
            Neo4jMapperConfig.RegisterLocalTimeToTimeSpanConverter();
            LocalTime localTime = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.TimeValue), localTime}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            Assert.AreEqual((TimeSpan)default, entity.TimeValue);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_LocalTime_To_Nullable_TimeSpan()
        {
            Neo4jMapperConfig.RegisterLocalTimeToTimeSpanConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableTimeValue), new LocalTime(8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            Assert.IsNotNull(entity.NullableTimeValue);
            Assert.AreEqual(8, entity.NullableTimeValue.Value.Hours);
            Assert.AreEqual(45, entity.NullableTimeValue.Value.Minutes);
            Assert.AreEqual(34, entity.NullableTimeValue.Value.Seconds);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_LocalTime_To_Nullable_TimeSpan_As_Null()
        {
            Neo4jMapperConfig.RegisterLocalTimeToTimeSpanConverter();
            LocalTime localTime = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableTimeValue), localTime}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            Assert.IsNull(entity.NullableTimeValue);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_TimeSpan_To_LocalTime()
        {
            Neo4jMapperConfig.RegisterLocalTimeToTimeSpanConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.TimeValue), new TimeSpan(8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            Assert.AreEqual(8, entity.TimeValue.Hour);
            Assert.AreEqual(45, entity.TimeValue.Minute);
            Assert.AreEqual(34, entity.TimeValue.Second);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Nullable_TimeSpan_To_LocalTime()
        {
            Neo4jMapperConfig.RegisterLocalTimeToTimeSpanConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableTimeValue), new TimeSpan(8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            Assert.AreEqual(8, entity.NullableTimeValue.Hour);
            Assert.AreEqual(45, entity.NullableTimeValue.Minute);
            Assert.AreEqual(34, entity.NullableTimeValue.Second);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Nullable_TimeSpan_To_LocalTime_As_Null()
        {
            Neo4jMapperConfig.RegisterLocalTimeToTimeSpanConverter();
            TimeSpan? timeSpan = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableTimeValue), timeSpan}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            Assert.IsNull(entity.NullableTimeValue);

            AutoMappingUtils.Reset();
        }
        
        [Test]
        public void Should_Convert_ZonedDateTime_To_DateTimeOffset()
        {
            Neo4jMapperConfig.RegisterZonedDateTimeToDateTimeOffsetConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.DateTimeOffsetValue), new ZonedDateTime(2019, 1, 12, 8, 45, 34, Zone.Of(0))}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            Assert.AreEqual(2019, entity.DateTimeOffsetValue.Year);
            Assert.AreEqual(1, entity.DateTimeOffsetValue.Month);
            Assert.AreEqual(12, entity.DateTimeOffsetValue.Day);
            Assert.AreEqual(8, entity.DateTimeOffsetValue.Hour);
            Assert.AreEqual(45, entity.DateTimeOffsetValue.Minute);
            Assert.AreEqual(34, entity.DateTimeOffsetValue.Second);
            Assert.AreEqual(0, entity.DateTimeOffsetValue.Offset.Seconds);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Null_ZonedDateTime_To_DateTimeOffset_Default()
        {
            Neo4jMapperConfig.RegisterZonedDateTimeToDateTimeOffsetConverter();
            ZonedDateTime zonedDateTime = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.DateTimeOffsetValue), zonedDateTime}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            Assert.AreEqual((DateTimeOffset)default, entity.DateTimeOffsetValue);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_ZonedDateTime_To_Nullable_DateTimeOffset()
        {
            Neo4jMapperConfig.RegisterZonedDateTimeToDateTimeOffsetConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateTimeOffsetValue), new ZonedDateTime(2019, 1, 12, 8, 45, 34, Zone.Of(0))}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            Assert.IsNotNull(entity.NullableDateTimeOffsetValue);
            Assert.AreEqual(2019, entity.NullableDateTimeOffsetValue.Value.Year);
            Assert.AreEqual(1, entity.NullableDateTimeOffsetValue.Value.Month);
            Assert.AreEqual(12, entity.NullableDateTimeOffsetValue.Value.Day);
            Assert.AreEqual(8, entity.NullableDateTimeOffsetValue.Value.Hour);
            Assert.AreEqual(45, entity.NullableDateTimeOffsetValue.Value.Minute);
            Assert.AreEqual(34, entity.NullableDateTimeOffsetValue.Value.Second);
            Assert.AreEqual(0, entity.NullableDateTimeOffsetValue.Value.Offset.Seconds);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_ZonedDateTime_To_Nullable_DateTimeOffset_As_Null()
        {
            Neo4jMapperConfig.RegisterZonedDateTimeToDateTimeOffsetConverter();
            ZonedDateTime zonedDateTime = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateTimeOffsetValue), zonedDateTime}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            Assert.IsNull(entity.NullableDateTimeOffsetValue);

            AutoMappingUtils.Reset();
        }
        
        [Test]
        public void Should_Convert_DateTimeOffset_To_ZonedDateTime()
        {
            Neo4jMapperConfig.RegisterZonedDateTimeToDateTimeOffsetConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.DateTimeOffsetValue), new DateTimeOffset(2019, 1, 12, 8, 45, 34, TimeSpan.Zero)}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            Assert.AreEqual(2019, entity.DateTimeOffsetValue.Year);
            Assert.AreEqual(1, entity.DateTimeOffsetValue.Month);
            Assert.AreEqual(12, entity.DateTimeOffsetValue.Day);
            Assert.AreEqual(8, entity.DateTimeOffsetValue.Hour);
            Assert.AreEqual(45, entity.DateTimeOffsetValue.Minute);
            Assert.AreEqual(34, entity.DateTimeOffsetValue.Second);
            Assert.AreEqual(Zone.Of(0), entity.DateTimeOffsetValue.Zone);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Nullable_DateTimeOffset_To_ZonedDateTime()
        {
            Neo4jMapperConfig.RegisterZonedDateTimeToDateTimeOffsetConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateTimeOffsetValue), new DateTimeOffset(2019, 1, 12, 8, 45, 34, TimeSpan.Zero)}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            Assert.AreEqual(2019, entity.NullableDateTimeOffsetValue.Year);
            Assert.AreEqual(1, entity.NullableDateTimeOffsetValue.Month);
            Assert.AreEqual(12, entity.NullableDateTimeOffsetValue.Day);
            Assert.AreEqual(8, entity.NullableDateTimeOffsetValue.Hour);
            Assert.AreEqual(45, entity.NullableDateTimeOffsetValue.Minute);
            Assert.AreEqual(34, entity.NullableDateTimeOffsetValue.Second);
            Assert.AreEqual(Zone.Of(0), entity.NullableDateTimeOffsetValue.Zone);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Nullable_DateTimeOffset_To_ZonedDateTime_As_Null()
        {
            Neo4jMapperConfig.RegisterZonedDateTimeToDateTimeOffsetConverter();
            DateTimeOffset? dateTimeOffset = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateTimeOffsetValue), dateTimeOffset}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            Assert.IsNull(entity.NullableDateTimeOffsetValue);

            AutoMappingUtils.Reset();
        }
        
        [Test]
        public void Should_Convert_LocalDateTime_To_DateTime()
        {
            Neo4jMapperConfig.RegisterLocalDateTimeToDateTimeConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.DateTimeValue), new LocalDateTime(2019, 1, 12, 8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            Assert.AreEqual(2019, entity.DateTimeValue.Year);
            Assert.AreEqual(1, entity.DateTimeValue.Month);
            Assert.AreEqual(12, entity.DateTimeValue.Day);
            Assert.AreEqual(8, entity.DateTimeValue.Hour);
            Assert.AreEqual(45, entity.DateTimeValue.Minute);
            Assert.AreEqual(34, entity.DateTimeValue.Second);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_LocalDateTime_To_DateTime_Default()
        {
            Neo4jMapperConfig.RegisterLocalDateTimeToDateTimeConverter();
            LocalDateTime localDateTime = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.DateTimeValue), localDateTime}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            Assert.AreEqual((DateTime)default, entity.DateTimeValue);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_LocalDateTime_To_Nullable_DateTime()
        {
            Neo4jMapperConfig.RegisterLocalDateTimeToDateTimeConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateTimeValue), new LocalDateTime(2019, 1, 12, 8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            Assert.IsNotNull(entity.NullableDateTimeValue);
            Assert.AreEqual(2019, entity.NullableDateTimeValue.Value.Year);
            Assert.AreEqual(1, entity.NullableDateTimeValue.Value.Month);
            Assert.AreEqual(12, entity.NullableDateTimeValue.Value.Day);
            Assert.AreEqual(8, entity.NullableDateTimeValue.Value.Hour);
            Assert.AreEqual(45, entity.NullableDateTimeValue.Value.Minute);
            Assert.AreEqual(34, entity.NullableDateTimeValue.Value.Second);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_LocalDateTime_To_Nullable_DateTime_As_Null()
        {
            Neo4jMapperConfig.RegisterLocalDateTimeToDateTimeConverter();
            LocalDateTime localDateTime = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateTimeValue), localDateTime}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            Assert.IsNull(entity.NullableDateTimeValue);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_DateTime_To_LocalDateTime()
        {
            Neo4jMapperConfig.RegisterLocalDateTimeToDateTimeConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.DateTimeValue), new DateTime(2019, 1, 12, 8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            Assert.AreEqual(2019, entity.DateTimeValue.Year);
            Assert.AreEqual(1, entity.DateTimeValue.Month);
            Assert.AreEqual(12, entity.DateTimeValue.Day);
            Assert.AreEqual(8, entity.DateTimeValue.Hour);
            Assert.AreEqual(45, entity.DateTimeValue.Minute);
            Assert.AreEqual(34, entity.DateTimeValue.Second);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Nullable_DateTime_To_LocalDateTime()
        {
            Neo4jMapperConfig.RegisterLocalDateTimeToDateTimeConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateTimeValue), new DateTime(2019, 1, 12, 8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            Assert.AreEqual(2019, entity.NullableDateTimeValue.Year);
            Assert.AreEqual(1, entity.NullableDateTimeValue.Month);
            Assert.AreEqual(12, entity.NullableDateTimeValue.Day);
            Assert.AreEqual(8, entity.NullableDateTimeValue.Hour);
            Assert.AreEqual(45, entity.NullableDateTimeValue.Minute);
            Assert.AreEqual(34, entity.NullableDateTimeValue.Second);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_Nullable_DateTime_To_LocalDateTime_As_Null()
        {
            Neo4jMapperConfig.RegisterLocalDateTimeToDateTimeConverter();
            DateTime? dateTime = null;

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.NullableDateTimeValue), dateTime}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            Assert.IsNull(entity.NullableDateTimeValue);

            AutoMappingUtils.Reset();
        }
    }
}

using System;
using System.Collections.Generic;
using Neo4j.Driver.V1;
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
        public void Should_Convert_LocalDateTime_To_DateTime()
        {
            Neo4jMapperConfig.RegisterLocalDateTimeToDateTimeConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.LocalDateTime), new LocalDateTime(2019, 1, 12, 8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithClrTypes>(map);

            Assert.AreEqual(2019, entity.LocalDateTime.Year);
            Assert.AreEqual(1, entity.LocalDateTime.Month);
            Assert.AreEqual(12, entity.LocalDateTime.Day);
            Assert.AreEqual(8, entity.LocalDateTime.Hour);
            Assert.AreEqual(45, entity.LocalDateTime.Minute);
            Assert.AreEqual(34, entity.LocalDateTime.Second);

            AutoMappingUtils.Reset();
        }

        [Test]
        public void Should_Convert_DateTime_To_LocalDateTime()
        {
            Neo4jMapperConfig.RegisterLocalDateTimeToDateTimeConverter();

            var map = new Dictionary<string, object>
            {
                {nameof(EntityWithClrTypes.LocalDateTime), new DateTime(2019, 1, 12, 8, 45, 34)}
            };

            var entity = ValueMapper.MapValue<EntityWithDriverTypes>(map);

            Assert.AreEqual(2019, entity.LocalDateTime.Year);
            Assert.AreEqual(1, entity.LocalDateTime.Month);
            Assert.AreEqual(12, entity.LocalDateTime.Day);
            Assert.AreEqual(8, entity.LocalDateTime.Hour);
            Assert.AreEqual(45, entity.LocalDateTime.Minute);
            Assert.AreEqual(34, entity.LocalDateTime.Second);

            AutoMappingUtils.Reset();
        }
    }
}

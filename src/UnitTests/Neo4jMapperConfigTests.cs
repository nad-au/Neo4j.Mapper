using System;
using FluentAssertions;
using Neo4j.Driver;
using Neo4jMapper;
using NUnit.Framework;
using ServiceStack;

namespace UnitTests
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public class Neo4jMapperConfigTests
    {
        [Test]
        public void RegisterTypeConverters_Should_Register_All_Converters()
        {
            Neo4jMapperConfig.RegisterTypeConverters();

            AssertLocalDateConvertersAreRegistered();
            AssertLocalTimeConvertersAreRegistered();
            AssertZonedDateTimeConvertersAreRegistered();
            AssertLocalDateTimeConvertersAreRegistered();

            AutoMappingUtils.Reset();
        }

        [Test]
        public void RegisterLocalDateToDateTimeConverter_Should_Register_Converters()
        {
            Neo4jMapperConfig.RegisterLocalDateToDateTimeConverter();

            AssertLocalDateConvertersAreRegistered();

            AutoMappingUtils.Reset();
        }

        [Test]
        public void RegisterLocalTimeToTimeSpanConverter_Should_Register_Converters()
        {
            Neo4jMapperConfig.RegisterLocalTimeToTimeSpanConverter();

            AssertLocalTimeConvertersAreRegistered();

            AutoMappingUtils.Reset();
        }

        [Test]
        public void RegisterZonedDateTimeToDateTimeOffsetConverter_Should_Register_Converters()
        {
            Neo4jMapperConfig.RegisterZonedDateTimeToDateTimeOffsetConverter();

            AssertZonedDateTimeConvertersAreRegistered();

            AutoMappingUtils.Reset();
        }
        
        [Test]
        public void RegisterLocalDateTimeToDateTimeConverter_Should_Register_Converters()
        {
            Neo4jMapperConfig.RegisterLocalDateTimeToDateTimeConverter();

            AssertLocalDateTimeConvertersAreRegistered();

            AutoMappingUtils.Reset();
        }
        
        private void AssertLocalDateConvertersAreRegistered()
        {
            var localDateToDateTimeConverter = AutoMappingUtils.GetConverter(typeof(LocalDate), typeof(DateTime));
            localDateToDateTimeConverter.Should().NotBeNull();

            var localDateToNullableDateTimeConverter = AutoMappingUtils.GetConverter(typeof(LocalDate), typeof(DateTime?));
            localDateToNullableDateTimeConverter.Should().NotBeNull();

            var dateTimeToLocalDateTimeConverter = AutoMappingUtils.GetConverter(typeof(DateTime), typeof(LocalDate));
            dateTimeToLocalDateTimeConverter.Should().NotBeNull();
            
            var nullableDateTimeToLocalDateTimeConverter = AutoMappingUtils.GetConverter(typeof(DateTime?), typeof(LocalDate));
            nullableDateTimeToLocalDateTimeConverter.Should().NotBeNull();
        }

        private void AssertLocalTimeConvertersAreRegistered()
        {
            var localTimeToTimeSpanConverter = AutoMappingUtils.GetConverter(typeof(LocalTime), typeof(TimeSpan));
            localTimeToTimeSpanConverter.Should().NotBeNull();

            var localTimeToNullableTimeSpanConverter = AutoMappingUtils.GetConverter(typeof(LocalTime), typeof(TimeSpan?));
            localTimeToNullableTimeSpanConverter.Should().NotBeNull();

            var timeSpanToLocalTimeConverter = AutoMappingUtils.GetConverter(typeof(TimeSpan), typeof(LocalTime));
            timeSpanToLocalTimeConverter.Should().NotBeNull();
            
            var nullableTimeSpanToLocalTimeConverter = AutoMappingUtils.GetConverter(typeof(TimeSpan?), typeof(LocalTime));
            nullableTimeSpanToLocalTimeConverter.Should().NotBeNull();
        }

        private void AssertZonedDateTimeConvertersAreRegistered()
        {
            var zonedDateTimeToDateTimeOffsetConverter = AutoMappingUtils.GetConverter(typeof(ZonedDateTime), typeof(DateTimeOffset));
            zonedDateTimeToDateTimeOffsetConverter.Should().NotBeNull();

            var zonedDateTimeToNullableDateTimeOffsetConverter = AutoMappingUtils.GetConverter(typeof(ZonedDateTime), typeof(DateTimeOffset?));
            zonedDateTimeToNullableDateTimeOffsetConverter.Should().NotBeNull();

            var dateTimeOffsetToZonedDateTimeConverter = AutoMappingUtils.GetConverter(typeof(DateTimeOffset), typeof(ZonedDateTime));
            dateTimeOffsetToZonedDateTimeConverter.Should().NotBeNull();
            
            var nullableDateTimeOffsetToZonedDateTimeConverter = AutoMappingUtils.GetConverter(typeof(DateTimeOffset?), typeof(ZonedDateTime));
            nullableDateTimeOffsetToZonedDateTimeConverter.Should().NotBeNull();
        }

        private void AssertLocalDateTimeConvertersAreRegistered()
        {
            var localDateTimeToDateTimeConverter = AutoMappingUtils.GetConverter(typeof(LocalDateTime), typeof(DateTime));
            localDateTimeToDateTimeConverter.Should().NotBeNull();

            var localDateTimeToNullableDateTimeConverter = AutoMappingUtils.GetConverter(typeof(LocalDateTime), typeof(DateTime?));
            localDateTimeToNullableDateTimeConverter.Should().NotBeNull();

            var dateTimeToLocalDateTimeConverter = AutoMappingUtils.GetConverter(typeof(DateTime), typeof(LocalDateTime));
            dateTimeToLocalDateTimeConverter.Should().NotBeNull();
            
            var nullableDateTimeToLocalDateTimeConverter = AutoMappingUtils.GetConverter(typeof(DateTime?), typeof(LocalDateTime));
            nullableDateTimeToLocalDateTimeConverter.Should().NotBeNull();
        }
    }
}

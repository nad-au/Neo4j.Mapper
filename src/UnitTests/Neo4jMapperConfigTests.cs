using System;
using Neo4j.Driver.V1;
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
            Assert.IsNotNull(localDateToDateTimeConverter);

            var dateTimeToLocalDateTimeConverter = AutoMappingUtils.GetConverter(typeof(DateTime), typeof(LocalDate));
            Assert.IsNotNull(dateTimeToLocalDateTimeConverter);
        }

        private void AssertLocalTimeConvertersAreRegistered()
        {
            var localTimeToTimeSpanConverter = AutoMappingUtils.GetConverter(typeof(LocalTime), typeof(TimeSpan));
            Assert.IsNotNull(localTimeToTimeSpanConverter);

            var timeSpanToLocalTimeConverter = AutoMappingUtils.GetConverter(typeof(TimeSpan), typeof(LocalTime));
            Assert.IsNotNull(timeSpanToLocalTimeConverter);
        }

        private void AssertZonedDateTimeConvertersAreRegistered()
        {
            var zonedDateTimeToDateTimeOffsetConverter = AutoMappingUtils.GetConverter(typeof(ZonedDateTime), typeof(DateTimeOffset));
            Assert.IsNotNull(zonedDateTimeToDateTimeOffsetConverter);

            var dateTimeOffsetToZonedDateTimeOffsetConverter = AutoMappingUtils.GetConverter(typeof(DateTimeOffset), typeof(ZonedDateTime));
            Assert.IsNotNull(dateTimeOffsetToZonedDateTimeOffsetConverter);
        }

        private void AssertLocalDateTimeConvertersAreRegistered()
        {
            var localDateTimeToDateTimeConverter = AutoMappingUtils.GetConverter(typeof(LocalDateTime), typeof(DateTime));
            Assert.IsNotNull(localDateTimeToDateTimeConverter);

            var dateTimeToLocalDateTimeConverter = AutoMappingUtils.GetConverter(typeof(DateTime), typeof(LocalDateTime));
            Assert.IsNotNull(dateTimeToLocalDateTimeConverter);
        }
    }
}

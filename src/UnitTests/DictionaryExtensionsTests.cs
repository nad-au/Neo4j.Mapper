using System;
using Neo4j.Driver.V1;
using Neo4jMapper;
using NUnit.Framework;

namespace UnitTests
{
    // ReSharper disable once InconsistentNaming
    [TestFixture]
    public class DictionaryExtensionsTests
    {
        [Test]
        public void Should_Not_Convert_Value_If_ValueConvert_Is_Not_Implemented()
        {
            Neo4jParameters.ValueConvert = o => o.value;

            var parameters = new Neo4jParameters()
                .WithValue("1", DateTime.Now)
                .WithValue("2", true)
                .WithValue("3", "Read")
                .WithValue("4", 123);

            Assert.AreEqual(4, parameters.Count);
            Assert.IsAssignableFrom<DateTime>(parameters["1"]);
            Assert.IsAssignableFrom<bool>(parameters["2"]);
            Assert.IsAssignableFrom<string>(parameters["3"]);
            Assert.IsAssignableFrom<int>(parameters["4"]);
        }

        public class ModelWithNullableTypes
        {
            public DateTime DateTime1 { get; set; }
            public DateTime? DateTime2 { get; set; }
            public bool Bool1 { get; set; }
            public bool? Bool2 { get; set; }
            public AccessMode AccessMode1 { get; set; }
            public AccessMode? AccessMode2 { get; set; }
            public int Int1 { get; set; }
            public int? Int2 { get; set; }
        }

        [Test]
        public void Should_Convert_Values_When_ValueConvert_Is_Implemented()
        {
            var model = new ModelWithNullableTypes
            {
                DateTime1 = new DateTime(2019, 5, 20, 14, 20, 15),
                DateTime2 = new DateTime(2018, 8, 30, 3, 10, 7),
                Bool1 = true,
                Bool2 = false,
                AccessMode1 = AccessMode.Read,
                AccessMode2 = AccessMode.Write,
                Int1 = 123,
                Int2 = 456
            };

            Neo4jParameters.ValueConvert = kv =>
            {
                var (key, value) = kv;

                if (value is DateTime dateTime)
                {
                    return dateTime.ToString("s");
                }

                if (value is bool b)
                {
                    return b.ToString().ToUpper();
                }

                if (value.GetType().IsEnum)
                {
                    return value.ToString();
                }

                if (value is int i)
                {
                    return i.ToString();
                }

                return value;
            };

            var parameters = new Neo4jParameters()
                .WithValue("1", model.DateTime1)
                .WithValue("2", model.DateTime2)
                .WithValue("3", model.Bool1)
                .WithValue("4", model.Bool2)
                .WithValue("5", model.AccessMode1)
                .WithValue("6", model.AccessMode2)
                .WithValue("7", model.Int1)
                .WithValue("8", model.Int2);

            Assert.AreEqual(8, parameters.Count);
            Assert.IsAssignableFrom<string>(parameters["1"]);
            Assert.AreEqual("2019-05-20T14:20:15", parameters["1"]);
            Assert.IsAssignableFrom<string>(parameters["2"]);
            Assert.AreEqual("2018-08-30T03:10:07", parameters["2"]);
            Assert.IsAssignableFrom<string>(parameters["3"]);
            Assert.AreEqual("TRUE", parameters["3"]);
            Assert.IsAssignableFrom<string>(parameters["4"]);
            Assert.AreEqual("FALSE", parameters["4"]);
            Assert.IsAssignableFrom<string>(parameters["5"]);
            Assert.AreEqual("Read", parameters["5"]);
            Assert.IsAssignableFrom<string>(parameters["6"]);
            Assert.AreEqual("Write", parameters["6"]);
            Assert.IsAssignableFrom<string>(parameters["7"]);
            Assert.AreEqual("123", parameters["7"]);
            Assert.IsAssignableFrom<string>(parameters["8"]);
            Assert.AreEqual("456", parameters["8"]);
        }
    }
}

using System.Collections.Generic;
using System.Runtime.Serialization;
using Neo4j.Driver.V1;
using Neo4jMapper;
using NUnit.Framework;

namespace UnitTests
{
    // ReSharper disable once InconsistentNaming
    [TestFixture]
    public class Neo4jParametersTests
    {
        [Test]
        public void Should_Add_Constructor_Parameters_From_Anonymous_Type_Key_Value_Pairs()
        {
            var parameters = new Neo4jParameters(new
            {
                First = "Foo",
                Second = "Bar",
                Third = 56,
                Fourth = new LocalDate(2019, 1, 13)
            });

            Assert.AreEqual(4, parameters.Count);
            Assert.AreEqual("Foo", parameters["First"]);
            Assert.AreEqual("Bar", parameters["Second"]);
            Assert.AreEqual(56, parameters["Third"]);
            Assert.AreEqual(2019, ((LocalDate)parameters["Fourth"]).Year);
            Assert.AreEqual(1, ((LocalDate)parameters["Fourth"]).Month);
            Assert.AreEqual(13, ((LocalDate)parameters["Fourth"]).Day);
        }

        [Test]
        public void AddParams_Should_Add_Dictionary_Values_From_Anonymous_Type_Key_Value_Pairs()
        {
            var parameters = new Neo4jParameters().WithParams(new
            {
                First = "Foo",
                Second = "Bar",
                Third = 56,
                Fourth = new LocalDate(2019, 1, 13)
            });

            Assert.AreEqual(4, parameters.Count);
            Assert.AreEqual("Foo", parameters["First"]);
            Assert.AreEqual("Bar", parameters["Second"]);
            Assert.AreEqual(56, parameters["Third"]);
            Assert.AreEqual(2019, ((LocalDate)parameters["Fourth"]).Year);
            Assert.AreEqual(1, ((LocalDate)parameters["Fourth"]).Month);
            Assert.AreEqual(13, ((LocalDate)parameters["Fourth"]).Day);
        }

        [Test]
        public void AddParams_Should_Do_Nothing_With_Null_Argument()
        {
            var parameters = new Neo4jParameters().WithParams(null);

            Assert.AreEqual(0, parameters.Count);
        }

        public class Entity
        {
            public string First { get; set; }
            public string Second { get; set; }
            public int Third { get; set; }
            public LocalDate Fourth { get; set; }
        }

        [Test]
        public void Should_Convert_Entity_To_Dictionary()
        {
            var entity = new Entity
            {
                First = "Foo",
                Second = "Bar",
                Third = 56,
                Fourth = new LocalDate(2019, 1, 13)
            };

            // ReSharper disable once UseObjectOrCollectionInitializer
            var parameters = new Neo4jParameters().WithEntity("p1", entity);

            Assert.AreEqual(1, parameters.Count);
            Assert.IsInstanceOf<IReadOnlyDictionary<string, object>>(parameters["p1"]);

            var p1 = (IReadOnlyDictionary<string, object>) parameters["p1"];
            
            Assert.AreEqual("Foo", p1["First"]);
            Assert.AreEqual("Bar", p1["Second"]);
            Assert.AreEqual(56, p1["Third"]);
            Assert.AreEqual(2019, ((LocalDate)p1["Fourth"]).Year);
            Assert.AreEqual(1, ((LocalDate)p1["Fourth"]).Month);
            Assert.AreEqual(13, ((LocalDate)p1["Fourth"]).Day);
        }

        [Test]
        public void Should_Add_Inner_Dictionary()
        {
            var parameters = new Neo4jParameters
            {
                {"First", "Foo"}
            };

            var innerParameters = new Neo4jParameters(new
            {
                Inner1 = 13,
                Inner2 = true
            });

            parameters.Add("Second", innerParameters);

            Assert.AreEqual(2, parameters.Count);
            Assert.IsInstanceOf<IReadOnlyDictionary<string, object>>(parameters["Second"]);

            var second = (IReadOnlyDictionary<string, object>) parameters["Second"];
            Assert.AreEqual(13, second["Inner1"]);
            Assert.AreEqual(true, second["Inner2"]);
        }

        public class EntityWithIgnoredProperties
        {
            public int First { get; set; }

            [IgnoreDataMember]
            public string Second { get; set; }
            
            public int Third { get; set; }

            [IgnoreDataMember]
            public string Fourth { get; set; }
        }

        [Test]
        public void Should_Not_Populate_Ignored_Properties()
        {
            var entity = new EntityWithIgnoredProperties
            {
                First = 1,
                Second = "2",
                Third = 3,
                Fourth = "4"
            };

            var parameter = entity.ToParameterMap("entity");

            Assert.AreEqual("entity", parameter.Key);
            Assert.AreEqual(2, parameter.Value.Count);
            Assert.AreEqual(1, parameter.Value["First"]);
            Assert.AreEqual(3, parameter.Value["Third"]);
        }
    }
}

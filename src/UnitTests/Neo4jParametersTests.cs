using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Neo4j.Driver;
using Neo4j.Mapper;
using NUnit.Framework;
using ServiceStack;
using UnitTests.Models;

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
        public void Should_Convert_Entities_To_Dictionary()
        {
            var entities = new[]
            {
                new Entity
                {
                    First = "Foo",
                    Second = "Bar",
                    Third = 56,
                    Fourth = new LocalDate(2019, 1, 13)
                },
                new Entity
                {
                    First = "Lu",
                    Second = "Var",
                    Third = 65,
                    Fourth = new LocalDate(2019, 1, 26)
                },
            };

            var parameters = new Neo4jParameters().WithEntities("p1", entities);

            Assert.AreEqual(1, parameters.Count);

            var p1 = parameters["p1"];

            Assert.IsInstanceOf<IEnumerable<IReadOnlyDictionary<string, object>>>(p1);

            var innerItems = ((IEnumerable<IReadOnlyDictionary<string, object>>) p1).ToArray();

            Assert.AreEqual(2, innerItems.Length);

            var firstItem = innerItems[0];

            Assert.AreEqual("Foo", firstItem["First"]);
            Assert.AreEqual("Bar", firstItem["Second"]);
            Assert.AreEqual(56, firstItem["Third"]);
            Assert.AreEqual(2019, ((LocalDate)firstItem["Fourth"]).Year);
            Assert.AreEqual(1, ((LocalDate)firstItem["Fourth"]).Month);
            Assert.AreEqual(13, ((LocalDate)firstItem["Fourth"]).Day);

            var secondItem = innerItems[1];

            Assert.AreEqual("Lu", secondItem["First"]);
            Assert.AreEqual("Var", secondItem["Second"]);
            Assert.AreEqual(65, secondItem["Third"]);
            Assert.AreEqual(2019, ((LocalDate)secondItem["Fourth"]).Year);
            Assert.AreEqual(1, ((LocalDate)secondItem["Fourth"]).Month);
            Assert.AreEqual(26, ((LocalDate)secondItem["Fourth"]).Day);
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

        [Test]
        [NonParallelizable]
        public void Should_Convert_Enum_Type_To_String()
        {
            var entity = new MovieWithType
            {
                Title = "Top Gun",
                Released = 1986,
                Tagline = "As students at the United States Navy's elite fighter weapons school compete to be best in the class, one daring young pilot learns a few things from a civilian instructor that are not taught in the classroom.",
                MovieType = MovieType.Action
            };

            try
            {
                AutoMapping.RegisterConverter<MovieWithType, Dictionary<string, object>>(movieWithType =>
                {
                    var dictionary = movieWithType.ConvertTo<Dictionary<string, object>>(true);
                    dictionary[nameof(movieWithType.MovieType)] = movieWithType.MovieType.ToString();
                    return dictionary;
                });
                
                var parameter = entity.ToParameterMap("entity");

                Assert.AreEqual("entity", parameter.Key);
                Assert.AreEqual(4, parameter.Value.Count);
            
                Assert.IsTrue(parameter.Value.ContainsKey("Title"));
                var titleValue = parameter.Value["Title"];
                Assert.IsInstanceOf<string>(titleValue);
                Assert.AreEqual("Top Gun", titleValue);
            
                Assert.IsTrue(parameter.Value.ContainsKey("Released"));
                var releasedValue = parameter.Value["Released"];
                Assert.IsInstanceOf<int>(releasedValue);
                Assert.AreEqual(1986, releasedValue);
            
                Assert.IsTrue(parameter.Value.ContainsKey("MovieType"));
                var movieTypeValue = parameter.Value["MovieType"];
                Assert.IsInstanceOf<string>(movieTypeValue);
                Assert.AreEqual("Action", movieTypeValue);
            }
            finally 
            {
                AutoMappingUtils.Reset();
            }
        }
    }
}

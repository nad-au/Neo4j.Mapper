using System.Collections.Generic;
using System.Linq;
using IntegrationTests.Models;
using Neo4jMapper;
using NUnit.Framework;

namespace IntegrationTests.Tests
{
    [TestFixture]
    [NonParallelizable]
    public class BasicTests : MoviesFixtureBase
    {
        [Test]
        public void Should_Map_Cypher_Map_With_Inner_Item()
        {
            var result = Session.Run(@"
                RETURN { name: 'Foo', otherName: { name: 'Fu'}}");

            var map = result.Single().Map<PersonIdentity>();

            Assert.AreEqual("Foo", map.Name);
            Assert.IsNotNull(map.OtherName);
            Assert.AreEqual("Fu", map.OtherName.Name);
        }

        [Test]
        public void Should_Map_Literal_List()
        {
            var result = Session.Run(@"
                RETURN range(0, 10)[..4]");

            var sequence = result.Single().Map<List<byte>>();

            Assert.AreEqual(4, sequence.Count);
            Assert.AreEqual(0, sequence.First());
            Assert.AreEqual(3, sequence.Last());
        }

        [Test]
        public void Should_Map_Cypher_Map_With_Inner_List()
        {
            var result = Session.Run(@"
                RETURN { name: 'Foo', otherNames: [{ name: 'Fu' }, { name: 'Fuey' }]}");

            var map = result.Single().Map<PersonWithIdentities>();

            Assert.AreEqual("Foo", map.Name);
            Assert.AreEqual(2, map.OtherNames.Count);
            Assert.AreEqual("Fu", map.OtherNames.First().Name);
            Assert.AreEqual("Fuey", map.OtherNames.Last().Name);
        }
    }
}

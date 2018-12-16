using Neo4j.Driver.V1;
using NUnit.Framework;

namespace Neo4j.Driver.Extensions.Tests
{
    public abstract class TestFixtureBase
    {
        protected IDriver Driver { get; private set; }

        [OneTimeSetUp]
        protected virtual void TestFixtureSetUp()
        {
            Driver = GraphDatabase.Driver("bolt://localhost:7687");
        }

        [OneTimeTearDown]
        protected virtual void TestFixtureTearDown()
        {
            Driver.Dispose();
        }
    }
}

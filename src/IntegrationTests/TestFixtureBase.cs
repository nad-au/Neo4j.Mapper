using Neo4j.Driver;
using NUnit.Framework;

namespace IntegrationTests
{
    public abstract class TestFixtureBase
    {
        protected IDriver Driver { get; private set; }

        [OneTimeSetUp]
        protected virtual void TestFixtureSetUp()
        {
            Driver = GraphDatabase.Driver("bolt://localhost:7687");

            // Ensure node exists to avoid flaky test where node id = 0
            using (var session = Driver.Session())
            {
                session.Run("MERGE (:Neo4jMapperTest)");
            }
        }

        [OneTimeTearDown]
        protected virtual void TestFixtureTearDown()
        {
            using (var session = Driver.Session())
            {
                session.Run("MATCH (n:Neo4jMapperTest) DELETE n");
            }
            Driver.Dispose();
        }
    }
}

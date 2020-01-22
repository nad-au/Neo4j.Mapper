using Neo4j.Driver.V1;
using NUnit.Framework;

namespace IntegrationTests
{
    public abstract class TestFixtureBase
    {
        protected IDriver Driver { get; private set; }

        [OneTimeSetUp]
        protected virtual void TestFixtureSetUp()
        {
            Driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "123456"));

            // Ensure node exists to avoid flaky test where node id = 0
            using (var session = Driver.Session())
            {
                session.RunAsync("MERGE (:Neo4jMapperTest)");
            }
        }

        [OneTimeTearDown]
        protected virtual void TestFixtureTearDown()
        {
            using (var session = Driver.Session())
            {
                session.RunAsync("MATCH (n:Neo4jMapperTest) DELETE n");
            }
            Driver.Dispose();
        }
    }
}

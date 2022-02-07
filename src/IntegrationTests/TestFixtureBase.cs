using System.Threading.Tasks;
using Neo4j.Driver;
using NUnit.Framework;

namespace IntegrationTests
{
    public abstract class TestFixtureBase
    {
        protected IDriver Driver { get; private set; }
        protected IAsyncSession Session;


        [OneTimeSetUp]
        protected virtual void TestFixtureSetUp()
        {
            Driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "s3cr3t"));
            Session = Driver.AsyncSession();
        }

        [OneTimeTearDown]
        protected virtual async Task TestFixtureTearDown()
        {
            await Session.CloseAsync();
            Driver.Dispose();
        }

        [SetUp]
        protected virtual async Task SetUp()
        {
            await Session.RunAsync("MATCH (n) DETACH DELETE n");
        }

    }
}

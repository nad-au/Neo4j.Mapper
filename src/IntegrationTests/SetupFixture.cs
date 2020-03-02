using System.Threading.Tasks;
using Neo4j.Driver;
using NUnit.Framework;

namespace IntegrationTests
{
    [SetUpFixture]
    public class SetupFixture
    {
        [OneTimeSetUp]
        public async Task TestFixtureSetUp()
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7687"))
            {
                var session = driver.AsyncSession();
                await session.RunAsync("MERGE (:Neo4jMapperTest)");
                await session.CloseAsync();
            }
        }

        [OneTimeTearDown]
        public async Task TestFixtureTearDown()
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7687"))
            {
                var session = driver.AsyncSession();
                await session.RunAsync("MATCH (n:Neo4jMapperTest) DELETE n");
                await session.CloseAsync();
            }
        }
    }
}

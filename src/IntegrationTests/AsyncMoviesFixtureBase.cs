using System.Threading.Tasks;
using Neo4j.Driver;
using NUnit.Framework;
using Query = Queries.Query;

namespace IntegrationTests
{
    public abstract class AsyncMoviesFixtureBase : TestFixtureBase
    {
        protected IAsyncSession Session;

        [SetUp]
        protected async Task  SetUp()
        {
            Session = Driver.AsyncSession();
            await Session.RunAsync(Query.CreateMovies);
        }

        [TearDown]
        protected async Task TearDown()
        {
            await Session.RunAsync(Query.DeleteMovies);
            await Session.CloseAsync();
        }
    }
}

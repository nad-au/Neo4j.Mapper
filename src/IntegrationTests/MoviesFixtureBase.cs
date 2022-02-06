using System.Threading.Tasks;
using NUnit.Framework;
using Query = Queries.Query;

namespace IntegrationTests
{
    public abstract class MoviesFixtureBase : TestFixtureBase
    {
        [SetUp]
        protected override async Task SetUp()
        {
            await base.SetUp();
            await Session.RunAsync(Query.CreateMovies);
        }
    }
}

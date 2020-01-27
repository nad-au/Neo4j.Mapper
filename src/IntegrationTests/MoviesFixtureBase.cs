using Neo4j.Driver;
using NUnit.Framework;
using Query = Queries.Query;

namespace IntegrationTests
{
    public abstract class MoviesFixtureBase : TestFixtureBase
    {
        protected ISession Session;

        [SetUp]
        protected void SetUp()
        {
            Session = Driver.Session();
            Session.Run(Query.CreateMovies);
        }

        [TearDown]
        protected void TearDown()
        {
            Session.Run(Query.DeleteMovies);
            Session.Dispose();
        }
    }
}

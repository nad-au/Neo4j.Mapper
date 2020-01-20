using System.Linq;
using FluentAssertions;
using IntegrationTests.Models;
using Neo4jMapper;
using NUnit.Framework;

namespace IntegrationTests.Tests
{
    [TestFixture]
    [NonParallelizable]
    public class SessionExtensionsTests : MoviesFixtureBase
    {
        [Test]
        public void GetNode_Should_Populate_Node_Id()
        {
            var result = Session.Run(@"
                MATCH (movie:Movie {title: 'Top Gun'})
                RETURN movie");

            var movie = result.Single().Map<Movie>();

            Assert.Greater(movie.Id, default);

            // Act
            var node = Session.GetNode<Movie>(movie.Id);

            node.Should().BeEquivalentTo(movie);
        }

        [Test]
        public void GetNode_Tx_Should_Populate_Node_Id()
        {
            Session.ReadTransaction(tx =>
            {
                var result = tx.Run(@"
                MATCH (movie:Movie {title: 'Top Gun'})
                RETURN movie");

                var movie = result.Single().Map<Movie>();

                Assert.Greater(movie.Id, default);

                // Act
                var node = tx.GetNode<Movie>(movie.Id);

                node.Should().BeEquivalentTo(movie);

                return node;
            });
        }

        [Test]
        public void SetNode_Should_Update_Values()
        {
            var result = Session.Run(@"
                MATCH (movie:Movie {title: 'Top Gun'})
                RETURN movie");

            var movie = result.Map<Movie>().SingleOrDefault();

            Assert.IsNotNull(movie);
            Assert.Greater(movie.Id, default);

            movie.title = "Top Gun 2";

            // Act
            Session.SetNode(movie);

            var node = Session.GetNode<Movie>(movie.Id);

            node.Should().BeEquivalentTo(movie);
        }

        [Test]
        public void SetNode_Tx__Should_Update_Values()
        {
            Session.WriteTransaction(tx =>
            {
                var result = tx.Run(@"
                MATCH (movie:Movie {title: 'Top Gun'})
                RETURN movie");

                var movie = result.Map<Movie>().SingleOrDefault();

                Assert.IsNotNull(movie);
                Assert.Greater(movie.Id, default);

                movie.title = "Top Gun 2";

                // Act
                tx.SetNode(movie);

                var node = tx.GetNode<Movie>(movie.Id);

                node.Should().BeEquivalentTo(movie);

                return node;
            });
        }
    }
}

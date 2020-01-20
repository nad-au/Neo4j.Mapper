using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Models;
using Neo4jMapper;
using NUnit.Framework;

namespace IntegrationTests.Tests
{
    [TestFixture]
    [NonParallelizable]
    public class AsyncSessionExtensionsTests : AsyncMoviesFixtureBase
    {
        [Test]
        public async Task GetNodeAsync_Should_Populate_Node_Id()
        {
            var result = await Session.RunAsync(@"
                MATCH (movie:Movie {title: 'Top Gun'})
                RETURN movie");

            var movie = await result.MapSingleAsync<Movie>();

            Assert.Greater(movie.Id, default);

            // Act
            var node = await Session.GetNodeAsync<Movie>(movie.Id);

            node.Should().BeEquivalentTo(movie);
        }

        [Test]
        public async Task GetNodeAsync_Tx_Should_Populate_Node_Id()
        {
            await Session.ReadTransactionAsync(async tx =>
            {
                var result = await tx.RunAsync(@"
                    MATCH (movie:Movie {title: 'Top Gun'})
                    RETURN movie");

                var movie = await result.MapSingleAsync<Movie>();

                var node = await tx.GetNodeAsync<Movie>(movie.Id);
                
                node.Should().BeEquivalentTo(movie);
            });
        }

        [Test]
        public async Task SetNodeAsync_Should_Update_Values()
        {
            var result = await Session.RunAsync(@"
                MATCH (movie:Movie {title: 'Top Gun'})
                RETURN movie");

            var movie = await result.MapSingleAsync<Movie>();

            Assert.Greater(movie.Id, default);

            movie.title = "Top Gun 2";

            // Act
            await Session.SetNodeAsync(movie);

            var node = await Session.GetNodeAsync<Movie>(movie.Id);

            node.Should().BeEquivalentTo(movie);
        }

        [Test]
        public async Task SetNodeAsync_Tx_Should_Update_Values()
        {
            await Session.WriteTransactionAsync(async tx =>
            {
                var result = await tx.RunAsync(@"
                MATCH (movie:Movie {title: 'Top Gun'})
                RETURN movie");

                var movie = await result.MapSingleAsync<Movie>();

                Assert.Greater(movie.Id, default);

                movie.title = "Top Gun 2";

                // Act
                await tx.SetNodeAsync(movie);

                var node = await tx.GetNodeAsync<Movie>(movie.Id);

                node.Should().BeEquivalentTo(movie);
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Neo4j.Driver.V1;
using Neo4jMapper;
using NUnit.Framework;

namespace IntegrationTests.Tests
{
    [TestFixture]
    [NonParallelizable]
    public class EnumHandlingTests : TestFixtureBase
    {
        [Test]
        public async Task Should_Map_Enum()
        {
            using (var session = Driver.Session(AccessMode.Write))
            {
                await session.RunAsync(@"
                MATCH (n:Payload {Id: 1})
                Detach Delete n").ConfigureAwait(false);

                var payload = new Payload
                {
                    Id = 1,
                    Color = ConsoleColor.Red,
                    Name = "Test 1"
                };

                var parms = new Neo4jParameters()
                 .WithEntity<Payload>("map", payload,
                            (k, v) => k == nameof(Payload.Color) ? v.ToString() : v);

                var cursor = await session.RunAsync(@"
                CREATE (n:Payload $map)
                RETURN n", parms).ConfigureAwait(false);

                var result = await cursor
                    .MapSingleAsync<Payload>().ConfigureAwait(false);

                Assert.AreEqual(payload, result);
            }
        }
        [Test]
        public async Task Should_Map_Entities_Enum()
        {
            using (var session = Driver.Session(AccessMode.Write))
            {
                await session.RunAsync(@"
                MATCH (n:Payload {Id: 1})
                Detach Delete n").ConfigureAwait(false);
                await session.RunAsync(@"
                MATCH (n:Payload {Id: 2})
                Detach Delete n").ConfigureAwait(false);

                var payloads = new[]
                {
                new Payload
                {
                    Id = 1,
                    Color = ConsoleColor.Red,
                    Name = "Test 1"
                },
                new Payload
                {
                    Id = 2,
                    Color = ConsoleColor.Green,
                    Name = "Test 2"
                },
            };

                var parms = new Neo4jParameters()
                 .WithEntities<Payload>("items", payloads,
                            (k, v) => k == nameof(Payload.Color) ? v.ToString() : v);

                var cursor = await session.RunAsync(@"
                UNWIND $items AS item
                CREATE (n:Payload)
                SET n = item 
                RETURN n", parms).ConfigureAwait(false);

                var result = await cursor
                    .MapAsync<Payload>().ConfigureAwait(false);

                Assert.AreEqual(payloads, result);
            }
        }
        class Payload : IEquatable<Payload>
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public ConsoleColor Color { get; set; }

            public override bool Equals(object obj)
            {
                return Equals(obj as Payload);
            }

            public bool Equals([AllowNull] Payload other)
            {
                return other != null &&
                       Id == other.Id &&
                       Name == other.Name &&
                       Color == other.Color;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Id, Name, Color);
            }

            public static bool operator ==(Payload left, Payload right)
            {
                return EqualityComparer<Payload>.Default.Equals(left, right);
            }

            public static bool operator !=(Payload left, Payload right)
            {
                return !(left == right);
            }
        }
    }
}

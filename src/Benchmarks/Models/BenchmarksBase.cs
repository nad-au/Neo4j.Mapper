using System;
using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;
using Neo4j.Driver.V1;
using Neo4jClient;
using Queries;

namespace Benchmarks.Models
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public abstract class BenchmarksBase
    {
        protected IDriver Driver { get; private set; }
        protected GraphClient GraphClient { get; private set; }
        protected BoltGraphClient BoltGraphClient { get; private set; }

        [GlobalSetup]
        public void Setup()
        {
            Driver = GraphDatabase.Driver("bolt://localhost:7687");

            using (var session = Driver.Session())
            {
                session.Run(Query.CreateMovies);
            }

            GraphClient = new GraphClient(new Uri("http://localhost:7474/db/data"));
            GraphClient.Connect();

            BoltGraphClient = new BoltGraphClient(new Uri("bolt://localhost:7687"));
            BoltGraphClient.Connect();
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            using (var session = Driver.Session())
            {
                session.Run(Query.DeleteMovies);
            }

            Driver.Dispose();
            GraphClient.Dispose();
            BoltGraphClient.Dispose();
        }
    }
}

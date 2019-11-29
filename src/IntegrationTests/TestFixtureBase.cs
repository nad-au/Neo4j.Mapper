using Neo4j.Driver.V1;
using NUnit.Framework;
using System;

namespace IntegrationTests
{
    public abstract class TestFixtureBase
    {
        protected IDriver Driver { get; private set; }
        public static readonly string Url =
                Environment.GetEnvironmentVariable("N4J_URL") ?? "bolt://localhost:7687";
        public static readonly string User =
                Environment.GetEnvironmentVariable("N4J_USER") ?? string.Empty;
        public static readonly string Password =
                Environment.GetEnvironmentVariable("N4J_PASS") ?? string.Empty;

        public static IDriver CreateDriver()
        {
            if (string.IsNullOrEmpty(User))
                return GraphDatabase.Driver(Url);
            return GraphDatabase.Driver(Url, AuthTokens.Basic(User, Password));
        }

        [OneTimeSetUp]
        protected virtual void TestFixtureSetUp()
        {
            Driver = CreateDriver();

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
                session.RunAsync("MATCH (n:Neo4jMapperTest) DETACH DELETE n");
            }
            Driver.Dispose();
        }
    }
}

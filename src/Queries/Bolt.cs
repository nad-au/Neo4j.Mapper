using System;
using System.Threading.Tasks;
using Neo4j.Driver.V1;

namespace Queries
{
    public static class Bolt
    {
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

        public static async Task NewSession(Func<ISession, Task> statement)
        {

            using (var driver = CreateDriver())
            {
                using (var session = driver.Session())
                {
                    await statement(session);
                }
            }
        }

        public static async Task<T> NewSession<T>(Func<ISession, Task<T>> statement)
        {
            using (var driver = CreateDriver())
            {
                using (var session = driver.Session())
                {
                    return await statement(session);
                }
            }
        }
    }
}

using System;
using System.Threading.Tasks;
using Neo4j.Driver;

namespace Queries
{
    public static class Bolt
    {
        public const string Url = "bolt://localhost:7687";

        public static async Task NewSession(Func<ISession, Task> statement)
        {
            using (var driver = GraphDatabase.Driver(Url))
            {
                using (var session = driver.Session())
                {
                    await statement(session);
                }
            }
        }

        public static async Task<T> NewSession<T>(Func<ISession, Task<T>> statement)
        {
            using (var driver = GraphDatabase.Driver(Url))
            {
                using (var session = driver.Session())
                {
                    return await statement(session);
                }
            }
        }
    }
}

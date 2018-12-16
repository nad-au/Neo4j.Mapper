namespace Neo4j.Driver.Extensions.Tests.Queries
{
    public static class Query
    {
        public const string ResourceBaseName = "Neo4j.Driver.Extensions.Tests.Queries";

        public static string CreateMovies => Utils.GetResourceContent($"{ResourceBaseName}.create_movies.cypher");
        public static string DeleteMovies => Utils.GetResourceContent($"{ResourceBaseName}.delete_movies.cypher");
    }
}
